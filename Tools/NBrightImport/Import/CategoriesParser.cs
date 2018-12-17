using System.Collections.Generic;
using System.IO;
using System.Linq;
using ZIndex.DNN.NBrightImport.Logger;
using ZIndex.DNN.NBrightImport.Model.Store;

namespace ZIndex.DNN.NBrightImport.Import
{
    public class CategoriesParser : ICategoriesParser
    {
        private readonly ILog _log = new LoggerBase(typeof(CategoriesParser)).Logger;

        /*
         * Typical directory structure
         * Root | Path1
         * Root | Path1 | Path11
         * Root | Path1 | Path12
         * Root | Path2
         */

        /// <summary>
        /// Parses recursively the categories from the root path
        /// </summary>
        /// <param name="rootPath">The root path.</param>
        /// <returns></returns>
        public List<Category> Parse(string rootPath)
        {

            var directoryInfo = new DirectoryInfo(rootPath);
            _log.Info("Parsing categories from root path {0}", directoryInfo.Name);

            var id = 1;

            // create the root category, will be the parent of the sub-folders
            var rootCategory = new Category
            {
                Id = id++, 
                Name = directoryInfo.Name
            };
            var list = new List<Category> {rootCategory};

            Parse(rootPath, id, list, rootCategory);

            _log.Info("{0} categories parsed", list.Count());

            return list;
        }

        /// <summary>
        /// Parses the categories and recurses for each sub-folders.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="nextId">The next category identifier.</param>
        /// <param name="categories">The categories.</param>
        /// <param name="parent">The parent.</param>
        private int Parse(string path, int nextId, List<Category> categories, Category parent)
        {
            var directoryInfo = new DirectoryInfo(path);
            _log.Info("Parsing categories from path {0}", directoryInfo.Name);

            // use a local variable accessible from the Select() scope
            var id = nextId;

            categories.AddRange(
                directoryInfo.GetDirectories("*.*", SearchOption.TopDirectoryOnly)
                    .Select(di =>
                    {
                        _log.Debug("Creating categoty {0}", di.FullName);
                        var category = new Category
                        {
                            Id = id++,
                            Name = di.Name,
                            Parent = parent
                        };
                        id = Parse(di.FullName, id, categories, category);


                        return category;
                    })
            );

            // return the next id to the calling method
            return id;

        }
    }
}
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ZIndex.DNN.NBrightImport.Logger;
using ZIndex.DNN.NBrightImport.Model.Store;

namespace ZIndex.DNN.NBrightImport.Import
{
    public class ProductsParser : IProductsParser
    {
        private readonly ILog _log = new LoggerBase(typeof(ProductsParser)).Logger;
        private List<Category> _categories;

        public List<Product> Parse(string rootPath, List<Category> categories)
        {
            _categories = categories;
            var directoryInfo = new DirectoryInfo(rootPath);
            _log.Info("Parsing products from root path {0}", directoryInfo.Name);

            var list = new List<Product> { };

            var maxid = _categories.Max(category => category.Id);
            var maxidlang = _categories.Max(category => category.IdLang);
            var id = maxid > maxidlang ? maxid : maxidlang;
            id++;

            // parse the files of the root path
            list.AddRange(EnumerateFilesAndCreateProducts(directoryInfo, ref id));

            // parse the root's children folders
            Parse(rootPath, id, list, categories);

            _log.Info("{0} products parsed", list.Count());

            return list;
        }

        private int Parse(string path, int nextId, List<Product> products, List<Category> categories)
        {
            var directoryInfo = new DirectoryInfo(path);
            _log.Info("Parsing products from path {0}", directoryInfo.Name);

            // use a local variable accessible from the Select() scope
            var id = nextId;

            products.AddRange(
                directoryInfo.GetDirectories("*.*", SearchOption.TopDirectoryOnly)
                    //.Where(p => p.EnumerateFiles("*.jpg").Count() != 0) // work only with folder containing images
                    .SelectMany(di =>
                    {
                        var localproducts = EnumerateFilesAndCreateProducts(di, ref id);

                        // recurse to child folders
                        id = Parse(di.FullName, id, localproducts, categories);

                        return localproducts;
                    })
            );

            // return the next id to the calling method
            return id;

        }

        /// <summary>
        /// Enumerates the files and creates the products.
        /// </summary>
        /// <param name="di">The di.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        private List<Product> EnumerateFilesAndCreateProducts(DirectoryInfo di, ref int id)
        {
            var localId = id;
            var products = di.EnumerateFiles("*.jpg").Select(p =>
            {
                _log.Debug("Creating product {0}", di.FullName);
                var product = new Product
                {
                    Id = localId++,
                    IdLang = localId++,
                    IdCatXRef = localId++,
                    Name = Path.GetFileNameWithoutExtension(p.Name),
                    ImageFilename = p.Name,
                    FullPath = di.FullName,
                    // look up the category
                    Category = _categories.SingleOrDefault(category => category.Name == di.Name),
                };

                return product;
            }).ToList();
            id = localId;
            return products;
        }
    }
}
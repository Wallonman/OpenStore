using System.Collections.Generic;
using ZIndex.DNN.NBrightImport.Model.Store;

namespace ZIndex.DNN.NBrightImport.Import
{
    public interface ICategoriesParser
    {

        /// <summary>
        /// Parses recursively the categories from the root path
        /// </summary>
        /// <param name="rootPath">The root path.</param>
        /// <returns></returns>
        List<Category> Parse(string rootPath);
    }
}
using System.Collections.Generic;
using ZIndex.DNN.NBrightImport.Model.Store;

namespace ZIndex.DNN.NBrightImport.Import
{
    public interface IProductsParser
    {

        /// <summary>
        /// Parses recursively the products from the root path
        /// </summary>
        /// <param name="rootPath">The root path.</param>
        /// <param name="categories">The categories.</param>
        /// <returns></returns>
        List<Product> Parse(string rootPath, List<Category> categories);
    }
}
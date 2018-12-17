using System.Globalization;
using ZIndex.DNN.NBrightImport.Model.Store;

namespace ZIndex.DNN.NBrightImport.Import
{
    public interface IStoreParser
    {
        /// <summary>
        ///     Parses the products and categories from the root path
        /// </summary>
        /// <param name="rootPath">The root path.</param>
        /// <param name="culture">The culture.</param>
        /// <param name="imageBasePath">The image base path.</param>
        /// <param name="imageBaseUrl">The image base URL.</param>
        /// <param name="productUnitCost">The product unit cost.</param>
        /// <returns></returns>
        Store Parse(string rootPath, CultureInfo culture, string imageBasePath, string imageBaseUrl,
            decimal productUnitCost);
    }
}
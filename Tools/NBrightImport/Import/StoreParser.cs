using System.Globalization;
using ZIndex.DNN.NBrightImport.Model.Store;

namespace ZIndex.DNN.NBrightImport.Import
{
    public class StoreParser : IStoreParser
    {
        private readonly ICategoriesParser _categoriesParser;
        private readonly IProductsParser _productsParser;

        public StoreParser(ICategoriesParser categoriesParser, IProductsParser productsParser)
        {
            _categoriesParser = categoriesParser;
            _productsParser = productsParser;
        }

        /// <summary>
        ///     Parses the products and categories from the root path
        /// </summary>
        /// <param name="rootPath">The root path.</param>
        /// <param name="culture">The culture.</param>
        /// <param name="imageBasePath">The image base path.</param>
        /// <param name="imageBaseUrl">The image base URL.</param>
        /// <param name="productUnitCost">The product unit cost.</param>
        /// <returns></returns>
        public Store Parse(string rootPath, CultureInfo culture, string imageBasePath, string imageBaseUrl,
            decimal productUnitCost)
        {
            var categories = _categoriesParser.Parse(rootPath);
            var products = _productsParser.Parse(rootPath, categories);

            return new Store
            {
                Categories = categories,
                Products = products,
                Culture = culture,
                ImageBasePath = imageBasePath,
                ImageBaseUrl = imageBaseUrl,
                ProductUnitCost = productUnitCost
            };
        }
    }
}
using NBrightDNN;
using ZIndex.DNN.NBrightImport.Model.Store;

namespace ZIndex.DNN.NBrightImport.Import
{
    public interface IConverter
    {
        /// <summary>
        /// To the image path.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="imageBasePath">The image base path.</param>
        /// <returns></returns>
        string ToImagePath(Product product, string imageBasePath);

        /// <summary>
        /// To the image base URL.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="imageBaseUrl">The image base URL.</param>
        /// <returns></returns>
        string ToImageBaseUrl(Product product, string imageBaseUrl);

        NBrightInfo CreateCategory(Category category, Store store);
        NBrightInfo CreateCategoryLang(Category category, Store store);
    }
}
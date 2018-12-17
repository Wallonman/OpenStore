using System;
using System.IO;
using ZIndex.DNN.NBrightImport.Model.Store;

namespace ZIndex.DNN.NBrightImport.Import
{
    public class Converter : IConverter
    {
        /// <summary>
        /// To the image path.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="imageBasePath">The image base path.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// product
        /// or
        /// imageBasePath
        /// </exception>
        public string ToImagePath(Product product, string imageBasePath)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            if (imageBasePath == null) throw new ArgumentNullException(nameof(imageBasePath));

            return Path.Combine(imageBasePath, product.ImageFilename);
        }

        /// <summary>
        /// To the image base URL.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="imageBaseUrl">The image base URL.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// product
        /// or
        /// imageBaseUrl
        /// </exception>
        public string ToImageBaseUrl(Product product, string imageBaseUrl)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            if (imageBaseUrl == null) throw new ArgumentNullException(nameof(imageBaseUrl));

            var uri = new Uri(string.Concat(imageBaseUrl.TrimEnd('/', '\\'), "/", product.ImageFilename) , UriKind.Relative);

            return uri.ToString();
        }
    }
}
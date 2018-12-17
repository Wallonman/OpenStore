using System;
using System.Globalization;
using System.IO;
using ZIndex.DNN.NBrightImport.Import;

namespace ZIndex.DNN.NBrightImport.Extensions
{
    public static class CommonExtensions
    {
        /// <summary>
        /// Returns an Xsd datetime
        /// </summary>
        /// <param name="now"></param>
        /// <returns></returns>
        public static string ToXsdDatetime(this DateTime now)
        {
            return now.ToString("yyyy-MM-ddThh:mm:ss.ff");
        }

        /// <summary>
        /// Returns the value formated as UnitCost
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToUniCost(this decimal value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Returns the Category's Image Url
        /// </summary>
        /// <param name="simpleProduct"></param>
        /// <returns></returns>
        public static string ToCategoryImageUrl(this ISimpleProduct simpleProduct)
        {
            return PrepareImageUrl(simpleProduct.ImageBaseUrl, Path.GetFileNameWithoutExtension(simpleProduct.CategoryImage), "/", Path.GetExtension(simpleProduct.CategoryImage));
        }

        /// <summary>
        /// Returns the Category's Image Url
        /// </summary>
        /// <param name="simpleProduct"></param>
        /// <param name="imageName"></param>
        /// <returns></returns>
        public static string ToProductImageUrl(this ISimpleProduct simpleProduct, string imageName)
        {
            return PrepareImageUrl(simpleProduct.ImageBaseUrl, imageName, "/", simpleProduct.ProductsImageExtension);
        }

        /// <summary>
        /// Returns the Category's Image Url
        /// </summary>
        /// <param name="simpleProduct"></param>
        /// <param name="imageName"></param>
        /// <returns></returns>
        public static string ToProductImagePath(this ISimpleProduct simpleProduct, string imageName)
        {
            return PrepareImageUrl(simpleProduct.ImageBasePath, imageName, @"\", simpleProduct.ProductsImageExtension);
        }

        /// <summary>
        /// Concats the base url with the image name using a separator and adds the extension
        /// </summary>
        /// <param name="imageBaseUrl"></param>
        /// <param name="imageName"></param>
        /// <param name="separator"></param>
        /// <param name="extension"></param>
        /// <returns></returns>
        private static string PrepareImageUrl(string imageBaseUrl, string imageName, string separator, string extension)
        {
            return string.Concat(imageBaseUrl, separator, imageName, ".", extension).Replace(separator + separator, separator);
//            return Path.ChangeExtension(string.Concat(imageBaseUrl, separator, imageName).Replace(separator + separator, separator), extension);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using ZIndex.DNN.NBrightImport.Logger;

namespace ZIndex.DNN.NBrightImport.Import
{
    /// <summary>
    /// Provides the list of product using the names of the files located in the given productImagesPath
    /// </summary>
    public class SimpleProduct : ISimpleProduct
    {
        private readonly ILog _log = new LoggerBase(typeof (SimpleProduct)).Logger;

        /// <summary>
        /// Gets a list of strings representing product names
        /// </summary>
        public List<string> ProductNames { get; private set; }

        /// <summary>
        /// Gets the file extension used for product images
        /// </summary>
        public string ProductsImageExtension { get; private set; }

        /// <summary>
        /// Gets the Category name
        /// </summary>
        public string CategoryName { get; private set; }

        /// <summary>
        /// Gets the Category Image name (local filename with extension)
        /// </summary>
        public string CategoryImage { get; private set; }

        /// <summary>
        /// Gets the base url for images (e.g. /mcpaquot/Portals/0/productimages/)
        /// </summary>
        public string ImageBaseUrl { get; private set; }

        /// <summary>
        /// Gets the base path for product images (e.g. C:\Users\Eric\Documents\Work\Svn\McPaquot\Website\Portals\0\productimages\)
        /// </summary>
        public string ImageBasePath { get; private set; }

        /// <summary>
        /// Gets the unit cost
        /// </summary>
        public decimal UnitCost { get; private set; }

        /// <summary>
        /// Gets the culture
        /// </summary>
        public CultureInfo Culture { get; private set; }

        /// <summary>
        /// Constructor of a SimpleProduct instance
        /// </summary>
        /// <param name="productImagesPath"></param>
        /// <param name="categoryName"></param>
        /// <param name="categoryImage"></param>
        /// <param name="imageBaseUrl"></param>
        /// <param name="imageBasePath"></param>
        /// <param name="unitCost"></param>
        /// <param name="culture"></param>
        public SimpleProduct(string productImagesPath, string categoryName, string categoryImage, string imageBaseUrl, string imageBasePath, decimal unitCost, CultureInfo culture)
        {
            ProductsImageExtension = "jpg";
            ImageBasePath = imageBasePath;
            CategoryImage = categoryImage;
            Culture = culture;
            UnitCost = unitCost;
            ImageBaseUrl = imageBaseUrl;
            CategoryName = categoryName;

            if (!Directory.Exists(productImagesPath))
                throw new ArgumentException(string.Format("Invalid productImagesPath: {0}", productImagesPath));

            // get the list of product names from the images filenames without extension 
            ProductNames =
                new List<string>(Directory.EnumerateFiles(productImagesPath, string.Concat("*.", ProductsImageExtension)).ToList().Select(Path.GetFileNameWithoutExtension));

        }
    }
}
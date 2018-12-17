using System.Collections.Generic;
using System.Globalization;

namespace ZIndex.DNN.NBrightImport.Import
{
    /// <summary>
    /// Provide the list of product names, there is one model per product, all products belongs to one category
    /// </summary>
    public interface ISimpleProduct
    {
        /// <summary>
        /// Gets the list of product names 
        /// </summary>
        List<string> ProductNames { get; }
        /// <summary>
        /// Gets the file extension of the product images
        /// </summary>
        string ProductsImageExtension { get; }
        /// <summary>
        /// Gets the Category name
        /// </summary>
        string CategoryName { get; }
        /// <summary>
        /// Gets the Category Image full local name (i.e. myCategory.jpg)
        /// </summary>
        string CategoryImage { get; }
        /// <summary>
        /// Gets the Image Base Url (i.e. /website/portal/0/productimages/)
        /// </summary>
        string ImageBaseUrl { get; }
        /// <summary>
        /// Gets the Image base path (i.e. C:\Users\Eric\Documents\Work\Svn\McPaquot\Website\Portals\0\productimages)
        /// </summary>
        string ImageBasePath { get; }
        /// <summary>
        /// Gets the product uni cost
        /// </summary>
        decimal UnitCost { get; }
        /// <summary>
        /// Gets the CultureInfo
        /// </summary>
        CultureInfo Culture { get; }
    }
}

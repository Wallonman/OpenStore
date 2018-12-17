using System.Collections.Generic;
using System.Globalization;

namespace ZIndex.DNN.NBrightImport.Model.Store
{
    public class Store
    {
        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        public List<Product> Products { get; set; }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        /// <value>
        /// The categories.
        /// </value>
        public List<Category> Categories { get; set; }

        /// <summary>
        /// Gets or sets the CultureInfo
        /// </summary>
        public CultureInfo Culture { get; set; }

        /// <summary>
        /// Gets or sets the image base path.
        /// </summary>
        /// <value>
        /// The image base path.
        /// </value>
        public string ImageBasePath { get; set; }

        /// <summary>
        /// Gets or sets the product unit cost.
        /// </summary>
        /// <value>
        /// The product unit cost.
        /// </value>
        public decimal ProductUnitCost { get; set; }

        /// <summary>
        /// Gets or sets the image base URL.
        /// </summary>
        /// <value>
        /// The image base URL.
        /// </value>
        public string ImageBaseUrl { get; set; }
    }
}
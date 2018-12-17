namespace ZIndex.DNN.NBrightImport.Model.Store
{
    public class Product
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        public Category Category { get; set; }

        /// <summary>
        /// Gets or sets the image filename.
        /// </summary>
        /// <value>
        /// The image filename.
        /// </value>
        public string ImageFilename { get; set; }

        /// <summary>
        /// Gets or sets the store.
        /// </summary>
        /// <value>
        /// The store.
        /// </value>
        public Store Store { get; set; }

        /* /// <summary>
        /// Gets or sets the unit cost.
        /// </summary>
        /// <value>
        /// The unit cost.
        /// </value>
        private decimal UnitCost { get; set; }

        /// <summary>
        /// Gets or sets the Image path (i.e. C:\Users\Eric\Documents\Work\Svn\McPaquot\Website\Portals\0\productimages\100_1117.jpg)
        /// </summary>
        private string ImagePath { get; set; }

        /// <summary>
        /// Gets or sets the Image Url (i.e. /website/portal/0/productimages/100_1117.jpg)
        /// </summary>
        private string ImageUrl { get; set; }
        */
    }
}
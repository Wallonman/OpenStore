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
        public int IdLang { get; set; }
        public int IdCatXRef { get; set; }
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

        /*/// <summary>
        /// Gets or sets the store.
        /// </summary>
        /// <value>
        /// The store.
        /// </value>
        public Store Store { get; set; }
*/

        /// <summary>
        /// Gets or Sets the full path
        /// </summary>
        public string FullPath { get; set; }

    }
}
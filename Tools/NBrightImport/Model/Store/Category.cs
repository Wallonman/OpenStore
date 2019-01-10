namespace ZIndex.DNN.NBrightImport.Model.Store
{
    public class Category
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }
        public int IdLang { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the parent Category.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        public Category Parent { get; set; }


    }
}
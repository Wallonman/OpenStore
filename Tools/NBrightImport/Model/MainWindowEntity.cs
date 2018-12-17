using System.ComponentModel;
using System.Runtime.CompilerServices;
using ZIndex.DNN.NBrightImport.Properties;

namespace ZIndex.DNN.NBrightImport.Model
{
    internal class MainWindowEntity : INotifyPropertyChanged
    {
        private string _categoryImage;
        private string _categoryName;

        private string _destFile;

        private string _srcPath;

        private string _unitCost;

        /// <summary>
        ///     Gets the Category name
        /// </summary>
        public string CategoryName
        {
            get { return _categoryName; }
            set
            {
                if (value == _categoryName)
                    return;

                _categoryName = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets the base path for product images (e.g.
        ///     C:\Users\Eric\Documents\Work\Svn\McPaquot\Website\Portals\0\productimages\)
        /// </summary>
        public string SrcPath
        {
            get { return _srcPath; }
            set
            {
                if (value == _srcPath)
                    return;

                _srcPath = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets the Category Image name (local filename with extension)
        /// </summary>
        public string CategoryImage
        {
            get { return _categoryImage; }
            set
            {
                if (value == _categoryImage)
                    return;

                _categoryImage = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets the ouput filename
        /// </summary>
        public string DestFile
        {
            get { return _destFile; }
            set
            {
                if (value == _destFile)
                    return;

                _destFile = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets the unit cost
        /// </summary>
        public string UnitCost
        {
            get { return _unitCost; }
            set
            {
                if (value == _unitCost)
                    return;

                _unitCost = value;
                OnPropertyChanged();
            }
        }

        public override string ToString()
        {
            return string.Concat("CategoryName: ", CategoryName, "\nCategoryImage: ", CategoryImage, "\nSrcPath: ",
                SrcPath,
                "\nDestFile: ", DestFile, "\nUnitCost: ", UnitCost);
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ZIndex.DNN.NBrightImport.Properties;

namespace ZIndex.DNN.NBrightImport.Model.Window
{
    internal class MainWindowEntity : INotifyPropertyChanged
    {
        private string _srcPath;

        private string _unitCost;

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
        ///     Gets the unit cost
        /// </summary>
        public string UnitCost
        {
            get
            {
                return _unitCost;}
            set
            {
                if (value == _unitCost)
                    return;

                _unitCost = value;
                OnPropertyChanged();
            }
        }

        public string ImageBasePath { get; set; }
        public string ImageBaseUrl { get; set; }
        public string Culture { get; set; }
        public bool GenerateZip { get; set; }

        public override string ToString()
        {
            return string.Concat("SrcPath: ", SrcPath,
                                 "\nUnitCost: ", UnitCost);
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
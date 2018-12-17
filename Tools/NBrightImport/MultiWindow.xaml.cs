using System;
using System.Globalization;
using System.Windows;
using System.Windows.Forms;
using McPaquot.Utils.Logger;
using NBStoreConnect.Import;
using NBStoreConnect.Model;
using NBStoreConnect.Properties;

namespace NBStoreConnect
{
    /// <summary>
    ///     Interaction logic for MultiWindow.xaml
    /// </summary>
    public partial class MultiWindow : Window
    {
        private readonly MultiWindowEntity _entity;
        private readonly ILog _log = new LoggerBase(typeof(MultiWindow)).Logger;

        public MultiWindow()
        {
            InitializeComponent();

            DataContext = _entity = new MultiWindowEntity
            {
                UnitCost = Settings.Default.UnitCost.ToString(CultureInfo.InvariantCulture),
                Culture = Settings.Default.Culture,
                ImageBasePath = Settings.Default.ImageBasePath,
                ImageBaseUrl = Settings.Default.ImageBaseUrl,
            };
        }

        /// <summary>
        ///     Select the Src Folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSrcOpenFile_Click(object sender, RoutedEventArgs e)
        {
            // Displays an OpenFileDialog so the user can select a folder.
            var dialog = new FolderBrowserDialog();

            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            _entity.SrcPath = dialog.SelectedPath;
            _log.Debug("User selected SrcPath: {0}", _entity.SrcPath);
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnGenerate_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                AppendStatusText($"--- Début de la génération à {DateTime.Now} ---------");
                AppendStatusText(_entity.ToString());

                var importManager = new ImportManager(new StoreParser(new CategoriesParser(), new ProductsParser()),
                    new ImportFileGenerator(new Converter()), new ZipFileGenerator());

                importManager.GenerateImportFiles(_entity.SrcPath
                    , CultureInfo.GetCultureInfo(_entity.Culture)
                    , _entity.ImageBasePath // imageBasePath
                    , _entity.ImageBaseUrl // imageBaseUrl
                    , decimal.Parse(_entity.UnitCost) // unitCost
                );

                AppendStatusText($"Fichiers Xml et Zip créés dans {_entity.SrcPath}");
                AppendStatusText($"--- Fin de la génération à {DateTime.Now} ---------");
            }
            catch (Exception ex)
            {
                AppendStatusText(@"Une erreur s'est produite lors de la génération du fichier :");
                AppendStatusText(ex.Message + "\n\r" + ex.StackTrace);
            }
        }

        /// <summary>
        ///     Add the text to the status panel with a carriage return by default
        /// </summary>
        /// <param name="text"></param>
        /// <param name="addCarriageReturn"></param>
        private void AppendStatusText(string text, bool addCarriageReturn = true)
        {
            TbStatus.Text += text + (addCarriageReturn ? "\n" : string.Empty);
        }
    }
}
using System.IO;
using ZIndex.DNN.NBrightImport.Model.Store;

namespace ZIndex.DNN.NBrightImport.Import
{
    public interface IImportFileGenerator
    {
        void Generate(TextWriter writer, Store store);
    }
}
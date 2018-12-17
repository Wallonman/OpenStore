using System.IO;
using System.IO.Compression;

namespace ZIndex.DNN.NBrightImport.Import
{
    public class ZipFileGenerator : IZipFileGenerator
    {
        public void Zip(string zipFilename, string filesPath, string searchPattern, bool recursive)
        {
            if (File.Exists(zipFilename))
                File.Delete(zipFilename);

            using (var archive = ZipFile.Open(zipFilename, ZipArchiveMode.Create))
            {
                foreach (
                    var f in
                    Directory.EnumerateFiles(filesPath, searchPattern,
                        recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly))
                    archive.CreateEntryFromFile(f, Path.GetFileName(f));
            }
        }
    }
}
namespace ZIndex.DNN.NBrightImport.Import
{
    public interface IZipFileGenerator
    {
        /// <summary>
        /// Generates the Zip file with the images found in the filePath using the searchPattern
        /// </summary>
        /// <param name="zipFilename">the name of the generated zip file</param>
        /// <param name="filesPath">the file path where the files to zip are located</param>
        /// <param name="searchPattern">the search pattern (e.i. *.jpg)</param>
        /// <param name="recursive">if true the file selection is recurisve</param>
        void Zip(string zipFilename, string filesPath, string searchPattern, bool recursive);
    }
}
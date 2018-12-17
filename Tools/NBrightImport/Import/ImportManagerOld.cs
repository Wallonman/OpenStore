using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml.Linq;
using ZIndex.DNN.NBrightImport.Extensions;
using ZIndex.DNN.NBrightImport.Logger;

namespace ZIndex.DNN.NBrightImport.Import
{
    /*
        * 1. get list of files from folder
        * 2. get parameters : 
        *      - CategoryName
        *      - Category ImageURL
        *      - UnitCost
        * 3. generate Xml
        *      - 1 element NB_Store_CategoriesInfo
        *      - n elements P (1 per picture)
        *          - ProductName = picture file name
        *          - ProductRef = picture file name
        *      - save to output destination file
        * 4. Zip images (optional)
        */

    /// <summary>
    /// </summary>
    public class ImportManagerOld
    {
        private readonly ILog _log = new LoggerBase(typeof(ImportManagerOld)).Logger;

        private DateTime _now;


        protected ImportManagerOld()
        {
            _now = DateTime.Now;
        }

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <returns></returns>
        public static ImportManagerOld CreateInstance()
        {
            return new ImportManagerOld();
        }

/*
        /// <summary>
        /// Generates Import files recursively including all folders under productImagesPath
        /// </summary>
        /// <param name="productImagesPath"></param>
        /// <param name="imageBaseUrl"></param>
        /// <param name="imageBasePath"></param>
        /// <param name="unitCost"></param>
        /// <param name="culture"></param>
        public void GenerateRecursiveImportFiles(string productImagesPath, string imageBaseUrl, string imageBasePath,
            decimal unitCost, CultureInfo culture)
        {
            var outputFilename = Path.Combine(productImagesPath, "Import.xml");
            var zipFilename = Path.ChangeExtension(outputFilename, "zip");
            _log.Info("Generating zip file {0}", zipFilename);
            GenerateZipFile(zipFilename, productImagesPath, "*.jpg", true);


            var path = new DirectoryInfo(productImagesPath);
            _log.Info("Getting products");
            var products =
                new List<ISimpleProduct>(path.GetDirectories("*.*", SearchOption.AllDirectories)
                    .Where(p => p.EnumerateFiles("*.jpg").Count() != 0) // work only with folder containing images
                    .Select(p =>
                    {
                        
                        _log.Debug("Working with folder {0}", p.FullName);
                        return new SimpleProduct(p.FullName, p.Name,
                            "categoryImage", imageBaseUrl, imageBasePath,
                            unitCost, culture);
                    }).ToList());
            _log.Info("{0} products found", products.Count());

            _log.Info("Generating file {0}", outputFilename);
            using (var writer = File.CreateText(outputFilename))
            {
                GenerateImportFile(writer, products);
            }
            ;
        }
*/

        public void GenerateMultiImportFiles(string productImagesPath, string imageBaseUrl, string imageBasePath,
            decimal unitCost, CultureInfo culture)
        {
            var path = new DirectoryInfo(productImagesPath);
            path.GetDirectories("*.*", SearchOption.AllDirectories).ToList().ForEach(p =>
            {
                _log.Debug("Working with folder {0}", p.FullName);
                if (p.EnumerateFiles("*.jpg").Count() != 0) // work only with folder containing images
                    GenerateImportFile(Path.Combine(p.FullName, string.Concat(p.Name, ".xml")), p.FullName, p.Name,
                        "categoryImage", imageBaseUrl, imageBasePath, unitCost, culture);
                else
                    _log.Debug("No files in folder {0}", p.FullName);
            });
        }

        /// <summary>
        /// Generates the NB Strore Import Files : the XML containing products and category, and the ZIP containing the images)
        /// </summary>
        /// <param name="outputFilename"></param>
        /// <param name="productImagesPath"></param>
        /// <param name="categoryName"></param>
        /// <param name="categoryImage"></param>
        /// <param name="imageBaseUrl"></param>
        /// <param name="imageBasePath"></param>
        /// <param name="unitCost"></param>
        /// <param name="culture"></param>
        public void GenerateImportFile(string outputFilename, string productImagesPath, string categoryName,
            string categoryImage, string imageBaseUrl, string imageBasePath, decimal unitCost, CultureInfo culture)
        {
            //var zipFilename =  Path.Combine(productImagesPath, string.Concat(Path.GetFileName(productImagesPath), ".zip"));
            var zipFilename = Path.ChangeExtension(outputFilename, "zip");
            _log.Info("Generating zip file {0}", zipFilename);
            GenerateZipFile(zipFilename, productImagesPath, "*.jpg", false);

            _log.Info("Generating file {0}", outputFilename);
            using (var writer = File.CreateText(outputFilename))
            {
                GenerateImportFile(writer,
                    new SimpleProduct(productImagesPath, categoryName, categoryImage, imageBaseUrl, imageBasePath,
                        unitCost, culture));
            }
        }


        /// <summary>
        /// Generates the Zip file with the images found in the filePath using the searchPattern
        /// </summary>
        /// <param name="zipFilename">the name of the generated zip file</param>
        /// <param name="filesPath">the file path where the files to zip are located</param>
        /// <param name="searchPattern">the search pattern (e.i. *.jpg)</param>
        /// <param name="recursive">if true the file selection is recurisve</param>
        public void GenerateZipFile(string zipFilename, string filesPath, string searchPattern, bool recursive)
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

        /// <summary>
        /// Generates the Zip file with the images found in the filePath using the searchPattern
        /// </summary>
        /// <param name="zipFilename">the name of the generated zip file</param>
        /// <param name="filesPath">the file path where the files to zip are located</param>
        /// <param name="searchPattern">the search pattern (e.i. *.jpg)</param>
        public void GenerateZipFile(string zipFilename, string filesPath, string searchPattern)
        {
            GenerateZipFile(zipFilename, filesPath, searchPattern, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="simpleProducts"></param>
        public void GenerateImportFile(TextWriter writer, List<ISimpleProduct> simpleProducts)
        {
            Debug.Assert(writer != null, "writer != null");
            Debug.Assert(simpleProducts != null, "simpleProducts != null");

            var culture = simpleProducts.First().Culture.ToString(); // same culture for all products/categories

            var categories = simpleProducts.GroupBy(product => product.CategoryName);

            var root =
                new XElement("root"
                    ,
                    new XElement("products"
                        , new XElement(culture
                            , simpleProducts.Select(product => CreateProducts(product))
                            )
                        )
                    ,
                    new XElement("categories"
                        , new XElement(culture
//                            , simpleProducts.Select(product => CreateNBStoreCategoriesInfoElement(product)))
                            , categories.ToList().Select(products => 
                            CreateNBStoreCategoriesInfoElement(products.ToList().First()))
                            )
                        )
                    );

            root.Save(writer);
        }

        /// <summary>
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="simpleProduct"></param>
        public void GenerateImportFile(TextWriter writer, ISimpleProduct simpleProduct)
        {
            Debug.Assert(writer != null, "writer != null");
            Debug.Assert(simpleProduct != null, "simpleProduct != null");

            var root =
                new XElement("root"
                    , new XElement("products"
                        , new XElement(simpleProduct.Culture.ToString()
                            , CreateProducts(simpleProduct)
                            )
                        )
                    , new XElement("categories"
                        , new XElement(simpleProduct.Culture.ToString()
                            , CreateNBStoreCategoriesInfoElement(simpleProduct)))
                    );

            root.Save(writer);
        }

        /// <summary>
        ///     Returns an array of &lt;P> elements with product information childs
        /// </summary>
        /// <param name="simpleProduct"></param>
        /// <returns></returns>
        private IEnumerable<object> CreateProducts(ISimpleProduct simpleProduct)
        {
            var i = 0;
            return simpleProduct.ProductNames.Select(productName =>
            {
                var product = string.Format(@"<P>
                                                    <NB_Store_ProductsInfo>
                                                        <ProductID>{7}</ProductID>
                                                        <CreatedDate>{0}</CreatedDate>
                                                        <IsDeleted>false</IsDeleted>
                                                        <ProductRef>{1}</ProductRef>
                                                        <Lang>{4}</Lang>
                                                        <ProductName>{2}</ProductName>
                                                        <XMLData/>
                                                        <IsHidden>false</IsHidden>
                                                    </NB_Store_ProductsInfo>
                                                    <M>
                                                        <NB_Store_ModelInfo>
                                                            <ModelID>{8}</ModelID>
                                                            <ProductID>{7}</ProductID>
                                                            <ListOrder>1</ListOrder>
                                                            <UnitCost>{3}</UnitCost>
                                                            <ModelRef>{1}</ModelRef>
                                                            <Lang>{4}</Lang>
                                                            <QtyRemaining>-1</QtyRemaining>
                                                            <QtyTrans>0</QtyTrans>
                                                            <QtyTransDate>{0}</QtyTransDate>
                                                            <Deleted>false</Deleted>
                                                            <QtyStockSet>0</QtyStockSet>
                                                            <DealerCost>0.0000</DealerCost>
                                                            <PurchaseCost>0.0000</PurchaseCost>
                                                        </NB_Store_ModelInfo>
                                                    </M>
                                                    <I>
                                                        <NB_Store_ProductImageInfo>
                                                            <ImageID>{9}</ImageID>
                                                            <ProductID>{7}</ProductID>
                                                            <ImagePath>{5}</ImagePath>
                                                            <ListOrder>1</ListOrder>
                                                            <Hidden>false</Hidden>
                                                            <Lang>{4}</Lang>
                                                            <ImageDesc/>
                                                            <ImageURL>{6}</ImageURL>
                                                        </NB_Store_ProductImageInfo>
                                                    </I>
                                                    <D/>
                                                    <C>
                                                        <NB_Store_ProductCategoryInfo>
                                                            <ProductID>{7}</ProductID>
                                                            <CategoryID>1</CategoryID>
                                                        </NB_Store_ProductCategoryInfo>
                                                    </C>
                                                    <R/>
                                                    <options/>
                                                </P>"
                    , _now.ToXsdDatetime() // <CreatedDate>2013-04-27T09:30:06.8</CreatedDate>
                    , productName // <ProductRef>CCC</ProductRef>
                    , productName // <ProductName>CCC</ProductName>
                    , simpleProduct.UnitCost.ToUniCost() // <UnitCost>14.0000</UnitCost>
                    , simpleProduct.Culture // <Lang>en-US</Lang>
                    , simpleProduct.ToProductImagePath(productName)
                    // <ImagePath>C:\Users\Eric\Documents\Work\Svn\McPaquot\Website\Portals\0\productimages\2_0d922-11111.jpg</ImagePath>
                    , simpleProduct.ToProductImageUrl(productName)
                    // <ImageURL>/mcpaquot/Portals/0/productimages/2_0d922.jpg</ImageURL>
                    , i++ // ProductId
                    , i++ // ModelId
                    , i++ // ImageId
                    );
                return XElement.Parse(product);
            });
        }

        /// <summary>
        ///     Returns a new &lt;NB_Store_CategoriesInfo> element
        /// </summary>
        /// <returns></returns>
        private XElement CreateNBStoreCategoriesInfoElement(ISimpleProduct simpleProduct)
        {
            var i = 0;
            var category = string.Format(@"<NB_Store_CategoriesInfo>
                                                <CategoryID>{5}</CategoryID>
                                                <PortalID>0</PortalID>
                                                <Archived>false</Archived>
                                                <Hide>false</Hide>
                                                <CreatedByUser>1</CreatedByUser>
                                                <CreatedDate>{0}</CreatedDate>
                                                <ParentCategoryID>0</ParentCategoryID>
                                                <ListOrder>1</ListOrder>
                                                <Lang>{1}</Lang>
                                                <CategoryName>{2}</CategoryName>
                                                <ParentName/>
                                                <CategoryDesc></CategoryDesc>
                                                <Message/>
                                                <ProductCount>{3}</ProductCount>
                                                <ProductTemplate/>
                                                <ListItemTemplate/>
                                                <ListAltItemTemplate/>
                                                <ImageURL>{4}</ImageURL>
                                                <SEOPageTitle/>
                                                <SEOName/>
                                                <MetaDescription/>
                                                <MetaKeywords/>
                                            </NB_Store_CategoriesInfo>"
                , _now.ToXsdDatetime() // <CreatedDate>2013-04-27T09:30:06.8</CreatedDate>
                , simpleProduct.Culture // <Lang>en-US</Lang>
                , simpleProduct.CategoryName // <CategoryName>Reportage C</CategoryName>
                , simpleProduct.ProductNames.Count // <ProductCount>2</ProductCount>
                , simpleProduct.ToCategoryImageUrl()
                , i++ // category id
                );

            return XElement.Parse(category);
        }
    }
}
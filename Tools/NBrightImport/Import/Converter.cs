using System;
using System.IO;
using NBrightDNN;
using ZIndex.DNN.NBrightImport.Model.Store;

namespace ZIndex.DNN.NBrightImport.Import
{
    public class Converter : IConverter
    {
       /// <summary>
       /// To the image path.
       /// </summary>
       /// <param name="product">The product.</param>
       /// <param name="imageBasePath">The image base path.</param>
       /// <returns></returns>
       /// <exception cref="System.ArgumentNullException">
       /// product
       /// or
       /// imageBasePath
       /// </exception>
        public string ToImagePath(Product product, string imageBasePath)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            if (imageBasePath == null) throw new ArgumentNullException(nameof(imageBasePath));

            return Path.Combine(imageBasePath, product.ImageFilename);
        }

       /// <summary>
       /// To the image base URL.
       /// </summary>
       /// <param name="product">The product.</param>
       /// <param name="imageBaseUrl">The image base URL.</param>
       /// <returns></returns>
       /// <exception cref="System.ArgumentNullException">
       /// product
       /// or
       /// imageBaseUrl
       /// </exception>
        public string ToImageBaseUrl(Product product, string imageBaseUrl)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            if (imageBaseUrl == null) throw new ArgumentNullException(nameof(imageBaseUrl));

            var uri = new Uri(string.Concat(imageBaseUrl.TrimEnd('/', '\\'), "/", product.ImageFilename) , UriKind.Relative);

            return uri.ToString();
        }

        private NBrightInfo CreateNBrightInfo(Store store, string typeCode)
        {
            var nbi = new NBrightInfo(true)
            {
                PortalId = 0,//todo: add portalId in store
                GUIDKey = "",
                Lang = store.Culture.ToString(),
                ModifiedDate = DateTime.Now,
                ModuleId = -1,
                ParentItemId = 0,
                RowCount = 0,
                TextData = "",
                TypeCode = typeCode,
                UserId = 0,
                XMLData = "",// ?????
               //XMLDoc =  ????
                XrefItemId = 0,

            };
            return nbi;

        }

        public NBrightInfo CreateProduct(Product product, Store store)
        {
            var nbi = CreateNBrightInfo(store, "PRD");
            nbi.ItemID = product.Id;
            nbi.XMLData = $@"<genxml></genxml>";
            return nbi;
        }

        public NBrightInfo CreateProductLang(Product product, Store store)
        {
            var nbi = CreateNBrightInfo(store, "PRDLANG");
            nbi.ItemID = product.Id+1;
            nbi.XMLData = $@"<genxml></genxml>";
            return nbi;
        }

        public NBrightInfo CreateCategory(Category category, Store store)
        {
            var nbi = CreateNBrightInfo(store, "CATEGORY");
            nbi.ItemID = category.Id;//category.Id*10;
            nbi.ParentItemId = category.Parent?.Id ?? 0;//category.Parent?.Id*10 ?? 0;
            nbi.XMLData = $@"<genxml>
                                <files/>
                                <hidden>
                                    <recordsortorder datatype=""double"">99999</recordsortorder>
                                </hidden>
                                <textbox>
                                    <txtcategoryref>{category.Name}</txtcategoryref>
                                    <propertyref/>
                                </textbox>
                                <checkbox>
                                    <chkishidden>False</chkishidden>
                                    <chkdisable>False</chkdisable>
                                </checkbox>
                                <dropdownlist>
                                    <ddlgrouptype>cat</ddlgrouptype>
                                    <ddlparentcatid>{category.Parent?.Id ?? 0}</ddlparentcatid>
                                    <ddlattrcode/>
                                    <selectgrouptype>null</selectgrouptype>
                                    <selectcatid/>
                                </dropdownlist>
                                <checkboxlist/>
                                <radiobuttonlist/>
                             </genxml>";

            return nbi;
        }
    
        public NBrightInfo CreateCategoryLang(Category category, Store store)
        {
            var nbi = CreateNBrightInfo(store, "CATEGORYLANG");
            nbi.ItemID = category.Id+1;//category.Id*10+1;
            nbi.ParentItemId = category.Id;//category.Id*10;
            nbi.XMLData = $@"<genxml>
                              <files/>
                            <hidden/>
                            <textbox>
                            <txtcategoryname>{category.Name}</txtcategoryname>
                            <txtcategoryref>{category.Name}</txtcategoryref>
                            </textbox>
                            <checkbox/>
                            <dropdownlist/>
                            <checkboxlist/>
                            <radiobuttonlist/>
                            </genxml> ";
            return nbi;
        }
    
    }
}
using System;
using System.Collections.Generic;
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
                XMLData = "",
               //XMLDoc =  ????
                XrefItemId = 0,

            };
            return nbi;

        }

        public List<NBrightInfo> CreateProductElements(Product product, Store store)
        {
            return new List<NBrightInfo>
                {CreateProduct(product, store), CreateProductLang(product, store), CreateCategoryXRef(product, store)};
        }
        private NBrightInfo CreateProduct(Product product, Store store)
        {
            var nbi = CreateNBrightInfo(store, "PRD");
            nbi.ItemID = product.Id;
            nbi.XMLData = $@"<genxml>
                              <textbox>
                                <txtproductref>{product.Name}</txtproductref>
                                <txtmodelref>{product.Name}</txtmodelref>
                              </textbox>
                              <models>
                                <genxml>
                                  <textbox>
                                    <txtmodelref>{product.Name}</txtmodelref>
                                    <txtunitcost datatype=""double"">{store.ProductUnitCost}</txtunitcost>
                                    <unitqty datatype=""double"">1</unitqty>
                                  </textbox>
                                </genxml>
                              </models>
                              <options />
                              <imgs>
                                <genxml>
                                  <hidden>
                                    <imageurl>{ToImageBaseUrl(product, store.ImageBaseUrl)}</imageurl>
                                    <imagepath>{ToImagePath(product, store.ImageBasePath)}</imagepath>
                                  </hidden>
                                </genxml>
                              </imgs>
                            </genxml>";
            return nbi;
        }

        private NBrightInfo CreateProductLang(Product product, Store store)
        {
            var nbi = CreateNBrightInfo(store, "PRDLANG");
            nbi.ItemID = product.IdLang;
            nbi.ParentItemId = product.Id;
            nbi.XMLData = $@"<genxml>
                                  <textbox>
                                    <txtproductname update=""lang"">{product.Name}</txtproductname>
                                    <txtmodelname update=""lang"">{product.Name}</txtmodelname>
                                  </textbox>
                                  <models>
                                    <genxml>
                                      <textbox>
                                        <txtmodelname>{product.Name}</txtmodelname>
                                      </textbox>
                                    </genxml>
                                  </models>
                                </genxml>";
            return nbi;
        }

        public List<NBrightInfo> CreateCategoryElements(Category category, Store store)
        {
            return new List<NBrightInfo>
                {CreateCategory(category, store), CreateCategoryLang(category, store)};
        }

        private NBrightInfo CreateCategory(Category category, Store store)
        {
            var nbi = CreateNBrightInfo(store, "CATEGORY");
            nbi.ItemID = category.Id;
            nbi.ParentItemId = category.Parent?.Id ?? 0;
            nbi.XMLData = $@"<genxml>
                                <textbox>
                                    <txtcategoryref>{category.Name}</txtcategoryref>
                                    <propertyref/>
                                </textbox>
                                <dropdownlist>
                                    <ddlgrouptype>cat</ddlgrouptype>
                                    <ddlparentcatid>{category.Parent?.Id ?? 0}</ddlparentcatid>
                                    <ddlattrcode/>
                                    <selectgrouptype>null</selectgrouptype>
                                    <selectcatid/>
                                </dropdownlist>
                             </genxml>";

            return nbi;
        }
    
        public NBrightInfo CreateCategoryLang(Category category, Store store)
        {
            var nbi = CreateNBrightInfo(store, "CATEGORYLANG");
            nbi.ItemID = category.IdLang;
            nbi.ParentItemId = category.Id;
            nbi.XMLData = $@"<genxml>
                            <textbox>
                                <txtcategoryname>{category.Name}</txtcategoryname>
                                <txtcategoryref>{category.Name}</txtcategoryref>
                            </textbox>
                            </genxml>";
            return nbi;
        }
    
        private NBrightInfo CreateCategoryXRef(Product product, Store store)
        {
            var nbi = CreateNBrightInfo(store, "CATXREF");
            nbi.Lang = string.Empty; // force de lang to "", if not the import is not correct (the product is not editable) bug in openstore ?
            nbi.ItemID = product.IdCatXRef;
            nbi.XrefItemId = product.Category.Id;
            nbi.ParentItemId = product.Id;
            nbi.XMLData = $@"<genxml></genxml> ";
            return nbi;
        }
    
    }
}

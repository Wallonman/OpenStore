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
                GUIDKey = Guid.NewGuid().ToString().Substring(0, 8),
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
                              <files />
                              <textbox>
                                <txtproductref>{product.Name}</txtproductref>
                                <manufacturer />
                                <txtaddmodelqty>1</txtaddmodelqty>
                                <txtmodelref>{product.Name}</txtmodelref>
                                <txtunitcost datatype=""double"">{store.ProductUnitCost}</txtunitcost>
                                <txtsaleprice datatype=""double"">0</txtsaleprice>
                                <txtdealercost datatype=""double"">0</txtdealercost>
                                <txtdealersale datatype=""double"">0</txtdealersale>
                                <txtpurchasecost datatype=""double"">0</txtpurchasecost>
                                <txtbarcode />
                                <weight datatype=""double"">0</weight>
                                <depth datatype=""double"">0</depth>
                                <width datatype=""double"">0</width>
                                <height datatype=""double"">0</height>
                                <unitqty datatype=""double"">1</unitqty>
                                <txtqtyremaining datatype=""double"">0</txtqtyremaining>
                                <txtqtyminstock datatype=""double"">0</txtqtyminstock>
                                <txtqtystockset datatype=""double"">0</txtqtystockset>
                                <unit />
                                <availabledate datatype=""date"" />
                                <delay />
                                <txtaddoptqty>1</txtaddoptqty>
                                <txtaddoptvalueqty>1</txtaddoptvalueqty>
                              </textbox>
                              <checkbox>
                                <chkishidden>False</chkishidden>
                                <chkdisable>False</chkdisable>
                                <chkdisablesale>False</chkdisablesale>
                                <chkdisabledealer>False</chkdisabledealer>
                                <chkstockon>False</chkstockon>
                                <chkdeleted>False</chkdeleted>
                                <chkdealeronly>False</chkdealeronly>
                              </checkbox>
                              <dropdownlist>
                                <selectcategory>{product.Category.Id}</selectcategory>
                                <selectgrouptype>null</selectgrouptype>
                              </dropdownlist>
                              <checkboxlist />
                              <radiobuttonlist />
                              <models>
                                <genxml>
                                  <files />
                                  <hidden>
                                    <modelid>{Guid.NewGuid().ToString().Substring(0, 8)}</modelid>
                                  </hidden>
                                  <textbox>
                                    <txtmodelref>{product.Name}</txtmodelref>
                                    <txtunitcost datatype=""double"">{store.ProductUnitCost}</txtunitcost>
                                    <txtsaleprice datatype=""double"">0</txtsaleprice>
                                    <txtdealercost datatype=""double"">0</txtdealercost>
                                    <txtdealersale datatype=""double"">0</txtdealersale>
                                    <txtpurchasecost datatype=""double"">0</txtpurchasecost>
                                    <txtbarcode />
                                    <weight datatype=""double"">0</weight>
                                    <depth datatype=""double"">0</depth>
                                    <width datatype=""double"">0</width>
                                    <height datatype=""double"">0</height>
                                    <unitqty datatype=""double"">1</unitqty>
                                    <txtqtyremaining datatype=""double"">0</txtqtyremaining>
                                    <txtqtyminstock datatype=""double"">0</txtqtyminstock>
                                    <txtqtystockset datatype=""double"">0</txtqtystockset>
                                    <unit />
                                    <availabledate datatype=""date"" />
                                    <delay />
                                  </textbox>
                                  <checkbox>
                                    <chkdisablesale>False</chkdisablesale>
                                    <chkdisabledealer>False</chkdisabledealer>
                                    <chkstockon>False</chkstockon>
                                    <chkishidden>False</chkishidden>
                                    <chkdeleted>False</chkdeleted>
                                    <chkdealeronly>False</chkdealeronly>
                                  </checkbox>
                                  <dropdownlist>
                                    <taxrate />
                                    <modelstatus>010</modelstatus>
                                  </dropdownlist>
                                  <checkboxlist />
                                  <radiobuttonlist />
                                </genxml>
                              </models>
                              <options />
                              <imgs>
                                <genxml>
                                  <files />
                                  <hidden>
                                    <imageref></imageref>
                                    <imageurl>{ToImageBaseUrl(product, store.ImageBaseUrl)}</imageurl>
                                    <imagepath>{ToImagePath(product, store.ImageBasePath)}</imagepath>
                                  </hidden>
                                  <textbox />
                                  <checkbox>
                                    <chkhidden>False</chkhidden>
                                  </checkbox>
                                  <dropdownlist />
                                  <checkboxlist />
                                  <radiobuttonlist />
                                </genxml>
                              </imgs>
                              <docs />
                              <importref>{Guid.NewGuid().ToString().Substring(0, 8)}</importref>
                              <calcfromprice datatype=""double"">0</calcfromprice>
                              <calcsaleprice datatype=""double"">0</calcsaleprice>
                              <calcfrombulkprice datatype=""double"">0</calcfrombulkprice>
                              <calcsalebulkprice datatype=""double"">0</calcsalebulkprice>
                              <calcdealerfromprice datatype=""double"">0</calcdealerfromprice>
                              <calcdealersaleprice datatype=""double"">0</calcdealersaleprice>
                              <calcfrompriceunit datatype=""double"">0</calcfrompriceunit>
                              <calcsalepriceunit datatype=""double"">0</calcsalepriceunit>
                              <calcfrombulkpriceunit datatype=""double"">0</calcfrombulkpriceunit>
                              <calcsalebulkpriceunit datatype=""double"">0</calcsalebulkpriceunit>
                              <calcdealerfrompriceunit datatype=""double"">0</calcdealerfrompriceunit>
                              <calcdealersalepriceunit datatype=""double"">0</calcdealersalepriceunit>
                              <calchighunitprice datatype=""double"">0</calchighunitprice>
                              <calchighdealerunitprice datatype=""double"">0</calchighdealerunitprice>
                              <calcbestprice datatype=""double"">0</calcbestprice>
                              <calcbestpriceunit datatype=""double"">0</calcbestpriceunit>
                              <calcdealerbestprice datatype=""double"">0</calcdealerbestprice>
                              <calcdealerbestpriceunit datatype=""double"">0</calcdealerbestpriceunit>
                              <calcbestpriceall datatype=""double"">0</calcbestpriceall>
                              <calcbestpriceallunit datatype=""double"">0</calcbestpriceallunit>
                            </genxml>";
            return nbi;
        }

        private NBrightInfo CreateProductLang(Product product, Store store)
        {
            var nbi = CreateNBrightInfo(store, "PRDLANG");
            nbi.ItemID = product.IdLang;
            nbi.ParentItemId = product.Id;
            nbi.XMLData = $@"<genxml>
                                  <files />
                                  <textbox>
                                    <description />
                                    <txtproductname update=""lang"">{product.Name}</txtproductname>
                                    <extrafield update=""lang"" />
                                    <txtsummary update=""lang"" />
                                    <txtmodelname update=""lang"">{product.Name}</txtmodelname>
                                    <txtextra update=""lang"" />
                                    <txtseoname update=""lang"" />
                                    <txtseopagetitle update=""lang"" />
                                    <seodescription update=""lang"" />
                                    <txtimagedesc update=""lang"" />
                                  </textbox>
                                  <checkbox>
                                    <chkishiddenlang update=""lang"">False</chkishiddenlang>
                                  </checkbox>
                                  <dropdownlist />
                                  <checkboxlist />
                                  <radiobuttonlist />
                                  <options />
                                  <docs />
                                  <models>
                                    <genxml>
                                      <files />
                                      <hidden />
                                      <textbox>
                                        <txtmodelname>{product.Name}</txtmodelname>
                                        <txtextra />
                                      </textbox>
                                      <checkbox />
                                      <dropdownlist />
                                      <checkboxlist />
                                      <radiobuttonlist />
                                    </genxml>
                                  </models>
                                  <edt>
                                    <description />
                                  </edt>
                                  <imgs>
                                    <genxml>
                                      <files />
                                      <hidden>
                                        <fimageurl>{ToImageBaseUrl(product, store.ImageBaseUrl)}</fimageurl>
                                        <fimagepath>{ToImagePath(product, store.ImageBasePath)}</fimagepath>
                                        <fimageref></fimageref>
                                      </hidden>
                                      <textbox>
                                        <txtimagedesc />
                                      </textbox>
                                      <checkbox />
                                      <dropdownlist />
                                      <checkboxlist />
                                      <radiobuttonlist />
                                    </genxml>
                                  </imgs>
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
            nbi.ItemID = category.IdLang;
            nbi.ParentItemId = category.Id;
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
    
        private NBrightInfo CreateCategoryXRef(Product product, Store store)
        {
            var nbi = CreateNBrightInfo(store, "CATXREF");
            nbi.Lang = string.Empty; // force de lang to "", if not the import is not correct (the product is not editable) bug in openstore ?
            nbi.ItemID = product.IdCatXRef;
            nbi.XrefItemId = product.Category.Id;
            nbi.ParentItemId = product.Id;
            nbi.XMLData = $@"<genxml>
                                <sort></sort>
                                <typecode>PRD</typecode>
                            </genxml> ";
            return nbi;
        }
    
    }
}

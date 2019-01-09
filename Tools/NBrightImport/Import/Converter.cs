﻿using System;
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
            nbi.XMLData = $@"<genxml>
                              <files />
                              <hidden>
                                <imageref>{product.Name}</imageref>
                                <imageurl>{ToImageBaseUrl(product, store.ImageBaseUrl)}</imageurl>
                                <imagepath>{ToImagePath(product, store.ImageBasePath)}</imagepath>
                              </hidden>
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
                                <unitqty datatype=""double"">0</unitqty>
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
                                <chkhidden>False</chkhidden>
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
                                    <modelid>{product.Name}</modelid>
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
                                    <unitqty datatype=""double"">0</unitqty>
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
                                    <imageref>{product.Name}</imageref>
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
                              <importref>{product.Name}</importref>
                              <calcfromprice datatype=""double"">10</calcfromprice>
                              <calcsaleprice datatype=""double"">0</calcsaleprice>
                              <calcfrombulkprice datatype=""double"">0</calcfrombulkprice>
                              <calcsalebulkprice datatype=""double"">0</calcsalebulkprice>
                              <calcdealerfromprice datatype=""double"">0</calcdealerfromprice>
                              <calcdealersaleprice datatype=""double"">0</calcdealersaleprice>
                              <calcfrompriceunit datatype=""double"">10</calcfrompriceunit>
                              <calcsalepriceunit datatype=""double"">0</calcsalepriceunit>
                              <calcfrombulkpriceunit datatype=""double"">0</calcfrombulkpriceunit>
                              <calcsalebulkpriceunit datatype=""double"">0</calcsalebulkpriceunit>
                              <calcdealerfrompriceunit datatype=""double"">0</calcdealerfrompriceunit>
                              <calcdealersalepriceunit datatype=""double"">0</calcdealersalepriceunit>
                              <calchighunitprice datatype=""double"">10</calchighunitprice>
                              <calchighdealerunitprice datatype=""double"">0</calchighdealerunitprice>
                              <calcbestprice datatype=""double"">10</calcbestprice>
                              <calcbestpriceunit datatype=""double"">10</calcbestpriceunit>
                              <calcdealerbestprice datatype=""double"">0</calcdealerbestprice>
                              <calcdealerbestpriceunit datatype=""double"">0</calcdealerbestpriceunit>
                              <calcbestpriceall datatype=""double"">10</calcbestpriceall>
                              <calcbestpriceallunit datatype=""double"">10</calcbestpriceallunit>
                              <defaultcatid>121</defaultcatid>
                            </genxml>";
            return nbi;
        }

        public NBrightInfo CreateProductLang(Product product, Store store)
        {
            var nbi = CreateNBrightInfo(store, "PRDLANG");
            nbi.ItemID = product.Id+1;
            nbi.ParentItemId = product.Id;
            nbi.XMLData = $@"<genxml>
                                  <files />
                                  <hidden>
                                    <fimageurl update=""lang"">{ToImageBaseUrl(product, store.ImageBaseUrl)}</fimageurl>
                                    <fimagepath update=""lang"">{ToImagePath(product, store.ImageBasePath)}</fimagepath>
                                    <fimageref update=""lang"">{product.Name}</fimageref>
                                  </hidden>
                                  <textbox>
                                    <description />
                                    <txtproductname update=""lang"" />
                                    <extrafield update=""lang"" />
                                    <txtsummary update=""lang"" />
                                    <txtmodelname update=""lang"" />
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
                                        <txtmodelname />
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
                                        <fimageref>{product.Name}</fimageref>
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
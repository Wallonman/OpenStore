using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using NBrightDNN;
using ZIndex.DNN.NBrightImport.Extensions;
using ZIndex.DNN.NBrightImport.Model.Store;

namespace ZIndex.DNN.NBrightImport.Import
{
    public class ImportV2FileGenerator : IImportFileGenerator
    {
        private readonly IConverter _converter;

        public ImportV2FileGenerator(IConverter converter)
        {
            _converter = converter;
        }

        public void Generate(TextWriter writer, Store store)
        {
            if (writer == null) throw new ArgumentNullException(nameof(writer));
            if (store == null) throw new ArgumentNullException(nameof(store));
            if (!store.Products.Any()) throw new ArgumentNullException(nameof(store.Products));
            if (!store.Categories.Any()) throw new ArgumentNullException(nameof(store.Categories));
            if (store.Culture == null) throw new ArgumentNullException("store.Culture");
            if (store.ImageBasePath == null) throw new ArgumentNullException("store.ImageBasePath");
            if (store.ImageBaseUrl == null) throw new ArgumentNullException("store.ImageBaseUrl");

            // initialize the id for models and images (use a value > product or category id)
            var id = store.Products.Max(product => product.Id) + store.Categories.Max(category => category.Id);

            var root =
                new XElement("root"
                    ,
                    new XElement("products"
                        , new XElement(store.Culture.ToString()
                            , store.Products.Select(product => CreateProduct(product, store, ref id))
                        )
                    )
                    ,
                    new XElement("categories"
                        , new XElement(store.Culture.ToString()
                            , store.Categories.ToList().Select(category => CreateCategory(category, store))
                        )
                    )
                );

            root.Save(writer);
        }

        /// <summary>
        /// Creates the products.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="store">The store.</param>
        /// <param name="id">The identifier for models and images, it will be incremented.</param>
        /// <returns></returns>
        private XElement CreateProduct(Product product, Store store, ref int id)
        {
            var element = string.Format(@"<P>
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
                                                            <CategoryID>{10}</CategoryID>
                                                        </NB_Store_ProductCategoryInfo>
                                                    </C>
                                                    <R/>
                                                    <options/>
                                                </P>"
                    , DateTime.Now.ToXsdDatetime() // {0} <CreatedDate>2013-04-27T09:30:06.8</CreatedDate>
                    , product.Name // {1} <ProductRef>CCC</ProductRef>
                    , product.Name // {2} <ProductName>CCC</ProductName>
                    , store.ProductUnitCost.ToUniCost() // {3} <UnitCost>14.0000</UnitCost>
                    , store.Culture // {4} <Lang>en-US</Lang>
                    , _converter.ToImagePath(product, store.ImageBasePath) // {5} 
                                                                     // <ImagePath>C:\Users\Eric\Documents\Work\Svn\McPaquot\Website\Portals\0\productimages\2_0d922-11111.jpg</ImagePath>
                    , _converter.ToImageBaseUrl(product, store.ImageBaseUrl) // {6}
                                                                   // <ImageURL>/mcpaquot/Portals/0/productimages/2_0d922.jpg</ImageURL>
                    , product.Id // {7} ProductId
                    , id++ // {8} ModelId
                    , id++ // {9} ImageId
                    , product.Category.Id // {10} CategoryId
                );
                return XElement.Parse(element);
            }

        private XElement CreateCategory(Category category, Store store)
        {
            var element = string.Format(@"<NB_Store_CategoriesInfo>
                                                <CategoryID>{4}</CategoryID>
                                                <PortalID>0</PortalID>
                                                <Archived>false</Archived>
                                                <Hide>false</Hide>
                                                <CreatedByUser>1</CreatedByUser>
                                                <CreatedDate>{0}</CreatedDate>
                                                <ParentCategoryID>{5}</ParentCategoryID>
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
                                                <ImageURL></ImageURL>
                                                <SEOPageTitle/>
                                                <SEOName/>
                                                <MetaDescription/>
                                                <MetaKeywords/>
                                            </NB_Store_CategoriesInfo>"
                , DateTime.Now.ToXsdDatetime() // {0} <CreatedDate>2013-04-27T09:30:06.8</CreatedDate>
                , store.Culture // {1} <Lang>en-US</Lang>
                , category.Name // {2} <CategoryName>Reportage C</CategoryName>
                , store.Products.Count(product => product.Category.Id == category.Id) // {3} <ProductCount>2</ProductCount>
                , category.Id // {4} category id
                , category.Parent?.Id.ToString() ?? "0" // {5} parent category id
            );

            return XElement.Parse(element);
        }
    }
}
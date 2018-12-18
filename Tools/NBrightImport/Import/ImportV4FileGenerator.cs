using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using NBrightDNN;
using ZIndex.DNN.NBrightImport.Extensions;
using ZIndex.DNN.NBrightImport.Model.Store;

namespace ZIndex.DNN.NBrightImport.Import
{
    public class ImportV4FileGenerator : IImportFileGenerator
    {
        private readonly IConverter _converter;

        public ImportV4FileGenerator(IConverter converter)
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


            var nbi = new NBrightInfo(false);
            nbi.Lang = store.Culture.ToString();
//            nbi.


/*            var root =
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

            root.Save(writer);*/
        }

    }
}
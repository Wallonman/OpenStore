using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
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

            var strXml = new StringBuilder("<root>");

            // add products
            store.Products.ForEach(product => 
                _converter.CreateProductElements(product, store)
                    .ForEach(nbi => strXml.Append(nbi.ToXmlItem())));
//            store.Products.ForEach(product => strXml.Append(_converter.CreateProductElements(product, store).ToXmlItem()));
//            store.Products.ForEach(product => strXml.Append(_converter.CreateProductLang(product, store).ToXmlItem()));
//            store.Products.ForEach(product => strXml.Append(_converter.CreateCategoryXRef(product, store).ToXmlItem()));

            // add categories
            store.Categories.ForEach(category =>
                _converter.CreateCategoryElements(category, store)
                    .ForEach(nbi => strXml.Append(nbi.ToXmlItem())));
//            store.Categories.ForEach(category => strXml.Append(_converter.CreateCategoryElements(category, store).ToXmlItem()));
//            store.Categories.ForEach(category => strXml.Append(_converter.CreateCategoryLang(category, store).ToXmlItem()));

            strXml.Append("</root>");

            writer.Write(strXml.ToString());

        }

    }
}
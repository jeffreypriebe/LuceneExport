using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using Lucene.Net.Documents;
using SimpleLucene;

namespace LuceneExport
{
    public class Export
    {
        private QueryResult results;
        private IList<string> fields;
        private string exportFilePath;

        public Export(QueryResult SearchQueryResults, IList<string> Fields, string ExportFilePath)
        {
            this.results = SearchQueryResults;
            this.fields = Fields;
            this.exportFilePath = ExportFilePath;
        }

        public bool Save()
        {
            using (var tw = File.CreateText(exportFilePath))
            using (var csv = new CsvWriter(tw))
            {
                csv.Configuration.QuoteAllFields = true;

                WriteHeaders(csv, fields);

                foreach (var result in results.Documents)
                {
                    foreach (var Field in fields)
                        csv.WriteField(FieldValue(result, Field));

                    csv.NextRecord();
                }
            }

            return true;
        }

        private void WriteHeaders(CsvWriter csv, IList<string> Fields)
        {
            foreach (var Field in Fields)
                csv.WriteField(Field);
            csv.NextRecord();
        }

        private string FieldValue(Document Doc, string FieldName)
        {
            var Field = Doc.GetField(FieldName);
            if (Field == null)
                return string.Empty;
            else
                return Field.StringValue();
        }
    }
}

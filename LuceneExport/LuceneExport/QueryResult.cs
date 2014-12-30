using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lucene.Net.Documents;

namespace LuceneExport
{
    public class QueryResult
    {
        public IList<Document> Documents { get; private set; }
        public int TotalHits { get; private set; }

        public QueryResult(IList<Document> Documents, int TotalHits)
        {
            this.Documents = Documents;
            this.TotalHits = TotalHits;
        }
    }
}

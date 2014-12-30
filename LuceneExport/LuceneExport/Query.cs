using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lucene.Net.Documents;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using SimpleLucene;

namespace LuceneExport
{
    public class Query
    {
        private string indexDirectory;
        private IIndexSearcher indexSearcher;
        private IIndexSearcher IndexSearcher
        {
            get
            {
                if (indexSearcher == null)
                {
                    var IndexDirectoryInfo = new DirectoryInfo(indexDirectory);
                    indexSearcher = new DirectoryIndexSearcher(IndexDirectoryInfo, true);
                }
                return indexSearcher;
            }
        }

        public Query(string IndexDirectory)
        {
            this.indexDirectory = IndexDirectory;
        }

        public QueryResult Search(string Query)
        {
            var searchQueryParser = new QueryParser(Lucene.Net.Util.Version.LUCENE_29, "", new Lucene.Net.Analysis.KeywordAnalyzer());
            var searchQuery = searchQueryParser.Parse(Query);

            QueryResult searchResults;
            using (var Searcher = new SearchService(IndexSearcher))
            {
                var tempSearchResults = Searcher.SearchIndex(searchQuery);
                searchResults = new QueryResult(tempSearchResults.Documents.ToList(), tempSearchResults.TotalHits);
            }

            return searchResults;
        }
    }
}

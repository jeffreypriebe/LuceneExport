using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LuceneExport;

namespace LuceneExportCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var argsParsed = new Options();
            if (!CommandLine.Parser.Default.ParseArguments(args, argsParsed))
            {
                Console.WriteLine("Invalid arguments.");
                return;
            }

            Console.WriteLine(String.Format("Running Query {0} on Index: {1}.", argsParsed.Query, argsParsed.IndexPath));

            var searchQuery = new Query(argsParsed.IndexPath);
            var searchQueryResults = searchQuery.Search(argsParsed.Query);

            Console.WriteLine(String.Format("Query run, {0} results.", searchQueryResults.TotalHits));

            if (searchQueryResults.TotalHits == 0)
                return;

            Console.WriteLine(String.Format("Outputting to: {0}.", argsParsed.ExportFilePath));

            var Exporter = new Export(searchQueryResults, argsParsed.Fields.Select(x => x.Trim()).ToList(), argsParsed.ExportFilePath);
            var exportResult = Exporter.Save();

            if (exportResult)
                Console.WriteLine("Export complete.");
            else
                Console.WriteLine("Export failed.");
        }
    }
}

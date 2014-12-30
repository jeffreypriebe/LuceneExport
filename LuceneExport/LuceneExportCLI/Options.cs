using System;
using System.Collections.Generic;
using CommandLine;

namespace LuceneExportCLI
{
    public class Options
    {
        [Option('i', "index", Required = true, HelpText = "Index directory to query.")]
        public string IndexPath { get; set; }

        [Option('x', "export", Required = true, HelpText = "Filepath to save export to.")]
        public string ExportFilePath { get; set; }
                
        [OptionList('f', "fields", Separator=',', Required = true, HelpText = "Fields to export.")]
        public IList<string> Fields { get; set; }

        [Option('q', "query", Required = true, HelpText = "Query to run.")]
        public string Query { get; set; }
    }
}

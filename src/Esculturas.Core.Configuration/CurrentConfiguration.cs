using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Esculturas.Core.Configuration
{
    public class CurrentConfiguration : ICurrentConfiguration
    {
        public string DataFilePath { get; set; }
        public string DataFilePathComplete { get; set; }


        public static ICurrentConfiguration Build(IConfiguration configuration, string contentRootPath)
        {
            var dataFilePath = ParserToString(configuration, "Esculturas:DataFilePath", null, throwError: true);

            return new CurrentConfiguration()
            {
                DataFilePath = dataFilePath,
                //DataFilePathComplete = System.IO.Path.Combine(contentRootPath, dataFilePath)
                DataFilePathComplete = PathCombine(contentRootPath, dataFilePath)
            };
        }

        public static string ParserToString(IConfiguration configuration, string key, string defaultValue, bool throwError)
        {
            string empty;
            string item = configuration[key];
            if (!string.IsNullOrEmpty(item))
            {
                empty = item;
            }
            else
            {
                if (throwError)
                {
                    throw new ApplicationException(String.Concat("[Config] Config key not found > ", key));
                }
                empty = defaultValue;
            }
            return empty;
        }

     
        private static string PathCombine(string path1, string path2)
        {
            if (Path.IsPathRooted(path2))
            {
                path2 = path2.TrimStart(Path.DirectorySeparatorChar);
                path2 = path2.TrimStart(Path.AltDirectorySeparatorChar);
            }

            return Path.Combine(path1, path2);
        }
    }
}

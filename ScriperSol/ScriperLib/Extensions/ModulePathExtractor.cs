using ScriperLib.Exceptions;
using System.Collections.Generic;
using System.IO;

namespace ScriperLib.Extensions
{
    public static class ModulePathExtractor
    {
        public const string pathIdentifier = "#${";

        public static IEnumerable<string> ExtractPaths(string fileName)
        {
            var result = new List<string>();
            using var reader = new StreamReader(fileName);
            while (true)
            {
                var line = reader.ReadLine();

                if (line is null)
                {
                    break;
                }
                if (string.IsNullOrEmpty(line) || !line.Contains(pathIdentifier))
                {
                    continue;
                }

                var start = line.IndexOf(pathIdentifier);
                var end = line.IndexOf("}");

                if (end < start)
                {
                    throw new ScriptException($"Can't extract module path from {fileName}");
                }

                var path = line.Substring(start + pathIdentifier.Length, end - (start + pathIdentifier.Length));
                result.Add(path);
            }

            return result;
        }
    }
}

using log4net.Config;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVSharp
{
    class CSVSharpSampler
    {
        public static void Main()
        {
            XmlConfigurator.Configure();

            Console.Write("Enter path: ");
            string path = Console.ReadLine();

            string directoryOut = "out";

            foreach (string pathIn in Directory.GetFiles(path, "*.csv"))
            {
                DataTable csv = CSVReader.Read(pathIn, true);
                csv.Print();
                string[] paths = { Path.GetDirectoryName(pathIn), directoryOut };
                string pathOut = Path.Combine(paths);
                Console.WriteLine(pathOut);
                if (!Directory.Exists(pathOut))
                {
                    Directory.CreateDirectory(pathOut);
                }
                string filename = Path.GetFileName(pathIn);
                CSVWriter.Write(csv, pathOut + "//" + filename);
                Console.ReadLine();
            }
        }
    }
}

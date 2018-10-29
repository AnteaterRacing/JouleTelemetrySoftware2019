using log4net;
using log4net.Config;
using System;
using System.Data;
using System.IO;
using System.Reflection;

namespace CSVSharp
{
    // Class demonstrating user of CSVReader and CSVWriter.
    /// <summary>
    /// Class demonstrating user of CSVReader and CSVWriter.
    /// </summary>
    class CSVSharpSampler
    {
        // Logger
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void Main()
        {
            // Logger configuration
            XmlConfigurator.Configure();

            // Output folder for CSVWrite wrt directoryIn
            string folderOut = "out";
            
            // Get path from user to directory of CSV's
            Console.Write("Enter folder of CSV files: ");
            string directoryIn = Console.ReadLine();
            string directoryOut = Path.Combine(new string[]{ directoryIn, folderOut });

            // Iterate through directory for CSV files, print out data, and write data to folderOut
            _log.Info("Attempting to read from " + directoryIn);
            try
            {
                foreach (string path in Directory.GetFiles(directoryIn, "*.csv"))
                {
                    // Read CSV file
                    DataTable csv = CSVReader.Read(path, true);
                    // Print CSV file
                    csv.Print();
                    // Make output folder if not present
                    string fileOut = Path.Combine(new string[]{ Path.GetDirectoryName(path), folderOut, Path.GetFileName(path) });
                    if (!Directory.Exists(directoryOut))
                    {
                        _log.Info("Created directory " + directoryOut);
                        Directory.CreateDirectory(directoryOut);
                    }
                    // Write CSV file
                    CSVWriter.Write(csv, fileOut);
                    _log.Info("Successfully wrote to " + fileOut);

                    Console.ReadLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not process file(s) due to: " + e);
            }

            Console.WriteLine("Done!");
            Console.ReadLine();
        }
    }
}

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

        #region Main
        public static void Main()
        {
            // Logger configuration
            XmlConfigurator.Configure();

            // Output folder for CSVWrite wrt directoryIn
            string folderOut = "out";
            
            // Get path from user to directory of CSV's
            Console.Write("Enter folder/path to CSV file(s): ");
            string pathIn = Console.ReadLine();

            _log.Info("Attempting to read from " + pathIn);

            // Open CSV file, print out data, and write data to folderOut/*.csv
            if (File.Exists(pathIn) && Path.GetExtension(pathIn) == ".csv")
            {
                string directoryOut = Path.Combine(new string[] { Path.GetDirectoryName(pathIn), folderOut });
                ProcessFile(pathIn, directoryOut);
            }
            // Iterate through directory for CSV files, print out data, and write data to folderOut
            else if (Directory.Exists(pathIn))
            {
                string directoryOut = Path.Combine(new string[] { pathIn, folderOut });
                foreach (string file in Directory.GetFiles(pathIn, "*.csv"))
                {
                    ProcessFile(file, directoryOut);
                    Console.ReadLine();
                }
            }
            // Not a CSV file or valid directory
            else
            {
                Console.WriteLine("Not a valid file or directory.");
            }

            Console.WriteLine("Done!");
            Console.ReadLine();
        }
        #endregion

        #region Helper Methods
        private static void ProcessFile(string file, string directoryOut)
        {
            _log.Debug(string.Format("ProcessFile({0}, {1})", file, directoryOut));

            try
            {
                // Read CSV file
                DataTable csv = CSVReader.Read(file, true);
                // Print CSV file
                csv.Print();
                // Make output folder if not present
                string fileOut = Path.Combine(new string[] { directoryOut, Path.GetFileName(file) });
                if (!Directory.Exists(directoryOut))
                {
                    _log.Info("Created directory " + directoryOut);
                    Directory.CreateDirectory(directoryOut);
                }
                // Write CSV file
                CSVWriter.Write(csv, fileOut);
                _log.Info("Successfully wrote to " + fileOut);
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not process file(s) due to: " + e);
            }
        }
        #endregion
    }

}

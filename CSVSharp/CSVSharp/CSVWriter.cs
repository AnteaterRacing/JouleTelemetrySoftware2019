using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;

namespace CSVSharp
{
    // Class for writing CSV files.
    /// <summary>
    /// Class for writing CSV files.
    /// </summary>
    public static class CSVWriter
    {
        // Logger
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #region Methods
        // Write the data as a CSV file.
        /// <summary>
        /// Write the data as a CSV file.
        /// </summary>
        /// <param name="dt">DataTable to write as CSV file.</param>
        /// <param name="path">Path to write CSV to.</param>
        /// <exception cref=""></exception>
        public static void Write(DataTable dt, string path)
        {
            _log.Debug(string.Format("CSVWriter.Write(DataTable size={0}x{1}, {2})", dt.Rows.Count, dt.Columns.Count, path));

            _log.Info("Attempting to build string from DataTable...");
            StringBuilder sb = new StringBuilder();
            // Add columns to string
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                sb.Append(StringToCSVFormat(dt.Columns[i].ToString()));
                if (i < dt.Columns.Count - 1)
                    sb.Append(",");
            }
            sb.AppendLine(); // newline after header

            // Add each row to string
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sb.Append(StringToCSVFormat(dr[i].ToString()));

                    if (i < dt.Columns.Count - 1)
                        sb.Append(",");
                }
                sb.AppendLine();
            }
            _log.Info("Sucessfully built string.");

            // Write StringBuilder to path
            _log.Info("Attempting to write string to path...");
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.Write(sb);
            }
            _log.Info("Successfully wrote to " + path);
        }
        #endregion

        #region Helper Methods
        // Convert string to CSV format.
        /// <summary>
        /// Convert string to CSV format.
        /// </summary>
        /// <remarks>
        /// Based on RFC 4180 §2 (https://tools.ietf.org/html/rfc4180#section-2).
        /// </remarks>
        /// <param name="s">A string to convert for writing into CSV.</param>
        /// <returns>A string converted for writing into CSV.</returns>
        /// <exception cref="CSVSharpException">Thrown when data could not fit in maximum StringBuilder capacity.</exception>
        private static string StringToCSVFormat(string s)
        {
            _log.Debug(string.Format("CSVWriter.StringToCSVFormat({0})", s));

            Console.WriteLine(s);
            if (IsDoubleQuoteEnclosed(s))
            {
                return "\"" + s + "\"";
            }
            // Enclose string in double quotes if string contains special character
            else if (HasSpecial(s)) // not double quote enclosed
            {
                List<char> chars = new List<char>();
                // Escape double quotes with double quotes
                for (int i = 0; i < s.Length; i++)
                {
                    char currentChar = s[i];
                    if (currentChar == '"')
                    {
                        chars.Add('"');
                    }
                    chars.Add(currentChar);
                }
                // Enclose string
                chars.Insert(0, '"');
                chars.Add('"');
                // Convert list to string
                StringBuilder sb = new StringBuilder();
                try
                {
                    foreach (char c in chars)
                    {
                        sb.Append(c);
                    }
                }
                catch (ArgumentOutOfRangeException e)
                {
                    _log.Info("Failed to convert " + s);
                    _log.Error(e);
                    throw new CSVSharpException("Could not convert string because exceeded maximum string capacity.");
                }
                return sb.ToString();
            }
            else
            {
                return s;
            }
        }

        // Determines if string starts and ends with double quote.
        /// <summary>
        /// Determines if string starts and ends with double quote.
        /// </summary>
        /// <param name="s">A string to check.</param>
        /// <returns>
        /// True if string starts with double quotes, else False.
        /// </returns>
        private static bool IsDoubleQuoteEnclosed(string s)
        {
            _log.Debug(string.Format("CSVWriter.IsDoubleQuoteEnclosed({0})", s));

            int length = s.Length;
            // Must be at least three characters (ex. "a")
            if (length <= 2) return false;
            // Check if string begins and ends with double quote
            return s[0] == '"' && s[length-1] == '"';
        }

        private static bool HasSpecial(string s)
        {
            return s.Contains("\n") || s.Contains("\"") || s.Contains(",") || s.Contains("#");
        }
        #endregion
    }
}

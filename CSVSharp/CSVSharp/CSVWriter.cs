using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVSharp
{
    public static class CSVWriter
    {
        public static void Write(DataTable dt, string path)
        {
            StringBuilder sb = new StringBuilder();
            // Add columns to string
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                sb.Append(StringToCSVFormat(dt.Columns[i].ToString()));
                if (i < dt.Columns.Count - 1)
                    sb.Append(",");
            }
            sb.AppendLine();
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
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.Write(sb);
            }
        }

        private static string StringToCSVFormat(string s)
        {
            string formattedString = s;
            // Enclose string in double quotes if string contains special character
            bool hasSpecial = s.Contains("\n") || s.Contains("\"") || s.Contains(",");
            if (!IsDoubleQuoteEnclosed(s) && hasSpecial)
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
                foreach (char c in chars)
                {
                    sb.Append(c);
                }
                formattedString = sb.ToString();
            }
            return formattedString;
        }

        private static bool IsDoubleQuoteEnclosed(string s)
        {
            int length = s.Length;
            // Must be at least two characters
            if (length <= 1) return false;
            // Check if string begins and ends with double quote
            return s[0] == '"' && s[length-1] == '"';
        }
    }
}

using System;
using System.Data;
using System.IO;
using CSVSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSVSharpTests
{
    [TestClass]
    public class CSVSharpTests
    {

        #region Tests
        [TestMethod]
        public void TestAreDataTablesEqual()
        {
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();

            dt1.Columns.Add("Col1", typeof(int));
            dt1.Columns.Add("Col2", typeof(int));
            dt1.Columns.Add("Col3", typeof(int));
            dt1.Columns.Add("Col4", typeof(int));
            dt1.Columns.Add("Col5", typeof(int));

            dt2.Columns.Add("Col1", typeof(int));
            dt2.Columns.Add("Col2", typeof(int));
            dt2.Columns.Add("Col3", typeof(int));
            dt2.Columns.Add("Col4", typeof(int));
            dt2.Columns.Add("Col5", typeof(int));

            dt1.Rows.Add(1, 2, 3, 4, 5);
            dt1.Rows.Add(6, 7, 8, 9, 10);
            dt1.Rows.Add(11, 12, 13, 14, 15);

            dt2.Rows.Add(1, 2, 3, 4, 5);
            dt2.Rows.Add(6, 7, 8, 9, 10);
            dt2.Rows.Add(11, 12, 13, 14, 15);

            Assert.IsTrue(AreDataTablesEqual(dt1, dt2));
        }

        [TestMethod]
        public void OriginalDataMatchesOutputData_WhenPassedThrough()
        {
            string directoryIn = Directory.GetCurrentDirectory() + "\\..\\..\\..\\CSVSharp\\samples\\random";
            string folderOut = "out";
            string directoryOut = Path.Combine(new string[] { directoryIn, folderOut });

            // Create output directory if not already present
            if (!Directory.Exists(directoryOut))
            {
                Directory.CreateDirectory(directoryOut);
            }

            foreach (string path in Directory.GetFiles(directoryIn, "*.csv"))
            {
                // Read data in
                DataTable csv = CSVReader.Read(path, true);
                string fileOut = Path.Combine(new string[] { Path.GetDirectoryName(path), folderOut, Path.GetFileName(path) });
                CSVWriter.Write(csv, fileOut);
                // Check data in is same as data out
                DataTable csvFromOutput = CSVReader.Read(fileOut, true);
                Assert.IsTrue(AreDataTablesEqual(csv, csvFromOutput));
            }
        }
        #endregion

        #region Helper Methods
        // Determine if two DataTables are equivalent.
        /// <summary>
        /// Determine if two DataTables are equivalent.
        /// </summary>
        /// <param name="expected">The expected DataTable.</param>
        /// <param name="actual">The actual DataTable.</param>
        /// <returns>True if objects stored in DataTable are equal, else false.</returns>
        private bool AreDataTablesEqual(DataTable expected, DataTable actual)
        {
            // Check DataTable sizes
            if ((expected.Rows.Count != actual.Rows.Count) || (expected.Columns.Count != actual.Columns.Count))
            {
                return false;
            }

            // Check field equality by string
            for (int i = 0; i < expected.Rows.Count; i++)
            {
                DataRow expectedRow = expected.Rows[i];
                DataRow actualRow = actual.Rows[i];

                for (int j = 0; j < expected.Columns.Count; j++)
                {
                    bool stringDataMatches = expected.Rows[i][j].ToString() == actual.Rows[i][j].ToString();
                    if (!stringDataMatches)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        #endregion
    }
}

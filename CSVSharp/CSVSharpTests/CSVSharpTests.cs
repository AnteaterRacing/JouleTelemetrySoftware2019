using System;
using System.Collections.Generic;
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
        public void OriginalCSVMatchesOutputCSV()
        {
            string directoryIn = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\test\\random");
            string directoryOut = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\test\\random.out");

            // Create output directory if not already present
            if (!Directory.Exists(directoryOut))
            {
                Directory.CreateDirectory(directoryOut);
            }

            // Process each CSV file
            foreach (string path in Directory.EnumerateFiles(directoryIn, "*.csv"))
            {
                // Read data in
                DataTable dt = CSVReader.Read(path, true);
                string fileOut = Path.Combine(directoryOut, Path.GetFileName(path));
                CSVWriter.Write(dt, fileOut);
                // Check data in is same as data out
                DataTable dtFromOutput = CSVReader.Read(fileOut, true);
                Assert.IsTrue(AreDataTablesEqual(dt, dtFromOutput));
            }
        }

        [TestMethod]
        public void DataTableMatchesOutputCSV()
        {
            string directoryOut = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\test\\test.out");
            Console.WriteLine(directoryOut);

            // Create output directory if not already present
            if (!Directory.Exists(directoryOut))
            {
                Directory.CreateDirectory(directoryOut);
            }

            // Create DataTable
            string name = "test";
            DataColumn[] columns =
            {
                new DataColumn("Throttle", typeof(int)),
                new DataColumn("Brake", typeof(int)),
                new DataColumn("Steering", typeof(int)),
                new DataColumn("Current", typeof(int)),
                new DataColumn("Voltage", typeof(int)),
                new DataColumn("Temperature", typeof(int))
            };
            DataColumn columnPK = new DataColumn("id", typeof(int));
            DataTable dt = MakeEmptyDataTable(name, columns, columnPK);
            dt.Rows.Add(new object[] { null, 50, 50, -100, 5, 12, 60 });
            dt.Rows.Add(new object[] { null, 50, 0, -100, 5, 12, 50 });
            dt.Rows.Add(new object[] { null, 50, 25, null, 5, 12, 51 });
            dt.Rows.Add(new object[] { null, 50, 50, -100, 5, 12, 34 });
            dt.Rows.Add(new object[] { null, 50, 50, -100, 5, 12, 57 });
            dt.Rows.Add(new object[] { null, 50, 2, -100, 5, 12, 12 });

            // Read data in
            string fileOut = Path.Combine(directoryOut, name + ".csv");
            CSVWriter.Write(dt, fileOut);
            
            // Check data in is same as data out
            DataTable dtFromOutput = CSVReader.Read(fileOut, true);
            Assert.IsTrue(AreDataTablesEqual(dt, dtFromOutput));
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

        // Generate an empty DataTable.
        /// <summary>
        /// Generate an empty DataTable.
        /// </summary>
        /// <param name="name">Name of the DataTable.</param>
        /// <param name="columns">Array of DataColumns to define DataTable.</param>
        /// <param name="columnPK">DataColumn for table's primary key. Defaults to null.</param>
        /// <returns>An empty DataTable.</returns>
        private DataTable MakeEmptyDataTable(string name, DataColumn[] columns, DataColumn columnPK = null)
        {
            DataTable table = new DataTable(name);

            // Add primary key column
            if (columnPK != null)
            {
                columnPK.AutoIncrement = true;
                columnPK.AutoIncrementSeed = 1;
                columnPK.AutoIncrementStep = 1;
                columnPK.ReadOnly = true;
                table.Columns.Add(columnPK);
                table.PrimaryKey = new DataColumn[] { columnPK };
            }

            // Add remaining columns
            table.Columns.AddRange(columns);

            return table;
        }
        #endregion
    }
}

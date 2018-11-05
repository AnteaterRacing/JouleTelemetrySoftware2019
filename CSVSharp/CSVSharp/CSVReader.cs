using GenericParsing;
using System.Data;
using log4net;
using System.Reflection;

namespace CSVSharp
{
    public static class CSVReader
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #region
        public static DataTable Read(string filePath, bool hasHeader=true, int skipStartingRows=0, int skipEndingRows=0)
        {
            _log.Debug(string.Format("CSVReader.Read({0}, {1}, {2}, {3})", filePath, hasHeader, skipStartingRows, skipStartingRows));

            DataTable dt;

            _log.Info("Attempting to parse " + filePath);
            try
            {
                // Read in CSV to DataTable with Generic Parser library
                using (GenericParserAdapter parser = new GenericParserAdapter(filePath))
                {
                    // Parser settings
                    parser.SkipEmptyRows = true;
                    parser.TrimResults = true;
                    parser.FirstRowHasHeader = hasHeader;
                    parser.SkipStartingDataRows = skipStartingRows;
                    parser.SkipEndingDataRows = skipEndingRows;
                    _log.Debug("Skip Empty Rows   : " + parser.SkipEmptyRows);
                    _log.Debug("Trim Results      : " + parser.TrimResults);
                    _log.Debug("Has Header        : " + parser.FirstRowHasHeader);
                    _log.Debug("Skip Starting Data: " + parser.SkipStartingDataRows);
                    _log.Debug("Skip Ending Data  : " + parser.SkipEndingDataRows);
                    // Store CSV into DataTable
                    dt = parser.GetDataTable();
                    _log.Info("Successfully parsed " + filePath);
                }
            }
            catch (System.IO.IOException e)
            {
                _log.Info("Failed to parse " + filePath);
                _log.Error(e);
                throw new CSVSharpException("File in use.");
            }
            
            return dt;
        }
        #endregion
    }
}

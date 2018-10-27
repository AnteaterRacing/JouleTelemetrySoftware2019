using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericParsing;
using System.Data;
using log4net;
using System.Reflection;

namespace CSVSharp
{
    public static class CSVReader
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static DataTable Read(string filePath, bool hasHeader=true, int skipStartingRows=0, int skipEndingRows=0)
        {
            DataTable dt;
            // TODO: Handle System.IO.IOException when file in use
            _log.Info("Attempting to parse " + filePath);
            using (GenericParserAdapter parser = new GenericParserAdapter(filePath))
            {
                parser.SkipEmptyRows = true;
                parser.TrimResults = true;
                parser.FirstRowHasHeader = hasHeader;
                parser.SkipStartingDataRows = skipStartingRows;
                parser.SkipEndingDataRows = skipEndingRows;
                _log.Info("Skip Empty Rows   : " + parser.SkipEmptyRows);
                _log.Info("Trim Results      : " + parser.TrimResults);
                _log.Info("Has Header        : " + parser.FirstRowHasHeader);
                _log.Info("Skip Starting Data: " + parser.SkipStartingDataRows);
                _log.Info("Skip Ending Data  : " + parser.SkipEndingDataRows);
                dt = parser.GetDataTable();
                _log.Info("Successfully parsed " + filePath);
            }
            
            return dt;
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using Csv;

namespace TelemetryApp.Models.Data
{
    public class CsvData : Data
    {
        private const int ColumnCount = 38;

        private static readonly List<double> DataBuffer = new List<double>(ColumnCount);
        private readonly IEnumerable<ICsvLine> _data;

        private readonly FileStream _stream;

        public CsvData(string filename)
        {
            // TODO: ensure file exists
            _stream = File.Open(filename, FileMode.Open);
            _data = CsvReader.ReadFromStream(_stream, new CsvOptions {ValidateColumnCount = true});
        }

        public override void Update()
        {
            Steering.Update();
            GForce.Update();
            Gps.Update();
            FrontLeftTire.Update();
            FrontRightTire.Update();
            BackLeftTire.Update();
            BackRightTire.Update();
        }

        public override void Unload()
        {
            throw new NotImplementedException();
        }

        public void UpdateDataBuffer()
        {
            DataBuffer.InsertRange(0, (IEnumerable<double>) _data);
        }
    }
}
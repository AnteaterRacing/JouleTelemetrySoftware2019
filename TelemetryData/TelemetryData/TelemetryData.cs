using log4net;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace TelemetryData
{
    public class TelemetryData
    {
        // Logger
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        // Random generation
        private static readonly string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 ";
        private static readonly int alphabetLength = 37;
        private static readonly Random rand = new Random();

        public TelemetryData()
        {

        }

        public static string getRandomString()
        {
            int length = rand.Next();
            string s = "";
            for (int i = 0; i < length; i++)
            {
                s += alphabet[rand.Next(0, alphabetLength)];
            }
            return s;
        }

        public static double getRandomDouble()
        {
            return rand.NextDouble();
        }

        public Dictionary<string, double> getData(bool random=false)
        {
            Dictionary<string, double> d = new Dictionary<string, double>();

            if (random)
            {
                int length = rand.Next();
                for (int i = 0; i < length; i++)
                {
                    d.Add(getRandomString(), getRandomDouble());
                }
            }

            return d;
        }
    }
}

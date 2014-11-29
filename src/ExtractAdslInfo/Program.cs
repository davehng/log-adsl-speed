using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ExtractAdslInfo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                return;
            }

            string filename = args[0];
            string output = args[1];

            //string filename = @"\\nyamo\starfox\adslstatus\stats-2014-11-28-16-51-32.html";
            //string output = "c:\blah.txt";

            var text = File.ReadAllText(filename);

            var snr = FindValues("SNR Margin", text);
            var lineAttenuation = FindValues("Line Attenuation", text);
            var dataRate = FindValues("Data Rate", text);
            var maxRate = FindValues("Max Rate", text);
            var power = FindValues("POWER", text);

            var toAppend = DateTime.Now.ToString("yyyy,MM,dd,HH,mm,ss,") + BuildCsv(snr, lineAttenuation, dataRate, maxRate, power) + Environment.NewLine;

            File.AppendAllText(output, toAppend);
        }

        private static DownUpValues FindValues(string key, string text)
        {
            var position = text.IndexOf(key, System.StringComparison.Ordinal);

            var values = new DownUpValues();

            const string firstHit = @"</td><td width=""70"" align=center class=""tabdata"">";
            var firstPositionStart = text.IndexOf(firstHit, position, System.StringComparison.Ordinal) + firstHit.Length;
            var firstPositionEnd = text.IndexOf("</td>", firstPositionStart, System.StringComparison.Ordinal);
            values.Downstream = text.Substring(firstPositionStart, firstPositionEnd - firstPositionStart).Trim();

            const string secondHit = @"class=""tabdata"">";
            var secondPositionStart = text.IndexOf(secondHit, firstPositionEnd, StringComparison.Ordinal) + secondHit.Length;
            var secondPositionEnd = text.IndexOf("</td>", secondPositionStart, System.StringComparison.Ordinal);
            values.Upstream = text.Substring(secondPositionStart, secondPositionEnd - secondPositionStart).Trim();

            return values;
        }

        private static string BuildCsv(params DownUpValues[] values)
        {
            return string.Join(",",
                values.ToList().ConvertAll(x => string.Format("{0},{1}", x.Downstream, x.Upstream)).ToArray());
        }

        private struct DownUpValues
        {
            public string Downstream { get; set; }
            public string Upstream { get; set; }
        }

    }
}

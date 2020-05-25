using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CV19Console
{
    class Program
    {
        /// <summary>
        /// Файл с данными по COVID-19
        /// </summary>
        private const string data_url = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";
        /// <summary>
        /// Получение данных по COVID-19 из веб в виде потока
        /// </summary>
        /// <returns>Поток данных</returns>
        private static async Task<Stream> GetDataStream()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(data_url, HttpCompletionOption.ResponseHeadersRead);
            return await response.Content.ReadAsStreamAsync();
        }
        /// <summary>
        /// Получение порций строк данных по CoVID-19
        /// </summary>
        /// <returns>Строки</returns>
        private static IEnumerable<string> GetDataLines()
        {
            using var data_stream = GetDataStream().Result;
            using var data_reader = new StreamReader(data_stream);

            while (!data_reader.EndOfStream)
            {
                var line = data_reader.ReadLine();
                if(string.IsNullOrWhiteSpace(line)) continue;
                yield return line.Replace("Korea,", "Korea -");
            }
        }
        /// <summary>
        /// Получение заголовочной первой строки - с датами
        /// </summary>
        /// <returns>Массив дат</returns>
        private static DateTime[] GetDates() => GetDataLines()
           .First()
           .Split(',')
           .Skip(4)
           .Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture))
           .ToArray();
        /// <summary>
        /// Получение порций данных о COVID-19
        /// </summary>
        /// <returns>Кортеж "страна, провинция, массив число заболевших"</returns>
        private static IEnumerable<(string Contry, string Province, int[] Counts)> GetData()
        {
            var lines = GetDataLines()
               .Skip(1)
               .Select(line => line.Split(','));

            foreach (var row in lines)
            {
                var province = row[0].Trim();
                var country_name = row[1].Trim(' ', '"');
                var counts = row.Skip(4).Select(int.Parse).ToArray();

                yield return (country_name, province, counts);
            }
        }

        static void Main(string[] args)
        {
            //var web_client = new WebClient();

            //var client = new HttpClient();

            //var items = new string[10];

            //var last_item = items[^1];
            //var prev_last_item = items[^2];

            //var response = client.GetAsync(data_url).Result;
            //var csv_str = response.Content.ReadAsStringAsync().Result;

            //foreach (var data_line in GetDataLines())
            //    Console.WriteLine(data_line);

            //var dates = GetDates();

            //Console.WriteLine(string.Join("\r\n", dates));

            var russia_data = GetData()
               .First(v => v.Contry.Equals("Russia", StringComparison.OrdinalIgnoreCase));

            Console.WriteLine(string.Join("\r\n", GetDates().Zip(russia_data.Counts, (date, count) => $"{date:dd:MM} - {count}")));


            Console.ReadLine();
        }
    }
}

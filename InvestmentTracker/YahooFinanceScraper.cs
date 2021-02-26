using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

namespace InvestmentTracker
{
    class YahooFinanceScraper : Scraper
    {
        public YahooFinanceScraper(string stock)
        {
            this.url = "https://finance.yahoo.com/quote/" + stock + "?p=" + stock;
            httpClient = new HttpClient();


            html = httpClient.GetStringAsync(url);
        }

        //for testing purposes
        public override void displayData()
        {
            //Console.WriteLine(html.Result);

            //string[] split = html.Result.Split(new Char[] { '<', '>', '\n' });
            string[] split = html.Result.Split(new string[] { "<div",  "<head", "<body", "<iframe", "<span", "<script", "<style", "<template" }, StringSplitOptions.None);
            string temp = split[188];
            string[] data = temp.Split(new char[] { '<', '>' });

            for(int i = 0; i < data.Length; i++)
            {
                Console.WriteLine(i + data[i]);
            }
        }

        public override double scrapePrice()
        {
            if (html.Result == null)
            {
                Console.WriteLine("Error scraping data");
                return 0;
            }

            string[] data = html.Result.Split(new string[] { "<div", "<head", "<body", "<iframe", "<span", "<script", "<style", "<template" }, StringSplitOptions.None);
            string temp = data[188];
            string[] temp2 = temp.Split(new char[] { '<', '>' });

            return Convert.ToDouble(temp2[1]);
        }

        //TODO
        public override string scrapeShortName()
        {
            string shortName;
            string[] data = html.Result.Split(',');
            string temp = "";

            foreach (string chunk in data)
            {
                if (chunk.StartsWith("\"currency\":"))
                {
                    temp = chunk;
                    break;
                }
            }

            string[] temp2 = temp.Split(":");
            shortName = temp2[1];

            shortName = shortName.Replace("\"", "");

            return shortName;
        }
    }
}

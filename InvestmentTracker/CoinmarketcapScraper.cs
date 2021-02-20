using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace InvestmentTracker
{
    class CoinmarketcapScraper : Scraper
    {
        public CoinmarketcapScraper(string crypto)
        {
            this.url = "https://coinmarketcap.com/currencies/" + crypto;
            httpClient = new HttpClient();
            html = httpClient.GetStringAsync(url);
        }

        //for testing purposes
        public override void displayData()
        {
            Console.WriteLine(html.Result);
        }

        public override double scrapePrice()
        {
            double price = 0;
            string [] data = html.Result.Split(',');
            string temp = "";

            foreach(string chunk in data)
            {
                if (chunk.StartsWith("\"price\":"))
                {
                    temp = chunk;
                    break;
                }                    
            }

            string[] temp2 = temp.Split(":");

            price = Convert.ToDouble(temp2[1]);
            

            return price;
        }

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

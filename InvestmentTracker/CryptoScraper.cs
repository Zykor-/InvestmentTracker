using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;

namespace InvestmentTracker
{
    class CryptoScraper
    {
        private string url;
        private HttpClient httpClient;
        private Task<string> html;

        public CryptoScraper(string url)
        {
            this.url = url;
            httpClient = new HttpClient();
            html = httpClient.GetStringAsync(url);
        }

        public void displayData()
        {
            Console.WriteLine(html.Result);
        }

        public double scrapePrice()
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
    }
}

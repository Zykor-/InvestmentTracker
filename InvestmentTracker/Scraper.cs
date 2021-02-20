using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

namespace InvestmentTracker
{
    abstract class Scraper
    {
        protected string url;
        protected HttpClient httpClient;
        protected Task<string> html;

        public abstract double scrapePrice();
        public abstract void displayData();
        public abstract string scrapeShortName();
    }
}

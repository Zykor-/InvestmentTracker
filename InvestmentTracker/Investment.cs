using System;
using System.Collections.Generic;
using System.Text;

namespace InvestmentTracker
{
    class Investment : IComparable<Investment>
    {

        //netInvested in $
        //marketValue in $
        //currentValue in $
        //amountOwned in stock/currency
        private string name;
        protected string shortName;
        protected string URL;
        protected double netInvested;
        protected double marketValue;
        protected double currentValue;
        protected double amountOwned;
        protected CryptoScraper coinMarketCapScraper;

        protected string Name { get => name; set => name = value; }

        public Investment(string name, string shortName, double netInvested, double amountOwned)
        {
            this.Name = name;
            this.shortName = shortName;
            this.netInvested = netInvested;
            this.amountOwned = amountOwned;
            URL = "https://coinmarketcap.com/currencies/" + name + "/";

            coinMarketCapScraper = new CryptoScraper(URL);
            marketValue = coinMarketCapScraper.scrapePrice();
        } 

        public string getName() { return Name; }
        public string getShortName() { return shortName; }
        public string getURL() { return URL; }
        public double getNetInvested() { return netInvested; }
        public double getAmountOwned() { return amountOwned; }
        public double getMarketValue() { return marketValue; }
        public double getCurrentValue() { return currentValue; }
        public CryptoScraper getCoinMarketCapScraper() { return coinMarketCapScraper; }
        public void setMarketValue(double marketValue) { this.marketValue = marketValue; }

        public void purchase(double price, double amount)
        {
            if (amount > 0)
            {
                netInvested += price;
                amountOwned += amount;
            }
            else
                Console.WriteLine("Invalid purchase amount");
        }

        public void sell(double price, double amount)
        {
            if(amount > 0)
            {
                netInvested -= price;
                amountOwned -= amount;
            }
        }

        public void updateValues()
        {
            marketValue = coinMarketCapScraper.scrapePrice();
            currentValue = amountOwned * marketValue;
        }

        public double getGains()
        {
            return ((currentValue - netInvested) / Math.Abs(netInvested)) * 100;
        }

        public int CompareTo(Investment compareInvestment)
        {
            if (compareInvestment == null)
                return 1;
            else 
                return this.currentValue.CompareTo(compareInvestment.getCurrentValue());
        }

        public void display()
        {
            Console.WriteLine(Name + " " + shortName + " " + currentValue);
        }

    }
}

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
        //totalSpent in $
        //totalEarned in $
        //these last two will help track total amount actually spent as apposed to invested in some other manner, like mining, or recieving as a gift
        private string name;
        protected string shortName;
        protected double netInvested;
        protected double marketValue;
        protected double currentValue;
        protected double amountOwned;
        protected double totalSpent;
        protected double totalEarned;
        protected CoinmarketcapScraper coinMarketCapScraper;

        public Investment(string name, string shortName, double netInvested, double amountOwned)
        {
            this.name = name;
            this.shortName = shortName;
            this.netInvested = netInvested;
            this.amountOwned = amountOwned;

            coinMarketCapScraper = new CoinmarketcapScraper(name);
            marketValue = coinMarketCapScraper.scrapePrice();
        } 

        public string getName() { return name; }
        public string getShortName() { return shortName; }
        public double getNetInvested() { return netInvested; }
        public double getAmountOwned() { return amountOwned; }
        public double getMarketValue() { return marketValue; }
        public double getCurrentValue() { return currentValue; }
        public CoinmarketcapScraper getCoinMarketCapScraper() { return coinMarketCapScraper; }
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
            Console.WriteLine(name + " " + shortName + " " + currentValue);
        }

    }
}

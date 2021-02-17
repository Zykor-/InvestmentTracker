using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

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
        protected string filePath;
        protected double netInvested;
        protected double marketValue;
        protected double currentValue;
        protected double amountOwned;
        protected double totalSpent;
        protected double totalEarned;
        protected CoinmarketcapScraper coinMarketCapScraper;
        protected List<string>[] transactions;

        public Investment(string name, string shortName, double netInvested, double amountOwned, double marketPrice)
        {
            this.name = name;
            this.shortName = shortName;
            this.netInvested = netInvested;
            this.amountOwned = amountOwned;

            transactions = new List<string>[4];
            for(int i = 0; i < transactions.Length; i++)
            {
                transactions[i] = new List<string>();
            }

            transactions[0].Add(DateTime.Now.ToString("MM/dd/yyyy H:mm"));
            transactions[1].Add(Convert.ToString(netInvested));
            transactions[2].Add(Convert.ToString(amountOwned));
            transactions[3].Add(Convert.ToString(marketPrice));

            filePath = Directory.GetCurrentDirectory();

            coinMarketCapScraper = new CoinmarketcapScraper(name);
        }

        public string getName() { return name; }
        public string getShortName() { return shortName; }
        public double getNetInvested() { return netInvested; }
        public double getAmountOwned() { return amountOwned; }
        public double getMarketValue() { return marketValue; }
        public double getCurrentValue() { return currentValue; }
        public CoinmarketcapScraper getCoinMarketCapScraper() { return coinMarketCapScraper; }
        public void setMarketValue(double marketValue) { this.marketValue = marketValue; }

        public void purchase(double price, double amount, double marketPrice)
        {
            if (amount > 0)
            {
                netInvested += price;
                amountOwned += amount;
                totalSpent += price;

                transactions[0].Add(DateTime.Now.ToString("MM/dd/yyyy H:mm"));
                transactions[1].Add(Convert.ToString(price));
                transactions[2].Add(Convert.ToString(amount));
                transactions[3].Add(Convert.ToString(marketPrice));
            }
            else
                Console.WriteLine("Invalid purchase amount");
        }

        public void sell(double price, double amount, double marketPrice)
        {
            if (amount > 0)
            {
                netInvested -= price;
                totalEarned += price;
                amountOwned -= amount;

                // make negative for history
                price = 0 - price;
                amount = 0 - amount;

                transactions[0].Add(DateTime.Now.ToString("MM/dd/yyyy H:mm"));
                transactions[1].Add(Convert.ToString(price));
                transactions[2].Add(Convert.ToString(amount));
                transactions[3].Add(Convert.ToString(marketPrice));
            }
        }

        public void updateValues()
        {
            coinMarketCapScraper = new CoinmarketcapScraper(name);
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
        public void saveHistory(string profile)
        {
            filePath = Directory.GetCurrentDirectory();
            int endPathIndex = filePath.IndexOf("bin", 0);
            filePath = filePath.Substring(0, endPathIndex) + profile + name + ".txt";
            StreamWriter output = new StreamWriter(filePath);
            for (int i = 0; i < transactions[0].Count; i++)
            {
                output.WriteLine(transactions[0][i] + "#" + transactions[1][i] + "#" + transactions[2][i] + "#" + transactions[3][i]);
            }

            output.Close();
        }

        public void loadHistory(string profile)
        {
            int endPathIndex = filePath.IndexOf("bin", 0);
            filePath = filePath.Substring(0, endPathIndex) + profile + name + ".txt";

            if (File.Exists(filePath))
            {
                StreamReader input = new StreamReader(filePath);
                string temp;
                string[] data;

                transactions[0].Clear();
                transactions[1].Clear();
                transactions[2].Clear();
                transactions[3].Clear();

                while (!input.EndOfStream)
                {
                    temp = input.ReadLine();
                    data = temp.Split('#');

                    transactions[0].Add(data[0]);
                    transactions[1].Add(data[1]);
                    transactions[2].Add(data[2]);
                    transactions[3].Add(data[3]);
                }
                input.Close();
            }
            else
                Console.WriteLine("Failed to load transaction history, file not found");
        }
    }
}
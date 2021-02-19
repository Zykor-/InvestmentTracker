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
        protected List<Transaction> transactions;

        //Constructor for reading in data from file
        public Investment(string name, string shortName, double netInvested, double amountOwned, double marketPrice)
        {
            this.name = name;
            this.shortName = shortName;
            this.netInvested = netInvested;
            this.amountOwned = amountOwned;

            transactions = new List<Transaction>();

            filePath = Directory.GetCurrentDirectory();

            coinMarketCapScraper = new CoinmarketcapScraper(name);
        }

        //Constructor including purchase
        public Investment(string name, string shortName, double netInvested, double amountOwned, double marketPrice, string type)
        {
            this.name = name;
            this.shortName = shortName;
            this.netInvested = netInvested;
            this.amountOwned = amountOwned;

            transactions = new List<Transaction>();

            transactions.Add(new Transaction(type, DateTime.Now.ToString("MM/dd/yyyy H:mm"), netInvested, amountOwned, marketPrice));

            filePath = Directory.GetCurrentDirectory();

            coinMarketCapScraper = new CoinmarketcapScraper(name);
        }

        public string getName() { return name; }
        public string getShortName() { return shortName; }
        public double getNetInvested() { return netInvested; }
        public double getAmountOwned() { return amountOwned; }
        public double getMarketValue() 
        {
            coinMarketCapScraper = new CoinmarketcapScraper(name);
            marketValue = coinMarketCapScraper.scrapePrice();
            return marketValue; 
        }
        public double getCurrentValue() { return currentValue; }
        public double getTotalSpent() { return totalSpent; }
        public double getTotalEarned() { return totalEarned; }
        public CoinmarketcapScraper getCoinMarketCapScraper() { return coinMarketCapScraper; }
        public void setMarketValue(double marketValue) { this.marketValue = marketValue; }
        public double getOldValue()
        {
            return transactions[transactions.Count - 1].getMarketValue() * amountOwned;
        }

        public void purchase(double price, double amount, double marketPrice)
        {
            if (amount > 0)
            {
                netInvested += price;
                amountOwned += amount;
                totalSpent += price;

                transactions.Add(new Transaction("purchase", DateTime.Now.ToString("MM/dd/yyyy H:mm"), price, amount, marketPrice));
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

                transactions.Add(new Transaction("sell", DateTime.Now.ToString("MM/dd/yyyy H:mm"), price, amount, marketPrice));
            }
        }

        public void updateValues()
        {
            currentValue = amountOwned * getMarketValue();
        }

        public double getGains()
        {
            return ((currentValue - netInvested) / Math.Abs(netInvested)) * 100;
        }
        
        public double getRecentGains()
        {
            double oldValue = Math.Abs(transactions[transactions.Count-1].getMarketValue());
            if (amountOwned == 0)
            {
                double prevOwned = Math.Abs(transactions[transactions.Count - 1].getInvestmentAmount()); ;
                double prevCurrentValue = prevOwned * getMarketValue();
                oldValue *= prevOwned;
                return ((prevCurrentValue - oldValue) / oldValue) * 100;
            }
            else
                oldValue *= amountOwned;
            return ((currentValue - oldValue) / oldValue) * 100;
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
            for (int i = 0; i < transactions.Count; i++)
            {
                output.WriteLine(transactions[i].getTime() + "#" + transactions[i].getType() + "#" + transactions[i].getDollarAmount() + "#" 
                    + transactions[i].getInvestmentAmount() + "#" + transactions[i].getMarketValue());
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

                while (!input.EndOfStream)
                {
                    temp = input.ReadLine();
                    data = temp.Split('#');

                    transactions.Add(new Transaction(data[0], data[1], Convert.ToDouble(data[2]), Convert.ToDouble(data[3]), Convert.ToDouble(data[4])));
                }
                input.Close();

                for(int i = 0; i < transactions.Count; i++)
                {
                    if(transactions[i].getType() == "purchase")
                    {
                        totalSpent += transactions[i].getDollarAmount();
                    }
                    else if(transactions[i].getType() == "sell")
                    {
                        totalEarned -= transactions[i].getDollarAmount();
                    }
                }
            }
            else
                Console.WriteLine("Failed to load transaction history, file not found");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace InvestmentTracker
{
    class Transaction
    {
        private string type;
        private string time;
        private double dollarAmount;
        private double investmentAmount;
        private double marketValue;

        public Transaction(string time, string type, double dollarAmount, double investmentAmount, double marketValue)
        {
            this.time = time;
            this.type = type;
            this.dollarAmount = dollarAmount;
            this.investmentAmount = investmentAmount;
            this.marketValue = marketValue;
        }

        public string getType() { return type; }
        public string getTime() { return time; }
        public double getDollarAmount() { return dollarAmount; }
        public double getInvestmentAmount() { return investmentAmount; }
        public double getMarketValue() { return marketValue; }
    }
}

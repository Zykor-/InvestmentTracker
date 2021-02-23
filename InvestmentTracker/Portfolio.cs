using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace InvestmentTracker
{
    class Portfolio
    {
        private List<Investment> investments;
        private string filePath;
        string name;

        public Portfolio(string name) 
        {
            this.name = name;
            investments = new List<Investment>();
            filePath = Directory.GetCurrentDirectory();
            int endPathIndex = filePath.IndexOf("bin", 0);
            filePath = filePath.Substring(0, endPathIndex) + name + ".txt";
        }

        public void buyInvestment(string name, double amountInvested, double amountPurchased)
        {
            //if investment isn't already in portfolio, add to it
            if(!investments.Exists(p => p.getName() == name))
            {
                investments.Add(new Investment(name, amountInvested, amountPurchased, amountInvested/amountPurchased, "purchase"));
            }
            //else add to existing investment
            else
            {
                foreach(Investment invest in investments)
                {
                    if(invest.getName() == name)
                    {
                        invest.purchase(amountInvested, amountPurchased, amountInvested/amountPurchased);
                        break;
                    }
                }
            }
        }

        public void awardInvestment(string name, double dollarValue, double amountAwarded)
        {
            //temporary arbitrary fix for passive awards that are less than 1 cent
            if (dollarValue == 0)
                dollarValue = .006537931;

            if (!investments.Exists(p => p.getName() == name))
            {
                
                investments.Add(new Investment(name, dollarValue, amountAwarded, dollarValue / amountAwarded, "award"));
            }
            else
            {
                foreach (Investment invest in investments)
                {
                    if (invest.getName() == name)
                    {
                        invest.award(dollarValue, amountAwarded, dollarValue / amountAwarded);
                        break;
                    }
                }
            }
        }

        public void mineInvestment(string name, double dollarValue, double amountMined)
        {
            //temporary arbitrary fix for passive awards that are less than 1 cent
            if (dollarValue == 0)
                dollarValue = .006537931;

            if (!investments.Exists(p => p.getName() == name))
            {

                investments.Add(new Investment(name, dollarValue, amountMined, dollarValue / amountMined, "mine"));
            }
            else
            {
                foreach (Investment invest in investments)
                {
                    if (invest.getName() == name)
                    {
                        invest.award(dollarValue, amountMined, dollarValue / amountMined);
                        break;
                    }
                }
            }
        }

        public void sellInvestment(string name, double amountInvested, double amountSold)
        {
            //if investment isn't in portfolio, throw error
            if (!investments.Exists(p => p.getName() == name))
                Console.WriteLine("You don't own any of this investment");
            else
            {
                foreach(Investment invest in investments)
                {
                    if(name == invest.getName() || name == invest.getShortName())
                    {
                        if(amountSold > invest.getAmountOwned())
                        {
                            Console.WriteLine("You are trying to sell more than you own)");
                            break;
                        }
                        else
                        {
                            invest.sell(amountInvested, amountSold, amountInvested/amountSold);
                            break;
                        }
                    }
                }
            }
        }

        public void display()
        {
            Console.WriteLine("\nName \tOwned \t\tInvested \tSpent \t\tValue \t\tGain/Loss \tRecent Change");

            foreach (Investment invest in investments)
            {
                Console.WriteLine(invest.getShortName() + "\t{0:N8}\t${1:N2}\t\t${2:N2}\t\t${3:N2}\t\t{4:N2}%\t\t{5:N2}%", 
                    invest.getAmountOwned(), invest.getNetInvested(), invest.getTotalSpent(), invest.getCurrentValue(), invest.getGains(), invest.getRecentGains());
            }
            Console.WriteLine("Total" + "\t\t\t${0:N2} \t${1:N2} \t{2:N2} \t\t{3:N2}% \t\t{4:N2}%\n", getTotalNetInvested(), getTotalSpent(), getTotalValue(), getTotalGains(), getTotalRecentGains());
        }

        public void sortByValue()
        {
            investments.Sort();
            investments.Reverse();
        }

        public double getTotalValue()
        {
            double value = 0;

            foreach(Investment invest in investments)
            {
                value += invest.getCurrentValue();
            }

            return value;
        }

        public double getTotalGains()
        {
            double value = 0;
            double invested = 0;

            foreach (Investment invest in investments)
            {
                value += invest.getCurrentValue();
                invested += invest.getNetInvested();
            }

            return ((value - invested) / invested) * 100;
        }

        public double getTotalRecentGains()
        {
            double oldValue = 0;
            double value = 0;

            foreach (Investment invest in investments)
            {
                oldValue += invest.getOldValue();
                value += invest.getCurrentValue();
            }

            return ((value - oldValue) / oldValue) * 100;
        }
        public double getTotalNetInvested()
        {
            double value = 0;

            foreach (Investment invest in investments)
            {
                value += invest.getNetInvested();
            }

            return value;
        }
        public double getTotalSpent()
        {
            double total = 0;
            foreach(Investment invest in investments)
            {
                total += invest.getTotalSpent();
            }

            return total;
        }
        public void loadPortfolio(string profile)
        {
            if (File.Exists(filePath))
            {
                StreamReader input = new StreamReader(filePath);
                string temp;
                string[] data;

                while (!input.EndOfStream)
                {
                    temp = input.ReadLine();
                    data = temp.Split(' ');

                    investments.Add(new Investment(data[0], data[1], Convert.ToDouble(data[2]), Convert.ToDouble(data[3]), Convert.ToDouble(data[2])/Convert.ToDouble(data[3])));
                }

                foreach(Investment invest in investments)
                {
                    invest.loadHistory(name);
                }

                input.Close();
            }
            else
            {
                Console.WriteLine("New profile detected, creating new profile: " + profile);
            }
        }

        public void savePortfolio()
        {
            StreamWriter output = new StreamWriter(filePath);
            foreach(Investment invest in investments)
            {
                output.WriteLine(invest.getName() + " " + invest.getShortName() + " " + invest.getNetInvested() + " " + invest.getAmountOwned());
                invest.saveHistory(name);
            }

            output.Close();
        }

        public void loadMarket()
        {
            foreach(Investment invest in investments)
            {
                //Console.WriteLine("Loading " + invest.getName() + " price...");
                invest.updateValues();
            }
        }
    }
}

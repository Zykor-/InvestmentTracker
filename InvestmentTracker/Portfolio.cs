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

        public void buyInvestment(string name, string shortName, double amountInvested, double amountPurchased)
        {
            //if investment isn't already in portfolio, add to it
            if(!investments.Exists(p => p.getName() == name))
            {
                investments.Add(new Investment(name, shortName, amountInvested, amountPurchased));
            }
            //else add to existing investment
            else
            {
                foreach(Investment invest in investments)
                {
                    if(invest.getName() == name)
                    {
                        invest.purchase(amountInvested, amountPurchased);
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
                        if(amountSold >= invest.getAmountOwned())
                        {
                            Console.WriteLine("You are trying to sell more than you own)");
                            break;
                        }
                        else
                        {
                            invest.sell(amountInvested, amountSold);
                            break;
                        }
                    }
                }
            }
        }

        public void removeInvestment(string name)
        {
            for(int i = 0; i < investments.Count; i++)
            {
                if(name == investments[i].getName())
                {
                    investments.RemoveAt(i);
                    break;
                }
            }
        }

        public void display()
        {
            Console.WriteLine("\nName \t\t Value \t\t Gain/Loss");

            foreach (Investment invest in investments)
            {
                Console.WriteLine(invest.getShortName() + "\t\t ${0:N2}\t\t {1:N2}%", invest.getCurrentValue(), invest.getGains());
            }
            Console.WriteLine("Total" + "\t\t ${0:N2} \t {1:N2}%\n", getTotalValue(), getTotalGains());
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
        public void loadPortfolio(string profile)
        {
            string temp;
            string[] data = new string[4];

            if (File.Exists(filePath))
            {
                StreamReader input = new StreamReader(filePath);

                while (!input.EndOfStream)
                {
                    temp = input.ReadLine();
                    data = temp.Split(' ');

                    buyInvestment(data[0], data[1], Convert.ToDouble(data[2]), Convert.ToDouble(data[3]));
                }

                input.Close();
            }
            else
            {
                Console.WriteLine("New profile detected, creating new profile: " + profile);
            }
        }

        public void savePortfolio(string profile)
        {
            StreamWriter output = new StreamWriter(filePath);
            foreach(Investment invest in investments)
            {
                output.WriteLine(invest.getName() + " " + invest.getShortName() + " " + invest.getNetInvested() + " " + invest.getAmountOwned());
            }

            output.Close();
        }

        public void loadMarket()
        {
            foreach(Investment invest in investments)
            {
                invest.updateValues();
            }
        }

        public List<Investment> getInvestments() { return investments; }
    }
}

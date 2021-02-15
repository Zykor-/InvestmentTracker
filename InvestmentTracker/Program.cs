using System;
using System.IO;


//TODO
// Perpetual removal of code smells
// Transaction history
// Android app
// Web app
// buy/sell function (not actually functional)
// More analytics (probably after transaction history)
// Split Investment class into 2 subclasses, one for stocks and one for cryptos (each will have different scrapers)
// Real-time updating for market values
// Graphical data
// 

namespace InvestmentTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter your profile name");
            string profileName = Console.ReadLine();
            Portfolio myPortfolio = new Portfolio(profileName);
            Console.WriteLine("Loading portfolio...");
            myPortfolio.loadPortfolio(profileName);
            myPortfolio.loadMarket();
            myPortfolio.sortByValue();
            string temp, temp2, temp3, temp4;

            int choice = 0;

            while (choice != 9)
            {
                Console.WriteLine("Choose from the following options: ");
                Console.WriteLine("1: View portfolio");
                Console.WriteLine("2: Add investment");
                Console.WriteLine("3: Remove investment");
                Console.WriteLine("4: Buy investment");
                Console.WriteLine("5: Sell investment");
                Console.WriteLine("9: Save and exit");

                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        myPortfolio.display();
                        break;
                    case 2:
                        Console.WriteLine("Enter investment name: ");
                        temp = Console.ReadLine();
                        Console.WriteLine("Enter investment short name: ");
                        temp2 = Console.ReadLine();
                        Console.WriteLine("Enter initial investment amount: ");
                        temp3 = Console.ReadLine();
                        Console.WriteLine("Enter amount of stock/currency owned: ");
                        temp4 = Console.ReadLine();
                        myPortfolio.addInvestment(temp, temp2, Convert.ToDouble(temp3), Convert.ToDouble(temp4));
                        break;
                    case 3:
                        Console.WriteLine("Enter name of investment to remove");
                        temp = Console.ReadLine();
                        myPortfolio.removeInvestment(temp);
                        break;
                    case 4:

                        break;
                    case 5:
                        
                        break;
                    case 9:
                        myPortfolio.savePortfolio(profileName);
                        choice = 9;
                        break;
                }
            }

            

        }
    }
}

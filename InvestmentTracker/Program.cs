using System;


//TODO
// Perpetual removal of code smells
// Transaction history ***Next on list***
// Android app
// Web app
// buy/sell function (not actually functional) ***DONE***
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
            Console.WriteLine("Loading market...");
            myPortfolio.loadMarket();
            Console.WriteLine("Sorting...");
            myPortfolio.sortByValue();
            string temp, temp2, temp3, temp4;

            int choice = 0;

            while (choice != 9)
            {
                Console.WriteLine("Choose from the following options: ");
                Console.WriteLine("1: View portfolio");
                Console.WriteLine("2: Buy investment");
                Console.WriteLine("3: Sell investment");
                Console.WriteLine("9: Save and exit");

                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        myPortfolio.display();
                        break;
                    case 2:
                        Console.WriteLine("Enter the name of the investment you are buying");
                        temp = Console.ReadLine();
                        Console.WriteLine("Enter investment short name");
                        temp2 = Console.ReadLine();
                        Console.WriteLine("Enter amount spent in $");
                        temp3 = Console.ReadLine();
                        Console.WriteLine("Enter amount of investment purchased");
                        temp4 = Console.ReadLine();
                        myPortfolio.buyInvestment(temp, temp2, Convert.ToDouble(temp3), Convert.ToDouble(temp4));
                        break;
                    case 3:
                        Console.WriteLine("Enter the name of the investment you are selling");
                        temp = Console.ReadLine();
                        Console.WriteLine("Enter amount sold in $");
                        temp2 = Console.ReadLine();
                        Console.WriteLine("Enter amount of investment purchased");
                        temp3 = Console.ReadLine();
                        myPortfolio.sellInvestment(temp, Convert.ToDouble(temp2), Convert.ToDouble(temp3));
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

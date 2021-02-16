using System;


//TODO
// Perpetual removal of code smells
// Transaction history ***Next on list*** and gains since last transaction
// Android app
// Web app
// More analytics (probably after transaction history)
// Split Investment class into 2 subclasses, one for stocks and one for cryptos (each will have different scrapers) or do I split scraper into sub classes?
// Real-time updating for market values
// Graphical data
// Figure out why values don't update in 'real' time as you buy/sell investments
// make investment names input non-case sensitive

//RECENTLY TODO-ED
// buy/sell function (not actually functional) ***DONE***
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
            int subChoice = 0;

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
                        Console.WriteLine("Enter investment name: ");
                        temp = Console.ReadLine();
                        Console.WriteLine("Enter investment short name: ");
                        temp2 = Console.ReadLine();
                        Console.WriteLine("Enter initial investment amount: ");
                        temp3 = Console.ReadLine();
                        Console.WriteLine("Enter amount of stock/currency owned: ");
                        temp4 = Console.ReadLine();
                        while (subChoice != 2)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Does this look right?");
                            Console.WriteLine("1 Yes, 2 No");
                            Console.WriteLine("Enter investment name: ", temp);
                            Console.WriteLine("Enter investment short name: ", temp2);
                            Console.WriteLine("Enter initial investment amount: ", temp3);
                            Console.WriteLine("Enter amount of stock/currency owned: ", temp4);
                            subChoice = Convert.ToInt32(Console.ReadLine());
                            switch(subChoice)
                            {
                                case 1:
                                myPortfolio.addInvestment(temp, temp2, Convert.ToDouble(temp3), Convert.ToDouble(temp4));
                                Console.WriteLine("");
                                Console.WriteLine("Saved")
                                case 2:
                                break;
                            }


                        }
                        break;
                    case 3:
                        Console.WriteLine("Enter the name of the investment you are selling");
                        temp = Console.ReadLine();
                        Console.WriteLine("Enter amount sold in $");
                        temp2 = Console.ReadLine();
                        Console.WriteLine("Enter amount of investment sold");
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

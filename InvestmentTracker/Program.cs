using System;


//TODO
// Perpetual removal of code smells
// gains since last transaction **** JIM WILL DO ****
// Android app
// Web app
// More analytics (probably after transaction history)
// scraper factory, scraper for non-cryptos
// Graphical data
// Figure out why values don't update in 'real' time as you buy/sell investments
// make investment names input non-case sensitive
// 

//RECENTLY TODO-ED
// buy/sell function (not actually functional)
// added inheritence for scraper
// Real-time updating for market values *** I think i fixed this ***
// Transaction History saving and loading
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
            string temp, temp2, temp3, temp4;

            int choice = 0;
            //int subChoice = 0;

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
                        myPortfolio.update();
                        myPortfolio.sortByValue();
                        myPortfolio.display();
                        break;
                    case 2:
                        Console.WriteLine("Enter investment name: ");
                        temp = Console.ReadLine();
                        Console.WriteLine("Enter investment short name: ");
                        temp2 = Console.ReadLine();
                        Console.WriteLine("Enter invested dollar amount: ");
                        temp3 = Console.ReadLine();
                        Console.WriteLine("Enter amount of stock/currency purchased: ");
                        temp4 = Console.ReadLine();

                        myPortfolio.buyInvestment(temp, temp2, Convert.ToDouble(temp3), Convert.ToDouble(temp4));

                        // *** I commented this out because A) i removed the addInvestment method and B) I don't want to have to retype everything when buying investments and C) I want to avoid nested loops as much as possible
                        // Also instead of Console.WriteLine(""); you can just do Console.WriteLine(); quotations are not required
                        //while (subChoice != 2)
                        //{
                        //    Console.WriteLine("");
                        //    Console.WriteLine("Does this look right?");
                        //    Console.WriteLine("1 Yes, 2 No");
                        //    Console.WriteLine("Enter investment name: ", temp);
                        //    Console.WriteLine("Enter investment short name: ", temp2);
                        //    Console.WriteLine("Enter initial investment amount: ", temp3);
                        //    Console.WriteLine("Enter amount of stock/currency owned: ", temp4);
                        //    subChoice = Convert.ToInt32(Console.ReadLine());
                        //    switch(subChoice)
                        //    {
                        //        case 1:
                        //        myPortfolio.addInvestment(temp, temp2, Convert.ToDouble(temp3), Convert.ToDouble(temp4));
                        //        Console.WriteLine("");
                        //        Console.WriteLine("Saved")
                        //        case 2:
                        //        break;
                        //    }
                        //}

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
                        myPortfolio.savePortfolio();
                        choice = 9;
                        break;
                }
            }
        }
    }
}

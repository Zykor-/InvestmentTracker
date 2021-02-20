using System;


//TODO
// Perpetual removal of code smells
// Android app
// Web app
// database
// More analytics (probably after transaction history)
// scraper factory, scraper for non-cryptos
// Graphical data
// make investment names input non-case sensitive
// add different types of transactions, purchase, sell, gift, award, mined, etc
// when buying or selling, have either long name or short name be valid, non case sensitive
// different ways to sort like by gains, alphabetical, etc
// organize profile data into individual folders
// fix gains when netInvested is 0 or negative
// 

//RECENTLY TODO-ED
// Transaction History saving and loading
// gains since last transaction
// scrape short names so user doesn't have to input
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
            string temp, temp2, temp3;

            int choice = 0;
            //int subChoice = 0;

            while (choice != 9)
            {
                Console.WriteLine("Choose from the following options: ");
                Console.WriteLine("1: View portfolio");
                Console.WriteLine("2: Buy investment");
                Console.WriteLine("3: Sell investment");
                Console.WriteLine("4: Add awarded investment");
                Console.WriteLine("5: Add mined crypto");
                Console.WriteLine("8: Tests");
                Console.WriteLine("9: Save and exit");

                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        myPortfolio.loadMarket();
                        myPortfolio.sortByValue();
                        myPortfolio.display();
                        break;
                    case 2:
                        Console.WriteLine("Enter investment name: ");
                        temp = Console.ReadLine();
                        Console.WriteLine("Enter invested dollar amount: ");
                        temp2 = Console.ReadLine();
                        Console.WriteLine("Enter amount of stock/currency purchased: ");
                        temp3 = Console.ReadLine();

                        myPortfolio.buyInvestment(temp, Convert.ToDouble(temp2), Convert.ToDouble(temp3));

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
                    case 4:
                        Console.WriteLine("Enter investment name: ");
                        temp = Console.ReadLine();
                        Console.WriteLine("Enter invested dollar amount: ");
                        temp2 = Console.ReadLine();
                        Console.WriteLine("Enter amount of stock/currency awarded: ");
                        temp3 = Console.ReadLine();
                        myPortfolio.awardInvestment(temp, Convert.ToDouble(temp2), Convert.ToDouble(temp3));
                        break;
                    case 5:
                        Console.WriteLine("Enter investment name: ");
                        temp = Console.ReadLine();
                        Console.WriteLine("Enter invested dollar amount: ");
                        temp2 = Console.ReadLine();
                        Console.WriteLine("Enter amount of stock/currency mined: ");
                        temp3 = Console.ReadLine();
                        myPortfolio.mineInvestment(temp, Convert.ToDouble(temp2), Convert.ToDouble(temp3));
                        break;
                    case 8:
                        Scraper testScraper = new CoinmarketcapScraper("bitcoin");
                        Console.WriteLine(testScraper.scrapeShortName());
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

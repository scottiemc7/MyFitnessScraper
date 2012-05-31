using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyFitnessScraper;
using System.IO;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string userName, password, filePath;
            DateTime startDate = DateTime.Now.Date;
            int numDays = 1;
            if (args.Length == 0)
            {
                try
                {
                    //Ask for args
                    Console.Write("User Name: ");
                    userName = Console.ReadLine();
                    Console.Write("Password: ");
                    password = Console.ReadLine();
                    Console.Write("Start Date: ");
                    startDate = DateTime.Parse(Console.ReadLine());
                    Console.Write("Total Number Of Days: ");
                    numDays = Int32.Parse(Console.ReadLine());
                    Console.Write("Save To File ([Path][FileName].csv): ");
                    filePath = Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(String.Format("Argument exception {0}", ex.Message));                    
                    return;
                }//end try/catch
            }
            else if (args.Length == 5)
            {
                try
                {
                    userName = args[0];
                    password = args[1];
                    startDate = DateTime.Parse(args[2]);
                    numDays = Int32.Parse(args[3]);
                    filePath = args[4];
                }
                catch (Exception ex)
                {
                    Console.WriteLine(String.Format("Argument exception {0}", ex.Message));
                    return;
                }//end try/catch
            }
            else
            {
                Console.WriteLine("Invalid Arguments, usage should be: {UserName} {Password} {StartDate} {NumberOfDays} {PathToFile}");
                return;
            }//end if

            HttpDataGrabber dg = new HttpDataGrabber(new HttpWebRequestFactory(), userName, password);
            HtmlDataParser pg = new HtmlDataParser();

            Console.WriteLine("Grabbing Data...");

			List<ITrackableDay> allDays = new List<ITrackableDay>();
            for (int i = 0; i < numDays; i++)
            {
                DateTime date = startDate.AddDays(i);
				allDays.Add(pg.ParseFoodDiaryDate(dg.GrabFoodDataForDate(date), date));
            }//end for

            Console.WriteLine("Writing Data...");

            CSVDataFormatter formatter = new CSVDataFormatter();
			string formattedString = formatter.Format(allDays);

            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(formattedString);
                }//end using
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("Argument exception {0}", ex.Message));
            }

            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}

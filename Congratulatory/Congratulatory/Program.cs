using System;
using System.Configuration;

namespace Congratulatory
{
    /// <summary>
    /// Точка входа в Поздравлятор
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            DB db = null;
            try
            {
                db = new DB();
            } catch(Exception E)
            {
                Console.WriteLine("Database is fail " + E.Message);
            }
            Console.ReadKey();
            BirthDayList listing = new BirthDayList();
            do
            {
                listing.CheckTheBirthDays();
                Console.Clear();
                
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Birthdays for today");
                Console.ResetColor();
                listing.BirthDaysOnThisDate();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("The nearest birthdays in the range of 10 days");
                Console.ResetColor();
                listing.UpcomingBirthdays();
                //Выводим меню, его пункты с соответствующими цифрами\символами
                Console.WriteLine("### MENU ###");
                Console.WriteLine("1. Adding a new birthday to the list of birthdays");
                Console.WriteLine("2. Viewing a list of birthdays");
                Console.WriteLine("3. Deleting a day from the list by the name of the birthday person");
                Console.WriteLine("4. Edit the selected day by the name of the birthday boy");

                Console.WriteLine("5. Write a list to a file ");
                Console.WriteLine("6. Download the birtdays list from a file");

                Console.WriteLine("7. View birthdays for today");
                Console.WriteLine("8. View past birthdays");

                Console.WriteLine("9.View the nearest birthdays in the range of 10 days");
                Console.WriteLine("_________________________________________________________");
                Console.WriteLine("10. Adding a new birthday to the database");
                Console.WriteLine("11. Adding a new birthday from list to the database");
                Console.WriteLine("12. Delete birtday from a database");
                Console.WriteLine("13. Download the birtdays list from a database");
                Console.WriteLine("14. Update the birtday in database");

                Console.WriteLine("15. Exit");
                Console.Write("\n" + "Введите команду: ");
              
                int number = 0;
               
                String ch = Console.ReadLine();
                number = Convert.ToInt32(ch);
                  
            
       
                switch (number)
                {
                    case  1 :
                        {
                            listing.addToListBirthDays();
                            break;
                        }
                    case  2 :
                        {
                            listing.LookBirthdayList();
                            break;
                        }
                    case  3 :
                        {
                            listing.DeleteBirthDay();
                            break;
                        }
                    case  4 :
                        {
                            listing.ChangeBirthDay();
                            break;
                        }
                    case  5 :
                        {
                            listing.writeListToFile();
                            break;
                        }
                    case  6 :
                        {
                            listing.readListFromFile();
                            break;
                        }
                    case 7 :
                        {
                            listing.BirthDaysOnThisDate();
                            break;
                        }
                    case 8 :
                        {
                            listing.PastBirthdays();
                            break;
                        }
                    case 9 :
                        {
                            listing.UpcomingBirthdays();
                            break;
                         }
                    case 10:
                        {
                            listing.AddToDD(db);
                            break;
                        }
                    case 11:
                        {
                            listing.AddListToDb(db);
                            break;
                        }
                    case 12:
                        {
                            listing.DeleteFromDB(db);
                            break;
                        }
                    case 13:
                        {
                            listing.GetListFromDB(db);
                            break;
                        }
                    case 14:
                        {
                            listing.UpdateBirthdayFromDB(db);
                            break;
                        }
                    case 15:
                        {
                            return;
                            break;
                        }
                    default:
                        {
                            
                            break;
                        }
                }
            } while (true);
        }
    }
}

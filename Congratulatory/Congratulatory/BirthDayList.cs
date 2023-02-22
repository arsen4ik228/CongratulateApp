using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Congratulatory
{
    /// <summary>
    /// Список дней рождения.
    /// </summary>
    class BirthDayList
    {
        private Birthday BirthDay { set; get; }

        public List<Birthday> List;

        public BirthDayList()
        {
            this.List = new List<Birthday>();
            this.BirthDay = new Birthday();
        }

        /// <summary>
        /// Добавление дня рождения в список
        /// </summary>
        public void addToListBirthDays()
        {
            String fio, status, day, month, year;
           
            Console.WriteLine(" | Write a person's fio");
            fio = Console.ReadLine();

            Console.WriteLine(" | Write a person's status");
            status = Console.ReadLine();

            Console.WriteLine(" | Write the date of Birthday. Enter the day:");
            day = Console.ReadLine();
            Console.WriteLine(" | Write the date of Birthday. Enter the month:");
            month = Console.ReadLine();
            Console.WriteLine(" | Write the date of Birthday. Enter the year:");
            year = Console.ReadLine();

            DateTime date = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(day)); 

            Console.ReadKey();
            Birthday NewBirthDay = new Birthday(date, fio, -1, status, Birthday.CheckBirthDayPassed(date));
            this.List.Add(NewBirthDay);
        }

        /// <summary>
        ///  Просмотр списка дней рождений
        /// </summary>
        public void LookBirthdayList()
        {
            Console.WriteLine("List of BirthDays: ");
            foreach (Birthday t in List)
            {
                Birthday ThisBirthDay = new Birthday(t);
                bool done = t.getIsPassed();
                PrintDay(t, done);
            }
            Console.ReadKey();
        }

        /// <summary>
        /// Удаление выбранного по ФИО дня рождения из списка
        /// </summary>
        public void DeleteBirthDay()
        {
            String fio;
            Console.WriteLine(" | If you want to delete a Birthday you must write a fio of person :");
            fio = Console.ReadLine();
            bool result = false;
            try
            {
                foreach (Birthday t in List)
                {

                    String contains = t.getFio();
                    if (fio.Equals(contains))
                    {
                        result = List.Remove(t);
                    }
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            if (result)
            {
                Console.WriteLine("Delete is succesful!!!");
                Console.ReadKey();
            }
        }

        /// <summary>
        ///  Запись всего списка дней рождений в файл
        /// </summary>
        public void writeListToFile()
        {
            try
            {
                string fileName = "out.txt";
                FileStream aFile = new FileStream(fileName, FileMode.OpenOrCreate | FileMode.Truncate);
                StreamWriter sw = new StreamWriter(aFile);
                sw.Flush();
                aFile.Seek(0, SeekOrigin.End);
                try
                {
                    foreach (Birthday t in List)
                    {
                        sw.WriteLine(t.getFio());
                        sw.WriteLine(t.getDate());
                        sw.WriteLine(t.getStatusOfPerson());
                        sw.WriteLine(t.getIsPassed());
                        sw.WriteLine(t.getId());
                    }
                }

                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        /// <summary>
        /// Чтения файла записи дней рождений и составление списка.
        /// </summary>
        public void readListFromFile()
        {
            string fileName = "out.txt";

            int oneTaskMember = 0;

            StreamReader sr = new StreamReader("out.txt");
            string line;
            while (!sr.EndOfStream)
            {
                Birthday newBirthDay = new Birthday();
                newBirthDay.setFio(sr.ReadLine());
                newBirthDay.setDate(DateTime.Parse(sr.ReadLine()));
                newBirthDay.setStatusOfPerson(sr.ReadLine());
                newBirthDay.setIsPassed(bool.Parse(sr.ReadLine()));
                newBirthDay.setId(Convert.ToInt32(sr.ReadLine()));
               
                oneTaskMember = 0;
                List.Add(newBirthDay);
                //
                
            }
            sr.Close();

            Console.ReadKey();
        }

        /// <summary>
        /// Изменение дня рождения в списке. Поиск изменяемого дня рождения по ФИО
        /// </summary>
        public void ChangeBirthDay()
        {
            String fio, status, day, month, year; ;

            String NewFio, yesNo;

            Console.WriteLine(" | If you want to change a Task you must write a fio of person:");
            fio = Console.ReadLine();
            try
            {
                foreach (Birthday t in List)
                {
                    String contains = t.getFio();
                    if (fio.Equals(contains))
                    {
                        Console.WriteLine(" | Make change Birthday fio? - Y/N");
                        yesNo = Console.ReadLine();
                        if (yesNo.Equals("Y"))
                        {
                            Console.WriteLine(" | Write the new fio of person thi Berthday ");
                            NewFio = Console.ReadLine();
                            t.setFio(NewFio);
                        }
                        Console.WriteLine(" | Make change Birthday status of person? - Y/N");
                        yesNo = Console.ReadLine();
                        if (yesNo.Equals("Y"))
                        {
                            Console.WriteLine(" | Write the new status");
                            status = Console.ReadLine();
                            t.setStatusOfPerson(status);
                        }
                        Console.WriteLine(" | Make change Birthday date - Y/N");
                        yesNo = Console.ReadLine();
                        if (yesNo.Equals("Y"))
                        {
                            Console.WriteLine(" | Write the date of Birthday. Enter the day:");
                            day = Console.ReadLine();
                            Console.WriteLine(" | Write the date of Birthday. Enter the month:");
                            month = Console.ReadLine();
                            Console.WriteLine(" | Write the date of Birthday. Enter the year:");
                            year = Console.ReadLine();

                            DateTime date = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(day));
                            t.setDate(date);
                        }
                    }
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Уже прошедший дни рождения
        /// </summary>
        public void PastBirthdays()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("List of past birthdays: ");
            foreach (Birthday t in List)
            {
                bool done = t.getIsPassed();
                if (done)
                {
                    PrintDay(t, done);
                }
            }
            Console.ResetColor();
            Console.ReadKey();
        }

        /// <summary>
        /// Дни рождения на сегодняшний день
        /// </summary>
        public void BirthDaysOnThisDate()
        {
            DateTime BirthDayDate;
            foreach (Birthday t in List)
            {
                BirthDayDate = t.getDate();
               // DateTime thisDay = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day);
                if ((BirthDayDate.Month == DateTime.UtcNow.Month) && (BirthDayDate.Day == DateTime.UtcNow.Day))
                {
                    bool done = t.getIsPassed();
                    PrintDay(t, done);
                }
            }
            Console.ReadKey();

        }

        /// <summary>
        /// Печать дней рождений которые находятся в диапазоне: от -5 до +5 дней.
        /// </summary>
        public void UpcomingBirthdays()
        {
            DateTime BirthDayDate;
            DateTime now = DateTime.UtcNow;
            DateTime later = new DateTime(now.Year, now.Month, now.Day + 5);
            DateTime earlier = new DateTime(now.Year, now.Month, now.Day - 5);
            foreach (Birthday t in List)
            {
                BirthDayDate = t.getDate();
                if ((BirthDayDate.Month == earlier.Month) && (BirthDayDate.Month == later.Month) && (BirthDayDate.Day > earlier.Day) && (BirthDayDate.Day < later.Day) && (BirthDayDate.Day != now.Day))
                {
                    bool done = t.getIsPassed();
                    PrintDay(t, done);
                }
            }
            Console.ReadKey();

        }

        /// <summary>
        /// Печать записи о дне рождения на экран 
        /// </summary>
        /// <param name="day"> День рождения </param>
        /// <param name="done"> флаг указывающий прошёл день рождения уже или нет </param>
        public void PrintDay(Birthday day, bool done)
        {
            Console.WriteLine("________________");
            Console.WriteLine(day.getFio());
            Console.WriteLine(day.getDate());
            Console.WriteLine(day.getStatusOfPerson());
            if (done)
            {
                Console.WriteLine("The Birthday is passed");
            }
            else
            {
                Console.WriteLine("Birthday not yet passed");
            }

            Console.WriteLine("________________");
        }

        /// <summary>
        /// Проверка каждого дня рождения в списке на то прошёл он уже или нет
        /// </summary>
        public void CheckTheBirthDays()
        {
            foreach (Birthday t in List)
            {
                Birthday.SwitchPassedTrigger(t);
            }
        }

        /// <summary>
        /// Добавление записи в базу данных - дня рождения 
        /// </summary>
        /// <param name="db"> Объект подключения к базе данных </param>
        public void AddToDD(DB db)
        {
            String fio, status, day, month, year;

            Console.WriteLine(" | Write a person's fio");
            fio = Console.ReadLine();

            Console.WriteLine(" | Write a person's status");
            status = Console.ReadLine();

            Console.WriteLine(" | Write the date of Birthday. Enter the day:");
            day = Console.ReadLine();
            Console.WriteLine(" | Write the date of Birthday. Enter the month:");
            month = Console.ReadLine();
            Console.WriteLine(" | Write the date of Birthday. Enter the year:");
            year = Console.ReadLine();

            DateTime date = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(day));

            Console.ReadKey();
            Birthday NewBirthDay = new Birthday(date, fio, -1, status, Birthday.CheckBirthDayPassed(date));
            this.List.Add(db.AddToDB(NewBirthDay));
        }
        public void AddListToDb(DB db)
        {
            foreach (Birthday t in List)
            {
                if (t.getId() == -1)
                {
                    db.AddToDB(t);
                }
            }
        }

        /// <summary>
        /// Удаление записи из бд - день рождения ищется по ФИО, а удаляется по идентификатору
        /// </summary>
        /// <param name="db"> Объект подключения к базе данных </param>
        public void DeleteFromDB(DB db)
        {
            String fio;
            Console.WriteLine(" | If you want to delete a Birthday you must write a fio of person :");
            fio = Console.ReadLine();
            int answer = 0;
            try
            {
                foreach (Birthday t in List)
                {

                    String contains = t.getFio();
                    if (fio.Equals(contains))
                    {
                        answer = db.DeleteFromDB(t);
                    }
                    if (answer != 0)
                    {
                        List.Remove(t);
                    }
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public void GetListFromDB(DB db)
        {
            this.List.AddRange( db.GetAllBirthdaysToList(List));
        }

        /// <summary>
        /// Обновление дня рождения по фио. Обновление всех полей записи.
        /// </summary>
        /// <param name="db"> Объект подключения к базе данных</param>
        public void UpdateBirthdayFromDB(DB db)
        {
            String fio;
            Console.WriteLine(" | If you want to change a Birthday you must write a fio of person :");
            fio = Console.ReadLine();
            int answer = 0;
            try
            {
                foreach (Birthday t in List)
                {
                    answer = 0;
                    String contains = t.getFio();
                    if (fio.Equals(contains))
                    {
                        Console.WriteLine("ID = " + t.getId());
                        Console.ReadKey();
                        String newFio, status, day, month, year;

                        Console.WriteLine(" | Write a person's fio");
                        newFio = Console.ReadLine();

                        Console.WriteLine(" | Write a person's status");
                        status = Console.ReadLine();

                        Console.WriteLine(" | Write the date of Birthday. Enter the day:");
                        day = Console.ReadLine();
                        Console.WriteLine(" | Write the date of Birthday. Enter the month:");
                        month = Console.ReadLine();
                        Console.WriteLine(" | Write the date of Birthday. Enter the year:");
                        year = Console.ReadLine();

                        DateTime date = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(day));

                        t.setDate(date);
                        t.setFio(newFio);
                        t.setStatusOfPerson(status);


                        answer = db.EditBirthDayInDB(t);
                    }
                    if (answer != 0)
                    {
                        Console.WriteLine("Replace is succesful");
                        Console.ReadKey();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}

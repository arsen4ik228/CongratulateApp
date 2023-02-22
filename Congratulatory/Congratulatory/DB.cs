using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace Congratulatory
{
    /// <summary>
    /// Класс работы с базой данных
    /// </summary>
    class DB
    {
        private SqlConnection sqlConnection = null;
        public DB()
        {
            try
            { 
                sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["GracDB"].ConnectionString);
                sqlConnection.Open();
            } 
            catch (NullReferenceException R)
            {
                Console.WriteLine(R.Message);
            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
                Console.ReadKey();
            }

            if (sqlConnection.State == ConnectionState.Open)
            {
                Console.WriteLine("Hello World! Now is {0}", DateTime.UtcNow.Date);
            }
        }

        /// <summary>
        /// Добавление новой записи в базу данных - дня рождения 
        /// </summary>
        /// <param name="Day"> Добавляемый день рождения </param>
        /// <returns> Обновлённый день рождения содержащий айди записи в базе данных </returns>
        public Birthday AddToDB(Birthday Day)
        {
            SqlCommand coommand = null;
            String date = $"{Day.getDate().Month}/{Day.getDate().Day}/{Day.getDate().Year}";
            Object rowCount = null;
            try
            {
                coommand = new SqlCommand($"INSERT INTO [Birthdays] (date, fio, personStatus) VALUES ( '{date}', '{Day.getFio()}', '{Day.getStatusOfPerson()}' ) SELECT SCOPE_IDENTITY()", sqlConnection);
                rowCount = coommand.ExecuteScalar();
            }
            catch(Exception E)
            {
                Console.WriteLine(E.Message);
                Console.ReadKey();
            }
            
            Day.setId(long.Parse(rowCount.ToString()));
            return Day;

        }

        /// <summary>
        /// Редактирование дня рождения записи в базей данных 
        /// </summary>
        /// <param name="Day"> Редактируемый день. С новыми данными</param>
        /// <returns> Флаг успешности редактирования </returns>
        public int EditBirthDayInDB(Birthday Day)
        {
            SqlCommand coommand = null;
            String date = $"{Day.getDate().Month}/{Day.getDate().Day}/{Day.getDate().Year}";
            int result = 0;
            try
            {
                coommand = new SqlCommand($"UPDATE [Birthdays] SET date = '{date}', fio = '{Day.getFio()}' , personStatus = '{Day.getStatusOfPerson()}' WHERE IdBirthday = '{Day.getId()}'", sqlConnection);
                result = coommand.ExecuteNonQuery();
            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
                Console.ReadKey();
            }
            return result;
        }

        /// <summary>
        /// Удаление записи о дней рождения из базы данных
        /// </summary>
        /// <param name="Day"> Удаляемый день рождения </param>
        /// <returns> Флаг успешности удаления</returns>
        public int DeleteFromDB(Birthday Day)
        {
            SqlCommand coommand = null;
            int result = 0;
         
            try
            {
                coommand = new SqlCommand($"DELETE FROM [Birthdays] WHERE (IdBirthday) = (@IdBirthday)", sqlConnection);
                coommand.Parameters.AddWithValue("IdBirthday", Day.getId());
                result = coommand.ExecuteNonQuery();
            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
                Console.ReadKey();
            }
            return result;
        }

        /// <summary>
        /// Получение всего списка дней рождений из базы данных и добавление новых записей в текущий список.
        /// </summary>
        /// <param name="List"> Текущий список записей дней рождений</param>
        /// <returns> Новый список дней рождений </returns>
        public List<Birthday> GetAllBirthdaysToList(List<Birthday> List)
        {
            SqlCommand coommand = null;
            
            try
            {
                coommand = new SqlCommand("SELECT * FROM [Birthdays]", sqlConnection);

                SqlDataReader reader = coommand.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    // выводим названия столбцов
                    Console.WriteLine("{0}\t{1}\t{2}", reader.GetName(0), reader.GetName(1), reader.GetName(2));

                    while (reader.Read()) // построчно считываем данные
                    {
                        Birthday day = new Birthday();
                        day.setId(Convert.ToInt64(reader.GetValue(0)));
                        day.setDate(DateTime.Parse(reader.GetValue(1).ToString()));
                        day.setFio(Convert.ToString(reader.GetValue(2)));
                        day.setStatusOfPerson(Convert.ToString(reader.GetValue(3)));
                        List.Add(day);
                    }
                }
            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
                Console.ReadKey();
            }
            return List;
        }

    }
}

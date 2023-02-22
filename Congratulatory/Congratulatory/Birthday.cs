using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Congratulatory
{
    /// <summary>
    /// Класс - сущность дня Рождения.
    /// </summary>
    class Birthday
    {
        private long Id;

        public void setId(long id)
        {
            this.Id = id;
        }

        public long getId()
        {
            return this.Id;
        }
       
        private DateTime Date;
        public void setDate(DateTime date)
        {
            this.Date = date;
        }

        public DateTime getDate()
        {
            return this.Date;
        }
        private String Fio;

        public void setFio(String fio)
        {
            this.Fio = fio;
        }

        public String getFio()
        {
            return this.Fio;
        }
        
        private bool IsPassed;
        public void setIsPassed(bool isPassed)
        {
            this.IsPassed = isPassed;
        }

        public bool getIsPassed()
        {
            return this.IsPassed;
        }
       
        private String StatusOfPerson;
        public void setStatusOfPerson(String statusOfPerson)
        {
            this.StatusOfPerson = statusOfPerson;
        }

        public String getStatusOfPerson()
        {
            return this.StatusOfPerson;
        }

        public Birthday()
        {
            this.Date = DateTime.UtcNow;
            this.Fio = null;
            this.Id = -1;
            this.StatusOfPerson = null;
            this.IsPassed = false;
        }

        public Birthday(Birthday Day)
        {
            this.Date = Day.Date;
            this.Fio = Day.Fio;
            this.Id = Day.Id;
            this.StatusOfPerson = Day.StatusOfPerson;
            this.IsPassed = Day.IsPassed;
        }

        public Birthday(DateTime data, String fio, long id, string status, bool isPassed)
        {
            this.Date = data;
            this.Fio = fio;
            this.Id = id;
            this.StatusOfPerson = status;
            this.IsPassed = isPassed;
        }

        /// <summary>
        /// Проверка даты на то прошла ли она или нет.
        /// </summary>
        /// <param name="Date"> Дата Дня Рождения </param>
        /// <returns> результат проверки, true -  день рождения ещё прошёл.</returns>
        public static bool CheckBirthDayPassed(DateTime Date)
        {
            if (Date.Month > DateTime.UtcNow.Month)
            {
                return false;
            }
            else if ((Date.Month == DateTime.UtcNow.Month) && (Date.Day > DateTime.UtcNow.Day))
            {
                return false;
            }
            else
                return true;
        }

        /// <summary>
        /// Изменение триггера Дня Рождения, прошёл уже или нет
        /// </summary>
        /// <param name="Day">День Рождения </param>
        /// <returns>Измененённый День Рождения</returns>
        public static Birthday SwitchPassedTrigger(Birthday Day)
        {
            if (CheckBirthDayPassed(Day.Date))
            {
                Day.IsPassed = true;
            }
            return Day;
        }
    }
}

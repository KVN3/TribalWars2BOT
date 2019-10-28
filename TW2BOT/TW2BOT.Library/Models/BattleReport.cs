using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TW2BOT.Library.Models
{
    public enum MonthAbbreviations
    {
        Jan, Feb, Mar, Apr, May, Jun, Jul, Aug, Sep, Oct, Nov, Dec
    }

    public class BattleReport
    {
        public Village versusVillage;
        public DateTime timeOfBattle;
        public bool isFullHaul;

        public BattleReport(Village village, DateTime time, bool isFullyLooted)
        {
            this.versusVillage = village;
            this.timeOfBattle = time;
            this.isFullHaul = isFullyLooted;
        }

        public BattleReport(Village village, string tribalWarsTimeNotation, bool isFullyLooted)
        {
            this.versusVillage = village;
            this.timeOfBattle = ConvertToDateTime(tribalWarsTimeNotation);
            this.isFullHaul = isFullyLooted;
        }

        public bool ShouldAttackAgain()
        {
            // If the report is recent (within 2 hours) it's still reliable
            bool isReliable = timeOfBattle >= (DateTime.Now.AddHours(-2)) ? true : false;
            return !isReliable || isFullHaul ? true : false;
        }

        public DateTime ConvertToDateTime(string timeOfAttack)
        {
            DateTime convertedDateTime = new DateTime();

            string[] splitPieces = timeOfAttack.Split(' ');

            DateTime day = new DateTime();

            int modifier = 0;

            // If 'yesterday' notation
            if (splitPieces[0].ToLower().Equals("yesterday"))
            {
                day = DateTime.Now.AddDays(-1);
            }
            else if (splitPieces[0].ToLower().Equals("today"))
            {
                day = DateTime.Now;
            }
            else
            {
                modifier = 1;
            }

            // Do the time part
            string[] seperateTimeParts = splitPieces[2 + modifier].Split(':');

            int hours = Int32.Parse(seperateTimeParts[0]);

            // Game server 1 hour behind
            hours += 1;

            if (splitPieces[3].Equals("PM"))
            {
                if (hours < 12)
                    hours += 12;
            }

            int minutes = Int32.Parse(seperateTimeParts[1]);
            int seconds = Int32.Parse(seperateTimeParts[2]);



            if (modifier != 1)
                convertedDateTime = new DateTime(day.Year, day.Month, day.Day, hours, minutes, seconds);
            else
            {
                Enum.TryParse(splitPieces[0], out MonthAbbreviations month);


                convertedDateTime = new DateTime(Int32.Parse(splitPieces[2]), (int)month + 1, Int32.Parse(splitPieces[1]), hours, minutes, seconds);
            }

            return convertedDateTime;
        }
    }
}

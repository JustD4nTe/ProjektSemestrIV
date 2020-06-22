using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestrIV.DAL.Entities
{
    class Competition
    {
        #region Properties
        public uint Id { get; set; }
        public string Location { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        #endregion

        #region Constructors
        public Competition(DataRow data)
        {
            Id = uint.Parse(data["id"].ToString());
            Location = data["miejsce"].ToString();
            StartDate = data["rozpoczecie"].ToString();
            EndDate = data["zakonczenie"].ToString();
        }

        public Competition(string location, string startDate, string endDate)
        {
            Id = default;
            Location = location.Trim();
            StartDate = startDate.Trim();
            EndDate = string.IsNullOrWhiteSpace(endDate) ? null : endDate.Trim();
        }
        #endregion

        public IEnumerable<MySqlParameter> GetParameters()
            => new List<MySqlParameter>()
            {
                new MySqlParameter("@miejsce", Location),
                new MySqlParameter("@rozpoczecie", GetMySQLFormatDate(StartDate)),
                new MySqlParameter("@zakonczenie", GetMySQLFormatDate(EndDate))
            };

        private object GetMySQLFormatDate(string date)
        {
            if (date == null)
                return (object)DBNull.Value;
            else
                return DateTime.Parse(date).ToString("yyyy-MM-dd HH:mm:ss.fff");
        }
    }
}

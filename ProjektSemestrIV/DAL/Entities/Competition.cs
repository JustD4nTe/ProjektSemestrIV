using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestrIV.DAL.Entities
{
    class Competition : IBaseEntity
    {
        #region Properties
        public uint Id { get; private set; }
        public string Location { get; private set; }
        public string StartDate { get; private set; }
        public string EndDate { get; private set; }
        #endregion

        public Competition() { }

        public Competition(string location, string startDate, string endDate)
        {
            Id = default;
            Location = location.Trim();
            StartDate = startDate.Trim();
            EndDate = string.IsNullOrWhiteSpace(endDate) ? null : endDate.Trim();
        }

        public IEnumerable<MySqlParameter> GetParameters()
            => new List<MySqlParameter>()
            {
                new MySqlParameter("@miejsce", Location),
                new MySqlParameter("@rozpoczecie", GetMySQLFormatDate(StartDate)),
                new MySqlParameter("@zakonczenie", GetMySQLFormatDate(EndDate))
            };
        public void SetData(IDataReader dataReader)
        {
            Id = uint.Parse(dataReader["id"].ToString());
            Location = dataReader["miejsce"].ToString();
            StartDate = dataReader["rozpoczecie"].ToString();
            EndDate = dataReader["zakonczenie"].ToString();
        }

        // shallow copy
        public object Clone() => this.MemberwiseClone();

        private object GetMySQLFormatDate(string date)
        {
            if (date == null)
                return (object)DBNull.Value;
            else
                return DateTime.Parse(date).ToString("yyyy-MM-dd HH:mm:ss.fff");
        }

    }
}

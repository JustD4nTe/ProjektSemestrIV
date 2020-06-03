using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestrIV.DAL.Entities
{
    class Competition
    {
        #region Properties
        public UInt16 Id { get; set; } 
        public string Location { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        #endregion

        #region Constructors
        public Competition(MySqlDataReader reader)
        {
            Id = UInt16.Parse(reader["id"].ToString());
            Location = reader["miejsce"].ToString();
            StartDate = reader["rozpoczecie"].ToString();
            EndDate = reader["zakonczenie"].ToString();
        }

        public Competition(string location, string startDate, string endDate)
        {
            Id = default;
            Location = location.Trim();
            StartDate = startDate.Trim();
            EndDate = string.IsNullOrWhiteSpace(endDate) ? null : endDate.Trim();
        }
        #endregion

        public string ToInsert()
        {
            return $"{Location} {StartDate} {EndDate}";
        }

        public override string ToString()
        {
            return $"{Id} {Location} {StartDate} {EndDate}";
        }
    }
}

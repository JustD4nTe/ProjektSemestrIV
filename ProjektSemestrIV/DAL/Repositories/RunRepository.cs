using MySql.Data.MySqlClient;
using ProjektSemestrIV.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestrIV.DAL.Repositories
{
    class RunRepository : BaseRepository
    {
        #region CRUD
        public static bool AddRunToDatabase(Run run)
        {
            var query = @"INSERT INTO przebieg (`czas`, `id_strzelec`, `id_trasa`)
                            VALUES (@czas, @id_strzelec, @id_trasa)";

            return ExecuteAddQuery(query, run.GetParameters());
        }

        public static bool EditRunInDatabase(Run run, uint shooter_id, uint stage_id)
        {
            var query = $@"UPDATE przebieg 
                            SET `czas` = @czas, `id_strzelec` = @id_strzelec, `id_trasa` = @id_trasa 
                            WHERE (`id_strzelec` = '{shooter_id}' and `id_trasa` = '{stage_id}')";

            return ExecuteUpdateQuery(query, run.GetParameters());
        }

        public static Run GetRunWhere(uint shooter_id, uint stage_id)
        {
            var query = $@"SELECT * FROM przebieg 
                           WHERE (`id_strzelec` = '{shooter_id}' and `id_trasa` = '{stage_id}')";

            DataTable resultOfQuery = ExecuteSelectQuery(query);

            // when result contains only one row of run
            // return new Run object
            // otherwise return null
            return resultOfQuery.Rows.Count == 1 ? new Run(resultOfQuery.Rows[0]) : null;
        }

        public static bool DeleteRunFromDatabase(uint run_id)
        {
            var query = $@"DELETE FROM przebieg WHERE (`id` = '{run_id}')";

            return ExecuteDeleteQuery(query);
        }
        #endregion
    }
}

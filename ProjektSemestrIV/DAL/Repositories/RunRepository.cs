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
        public static bool AddRun(Run run)
        {
            var query = @"INSERT INTO przebieg (`czas`, `id_strzelec`, `id_trasa`)
                            VALUES (@czas, @id_strzelec, @id_trasa)";

            return ExecuteModifyQuery(query, run.GetParameters());
        }

        public static bool EditRun(Run run, uint shooter_id, uint stage_id)
        {
            var query = $@"UPDATE przebieg 
                            SET `czas` = @czas, `id_strzelec` = @id_strzelec, `id_trasa` = @id_trasa 
                            WHERE (`id_strzelec` = '{shooter_id}' and `id_trasa` = '{stage_id}')";

            return ExecuteModifyQuery(query, run.GetParameters());
        }

        public static Run GetRunWhere(uint shooter_id, uint stage_id)
        {
            var query = $@"SELECT * FROM przebieg 
                           WHERE (`id_strzelec` = '{shooter_id}' and `id_trasa` = '{stage_id}')";
                        
            // return first object or null (if returned collection is empty)
            return ExecuteSelectQuery<Run>(query).FirstOrDefault();
        }

        public static bool DeleteRun(uint run_id)
        {
            var query = $@"DELETE FROM przebieg WHERE (`id` = '{run_id}')";

            return ExecuteModifyQuery(query);
        }
        #endregion
    }
}

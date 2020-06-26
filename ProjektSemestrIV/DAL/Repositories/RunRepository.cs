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

        public static bool EditRun(Run run, uint shooterId, uint stageId)
        {
            var query = $@"UPDATE przebieg 
                            SET `czas` = @czas, `id_strzelec` = @id_strzelec, `id_trasa` = @id_trasa 
                            WHERE (`id_strzelec` = '{shooterId}' and `id_trasa` = '{stageId}')";

            return ExecuteModifyQuery(query, run.GetParameters());
        }

        public static Run GetRun(uint shooterId, uint stageId)
        {
            var query = $@"SELECT * FROM przebieg 
                           WHERE (`id_strzelec` = '{shooterId}' and `id_trasa` = '{stageId}')";
                        
            // return first object or null (if returned collection is empty)
            return ExecuteSelectQuery<Run>(query).FirstOrDefault();
        }

        public static bool DeleteRun(uint runId)
        {
            var query = $@"DELETE FROM przebieg WHERE (`id` = '{runId}')";

            return ExecuteModifyQuery(query);
        }
        #endregion
    }
}

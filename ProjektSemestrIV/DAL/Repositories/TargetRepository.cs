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
    class TargetRepository : BaseRepository
    {
        #region CRUD
        public static IEnumerable<Target> GetTargets(uint shooterId, uint stageId)
        {
            var query = $"SELECT * FROM tarcza WHERE strzelec_id = '{shooterId}' AND trasa_id = '{stageId}'";

            return ExecuteSelectQuery<Target>(query);
        }

        public static bool AddTarget(Target target)
        {
            var query = @"INSERT INTO tarcza (`strzelec_id`, `trasa_id`, `alpha`, `charlie`, `delta`, `miss`, `n-s`, `proc`, `extra`)
                            VALUES (@strzelec_id, @trasa_id, @alpha, @charlie, @delta, @miss, @ns, @proc, @extra)";

            return ExecuteModifyQuery(query, target.GetParameters());
        }

        public static bool EditTarget(Target target, uint targetId)
        {
            var query = $@"UPDATE `tarcza` 
                            SET `strzelec_id` = @strzelec_id, `trasa_id` = @trasa_id, 
                                `alpha` = @alpha, `charlie` = @charlie, 
                                `delta` = @delta, `miss` = @miss, `n-s` = @ns, 
                                `proc` = @proc, `extra` = @extra 
                            WHERE id = '{targetId}'";

            return ExecuteModifyQuery(query, target.GetParameters());
        }

        public static bool DeleteTarget(uint targetId)
        {
            var query = $"DELETE FROM tarcza WHERE (`id` = '{targetId}')";

            return ExecuteModifyQuery(query);
        }
        #endregion
    }
}

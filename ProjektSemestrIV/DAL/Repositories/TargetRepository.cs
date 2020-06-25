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
        public static IEnumerable<Target> GetTargetsWhere(uint shooter_id, uint stage_id)
        {
            var query = $"SELECT * FROM tarcza WHERE strzelec_id = '{shooter_id}' AND trasa_id = '{stage_id}'";

            return ExecuteSelectQuery<Target>(query);
        }

        public static bool AddTarget(Target target)
        {
            var query = @"INSERT INTO tarcza (`strzelec_id`, `trasa_id`, `alpha`, `charlie`, `delta`, `miss`, `n-s`, `proc`, `extra`)
                            VALUES (@strzelec_id, @trasa_id, @alpha, @charlie, @delta, @miss, @ns, @proc, @extra)";

            return ExecuteModifyQuery(query, target.GetParameters());
        }

        public static bool EditTarget(Target target, uint target_id)
        {
            var query = $@"UPDATE `tarcza` 
                            SET `strzelec_id` = @strzelec_id, `trasa_id` = @trasa_id, 
                                `alpha` = @alpha, `charlie` = @charlie, 
                                `delta` = @delta, `miss` = @miss, `n-s` = @ns, 
                                `proc` = @proc, `extra` = @extra 
                            WHERE id = '{target_id}'";

            return ExecuteModifyQuery(query, target.GetParameters());
        }

        public static bool DeleteTarget(uint targetID)
        {
            var query = $"DELETE FROM tarcza WHERE (`id` = '{targetID}')";

            return ExecuteModifyQuery(query);
        }
        #endregion
    }
}

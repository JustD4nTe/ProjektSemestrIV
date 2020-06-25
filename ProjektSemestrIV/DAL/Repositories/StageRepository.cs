using MySql.Data.MySqlClient;
using ProjektSemestrIV.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace ProjektSemestrIV.DAL.Repositories
{
    internal class StageRepository : BaseRepository
    {
        #region CRUD

        public static IEnumerable<Stage> GetAllStages()
        {
            var query = "SELECT * FROM trasa";

            return ExecuteSelectQuery<Stage>(query);
        }

        public static Stage GetStageById(uint id)
        {
            var query = $"SELECT * FROM trasa WHERE trasa.id = {id}";

            return ExecuteSelectQuery<Stage>(query).FirstOrDefault();
        }

        public static bool AddStage(Stage stage)
        {
            var query = @"INSERT INTO trasa (`id_zawody`, `nazwa`, `zasady`) 
                            VALUES (@id_zawody, @nazwa, @zasady)";

            return ExecuteModifyQuery(query, stage.GetParameters());
        }

        public static bool EditStage(Stage stage, uint id)
        {
            var query = $@"UPDATE `trasa` 
                            SET `id_zawody` = @id_zawody, `nazwa` = @nazwa,
                                `zasady` = @zasady 
                            WHERE (`id` = '{id}');";

            return ExecuteModifyQuery(query, stage.GetParameters());
        }

        public static bool DeleteStage(uint stageID)
        {
            var query = $"DELETE FROM trasa WHERE (`id` = '{stageID}')";

            return ExecuteModifyQuery(query);
        }

        #endregion CRUD

        #region Auxiliary queries

        public static uint GetNumOfTargetsOnStage(uint id)
        {
            var query = $@"SELECT DISTINCT(count(tarcza.id)) AS numOfTargets FROM trasa
                            INNER JOIN tarcza ON trasa.id = tarcza.trasa_id
                            INNER JOIN przebieg ON przebieg.id_trasa = trasa.id
                            INNER JOIN strzelec ON przebieg.id_strzelec = strzelec.id AND strzelec.id = tarcza.strzelec_id
                            WHERE trasa.id = {id}
                            GROUP BY strzelec.id;";
            
            return ExecuteSelectQuery<uint>(query).FirstOrDefault();
        }

        public static double GetAverageTimeOnStageById(uint id)
        {
            var query = $@"SELECT avg(przebieg.czas) AS averageTime FROM przebieg
                            INNER JOIN trasa ON trasa.id = przebieg.id_trasa
                            WHERE trasa.id = {id};";

            return ExecuteSelectQuery<uint>(query).FirstOrDefault();
        }

        #endregion Auxiliary queries
    }
}
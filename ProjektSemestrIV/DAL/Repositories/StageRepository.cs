using MySql.Data.MySqlClient;
using ProjektSemestrIV.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace ProjektSemestrIV.DAL.Repositories
{
    internal class StageRepository : BaseRepository
    {
        #region CRUD

        public static IEnumerable<Stage> GetAllStages()
        {
            var query = "SELECT * FROM trasa";

            List<Stage> stages = new List<Stage>();

            DataTable resultOfQuery = ExecuteSelectQuery(query);

            foreach (DataRow row in resultOfQuery.Rows)
                stages.Add(new Stage(row));

            return stages;
        }

        public static Stage GetStageByIdFromDB(uint id)
        {
            var query = $"SELECT * FROM trasa WHERE trasa.id = {id}";

            DataTable resultOfQuery = ExecuteSelectQuery(query);

            // when result contains only one row of stage
            // return new Stage object
            // otherwise return null
            return resultOfQuery.Rows.Count == 1 ? new Stage(resultOfQuery.Rows[0]) : null;
        }

        public static bool AddStageToDatabase(Stage stage)
        {
            var query = @"INSERT INTO trasa (`id_zawody`, `nazwa`, `zasady`) 
                            VALUES (@id_zawody, @nazwa, @zasady)";

            return ExecuteAddQuery(query, stage.GetParameters());
        }

        public static bool EditStageInDatabase(Stage stage, uint id)
        {
            var query = $@"UPDATE `trasa` 
                            SET `id_zawody` = @id_zawody, `nazwa` = @nazwa,
                                `zasady` = @zasady 
                            WHERE (`id` = '{id}');";

            return ExecuteUpdateQuery(query, stage.GetParameters());
        }

        public static bool DeleteStageFromDatabase(uint stageID)
        {
            var query = $"DELETE FROM trasa WHERE (`id` = '{stageID}')";
            return ExecuteDeleteQuery(query);
        }

        #endregion CRUD

        #region Auxiliary queries

        public static uint GetNumOfTargetsOnStageFromDB(uint id)
        {
            var query = $@"SELECT count(tarcza.id) AS numOfTargets FROM trasa
                            INNER JOIN tarcza ON trasa.id = tarcza.trasa_id
                            WHERE trasa.id = {id};";

            DataTable resultOfQuery = ExecuteSelectQuery(query);

            // when result contains only one row 
            // return first value, otherwise 0
            return resultOfQuery.Rows.Count == 1 
                    ? uint.Parse((resultOfQuery.Rows[0]["numOfTargets"].ToString())) 
                    : 0;
        }

        public static double GetAverageTimeOnStageByIdFromDB(uint id)
        {
            var query = $@"SELECT avg(przebieg.czas) AS averageTime FROM przebieg
                            INNER JOIN trasa ON trasa.id = przebieg.id_trasa
                            WHERE trasa.id = {id};";

            DataTable resultOfQuery = ExecuteSelectQuery(query);

            // when result contains only one row 
            // return first value, otherwise 0
            return resultOfQuery.Rows.Count == 1 
                    ? double.Parse((resultOfQuery.Rows[0]["averageTime"].ToString()))
                    : 0.0;
        }

        #endregion Auxiliary queries
    }
}
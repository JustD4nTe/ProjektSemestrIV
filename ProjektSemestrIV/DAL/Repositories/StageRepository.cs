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

        public static Stage GetStageByIdFromDB(uint id)
        {
            var query = $"SELECT * FROM trasa WHERE trasa.id = {id}";

            return ExecuteSelectQuery<Stage>(query).FirstOrDefault();
        }

        public static bool AddStageToDatabase(Stage stage)
        {
            var query = @"INSERT INTO trasa (`id_zawody`, `nazwa`, `zasady`) 
                            VALUES (@id_zawody, @nazwa, @zasady)";

            return ExecuteModifyQuery(query, stage.GetParameters());
        }

        public static bool EditStageInDatabase(Stage stage, uint id)
        {
            var query = $@"UPDATE `trasa` 
                            SET `id_zawody` = @id_zawody, `nazwa` = @nazwa,
                                `zasady` = @zasady 
                            WHERE (`id` = '{id}');";

            return ExecuteModifyQuery(query, stage.GetParameters());
        }

        public static bool DeleteStageFromDatabase(uint stageID)
        {
            var query = $"DELETE FROM trasa WHERE (`id` = '{stageID}')";

            return ExecuteModifyQuery(query);
        }

        #endregion CRUD

        #region Auxiliary queries

        public static uint GetNumOfTargetsOnStageFromDB(uint id)
        {
            var query = $@"SELECT count(tarcza.id) AS numOfTargets FROM trasa
                            INNER JOIN tarcza ON trasa.id = tarcza.trasa_id
                            WHERE trasa.id = {id};";
            
            return ExecuteSelectQuery<uint>(query).FirstOrDefault();
        }

        public static double GetAverageTimeOnStageByIdFromDB(uint id)
        {
            var query = $@"SELECT avg(przebieg.czas) AS averageTime FROM przebieg
                            INNER JOIN trasa ON trasa.id = przebieg.id_trasa
                            WHERE trasa.id = {id};";

            return ExecuteSelectQuery<uint>(query).FirstOrDefault();
        }

        public static IEnumerable<Stage> GetCompetitionStages( uint competition_id ) {
            var query = $"SELECT * FROM trasa WHERE trasa.id_zawody = {competition_id}";

            return ExecuteSelectQuery<Stage>(query);
        }

        #endregion Auxiliary queries
    }
}
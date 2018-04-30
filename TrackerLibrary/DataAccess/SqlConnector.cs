using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using TrackerLibrary.Models;
using Common.Database;
using Common.Database.Enums;
using Common.Utilities.Enums;


namespace TrackerLibrary.DataAccess
{
    public class SqlConnector : IDataConnection
    {
        //TODO Make the createPrize actually save to the database
        /// <summary>
        /// Saves a new prize to the database
        /// </summary>
        /// <param name="model"> the Prize information.</param>
        /// <returns> The prize information, incluting the unique Id</returns>
        public PrizeModel CreatePrize(PrizeModel model)
        {
            var p = new List<IDataParameter>();
            using (var connection = new Sql(ESqlDataBase.GlobalMarketOperation, NewEEnvironment.Test))
            {
                //var p = new DynamicParameters();
                //p.Add("@PlaceNumber", model.PlaceNumber);
                //p.Add("@Placename", model.PlaceName);
                //p.Add("PlaceAmoun", model.PrizeAmount);
                //p.Add("@prizePercentage", model.PrizePercentage);
                //p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                //connection.Execute("test.spPrizes_Insert", p, commandType: CommandType.StoredProcedure);

                p.Add(new SqlParameter("@PlaceNumber", model.PlaceNumber));
                p.Add(new SqlParameter("@Placename", model.PlaceName));
                p.Add(new SqlParameter("@PlaceAmount", model.PrizeAmount));
                p.Add(new SqlParameter("@prizePercentage", model.PrizePercentage));

                model.Id = connection.ExecuteNonQuery("test.spPrizes_Insert", CommandType.StoredProcedure,p);

                return model;
            }
        }

        public PersonModel CreatePerson(PersonModel model)
        {
            throw new NotImplementedException();
        }

        public void CreateTournament(TournamentModel model)
        {
            throw new NotImplementedException();
        }

        public List<PersonModel> GetPersonAll()
        {
            throw new NotImplementedException();
        }

        public List<TeamModel> GetTeam_All()
        {
            throw new NotImplementedException();
        }

        public List<TournamentModel> GetTournament_All()
        {
            throw new NotImplementedException();
        }

        public TeamModel CreateTeam(TeamModel model)
        {
            throw new NotImplementedException();
        }
    }
}

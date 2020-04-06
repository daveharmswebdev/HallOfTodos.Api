using HallOfTodos.API.Entities;
using HallOfTodos.API.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HallOfTodos.API.Services
{
    public class SuperBeingRepository : ISuperBeingRepository
    {
        private readonly string _cs;

        public SuperBeingRepository(IConfiguration configuration)
        {
            _cs = configuration["connectionStrings:SuperPeopleConnectionString"];
        }
        public SuperBeingPowerCreateUpdateDto CreatePower(int SuperBeingId, SuperBeingPowerCreateUpdateDto createPowerDto)
        {
            int returnValue;
            using (SqlConnection con = new SqlConnection(_cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Personnel.PowerAdd", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("@Name", createPowerDto.Name);
                cmd.Parameters.AddWithValue("@Description", createPowerDto.Description);
                cmd.Parameters.AddWithValue("@PowerType", createPowerDto.PowerType);
                cmd.Parameters.AddWithValue("@SuperBeingId", SuperBeingId);

                cmd.ExecuteNonQuery();
                returnValue = Convert.ToInt32(cmd.Parameters["@RowCount"].Value);
            }
            if (returnValue > 0)
                return createPowerDto;
            else
                throw new Exception("Something went wrong");
        }

        public int DeletePower(int PowerId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SuperBeingPower> GetPowers(int SuperBeingId)
        {
            var powers = new List<SuperBeingPower>();
            using (SqlConnection con = new SqlConnection(_cs))
            {
                SqlCommand cmd = new SqlCommand("Personnel.PowersRead", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@SuperBeingId", SuperBeingId);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var power = new SuperBeingPower()
                    { 
                        Id = Convert.ToInt32(rdr["Id"]),
                        Name = rdr["Name"].ToString(),
                        Description = rdr["Description"].ToString(),
                        PowerType = rdr["PowerType"].ToString(),
                        SuperBeingId = Convert.ToInt32(rdr["SuperBeingId"])
                    };
                    powers.Add(power);
                }
            }
            return powers;
        }
    }
}

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

        public SuperBeing CreateSuperBeing(SuperBeing superBeing)
        {
            int returnId;
            using (SqlConnection con = new SqlConnection(_cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Personnel.SuperBeingCreate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add(new SqlParameter("@Alias", superBeing.Alias)); 
                cmd.Parameters.Add(new SqlParameter("@FirstName", superBeing.FirstName));
                cmd.Parameters.Add(new SqlParameter("@LastName", superBeing.LastName));

                cmd.ExecuteNonQuery();
                returnId = Convert.ToInt32(cmd.Parameters["@Id"].Value);
            }
            if (returnId > 0)
            {
                superBeing.Id = returnId;
                return superBeing;
            }
            else
            {
                throw new Exception("Something went wrong trying to persist the new Super Being");
            }
        }

        public int DeletePower(int PowerId)
        {
            throw new NotImplementedException();
        }

        public SuperBeingPower GetPowerById(int PowerId)
        {
            SuperBeingPower power = new SuperBeingPower();
            using (SqlConnection con = new SqlConnection(_cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Personnel.PowerReadByPowerId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PowerId", PowerId));
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    power.Id = Convert.ToInt32(rdr["Id"]);
                    power.Name = rdr["Name"].ToString();
                    power.Description = rdr["Description"].ToString();
                    power.Id = Convert.ToInt32(rdr["Id"]);
                    power.SuperBeingId = Convert.ToInt32(rdr["SuperBeingId"]);
                }
                return power;
            }
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

        public SuperBeing GetSuperBeingById(int superBeingId)
        {
            var superBeing = new SuperBeing();
            using (SqlConnection con = new SqlConnection(_cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Personnel.SuperBeingGetById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SuperBeingId", superBeingId));
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    superBeing.Id = Convert.ToInt32(rdr["Id"]);
                    superBeing.Alias = rdr["Alias"].ToString();
                    superBeing.FirstName = rdr["FirstName"].ToString();
                    superBeing.LastName = rdr["LastName"].ToString();
                }
                if (superBeing.Id == 0)
                    return null;

                return superBeing;
            }

        }

        public IEnumerable<SuperBeing> GetSuperBeings()
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(_cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Personnel.SuperBeingGetAll", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                return ds.Tables[0].AsEnumerable()
                    .Select(dataRow => new SuperBeing
                    {
                        Id = dataRow.Field<int>("Id"),
                        Alias = dataRow.Field<string>("Alias"),
                        FirstName = dataRow.Field<string>("FirstName"),
                        LastName = dataRow.Field<string>("LastName")
                    }).ToList();
            }
        }

        public bool SuperBeingExists(int superBeingId)
        {
            var superBeing = GetSuperBeingById(superBeingId);
            return superBeing.Id != 0;
        }
    }
}

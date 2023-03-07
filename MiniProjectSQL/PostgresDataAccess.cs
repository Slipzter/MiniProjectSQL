using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Npgsql;

namespace MiniProjectSQL
{
    internal class PostgresDataAccess
    {
        private static List<NpgsqlParameter> parameters;

        public static List<Person> LoadPersons()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["postgres"].ConnectionString;

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                var output = conn.Query<Person>("select * from public.thm_person", new DynamicParameters());
                return output.ToList();

            }
        }
    }
}

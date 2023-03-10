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
    internal static class PostgresDataAccess
    {
        private static List<NpgsqlParameter> parameters;

        // Hämtar alla personer från databasen
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

        // Hämtar alla projekt från databasen

        public static List<Project> LoadProjects()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["postgres"].ConnectionString;

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                var output = conn.Query<Project>("select * from public.thm_project", new DynamicParameters());
                return output.ToList();

            }
        }

        //Hämtar tidtabeller från databasen
        public static List<ProjectsPersons> LoadProjectsPersons()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["postgres"].ConnectionString;

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                var output = conn.Query<ProjectsPersons>("select * from public.thm_project_person", new DynamicParameters());
                return output.ToList();

            }
        }

        // Registrerar en ny person
        public static void RegisterPerson(string name)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["postgres"].ConnectionString;

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText = "INSERT INTO \"public\".\"thm_person\"(\"person_name\") " + ($"VALUES ('{name}');");

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Registrerar ett nytt projekt
        public static void RegisterProject(string name)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["postgres"].ConnectionString;

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText = "INSERT INTO \"public\".\"thm_project\"(\"project_name\") " + ($"VALUES ('{name}');");

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // En person tilldelas ett projekt och antal timmar lagras i databasen
        public static void AssignProject(int personId, int projectId, int hours)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["postgres"].ConnectionString;

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText = "INSERT INTO \"public\".\"thm_project_person\" " +
                                            "(\"project_id\", \"person_id\", \"hours\") " +
                                            ($"VALUES ('{personId}', '{projectId}', '{hours}');");
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Catch.Connection
{
    public class DataBaseConnection
    {
        private NpgsqlConnection _connection;

        public DataBaseConnection(string connectionString)
        {
            _connection = new NpgsqlConnection(connectionString);
        }

        public void Open()
        {
            _connection.Open();
        }

        public void Close()
        {
            _connection.Close();
        }

        public NpgsqlCommand CreateCommand(string query)
        {
            return new NpgsqlCommand(query, _connection);
        }

        public NpgsqlDataReader ExecuteReader(string query)
        {
            var command = CreateCommand(query);
            return command.ExecuteReader();
        }

        public int ExecuteNonQuery(string query)
        {
            var command = CreateCommand(query);
            return command.ExecuteNonQuery();
        }

    }
}

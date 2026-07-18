using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EmployeManagementSoftware
{
    public static class DatabaseHelper
    {
        private const string DB_NAME = "SalaryDB.sqlite";

        // Fixed CS0229: Only keep one ConnectionString definition
        public static string ConnectionString { get; } = $"Data Source={DB_NAME};Version=3;";

        public static void InitializeDatabase()
        {
            // Creates the database file if it doesn't exist yet
            if (!File.Exists(DB_NAME))
            {
                SQLiteConnection.CreateFile(DB_NAME);
            }

            // Fixed IDE0063: Using simplified modern 'using' statement syntax
            using var conn = new SQLiteConnection(ConnectionString);
            conn.Open();

            // Fixed RCS1118: Marked local string as const since it doesn't change
            const string createUsersQuery = @"CREATE TABLE IF NOT EXISTS Users (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                FullName TEXT,
                Email TEXT,
                Username TEXT UNIQUE,
                ContactNumber TEXT)";

            using var cmd = new SQLiteCommand(createUsersQuery, conn);
            cmd.ExecuteNonQuery();

            // (Your code to execute the command goes here...)
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeManagementSoftware
{
    public static class DatabaseHelper
    {

        private static string dbFileName = "StaffroomDB.db";

        
        public static string ConnectionString => $"Data Source={dbFileName};Version=3;";

        public static void InitializeDatabase()
        {
           
            if (!File.Exists(dbFileName))
            {
                SQLiteConnection.CreateFile(dbFileName);
            }

           
            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS Users (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        FullName TEXT,
                        Email TEXT,
                        Username TEXT UNIQUE,
                        ContactNumber TEXT,
                        Password TEXT
                    );";

                using (var cmd = new SQLiteCommand(createTableQuery, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}


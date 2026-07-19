using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

namespace EmployeManagementSoftware
{
    public static class DatabaseHelper
    {
        // Changed to match AddEmployee's database file name
        private const string DB_NAME = "employees.db";

        public static string ConnectionString { get; } = $"Data Source={DB_NAME};Version=3;";

        public static void InitializeDatabase()
        {
            if (!File.Exists(DB_NAME))
            {
                SQLiteConnection.CreateFile(DB_NAME);
            }

            using var conn = new SQLiteConnection(ConnectionString);
            conn.Open();

            // 1. Users Table
            const string createUsersQuery = @"CREATE TABLE IF NOT EXISTS Users (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        FullName TEXT,
        Email TEXT,
        Username TEXT UNIQUE,
        ContactNumber TEXT)";
            using var cmdUsers = new SQLiteCommand(createUsersQuery, conn);
            cmdUsers.ExecuteNonQuery();

            // 2. Employees Table
            const string createEmployeesQuery = @"CREATE TABLE IF NOT EXISTS Employees (
        EmployeeID TEXT PRIMARY KEY,
        FullName TEXT NOT NULL,
        PhoneNumber TEXT,
        Email TEXT,
        Position TEXT,
        Gender TEXT,
        Photo BLOB,
        BasicSalary REAL DEFAULT 0,
        Allowances REAL DEFAULT 0,
        Deductions REAL DEFAULT 0)";
            using var cmdEmployees = new SQLiteCommand(createEmployeesQuery, conn);
            cmdEmployees.ExecuteNonQuery();

            // 3. New SalaryRecords Table (For historical payroll tracking)
            const string createSalaryRecordsQuery = @"CREATE TABLE IF NOT EXISTS SalaryRecords (
        RecordID INTEGER PRIMARY KEY AUTOINCREMENT,
        EmployeeID TEXT,
        BasicSalary REAL,
        Allowances REAL,
        Deductions REAL,
        NetSalary REAL,
        PaymentDate TEXT)";
            using var cmdSalary = new SQLiteCommand(createSalaryRecordsQuery, conn);
            cmdSalary.ExecuteNonQuery();
        }

        public static DataTable GetDashboardMetrics()
        {
            DataTable dt = new DataTable();

            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();

                // Reads the combined payroll values directly from the unified Employees table
                string query = @"SELECT 
                                    COUNT(EmployeeID) as TotalEmployees, 
                                    COALESCE(SUM(BasicSalary + Allowances), 0) as TotalGross, 
                                    COALESCE(SUM(Deductions), 0) as TotalDeductions, 
                                    COALESCE(SUM(BasicSalary + Allowances - Deductions), 0) as TotalNet 
                                 FROM Employees";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }

            return dt;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace CaloriesManagement
{

    public class Database : IDisposable
    {
        public const string DBPath = "../DataBase.db";
        private SQLiteConnection _connection;
        private readonly string _dbPath;
        private bool _disposed = false;

        public Database(string dbPath)
        {
            _dbPath = dbPath;
            InitializeDatabase();
            CreateTables();
            DefaultUser();
        }

        private void InitializeDatabase()
        {
            if (!File.Exists(_dbPath))
            {
                SQLiteConnection.CreateFile(_dbPath);
            }
            _connection = new SQLiteConnection($"Data Source={_dbPath};Version=3;");
            OpenConnection();
        }

        public void OpenConnection()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
        }

        public void CloseConnection()
        {
            if (_connection.State != ConnectionState.Closed)
            {
                _connection.Close();
            }
        }

        public void ExecuteNonQuery(string query, Dictionary<string, object> parameters = null)
        {
            using (SQLiteCommand command = new SQLiteCommand(query, _connection))
            {
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        command.Parameters.AddWithValue(param.Key, param.Value);
                    }
                }
                command.ExecuteNonQuery();
            }
        }

        public DataTable ExecuteQuery(string query, Dictionary<string, object> parameters = null)
        {
            using (SQLiteCommand command = new SQLiteCommand(query, _connection))
            {
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        command.Parameters.AddWithValue(param.Key, param.Value);
                    }
                }
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    return dataTable;
                }
            }
        }

        public void CreateTables()
        {
            string createUsersTable = @"
            CREATE TABLE IF NOT EXISTS Users (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                Age INTEGER,
                Height REAL,
                Weight REAL,
                Sex INTEGER
            )";
            string createIngredientsTable = @"
            CREATE TABLE IF NOT EXISTS Ingredients (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                CaloriesPer100g REAL NOT NULL,
                PhotoUrl TEXT
            )";
            string createDishesTable = @"
            CREATE TABLE IF NOT EXISTS Dishes (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL
            )";
            string createDishIngredientsTable = @"
            CREATE TABLE IF NOT EXISTS DishIngredients (
                DishId INTEGER,
                IngredientId INTEGER,
                Quantity REAL,
                FOREIGN KEY(DishId) REFERENCES Dishes(Id),
                FOREIGN KEY(IngredientId) REFERENCES Ingredients(Id)
            )";
            ExecuteNonQuery(createUsersTable);
            ExecuteNonQuery(createIngredientsTable);
            ExecuteNonQuery(createDishesTable);
            ExecuteNonQuery(createDishIngredientsTable);
        }

        public void DefaultUser()
        {
            string query = "SELECT COUNT(*) FROM Users";
            int userCount = Convert.ToInt32(ExecuteQuery(query).Rows[0][0]);
            if (userCount == 0)
            {
                string insertQuery = "INSERT INTO Users (Name, Age, Height, Weight, Sex) VALUES (@Name, @Age, @Height, @Weight, @Sex)";
                var parameters = new Dictionary<string, object>
                {
                    { "@Name", "Default User" },
                    { "@Age", 30 },
                    { "@Height", 170.0 },
                    { "@Weight", 70.0 },
                    { "@Sex", 1 }  
                };
                ExecuteNonQuery(insertQuery, parameters);
            }
        }

        public User GetUser(int userId)
        {
            string query = "SELECT * FROM Users WHERE Id = @Id";
            var parameters = new Dictionary<string, object> { { "@Id", userId } };

            DataTable result = ExecuteQuery(query, parameters);

            if (result.Rows.Count > 0)
            {
                DataRow row = result.Rows[0];
                string name = row["Name"].ToString();
                int age = Convert.ToInt32(row["Age"]);
                double weight = Convert.ToDouble(row["Weight"]);
                double height = Convert.ToDouble(row["Height"]);
                int gender = Convert.ToInt32(row["Sex"]);

                return new User(name, age, weight, height, gender);
            }

            return null;
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    CloseConnection();
                    _connection?.Dispose();
                }
                _disposed = true;
            }
        }
    }
}
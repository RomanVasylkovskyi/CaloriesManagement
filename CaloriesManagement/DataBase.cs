using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows;

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
            DefaultIngredients();
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

                return new User(userId,name, age, weight, height, gender);
            }

            return null;
        }


        public void SaveUser(User user)
        {
            string query;
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@Name", user.Name },
                { "@Age", user.Age },
                { "@Weight", user.Weight },
                { "@Height", user.Height },
                { "@Sex", user.Gender },
                { "@Id", user.Id }
            };
            try
            {
                query = @"UPDATE Users SET Name = @Name, Age = @Age, Weight = @Weight, Height = @Height, Sex = @Sex WHERE Id = @Id";
                ExecuteNonQuery(query, parameters);
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.ToString());
            }
           
        }
        /// <summary>
        /// Ingredients
        /// </summary>
        public void DefaultIngredients()
        {
            AddOrUpdateIngredient(new Ingredient("Tomato", 150));
            AddOrUpdateIngredient(new Ingredient("apple", 75));
            AddOrUpdateIngredient(new Ingredient("banana", 100));
            AddOrUpdateIngredient(new Ingredient("blueberry", 50));
        }
        public void AddIngredient(Ingredient ingredient)
        {
            string insertQuery = "INSERT INTO Ingredients (Name, CaloriesPer100g) VALUES (@Name, @CaloriesPer100g)";
            var parameters = new Dictionary<string, object>
            {
                { "@Name", ingredient.Name },
                { "@CaloriesPer100g", ingredient.CaloriesPer100g }
            };
            ExecuteNonQuery(insertQuery, parameters);
        }

        public void AddOrUpdateIngredient(Ingredient ingredient)
        {
            string checkQuery = "SELECT Id FROM Ingredients WHERE Name = @Name";
            var checkParameters = new Dictionary<string, object> { { "@Name", ingredient.Name } };

            DataTable result = ExecuteQuery(checkQuery, checkParameters);

            if (result.Rows.Count > 0)
            {
                int id = Convert.ToInt32(result.Rows[0]["Id"]);
                string updateQuery = "UPDATE Ingredients SET CaloriesPer100g = @CaloriesPer100g WHERE Id = @Id";
                var updateParameters = new Dictionary<string, object>
        {
            { "@CaloriesPer100g", ingredient.CaloriesPer100g },
            { "@Id", id }
        };
                ExecuteNonQuery(updateQuery, updateParameters);
            }
            else
            {
               AddIngredient(ingredient);
            }
        }


        public List<Ingredient> GetAllIngredients()
        {
            List<Ingredient> ingredients = new List<Ingredient>();
            string query = "SELECT * FROM Ingredients";

            DataTable result = ExecuteQuery(query);

            foreach (DataRow row in result.Rows)
            {
                int id = Convert.ToInt32(row["Id"]);
                string name = row["Name"].ToString();
                int caloriesPer100g = Convert.ToInt32(row["CaloriesPer100g"]);
                Ingredient tmp = new Ingredient(name, caloriesPer100g);
                tmp.Id = id;
                ingredients.Add(tmp);
            }
            return ingredients;
        }

        public Ingredient GetIngredientById(int id)
        {
            string query = "SELECT * FROM Ingredients WHERE Id = @Id";
            var parameters = new Dictionary<string, object> { { "@Id", id } };

            DataTable result = ExecuteQuery(query, parameters);

            if (result.Rows.Count > 0)
            {
                DataRow row = result.Rows[0];
                string name = row["Name"].ToString();
                int caloriesPer100g = Convert.ToInt32(row["CaloriesPer100g"]);
                Ingredient tmp = new Ingredient(name, caloriesPer100g);
                tmp.Id = id;
                return tmp;
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
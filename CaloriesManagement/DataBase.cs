using System;
using System.IO;
using Microsoft.Data.Sqlite;

namespace CaloriesManagement
{
    class DataBase
    {
        private string connectionString = "Data Source=calories_management.db;";

        public void InitializeDatabase()
        {
            bool dbExists = File.Exists("DataBase.db");

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                if (!dbExists)
                {
                    using (SqliteCommand command = connection.CreateCommand())
                    {
                        // Create Users table
                        command.CommandText = @"
                            CREATE TABLE IF NOT EXISTS Users (
                                UserId INTEGER PRIMARY KEY AUTOINCREMENT,
                                Username TEXT NOT NULL,
                                Age INTEGER NOT NULL,
                                Height REAL NOT NULL,
                                Weight REAL NOT NULL
                            )";
                        command.ExecuteNonQuery();

                        // Create Meals table
                        command.CommandText = @"
                            CREATE TABLE IF NOT EXISTS Meals (
                                MealId INTEGER PRIMARY KEY AUTOINCREMENT,
                                Name TEXT NOT NULL,
                                Description TEXT,
                                UserId INTEGER,
                                FOREIGN KEY (UserId) REFERENCES Users(UserId)
                            )";
                        command.ExecuteNonQuery();

                        // Create Ingredients table
                        command.CommandText = @"
                            CREATE TABLE IF NOT EXISTS Ingredients (
                                IngredientId INTEGER PRIMARY KEY AUTOINCREMENT,
                                Name TEXT NOT NULL,
                                Quantity REAL,
                                Unit TEXT,
                                MealId INTEGER,
                                FOREIGN KEY (MealId) REFERENCES Meals(MealId)
                            )";
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        // You can add more methods here for database operations
        // For example:
        // public void AddUser(string username, string email) { ... }
        // public void AddMeal(string name, string description, int userId) { ... }
        // public void AddIngredient(string name, double quantity, string unit, int mealId) { ... }
    }
}

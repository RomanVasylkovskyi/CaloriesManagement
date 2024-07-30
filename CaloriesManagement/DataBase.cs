using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows;
using System.Xml.Linq;

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
            //DefaultIngredients();
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
                Name TEXT NOT NULL,
                Calories INTEGER,
                Description TEXT
            )";
            ExecuteNonQuery(createUsersTable);
            ExecuteNonQuery(createIngredientsTable);
            ExecuteNonQuery(createDishesTable);
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
            AddOrUpdateIngredient(new Ingredient(1,"Tomato", 150));
            AddOrUpdateIngredient(new Ingredient(2,"apple", 75));
            AddOrUpdateIngredient(new Ingredient(3,"banana", 100));
            AddOrUpdateIngredient(new Ingredient(4,"blueberry", 50));
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

        public void AddOrUpdateIngredient(Ingredient ingredient, Ingredient newIngredient = null)
        {
            if (newIngredient != null)
            {
                string updateQuery = "UPDATE Ingredients SET Name = @Name, CaloriesPer100g = @CaloriesPer100g WHERE Id = @Id";
                var updateParameters = new Dictionary<string, object>
        {
            { "@Id", ingredient.Id },
            { "@Name", newIngredient.Name },
            { "@CaloriesPer100g", newIngredient.CaloriesPer100g }
        };
                ExecuteNonQuery(updateQuery, updateParameters);

                string checkQuery = "SELECT Id FROM Ingredients WHERE Id = @Id";
                var checkParameters = new Dictionary<string, object> { { "@Id", ingredient.Id } };
                DataTable result = ExecuteQuery(checkQuery, checkParameters);

                if (result.Rows.Count == 0)
                {
                    AddIngredient(newIngredient);
                }
            }
            else
            {
                string checkQuery = "SELECT Id FROM Ingredients WHERE Id = @Id";
                var checkParameters = new Dictionary<string, object> { { "@Id", ingredient.Id } };
                DataTable result = ExecuteQuery(checkQuery, checkParameters);

                if (result.Rows.Count == 0)
                {
                    AddIngredient(ingredient);
                }
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
                Ingredient tmp = new Ingredient(id,name, caloriesPer100g);
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
                Ingredient tmp = new Ingredient(id,name, caloriesPer100g);
                return tmp;
            }

            return null;
        }


        public int GetNewIngredientId()
        {
            string query = "SELECT MAX(Id) FROM Ingredients";
            DataTable result = ExecuteQuery(query);

            if (result.Rows.Count > 0 && result.Rows[0][0] != DBNull.Value)
            {
                int maxId = Convert.ToInt32(result.Rows[0][0]);
                return maxId + 1;
            }
            else
            {
                return 1;
            }
        }
        public void DeleteIngredientById(int id)
        {
            string query = "DELETE FROM Ingredients WHERE Id = @Id";
            var parameters = new Dictionary<string, object> { { "@Id", id } };
            try
            {
                ExecuteNonQuery(query, parameters);
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        /// <summary>
        /// Dish
        /// </summary>
        public void AddDish(Dish dish)
        {
            string query = "INSERT INTO Dishes (Name, Calories, Description) VALUES (@Name, @Calories, @Description)";
            var parameters = new Dictionary<string, object>
    {
        { "@Name", dish.Name },
        { "@Calories", dish.Calories },
        { "@Description", dish.Description }
    };
            ExecuteNonQuery(query, parameters);
        }

        public void AddIngredientToDish(int dishId, int ingredientId, double quantity)
        {
            string query = "INSERT INTO DishIngredients (DishId, IngredientId, Quantity) VALUES (@DishId, @IngredientId, @Quantity)";
            var parameters = new Dictionary<string, object>
    {
        { "@DishId", dishId },
        { "@IngredientId", ingredientId },
        { "@Quantity", quantity }
    };
            ExecuteNonQuery(query, parameters);
        }

        public List<Dish> GetAllDishes()
        {
            List<Dish> dishes = new List<Dish>();
            string query = "SELECT * FROM Dishes";
            DataTable result = ExecuteQuery(query);
            foreach (DataRow row in result.Rows)
            {
                int id = Convert.ToInt32(row["Id"]);
                string name = row["Name"].ToString();
                int calories = Convert.ToInt32(row["Calories"]);
                string description = row["Description"].ToString();
                Dish dish = new Dish(id, name, calories, description);
                dishes.Add(dish);
            }
            return dishes;
        }

        public Dish GetDishById(int id)
        {
            string query = "SELECT * FROM Dishes WHERE Id = @Id";
            var parameters = new Dictionary<string, object> { { "@Id", id } };
            DataTable result = ExecuteQuery(query, parameters);
            if (result.Rows.Count > 0)
            {
                DataRow row = result.Rows[0];
                string name = row["Name"].ToString();
                int calories = Convert.ToInt32(row["Calories"]);
                string description = row["Description"].ToString();
                return new Dish(id, name, calories, description);
            }
            return null;
        }

        public void UpdateDish(Dish dish)
        {
            string updateDishQuery = "UPDATE Dishes SET Name = @Name, Calories = @Calories, Description = @Description WHERE Id = @Id";
            var dishParameters = new Dictionary<string, object>
    {
        { "@Id", dish.Id },
        { "@Name", dish.Name },
        { "@Calories", dish.Calories },
        { "@Description", dish.Description }
    };
            ExecuteNonQuery(updateDishQuery, dishParameters);
        }

        public void DeleteDish(int id)
        {
            // Спочатку видаляємо зв'язки з інгредієнтами
            string deleteIngredientsQuery = "DELETE FROM DishIngredients WHERE DishId = @Id";
            var deleteIngParameters = new Dictionary<string, object> { { "@Id", id } };
            ExecuteNonQuery(deleteIngredientsQuery, deleteIngParameters);

            // Потім видаляємо саму страву
            string deleteDishQuery = "DELETE FROM Dishes WHERE Id = @Id";
            var deleteDishParameters = new Dictionary<string, object> { { "@Id", id } };
            ExecuteNonQuery(deleteDishQuery, deleteDishParameters);
        }

        // Додатковий метод для отримання інгредієнтів страви
        public List<Ingredient> GetDishIngredients(int dishId)
        {
            List<Ingredient> ingredients = new List<Ingredient>();
            string query = @"
        SELECT i.Id, i.Name, i.CaloriesPer100g, di.Quantity 
        FROM Ingredients i
        JOIN DishIngredients di ON i.Id = di.IngredientId
        WHERE di.DishId = @DishId";
            var parameters = new Dictionary<string, object> { { "@DishId", dishId } };
            DataTable result = ExecuteQuery(query, parameters);
            foreach (DataRow row in result.Rows)
            {
                int id = Convert.ToInt32(row["Id"]);
                string name = row["Name"].ToString();
                int caloriesPer100g = Convert.ToInt32(row["CaloriesPer100g"]);
                double quantity = Convert.ToDouble(row["Quantity"]);
                Ingredient ingredient = new Ingredient(id, name, caloriesPer100g);
                // Тут ви можете додати властивість Quantity до класу Ingredient, якщо потрібно
                ingredients.Add(ingredient);
            }
            return ingredients;
        }

        public int GetNewDishId()
        {
            string query = "SELECT MAX(Id) FROM Dishes";
            DataTable result = ExecuteQuery(query);

            if (result.Rows.Count > 0 && result.Rows[0][0] != DBNull.Value)
            {
                int maxId = Convert.ToInt32(result.Rows[0][0]);
                return maxId + 1;
            }
            else
            {
                return 1;
            }
        }




        ///////////////////////////////
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
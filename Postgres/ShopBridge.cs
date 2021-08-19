using System;
using Npgsql;


namespace Postgres
{
    class ShopBridge
    {
        private static string Host = "localhost";
        private static string User = "postgres";
        private static string DBname = "postgres";
        private static string Password = "9213";
        private static string Port = "5432";

        static void Main(string[] args)
        {
            int i = 1;
            while(i > 0)
            {
                Console.WriteLine("type:\n 1 - Add \n 2 - Modify \n 3- Delete \n 4 - List \n 0 - Quit");
                i = Convert.ToInt32(Console.ReadLine());
                if(i == 1)
                {
                    Console.WriteLine("Name of Item:");
                    string n = Console.ReadLine();
                    Console.WriteLine("Description:");
                    string d = Console.ReadLine();
                    Console.WriteLine("Price:");
                    decimal p = Convert.ToDecimal(Console.ReadLine());
                    Add(n, d, p);
                }
                else if(i == 2)
                {
                    Console.WriteLine("Name of Item:");
                    string n = Console.ReadLine();
                    Console.WriteLine(" 1 - change description \n 2 - change price");
                    int j = Convert.ToInt32(Console.ReadLine());
                    if(j == 1)
                    {
                        Console.WriteLine("New Description:");
                        string d = Console.ReadLine();
                        ModifyDescription(n, d);
                    }
                    else if(j == 2)
                    {
                        Console.WriteLine("New Price:");
                        decimal p = Convert.ToDecimal(Console.ReadLine());
                        ModifyPrice(n, p);
                    }
                }
                else if (i == 3)
                {
                    Console.WriteLine("Name of Item to delete:");
                    string n = Console.ReadLine();
                    Delete(n);
                }
                else if (i == 4)
                {
                    List();
                }
                else if  (i > 0)
                {
                    Console.WriteLine("Type in a different number");
                }
            }
        }

        public static void Add(string name, string description, decimal price)
        {
            string connString =
                String.Format(
                    "Server={0};Username={1};Database={2};Port={3};Password={4};SSLMode=Prefer",
                    Host,
                    User,
                    DBname,
                    Port,
                    Password);


            using (var conn = new NpgsqlConnection(connString))

            {
                conn.Open();

                using (var command = new NpgsqlCommand("INSERT INTO shopbrigde(name, description, Price) VALUES (@b, @c, @d)", conn))
                {
                    command.Parameters.AddWithValue("b", name);
                    command.Parameters.AddWithValue("c", description);
                    command.Parameters.AddWithValue("d", price);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static void ModifyDescription(string name, string description)
        {
            string connString =
                String.Format(
                    "Server={0}; User Id={1}; Database={2}; Port={3}; Password={4};SSLMode=Prefer",
                    Host,
                    User,
                    DBname,
                    Port,
                    Password);

            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (var command = new NpgsqlCommand("UPDATE shopbrigde SET description = @q WHERE name = @n", conn))
                {
                    command.Parameters.AddWithValue("n", name);
                    command.Parameters.AddWithValue("q", description);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void ModifyPrice(string name, decimal price)
        {
            string connString =
                String.Format(
                    "Server={0}; User Id={1}; Database={2}; Port={3}; Password={4};SSLMode=Prefer",
                    Host,
                    User,
                    DBname,
                    Port,
                    Password);

            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (var command = new NpgsqlCommand("UPDATE shopbrigde SET price = @q WHERE name = @n", conn))
                {
                    command.Parameters.AddWithValue("n", name);
                    command.Parameters.AddWithValue("q", price);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void Delete(string name)
        {
            string connString =
                String.Format(
                    "Server={0}; User Id={1}; Database={2}; Port={3}; Password={4};SSLMode=Prefer",
                    Host,
                    User,
                    DBname,
                    Port,
                    Password);

            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (var command = new NpgsqlCommand("DELETE FROM shopbrigde WHERE name = @n", conn))
                {
                    command.Parameters.AddWithValue("n", name);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static void List()
        {
            string connString =
                String.Format(
                    "Server={0};Username={1};Database={2};Port={3};Password={4};SSLMode=Prefer",
                    Host,
                    User,
                    DBname,
                    Port,
                    Password);


            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();


                using (var command = new NpgsqlCommand("SELECT * FROM shopbrigde", conn))
                {

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine(
                            string.Format(
                                "{0}, {1}, {2}",
                                reader.GetString(0),
                                reader.GetString(1),
                                reader.GetDecimal(2).ToString()
                                )
                            );
                    }
                    reader.Close();
                }
            }
        }
    }
}

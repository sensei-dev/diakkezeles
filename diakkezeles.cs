using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;

namespace Diákkezelő_rendszer
{
    public class Program
    {
        static string connectionString = "Server=localhost;Database=StudentDB;UserID = root; Password = ; ";
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("1. Diák hozzáadása");
                Console.WriteLine("2. Diákok listázása");
                Console.WriteLine("3. Diák adatainak módosítása.");
                Console.WriteLine("4. Diák törlése");
                Console.WriteLine("5. Kilépés");
                Console.WriteLine("Válassz egy lehetőséget: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddStudent();
                        break;
                    case "2":
                        ListStudents();
                        break;
                    case "3":
                        //UpdateStudents();
                        break;

                    case "4":
                        //DeleteStudents();
                        break;

                    case "5":
                        return;
                    default:
                        Console.WriteLine("Érvénytelen választás, próbáld újra. ");
                        break;

                }

            }

        }
        static void AddStudent()
        {
            Console.Write("Név: ");
            var name = Console.ReadLine();
            Console.Write("Kor: ");
            var age = int.Parse(Console.ReadLine());
            Console.Write("Évfolyam: ");
            var grade = Console.ReadLine();
            Console.Write("Email: ");
            var email = Console.ReadLine();
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var query = "INSERT INTO Students (Name, Age, Grade, Email) Values(@name, @age, @grade, @email)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@age", age);
                    command.Parameters.AddWithValue("@grade", grade);
                    command.Parameters.AddWithValue("@email", email);
                    command.ExecuteNonQuery();

                }

            }
            Console.WriteLine("Diákok hozzáadva! ");

        }
        static void ListStudents()
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Students";
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    Console.WriteLine("Diűkok listája: ");
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["StudentID"]}, Név: {reader["Name"]}, Kor: {reader["Age"]}, Osztály: {reader["Grade"]}, Email:{reader["email"]}");
                    }
                }
            }
        }
    }
}

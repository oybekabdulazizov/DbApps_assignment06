using Project01.Helpers;
using Project01.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Project01.Services
{
    public class SqlServerDbService : IDbService
    {
        public Student GetStudent(string index)
        {

            Student student;
            using (var connection = new SqlConnection(DbServer.localConnection))
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = 
                        @"SELECT IndexNumber, FirstName, LastName, BirthDate, IdENrollment FROM Student WHERE IndexNumber=@IdStudent;";
                    command.Parameters.AddWithValue("IdStudent", index);
                    connection.Open();

                    using var dr = command.ExecuteReader();

                    if (!dr.Read()) 
                    {
                        return null;
                    } 
                    else
                    {
                        student = new Student
                        {
                            IndexNumber = dr["IndexNumber"].ToString(),
                            FirstName = dr["FirstName"].ToString(),
                            LastName = dr["LastName"].ToString(),
                            BirthDate = DateTime.Parse(dr["BirthDate"].ToString()),
                            IdEnrollment = int.Parse(dr["IdEnrollment"].ToString())
                        };
                        return student;
                    }
                }
            }
        }

        public List<Student> GetStudents()
        {
            var students = new List<Student>();

            using (var connection = new SqlConnection(DbServer.localConnection))
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"SELECT IndexNumber, FirstName, LastName, BirthDate, IdENrollment FROM Student;";
                    connection.Open();

                    using var dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        var student = new Student
                        {
                            IndexNumber = dr["IndexNumber"].ToString(),
                            FirstName = dr["FirstName"].ToString(),
                            LastName = dr["LastName"].ToString(),
                            BirthDate = DateTime.Parse(dr["BirthDate"].ToString()),
                            IdEnrollment = int.Parse(dr["IdEnrollment"].ToString())
                        };
                        students.Add(student);
                    }

                    return students;
                }
            }
        }
    }
}

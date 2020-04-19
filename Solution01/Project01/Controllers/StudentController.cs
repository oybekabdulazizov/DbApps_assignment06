using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project01.Helpers;
using Project01.Models;

namespace Project01.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetStudents() 
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

                    return Ok(students);
                }
            }
        }
    }
}

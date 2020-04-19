using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project01.Helpers;
using Project01.Models;
using Project01.Services;

namespace Project01.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        private readonly IDbService _idbService;

        public StudentController(IDbService idbService)
        {
            _idbService = idbService;
        }

        [HttpGet]
        public IActionResult GetStudents() 
        {
            var result = _idbService.GetStudents();
            if(result.Count < 1) 
            {
                return BadRequest("There is no student record in your database server!");
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent(string index) 
        {
            var student = _idbService.GetStudent(index);
            if(student == null)
            {
                return NotFound($"Student with {index} id number does not exist!");
            }

            return Ok(student);
        }
    }
}

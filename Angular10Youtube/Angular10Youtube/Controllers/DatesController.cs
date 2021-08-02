using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using Angular10Youtube.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
namespace Angular10Youtube.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatesController : ControllerBase
    {
        static readonly DateTime[] holidays = 
        { 
            new DateTime(DateTime.Now.Year,01,01),
            new DateTime(DateTime.Now.Year,03,15),
            new DateTime(DateTime.Now.Year,05,01),
            new DateTime(DateTime.Now.Year,08,20),
            new DateTime(DateTime.Now.Year,10,23),
            new DateTime(DateTime.Now.Year,11,01),
            new DateTime(DateTime.Now.Year,12,24),
            new DateTime(DateTime.Now.Year,12,25),
            new DateTime(DateTime.Now.Year,12,26),
            new DateTime(DateTime.Now.Year,12,31)
        };


        //[HttpGet("{date}")]
        //public JsonResult Get(DateTime date)
        //{
        //    if (IsWorkingDay(date))
        //        return new JsonResult("Munkanap");

        //    return new JsonResult("Munkaszüneti nap");
        //}

        [HttpGet]
        public JsonResult Get(DateTime date)
        {
            try
            {
                if (IsWorkingDay(date))
                    return new JsonResult("Munkanap");

                return new JsonResult("Munkaszüneti nap");
            }
            catch (ArgumentOutOfRangeException)
            {
                return new JsonResult("A megadott időpontnak a következő 5 éven belül kell lennie!");
            }
            catch (Exception)
            {
                return new JsonResult("Valami hiba történt!");
            }
        }

        private string WhatDateType(DateTime date)
        {
            return "";
        }

        private static bool IsWorkingDay(DateTime date)
        {
            if (date > new DateTime((DateTime.Now.AddYears(5).Year), 12, 31))
                throw new ArgumentOutOfRangeException("A megadott időpontnak a következő 5 éven belül kell lennie!");

            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                return false;

            return true;
        }
    }
}

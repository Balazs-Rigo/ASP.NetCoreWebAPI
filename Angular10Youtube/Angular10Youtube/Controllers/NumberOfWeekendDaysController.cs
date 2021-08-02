using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using NLog;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Angular10Youtube.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumberOfWeekendDaysController : ControllerBase
    {
        private static ILogger<NumberOfWeekendDaysController> _logger;

        public NumberOfWeekendDaysController(ILogger<NumberOfWeekendDaysController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public JsonResult Get(DateTime date1, DateTime date2)
        {
            _logger.LogInformation("NumberOfWeekendDaysController requested!",date1,date2);

            try
            {
                if (date1 > date2)
                    throw new ArgumentException("A kezdődátum nem lehet nagyobb a végdátumnál!");

                var dayDifference = (int)date2.Subtract(date1).TotalDays;
                int weekendDays =  Enumerable
                    .Range(1, dayDifference)
                    .Select(x => date1.AddDays(x))
                    .Count(x => x.DayOfWeek == DayOfWeek.Saturday || x.DayOfWeek == DayOfWeek.Sunday);

                return new JsonResult(weekendDays);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex.Message + " " + ex.StackTrace);
                return new JsonResult(ex.StackTrace);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " " + ex.StackTrace);
                return new JsonResult("Valami hiba történt! " + ex.StackTrace);
            }
        }
    }
}

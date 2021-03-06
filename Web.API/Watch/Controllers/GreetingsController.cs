﻿using Microsoft.AspNetCore.Mvc;

namespace Watch.Controllers
{
    [Route("api/[controller]")]
    public class GreetingsController : Controller
    {
        public IActionResult Get()
        {
            return new OkObjectResult( new { greeting = "Greetings from Web API!" });
        }
    }
}

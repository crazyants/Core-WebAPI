﻿using ActionResults.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ActionResults.Controllers
{
    [Route("api/[controller]")]
    public class DroidsController : Controller
    {
        readonly IDroidRepository droidRepo;
        public DroidsController(IDroidRepository repository)
        {
            droidRepo = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var droids = droidRepo.GetAll();
            return new OkObjectResult(droids);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var droid = droidRepo.Get(id);

            if (droid == null)
            {
                return new NotFoundObjectResult(
                    new Error
                    {
                        Code = 404,
                        Description = $"Droid with id: {id} - Not found in database!"
                    }
                );
            }

            return new OkObjectResult(droid);
        }

        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            var droid = DroidRepo.Get(name);
            if (droid == null)
            {
                return new NotFoundObjectResult(
                    new Error
                    {
                        Code = 404,
                        Description = $"{name} - No such Droid in database!"
                    }
                );
            }
            return new OkObjectResult(DroidRepo.Get(name));
        }

        [HttpPost]
        public IActionResult Post([FromBody] Droid droid)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(new Error
                {
                    Code = 400,
                    Description = "Invalid payload"
                });
            }

            var result = droidRepo.Put(droid);

            if (!result)
            {
                return new BadRequestObjectResult(new Error
                {
                    Code = 409,
                    Description = "Droid already exists"
                });
            }


            return new CreatedAtRouteResult(new { controller = "Droids", model = droid.Name }, droid);
        }

        [HttpDelete("{name}")]
        public IActionResult Delete(string name)
        {
            var result = DroidRepo.Delete(name);

            if (!result)
            {
                return new BadRequestObjectResult(new Error
                {
                    Code = 404,
                    Description = "No such Droid in database!"
                });
            }

            return new NoContentResult();
        }

        [HttpPut("{name}")]
        public IActionResult Update(string name, [FromBody] Droid droid)
        {
            if (!ModelState.IsValid || name != droid.Name)
            {
                return new BadRequestObjectResult(new Error
                {
                    Code = 400,
                    Description = "Invalid payload"
                });
            }

            var result = DroidRepo.Update(droid);

            if(result == null)
            {
                return new NotFoundObjectResult(new Error
                {
                    Code = 410,
                    Description = "Could not find Droid in database!"
                });
            }

            return new OkObjectResult(droid);
        }
    }


    public class Error
    {
        public int Code { get; set; }
        public string Description { get; set; }
    }
}

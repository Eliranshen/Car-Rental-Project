using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using carBuisnnesLogic;
using Microsoft.AspNetCore.Authorization;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//This controller provide to the admin access to the main tables, that he can :
//unsert, update and delete

namespace rentalCar.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/allCars/admin")]
    [ApiController]
    public class AdminLevelsController : ControllerBase
    {


        // GET: api/<adminLevelsController> 

        // this api show to the manger the list of carType before updating.
        [HttpGet("GetCarByidOfCar/{id}")]
        [Authorize(Roles = "admin")]

        public IActionResult GetCars(int id)
        {
            if (id > 0) { 
            return Ok( new dbMange().GetCarByidOfCar(id));
            }
        else
            {
              return NotFound();
            }
        }


        // POST api/<adminLevelsController>  this api allow to insert a new car type row, to the cartype table, to the manager
        [HttpPost("[action]")]
        [Authorize(Roles = "admin")]
        //public void Post([FromBody] string value)
        public IActionResult PostNewcar([FromBody] CarType newCar)
        {
            if (newCar.IdOfCar > 0)
            {
                return Ok(new dbMange().insertNewCarTypes(newCar.IdOfCar,
                                                          newCar.ManufactureName,
                                                          newCar.CarModel,
                                                          newCar.DailyCost,
                                                          newCar.DailyDelayCost,
                                                          newCar.YearOfProduction,
                                                          newCar.Gear));
            }

            return NotFound();
        }

        [HttpPost("[action]")] // this api allow to insert a new car field table, to the manager
        //public void Post([FromBody] string value)
        [Authorize(Roles = "admin")]
        public IActionResult PostNewcarField([FromBody] CarField newCarField)
        {
            if (newCarField.IdOfCar > 0)
            {
               return Ok(new dbMange().insertNewCarFields(newCarField.IdOfCar,
                                                          newCarField.IdOfField,
                                                          newCarField.CarType,
                                                          newCarField.CurrentKm,
                                                          newCarField.Picture,
                                                          newCarField.ProperToRent,
                                                          newCarField.FreeForRent,
                                                          newCarField.CarNumber,
                                                          newCarField.BranchNumber));
            }
            return NotFound();
        }

        // PUT api/<adminLevelsController>/5 //this api service the worker area for the status of the car available or not
        [HttpPut("AvailableForRent/{carNumber}/{ActualReturnDate}")]
        [Authorize(Roles ="admin,worker")]
        public IActionResult PutAvailableForRent(int carNumber, string ActualReturnDate) //[FromBody] string value)
        {
            if (carNumber > 0)

            {
                return Ok(new dbMange().AvailableForRent(carNumber,ActualReturnDate));
            }
            return NotFound();
        }

        [HttpPut("[action]")]
        [Authorize(Roles = "admin")]
        //this api call function that update the details of the car type
        public IActionResult PutUpdateCarType( [FromBody] CarType ct) 
         
        {
            if (ct.IdOfCar> 0)

            {
                return Ok(new dbMange().updateCarType(ct.IdOfCar, ct.ManufactureName, ct.CarModel, ct.DailyCost, ct.DailyDelayCost, ct.YearOfProduction, ct.Gear));
            }
            return NotFound();
        }


        // DELETE api/<adminLevelsController>/5 //This api receive id of car, and delete the car from the car types table.
        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult deleteCarFromCarType(int id)
        {
            if (id > 0)
            {
                return Ok(new dbMange().deleteCarCarType(id));
            }

            return NotFound();
        }


        //this api receive id of car, and delete the car from the car fields table.
        [HttpDelete("delete/{id}/{idField}")]
        [Authorize(Roles = "admin")]
        public IActionResult deleteCarFields(int id, int idField)  
        {
            if (id > 0 && idField > 0)
            {
                return Ok(new dbMange().deleteCarCarFields(id,idField));
            }

            return NotFound();
        }



    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using carBuisnnesLogic;
using Microsoft.AspNetCore.Authorization;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace rentalCar.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/allCars")]
    [ApiController]
    public class carsController : ControllerBase
    {


        // GET: api/<carsController>
        [HttpGet]
        [Authorize(Roles ="admin,worker,user")]

        public IEnumerable<object> Get() //this api gets all the free for rent cars
       {

            return new dbMange().GetAllCars();
        }

        [HttpGet("getCarsMultiParm/{gear}/{year}/{ManufactureName}/{CarModel}")] //this api using the main filter, to search the car
        [Authorize(Roles ="admin,worker,user")]
        public IActionResult getCarsMultiParms(string gear, string year, string ManufactureName, string CarModel)
        {
            if (gear != null || year.ToString() != null || ManufactureName != null || CarModel != null)
            {
                return Ok(new dbMange().getCarsMultiParm(gear, year, ManufactureName, CarModel));
            }

            return NotFound();
        }
        
        [HttpGet("getCarsByCarNumber/{CarNumber}")] //this api present the car by the car number
        [Authorize(Roles ="admin,worker,user")]
        public IActionResult getCarsByCarNumbers(int CarNumber)
        {
            if (CarNumber.ToString() != null)
            {
                return Ok(new dbMange().getCarsByCarNumber(CarNumber));
            }

            return NotFound();
        }



        [HttpGet("getTotalCostForRent/{StartDay}/{ReturnDate}/{carNumber}/{active}")] // this api calculate the price for rent, shold recive the active number
        [Authorize(Roles = "admin,worker,user")]
        public IActionResult getTotalCostForRent(string StartDay, string ReturnDate, int carNumber, int active)
        {

            if (carNumber > 0 || StartDay != "" || ReturnDate != "")
            {
                return Ok(new dbMange().totalCostForPeriod(StartDay, ReturnDate, carNumber, active));
            }

            return NotFound();
        }

        // GET: api/<carsController>
        [HttpGet("GetAllRentedCars")] //this api connect to the all rentedcar 
        [Authorize(Roles ="admin,worker,user")]

        public IEnumerable<object> GetAllRentedCar()
        {

            return new dbMange().GetAllRentedCars();
        }


        [HttpGet("GetCarsBycarNum/{CarNumber}")] //this api receive the car number of the car, to bring the details of the car
        [Authorize(Roles = "admin,worker")]
        public IActionResult GetCarsBycarNumber(int CarNumber)
        {
            if (CarNumber > 0)
            {

                return Ok(new dbMange().GetCarsBycarNum(CarNumber));
            }

            else {
                return NotFound();
                    }
        }



        // POST api/<carsController> ---insert
        [HttpPost("[action]")]
        [Authorize(Roles = "admin,worker,user")]
        //public void Post([FromBody] string value)
        //{
        //}

        public IActionResult PostOrderCars([FromBody] CarRentalField orderCar1) //this api connect to  insert the details of the order car, to the car rental field column
        {
            if (orderCar1.StartDay >= orderCar1.ReturnDate || orderCar1.CarNumber> 0)
            {
                return Ok( new dbMange().insertOrderCar(orderCar1.StartDay.ToString(), orderCar1.ReturnDate.ToString(), orderCar1.CarNumber, orderCar1.UserId));
            }

            return NotFound();
        }

        // PUT api/<carsController>/5 ---update
        //[HttpPut("{id}")]
        [HttpPut("noAvailableForRent/{CarNumber}")] //this api update the car in the freeForRent Column to no
        public IActionResult PutCarNumber(int CarNumber) //[FromBody] string value)
        {
            
            if (CarNumber > 0)

                {
                return Ok(new dbMange().notAvailableForRent(CarNumber));
            }
            return NotFound();
        }

        // DELETE api/<carsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

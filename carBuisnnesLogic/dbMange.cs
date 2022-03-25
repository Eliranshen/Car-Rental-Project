using System.Collections.Generic;
using System.Linq;
using System;



namespace carBuisnnesLogic
{
    public class dbMange
    {

        car_rentalContext db = new car_rentalContext(); //the db of car rental

        //This function show all the ca that is available to rent
        public IEnumerable<object> GetAllCars()
        {


            var listOfCars1 = (from carFi in db.CarFields
                               join carT in db.CarTypes on carFi.IdOfCar equals carT.IdOfCar
                               where carFi.FreeForRent == "yes"
                               orderby carT.ManufactureName descending, carT.CarModel,carFi.IdOfCar,carFi.IdOfField
                               select new
                               {
                                   carFi.IdOfCar,
                                   carFi.IdOfField,
                                   carT.Gear,
                                   carT.YearOfProduction,
                                   carT.ManufactureName,
                                   carT.CarModel,
                                   carFi.CarNumber,
                                   carFi.CurrentKm,
                                   carFi.CarType,
                                   carFi.Picture
                               }
                      );

            return listOfCars1.ToList();
        }
        //This function show all the car that isn't availabe to rent
        //ActualReturnDate null, mean that the car is still not return
        public IEnumerable<object> GetCarsBycarNum(int carNo)
        {

            var existsCar = (from car in db.CarFields
                             select car.CarNumber).Contains(carNo); //check if the car exist


            var listOfCars1 = (from carFi in db.CarFields
                               join carCf in db.CarRentalFields
                               on carFi.CarNumber equals carCf.CarNumber
                               join carT in db.CarTypes on carFi.IdOfCar equals carT.IdOfCar
                               where (carFi.CarNumber == carNo
                               && carFi.FreeForRent == "no" && carCf.ActualReturnDate == null)
                               orderby carT.ManufactureName descending, carT.CarModel
                               select new
                               {
                                   carT.Gear,
                                   carT.YearOfProduction,
                                   carT.ManufactureName,
                                   carT.CarModel,
                                   carFi.CarNumber,
                                   carFi.CurrentKm,
                                   carFi.FreeForRent,
                                   carFi.CarType,
                                   carT.DailyCost,
                                   carT.DailyDelayCost,
                                   carCf.StartDay,
                                   carCf.ReturnDate,
                                   carFi.Picture,
                               }
                                   );

            return listOfCars1.ToList();
        }

        //This function show all the car that is available to rent
        // in this function ther is implement the main filter that the user can make.
        //filter by gear,year,ManufactureName,CarModel
        public IEnumerable<object> getCarsMultiParm(string gear, string year, string ManufactureName, string CarModel)

        {
            if (gear == "null") { gear = null; }
            if (year.ToString() == "null") { year = null; }
            if (ManufactureName == "null") { ManufactureName = null; }
            if (CarModel == "null") { CarModel = null; }



            var listOfCars1 = (from carFi in db.CarFields
                               join carT in db.CarTypes on carFi.IdOfCar equals carT.IdOfCar
                               where carFi.FreeForRent == "yes" &&
                               (((CarModel != null) ? carT.CarModel == CarModel : true) &&
                               ((ManufactureName != null) ? carT.ManufactureName == ManufactureName : true) &&
                               ((year != null) ? carT.YearOfProduction == Convert.ToInt32(year) : true) &&
                                ((gear != null) ? carT.Gear == gear : true)
                                )
                               orderby carT.ManufactureName descending, carT.CarModel
                               select new
                               {
                                   carFi.IdOfCar,
                                   carFi.IdOfField,
                                   carT.Gear,
                                   carT.YearOfProduction,
                                   carT.ManufactureName,
                                   carT.CarModel,
                                   carFi.CarNumber,
                                   carFi.CurrentKm,
                                   carFi.CarType,
                                   carFi.Picture
                               }
                       );

            return listOfCars1.ToList();
        }


        //This function show the details of the car, search by car number
        public IEnumerable<object> getCarsByCarNumber(int carnum)

        {

            var existsCar = (from car in db.CarFields
                             select car.CarNumber).Contains(carnum); //check if the car exist




            var listOfCars1 = (from carFi in db.CarFields
                               join carT in db.CarTypes on carFi.IdOfCar equals carT.IdOfCar
                               where (carFi.CarNumber == carnum
                                && carFi.FreeForRent == "yes")
                               orderby carT.ManufactureName descending, carT.CarModel
                               select new
                               {
                                   carT.Gear,
                                   carT.YearOfProduction,
                                   carT.ManufactureName,
                                   carT.CarModel,
                                   carFi.CarNumber,
                                   carFi.CurrentKm,
                                   carT.DailyCost,
                                   carT.DailyDelayCost,
                                   carFi.CarType,
                                   carFi.Picture
                               }
                        ).ToList();

            return listOfCars1;
        }

        //this function update the car in the freeForRent Column to no,
        //it's mean the car isn't available for rent 
        public IEnumerable<CarField> notAvailableForRent(int carNumber)
        {

            var existsCar = (from e in db.CarFields
                             select e.CarNumber).Contains(carNumber);
            if (existsCar)
            {

                CarField orderCar = db.CarFields.FirstOrDefault(carNum =>
                carNum.CarNumber == carNumber);

                orderCar.FreeForRent = "no";
                db.SaveChanges();
                return db.CarFields;
            }

            else
            {
                return db.CarFields;
            }
        }
        //this function handling to things:
        //1. update the car to available for rent
        //2. update the actual return date, according to the date. 
        //for updating the acyaul date, call to the updateStatus function.
        public IEnumerable<CarField> AvailableForRent(int carNumber, string acctualReturndate)
        {

            updateStatus(carNumber, acctualReturndate);


            var existsCar = (from e in db.CarFields
                             select e.CarNumber).Contains(carNumber); //check if the car exist

            int status = updateStatus(carNumber, acctualReturndate); //call functioon for update the acctual return filed column

            if (existsCar && status == 1) //status 1 mean that the ActualReturnDate is updated
            {

                var orderCar2 = (from carFi in db.CarFields
                                 join carT in db.CarTypes on carFi.IdOfCar equals carT.IdOfCar
                                 join carCf in db.CarRentalFields
                                 on carFi.CarNumber equals carCf.CarNumber
                                 where (carFi.FreeForRent == "no" && carCf.ActualReturnDate == null
                                 && carCf.CarNumber == carNumber)
                                 orderby carT.ManufactureName descending, carT.CarModel
                                 select new
                                 {
                                     carCf.CarNumber
                                 }
          ) as CarField;

                orderCar2 = db.CarFields.FirstOrDefault(carNum =>
               carNum.CarNumber == carNumber); //bring the car from the CarFields table

                orderCar2.FreeForRent = "yes"; //update free for rent
                db.SaveChanges();
                return db.CarFields;
            }

            else
            {
                return db.CarFields; 
            }
        }

        //this function update the ActualReturnDate field from null to the Actual Return Date
        public int updateStatus(int carNumber1, string acctualDate1)
        {
            DateTime accReturn = DateTime.Parse(acctualDate1);

            var existsCar1 = (from t in db.CarRentalFields
                              select t.CarNumber).Contains(carNumber1); //heck if the car exsit

            var existsCarNullDate = (from t in db.CarRentalFields
                                     select t.ActualReturnDate).Contains(null);

            if (existsCar1 && existsCarNullDate)
            {

                var orderCar1 = db.CarRentalFields.FirstOrDefault(carNum =>
               (carNum.CarNumber == carNumber1 && carNum.ActualReturnDate == null));

                orderCar1.ActualReturnDate = accReturn; //update the field.

                return 1; // can continue in the AvailableForRent function 
            }

            else
            {

                return 2; // go to else in AvailableForRent function
            };
        }


        //this function calculate the price for the period of time
        //depanding date that the client return the car, using the normal price and the penalty
        public int totalCostForPeriod(string start, string end, int carNumberf, int active)
        {

             //this query bring the price for daily cost

            var price = Convert.ToInt32((from carFi in db.CarFields
                                         join carCf in db.CarRentalFields
                                         on carFi.CarNumber equals carCf.CarNumber into outerTable
                                         from carCf in outerTable.DefaultIfEmpty()
                                         join carT in db.CarTypes on carFi.IdOfCar equals carT.IdOfCar
                                         where (carFi.CarNumber == carNumberf)
                                         select carT.DailyCost).FirstOrDefault());

            //this query bring the price for daily delay cost

            var penalty = Convert.ToInt32((from carFi in db.CarFields
                                           join carCf in db.CarRentalFields
                                           on carFi.CarNumber equals carCf.CarNumber into outerTable
                                           from carCf in outerTable.DefaultIfEmpty()
                                           join carT in db.CarTypes on carFi.IdOfCar equals carT.IdOfCar
                                           where (carFi.CarNumber == carNumberf)
                                           select carT.DailyDelayCost).FirstOrDefault());

            var existsCar = (from e in db.CarFields
                             select e.CarNumber).Contains(carNumberf); //check if the car exist

            DateTime start1 = DateTime.Parse(start);
            DateTime end1 = DateTime.Parse(end);

            if (start1 > end1 || !existsCar)
            {
                return 0;  //in case that the details not make sense
            }

            if (active == 1) //return the car in time
            {
                return Convert.ToInt32((end1 - start1).TotalDays) * price;
            }

            if (active == 2) //return the car  not in time
            {
                return Convert.ToInt32((end1 - start1).TotalDays) * penalty;
            }
            return 0;
        }

        //this function insert the details of the order car, to the car rental field column
        public IEnumerable<CarRentalField> insertOrderCar(string start, string end, int carNumber, int userid = 1)
        {
            {
                DateTime start1 = DateTime.Parse(start);

                DateTime end1 = DateTime.Parse(end);

                var existsCar = (from e in db.CarFields
                                 select e.CarNumber).Contains(carNumber); //check if the car exist
                if (existsCar)
                {
                    CarRentalField order1 = new CarRentalField { StartDay = start1, ReturnDate = end1, CarNumber = carNumber, UserId = userid };
                    db.CarRentalFields.Add(order1); //insert the row
                    db.SaveChanges();
                    return db.CarRentalFields;
                }
                return db.CarRentalFields;
            }
        }

        //this function return all the cars that isn't available to rent.
        public IEnumerable<object> GetAllRentedCars()
        {

            var listOfRentedCars = (from carFi in db.CarFields
                                    join carT in db.CarTypes on carFi.IdOfCar equals carT.IdOfCar
                                    join carCf in db.CarRentalFields
                                    on carFi.CarNumber equals carCf.CarNumber
                                    where (carFi.FreeForRent == "no" && carCf.ActualReturnDate == null)
                                    orderby carT.ManufactureName descending, carT.CarModel
                                    select new
                                    {
                                        carT.Gear,
                                        carT.YearOfProduction,
                                        carT.ManufactureName,
                                        carT.CarModel,
                                        carFi.CarNumber,
                                        carFi.CurrentKm,
                                        carFi.FreeForRent,
                                        carFi.CarType,
                                        carT.DailyCost,
                                        carT.DailyDelayCost,
                                        carCf.StartDay,
                                        carCf.ReturnDate,
                                        carFi.Picture,

                                    }
                      );

            return listOfRentedCars.ToList();

        }


        //insert registration punction ' insert new user to user details row

        public IEnumerable<UserDetail> insertNewUserDetails(string fName, string userN, string? bDate1, string gender, string mail, string pass, string pic)
        {
            DateTime bDate2 = DateTime.Parse(bDate1);

            fName = fName.ToLower();
            {
                var maxUserid1 = (from m in db.UserDetails
                                  select m.UserId
                                ); //check the max user id
                int maxUser = maxUserid1.Max();

                {
                    UserDetail newU = new UserDetail
                    {
                        FullName = fName,
                        UserId = maxUser + 1,
                        UserName = userN,
                        Bdate = bDate2,
                        Gender = gender,
                        Email = mail,
                        Password = pass,
                        Picture = pic,
                        Role = "user"
                    };
                    db.UserDetails.Add(newU);
                    db.SaveChanges();
                    return db.UserDetails;
                };
            }
        }


        // **admin area functions***//

        //this funcxtion insert a new car type to the carTypes table

        public IEnumerable<CarType> insertNewCarTypes(int id, string manufcature, string carModel, int? dailyCost, int? dailyDelayCo, int? year, string carGear)
        {

            var existsId = (from e in db.CarTypes
                            select e.IdOfCar).Contains(id); //check if the car exist
            {

                if (!existsId)
                {
                    CarType newCar = new CarType
                    {
                        IdOfCar = id,
                        ManufactureName = manufcature,
                        CarModel = carModel,
                        DailyCost = dailyCost,
                        DailyDelayCost = dailyDelayCo,
                        YearOfProduction = year,
                        Gear = carGear
                    };
                    db.CarTypes.Add(newCar); //insert the row
                    db.SaveChanges();
                    return db.CarTypes;
                }
                return db.CarTypes;
            }

        }

        //insert a new car fields to the CarFields table

        public IEnumerable<CarField> insertNewCarFields(int idCar, int idField, string cType, int? currenKm, string pic, string properTo, string freeForRe, int carN, int? branch)
        {
            freeForRe = freeForRe.ToLower();
            properTo = properTo.ToLower();

            var existsId = (from e in db.CarTypes
                            select e.IdOfCar).Contains(idCar);
            {

                if (existsId)
                {
                    CarField newCar = new CarField
                    {
                        IdOfCar = idCar,
                        IdOfField = idField,
                        CarType = cType,
                        CurrentKm = currenKm,
                        Picture = pic,
                        ProperToRent = properTo,
                        FreeForRent = freeForRe,
                        CarNumber = carN,
                        BranchNumber = branch
                    };
                    db.CarFields.Add(newCar);
                    db.SaveChanges();
                    return db.CarFields;
                }
                return db.CarFields;
            }
        }


        //delete car from carType table 
        public IEnumerable<CarType> deleteCarCarType(int id)
        {
            CarType car = db.CarTypes.FirstOrDefault(no => no.IdOfCar == id);

            if (car != null)
            {
                db.CarTypes.Remove(car); // deleting the row
                db.SaveChanges();
                return db.CarTypes;
            }
            return db.CarTypes;  
        }

        //delete car from CarFields table
        public IEnumerable<CarField> deleteCarCarFields(int id, int idField)
        {
            CarField car = db.CarFields.FirstOrDefault(field => field.IdOfCar == id && field.IdOfField == idField);

            if (car != null)
            {
                db.CarFields.Remove(car); // deleting the row
                db.SaveChanges();
                return db.CarFields;
            }
            return db.CarFields;
        }
        //search the details of the car by the idofcar
        public IEnumerable<object> GetCarByidOfCar(int idOfCar)
        {

            var listOfCarsType = (from carT in db.CarTypes
                                  where carT.IdOfCar == idOfCar
                                  select new
                               {
                                      carT.IdOfCar,
                                      carT.ManufactureName,
                                      carT.CarModel,
                                      carT.DailyCost,
                                      carT.DailyDelayCost,
                                      carT.YearOfProduction,
                                      carT.Gear
                                  }
                      );

            return listOfCarsType.ToList() ;

        }
        //update the details of the car type
        public IEnumerable<object> updateCarType(int idOfCar1,string ManufactureName1, string CarModel1,int? DailyCost1, int? DailyDelayCost1,int? YearOfProduction1, string Gear1)
        {

            CarType listOfCarsType = db.CarTypes.FirstOrDefault(id => id.IdOfCar == idOfCar1);


            listOfCarsType.ManufactureName = ManufactureName1;
            listOfCarsType.CarModel = CarModel1;
            listOfCarsType.DailyCost = DailyCost1;
            listOfCarsType.DailyDelayCost = DailyDelayCost1;
            listOfCarsType.YearOfProduction = YearOfProduction1;
            listOfCarsType.Gear = Gear1;

            db.SaveChanges();

            return db.CarTypes ;
        }

        //this function check the details by user name and password and return the userDetails roe
        public UserDetail chekUserPassLogin(UserDetail userPass)
        {
            UserDetail selectUser = db.UserDetails.FirstOrDefault(u => u.UserName == userPass.UserName && u.Password == userPass.Password) ;
            return selectUser;
        }


    } //close class
} // close namspace

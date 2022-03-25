import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-admin-area',
  templateUrl: './admin-area.component.html',
  styleUrls: ['./admin-area.component.css']
})
export class AdminAreaComponent implements OnInit {

  constructor(private h: HttpClient) { }

  carTypeMessage: string;
  carFiledMessage: string;
  delTypeMessage: string;
  delFieldMessage: string;
  updateCartypeMess: string;

  carTypeList : any[]

  ngOnInit(): void { }

  // this function will connect to the API of insert a new cartype - allowed just for admin users
  postNewCarType(id1: number, manuName1: string, CarModel1: string, dailyCost1: number, dailyDelay1: number, yearOf1: number, gear1: string) {
    let h1=new HttpHeaders();
    h1=h1.append("Authorization","Bearer "+localStorage.getItem("jwt")); //the jwt placed the token in the local storage
    h1.append("Content-Type","application/json");
    if (id1 <= 0) {
      alert("The id isn't valid or you are not aloowed to do this action")
    }

    else {
      this.h.post("http://localhost:29200/api/allCars/admin/PostNewcar",
        { "IdOfCar": id1, "ManufactureName": manuName1, "CarModel": CarModel1, "DailyCost": dailyCost1, "DailyDelayCost": dailyDelay1, "YearOfProduction": yearOf1, "Gear": gear1 },{"headers":h1})
        .subscribe(mess => {
          this.carTypeMessage = "please enter related car for id:  " + id1
        },
          err => {
            alert(" error - please check all the fileds again or you are not aloowed to do this action");
          })
    }

  }
// this function will connect to the API of insert a new id of car - allowed just for admin users
  postNewCarField(IdOfCar1: number, IdOfField1: number, CarType1: string, CurrentKm1: number, Pic1: string, ProperToRent1: string, FreeForRent1: string, CarNumber1:number, BranchNumber:number ) {
    let h1=new HttpHeaders();
    h1=h1.append("Authorization","Bearer "+localStorage.getItem("jwt"));
    h1.append("Content-Type","application/json");
    if (IdOfCar1 <= 0) {
      alert("The id isn't valid")
    }

    else {
      this.h.post("http://localhost:29200/api/allCars/admin/PostNewcarField",
        { "IdOfCar": IdOfCar1, "IdOfField": IdOfField1, "CarType": CarType1, "CurrentKm": CurrentKm1,"Picture":Pic1, "ProperToRent": ProperToRent1, "FreeForRent": FreeForRent1, "CarNumber": CarNumber1,"BranchNumber": BranchNumber },{"headers":h1})
        .subscribe(mess => {
          this.carFiledMessage = "You can check the car in:  order a car page"
        },
          err => {
            alert(" error - please check all the fileds again or you are not aloowed to do this action");
          })
    }

  }
// this function will connect to the API of delete the car from the car type table - allowed just for admin users
  deleteCarFromCarType(IdOfCar: number) {
    let h1=new HttpHeaders();
    h1=h1.append("Authorization","Bearer "+localStorage.getItem("jwt"));
    h1.append("Content-Type","application/json");

    if (IdOfCar <= 0) {
      alert("The id isn't valid or you are not aloowed to do this action")
    }
    else {
      this.h.delete("http://localhost:29200/api/allCars/admin/delete/" + IdOfCar,{"headers":h1})
      .subscribe(mess => {
        this.delTypeMessage = "Car type  ID number: "+ IdOfCar + " deleted from carTypes table," +  
                              "make sure you don't have cars with the same id, in the car rent field table"
      },
        err => {
          alert(" error - please check  the car type number again or you are not aloowed to do this action");
        })
    }
  }
// this function will connect to the API of delete the car from the car field table - allowed just for admin users
  deleteCarFromCarField(IdOfCar: number, idField: number) {
    let h1=new HttpHeaders();
    h1=h1.append("Authorization","Bearer "+localStorage.getItem("jwt"));
    h1.append("Content-Type","application/json");

    if (IdOfCar <= 0 || idField <= 0) {
      alert("The id or the idField isn't valid")
    }
    else {
      this.h.delete("http://localhost:29200/api/allCars/admin/delete/" + IdOfCar + "/" + idField,{"headers":h1})
      .subscribe(mess => {
        this.delFieldMessage = "Car type/Field : " + IdOfCar + "/"+ idField+ " deleted from carFields table," +  
                              "now you can delete the car type from the cartype table"
      },
        err => {
          alert(" error - please check  the car type number again or you are not aloowed to do this action");
        })
    }
  }
// this function will connect to the API to show the details of the car you want to update - allowed just for admin users
  getCarsbyIdofcar(idOf: number) {
    let h1=new HttpHeaders();
    h1=h1.append("Authorization","Bearer "+localStorage.getItem("jwt"));
    h1.append("Content-Type","application/json");
    this.h.get("http://localhost:29200/api/allCars/admin/GetCarByidOfCar/" + idOf,{"headers":h1})
    .subscribe(data => 
      {
         this.carTypeList = data as any;
      },err=> alert("you are not aloowed to do this action")
    )}

    // this function will connect to the API to supdate the details of the car you choose - allowed just for admin users
    UpdarCarType(idofcar1: number,ManufactureName1:string,CarModel1:string,DailyCost1: number,DailyDelayCost1:number,YearOfProduction1:number,Gear1:string){
      let h1=new HttpHeaders();
      h1=h1.append("Authorization","Bearer "+localStorage.getItem("jwt"));
      h1.append("Content-Type","application/json");

      if (idofcar1 == null || idofcar1 <= 0 || idofcar1 == undefined ) {
        alert ("The Id isn't coorect, please check the number in order car page")  
        
      }
      else {
        this.h.put("http://localhost:29200/api/allCars/admin/PutUpdateCarType",
        {"idofcar": idofcar1,"ManufactureName":ManufactureName1,"CarModel":CarModel1,"DailyCost": DailyCost1,"DailyDelayCost":DailyDelayCost1,"YearOfProduction":YearOfProduction1,"Gear":Gear1},{"headers":h1})
        .subscribe(id => {
          this.updateCartypeMess = "updae car type id number: " +idofcar1+ " finish sucsessfully" ;
        },err=> alert("you are not aloowed to do this action") 
        )
      }
    }



}






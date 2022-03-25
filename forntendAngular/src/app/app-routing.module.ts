//import { ModuleWithProviders } from '@angular/compiler/src/core';
import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ModuleWithProviders } from "@angular/core";
import { HeaderComponent } from './header/header.component';
import { HomePageComponent } from './home-page/home-page.component';
import { OrdersComponent } from './orders/orders.component';
import { CalcBeforeRentComponent } from './calc-before-rent/calc-before-rent.component';
import { WorkerAreaComponent } from './worker-area/worker-area.component';
import { AdminAreaComponent } from './admin-area/admin-area.component';


const routes: Routes = [
{path: "", redirectTo: "home-page" , pathMatch:'full'},
{path:"header",component:HeaderComponent},
{path:"home-page",component:HomePageComponent},
{path:"orders",component:OrdersComponent},
{path:"order-the-car/:carnum",component:CalcBeforeRentComponent},
{path: "worker-Area",component:WorkerAreaComponent},
{path: "admin-Area",component:AdminAreaComponent},
{path:"**",component:HomePageComponent}
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

export const routeing: ModuleWithProviders<any> = RouterModule.forRoot(routes);
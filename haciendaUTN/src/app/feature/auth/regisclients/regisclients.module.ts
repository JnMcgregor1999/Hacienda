import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { RegisclientsComponent } from './regisclients.component';


const routes: Routes = [{ path: '', component: RegisclientsComponent }]

@NgModule({
  declarations: [RegisclientsComponent],
  imports: [
    RouterModule.forChild(routes),
    CommonModule
  ]
})
export class RegisclientsModule { }





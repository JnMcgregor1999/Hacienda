import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { NotpagefoundComponent } from './notpagefound.component';


const routes: Routes = [{ path: '', component: NotpagefoundComponent }]
@NgModule({
  declarations: [NotpagefoundComponent],
  imports: [
    RouterModule.forChild(routes),
    CommonModule
  ]
})
export class NotpagefoundModule { }

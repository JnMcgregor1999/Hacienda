import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ModalErrorComponent } from './modal-error.component';
import { RouterModule, Routes } from '@angular/router';


const routes: Routes = [{ path: '', component: ModalErrorComponent }]
@NgModule({
  declarations: [ ModalErrorComponent],
  imports: [
    RouterModule.forChild(routes),
    CommonModule
  ]
})
export class ModalErrorModule { }

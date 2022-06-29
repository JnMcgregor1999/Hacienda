import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { NgxLoadingModule } from 'ngx-loading';
import { LoadingComponent } from './loading.component';



@NgModule({
  declarations: [LoadingComponent],
  exports: [
    RouterModule,
    LoadingComponent
  ],
  imports: [
    CommonModule,
    NgxLoadingModule.forRoot({}),
  ]
})
export class LoadingModule { }


import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';


// angular material
import { MatSelectModule } from '@angular/material/select';

// npm i
import { NgxLoadingModule } from "ngx-loading";

// cunstoms modules
import { LoadingModule } from './loading/loading.module';
import { ModalErrorModule } from './modal/modal-error/modal-error.module';

@NgModule({
    imports: [
        CommonModule,
        HttpClientModule,
        BrowserAnimationsModule,
        FormsModule,
        ReactiveFormsModule,
        NgxLoadingModule,
        LoadingModule,
        MatSelectModule,
        ModalErrorModule,
    ],
    exports: [CommonModule, RouterModule, MatSelectModule],
    declarations: [

    
    
  ]

})
export class SharedModule { }
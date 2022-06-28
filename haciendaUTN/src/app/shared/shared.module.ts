import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';


// npm i
import { NgxLoadingModule } from "ngx-loading";

// cunstoms modules
import { LoadingModule } from './loading/loading.module';

@NgModule({
    imports: [
        CommonModule,
        HttpClientModule,
        BrowserAnimationsModule,
        FormsModule,
        ReactiveFormsModule,
        NgxLoadingModule,
        LoadingModule
    ],
    exports: [CommonModule, RouterModule],
    declarations: [

    ]

})
export class SharedModule { }
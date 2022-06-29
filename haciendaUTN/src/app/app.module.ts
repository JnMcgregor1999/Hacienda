import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoadingModule } from './shared/loading/loading.module';
import { SharedModule } from './shared/shared.module';

@NgModule({
  declarations: [
    AppComponent,

  ],
  imports: [
    BrowserModule,
    RouterModule,
    ReactiveFormsModule, 

    AppRoutingModule,
    SharedModule,
    LoadingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

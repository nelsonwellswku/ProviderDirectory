import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';

import { AppRoutingModule } from './app-routing.module';

import { ProviderService } from './provider.service';
import { CreateProviderComponent } from './create-provider.component';
import { ProviderDetailComponent } from './provider-detail.component';

@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    AppRoutingModule
  ],
  declarations: [
    AppComponent,
    CreateProviderComponent,
    ProviderDetailComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

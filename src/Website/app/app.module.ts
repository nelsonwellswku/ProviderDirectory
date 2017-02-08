import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';

import { AppRoutingModule } from './app-routing.module';

import { ProviderService } from './feature/provider.service';
import { CreateProviderComponent } from './feature/createProvider/create-provider.component';
import { ProviderDetailComponent } from './feature/providerDetail/provider-detail.component';
import { ListProvidersComponent } from './list-providers.component';
import { NavigationComponent } from './navigation.component';
import { PagingComponent } from './paging.component';

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
    ProviderDetailComponent,
    ListProvidersComponent,
    NavigationComponent,
    PagingComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

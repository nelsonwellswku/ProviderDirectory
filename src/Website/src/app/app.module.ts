import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { SiteNavigationComponent } from './site-navigation/site-navigation.component';
import { ListProvidersComponent } from './list-providers/list-providers.component';
import { CreateProviderComponent } from './create-provider/create-provider.component';
import { ProviderDetailComponent } from './provider-detail/provider-detail.component';
import { PagingComponent } from './paging/paging.component';

@NgModule({
  declarations: [
    AppComponent,
    SiteNavigationComponent,
    ListProvidersComponent,
    CreateProviderComponent,
    ProviderDetailComponent,
    PagingComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ListProvidersComponent } from './list-providers.component';
import { CreateProviderComponent } from './create-provider.component';
import { ProviderDetailComponent } from './provider-detail.component';

const routes: Routes = [
    { path: '', component: ListProvidersComponent },
    { path: 'createProvider', component: CreateProviderComponent },
    { path: 'providers/:providerId', component: ProviderDetailComponent }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule{
}
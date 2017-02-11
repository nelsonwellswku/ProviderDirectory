import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ListProvidersComponent } from './feature/listProviders/list-providers.component';
import { CreateProviderComponent } from './feature/createProvider/create-provider.component';
import { ProviderDetailComponent } from './feature/providerDetail/provider-detail.component';

const routes: Routes = [
    { path: '', redirectTo: 'providers', pathMatch: 'full' },
    { path: 'providers', component: ListProvidersComponent },
    { path: 'createProvider', component: CreateProviderComponent },
    { path: 'providers/:providerId', component: ProviderDetailComponent }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule{
}
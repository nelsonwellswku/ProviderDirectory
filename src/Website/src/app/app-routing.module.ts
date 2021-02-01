import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ListProvidersComponent } from './list-providers/list-providers.component';
import { CreateProviderComponent } from './create-provider/create-provider.component';
import { ProviderDetailComponent } from './provider-detail/provider-detail.component';

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
export class AppRoutingModule {
}

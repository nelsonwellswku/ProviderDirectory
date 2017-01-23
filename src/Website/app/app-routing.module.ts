import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CreateProviderComponent } from './create-provider.component';
import { ProviderDetailComponent } from './provider-detail.component';

const routes: Routes = [
    { path: '', component: CreateProviderComponent },
    { path: 'providers/:providerId', component: ProviderDetailComponent }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule{
}
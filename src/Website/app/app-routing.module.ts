import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CreateProviderComponent } from './create-provider.component';

const routes: Routes = [
    { path: 'createProvider', component: CreateProviderComponent }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule{
}
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: "",
    loadChildren: () => import('./auth/login/login.module').then((m) => m.LoginModule),
    data: { title: "Login" }
  },
  {
    path: "**",
    redirectTo: ""
  }

];

@NgModule({
  declarations: [],
  imports: [RouterModule.forRoot(routes, { useHash: true, relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }

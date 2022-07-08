import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: "login",
    loadChildren: () => import('./feature/auth/login/login.module').then((m) => m.LoginModule),
    data: { title: "Login" }
  },
  {
    path: "register",
    loadChildren: () => import('./feature/auth/register/register.module').then((m) => m.RegisterModule),
    data: { title: "Register" }
  },
  {
    path: "registerCompany",
    loadChildren: () => import('./feature/auth/regisclients/regisclients.module').then((m) => m.RegisclientsModule),
    data: { title: "Register Company" }
  },
  {
    path: "dashboard",
    loadChildren: () => import('./feature/pages/dashboard/dashboard.module').then((m) => m.DashboardModule),
    data: { title: "Dashboard" }
  },
  {
    path: "404",
    loadChildren: () => import('./feature/pages/notpagefound/notpagefound.module').then((m) => m.NotpagefoundModule),
    data: { title: "404" }
  },
  {
    path: "**",
    redirectTo: "login"
  }

];

@NgModule({
  declarations: [],
  imports: [RouterModule.forRoot(routes, { useHash: true, relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }

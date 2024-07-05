import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule, Routes, provideRouter, withHashLocation } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ErrorTestComponent } from './core/error-test/error-test.component';
import { NotFoundComponent } from './core/not-found/not-found.component';
import { ServerErrorComponent } from './core/server-error/server-error.component';
import { BreadcrumbComponent } from 'xng-breadcrumb';
import { AuthGuard } from './core/guards/auth.guard';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,data:{breadcrumb:'Home'} },
  { path: 'shop',loadChildren: () => import('./shop/shop.module').then((m) => m.ShopModule) },
  { path: 'basket',loadChildren: () => import('./basket/basket.module').then((m) => m.BasketModule) },
  { path: 'checkout',loadChildren: () => import('./checkout/checkout.module').then((m) => m.CheckoutModule)
    ,canActivate:[AuthGuard]
   },
   {path:'orders',canActivate:[AuthGuard],loadChildren:()=>import('./order/order.module').then(m=>m.OrderModule)},
  { path: 'account',loadChildren: () => import('./account/account.module').then((m) => m.AccountModule) },
  { path: 'error-test', component: ErrorTestComponent },
  { path: 'not-found', component: NotFoundComponent },
  { path: 'server-error', component: ServerErrorComponent },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes), CommonModule],
  exports: [RouterModule],
  providers: [
    provideRouter(routes, withHashLocation())
  ]
})
export class AppRoutingModule {}

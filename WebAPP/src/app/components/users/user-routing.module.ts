import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

@NgModule({
  imports: [
    RouterModule.forChild([
      { path: 'user-table', loadChildren: () => import('./user-table/user-table-routing.module').then(m => m.UserTableRoutingModule) },
      { path: '**', redirectTo: '/notfound' }
    ])],
  exports: [RouterModule]
})

export class UserRoutingModule { }

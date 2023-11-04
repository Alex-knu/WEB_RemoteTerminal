import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { UserTableComponent } from './user-table.component';

@NgModule({
  imports: [RouterModule.forChild([
    { path: '', component: UserTableComponent }
  ])],
  exports: [RouterModule]
})

export class UserTableRoutingModule { }

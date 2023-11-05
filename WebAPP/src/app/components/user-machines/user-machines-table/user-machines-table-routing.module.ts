import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { UserMachineTableComponent } from './user-machines-table.component';

@NgModule({
  imports: [RouterModule.forChild([
    { path: '', component: UserMachineTableComponent }
  ])],
  exports: [RouterModule]
})

export class UserMachineTableRoutingModule { }

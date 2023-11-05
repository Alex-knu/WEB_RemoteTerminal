import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { UserMachineInfoComponent } from './user-machines-info.component';

@NgModule({
  imports: [RouterModule.forChild([
    { path: '', component: UserMachineInfoComponent }
  ])],
  exports: [RouterModule]
})

export class UserMachineInfoRoutingModule { }

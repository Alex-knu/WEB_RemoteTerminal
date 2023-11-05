import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { UserMachineExecuteCommandComponent } from './user-machines-execute-command.component';

@NgModule({
  imports: [RouterModule.forChild([
    { path: '', component: UserMachineExecuteCommandComponent }
  ])],
  exports: [RouterModule]
})

export class UserMachineExecuteCommandComponentRoutingModule { }

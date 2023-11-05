import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

@NgModule({
  imports: [
    RouterModule.forChild([
      { path: 'user-machines-table', loadChildren: () => import('./user-machines-table/user-machines-table-routing.module').then(m => m.UserMachineTableRoutingModule) },
      { path: 'user-machines-info', loadChildren: () => import('./user-machines-info/user-machines-info-routing.module').then(m => m.UserMachineInfoRoutingModule) },
      { path: 'user-machines-execute-command', loadChildren: () => import('./user-machines-execute-command/user-machines-execute-command-routing.module').then(m => m.UserMachineExecuteCommandComponentRoutingModule) },
      { path: '**', redirectTo: '/notfound' }
    ])],
  exports: [RouterModule]
})

export class UserMachineRoutingModule { }

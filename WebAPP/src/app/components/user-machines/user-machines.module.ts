import { NgModule } from '@angular/core';
import { PrimeNgComponentsModule } from 'src/app/modules/primeng-components-module/primeng-components.module';
import { UserMachineRoutingModule } from './user-machines-routing.module';

@NgModule({
  imports: [
    UserMachineRoutingModule,
    PrimeNgComponentsModule
  ]
})

export class UserMachineModule { }

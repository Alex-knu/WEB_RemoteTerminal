import { NgModule } from '@angular/core';
import { PrimeNgComponentsModule } from 'src/app/modules/primeng-components-module/primeng-components.module';
import { UserRoutingModule } from './user-routing.module';
import { UserInfoComponent } from './user-info/user-info.component';

@NgModule({
  imports: [
    UserRoutingModule,
    PrimeNgComponentsModule
  ]
})

export class UserModule { }

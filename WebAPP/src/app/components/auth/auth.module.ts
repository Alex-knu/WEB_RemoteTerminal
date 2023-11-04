import { NgModule } from '@angular/core';
import { AuthRoutingModule } from './auth-routing.module';
import { PrimeNgComponentsModule } from 'src/app/modules/primeng-components-module/primeng-components.module';

@NgModule({
  imports: [
    AuthRoutingModule,
    PrimeNgComponentsModule
  ]
})

export class AuthModule { }

import { NgModule } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { AccessRoutingModule } from './access-routing.module';
import { AccessComponent } from './access.component';

@NgModule({
  imports: [
    AccessRoutingModule,
    ButtonModule
  ],
  declarations: [AccessComponent]
})

export class AccessModule { }

import { NgModule } from '@angular/core';
import { ErrorRoutingModule } from './error-routing.module';
import { ErrorComponent } from './error.component';
import { ButtonModule } from 'primeng/button';

@NgModule({
  imports: [
    ErrorRoutingModule,
    ButtonModule
  ],
  declarations: [ErrorComponent]
})

export class ErrorModule { }

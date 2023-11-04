import { NgModule } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { FormsModule } from '@angular/forms';
import { PasswordModule } from 'primeng/password';
import { InputTextModule } from 'primeng/inputtext';
import { RegisterRoutingModule } from './register-routing.module';
import { ToastModule } from 'primeng/toast';

@NgModule({
  imports: [
    RegisterRoutingModule,
    ButtonModule,
    ToastModule,
    CheckboxModule,
    InputTextModule,
    FormsModule,
    PasswordModule
  ]
})

export class RegisterModule { }

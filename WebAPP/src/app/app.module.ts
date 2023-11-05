import { NgModule } from '@angular/core';
import { HashLocationStrategy, LocationStrategy } from '@angular/common';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { AppLayoutModule } from './layout/app.layout.module';
import { NotfoundComponent } from './components/notfound/notfound.component';
import { PrimeNgComponentsModule } from './modules/primeng-components-module/primeng-components.module';
import { services } from './shared/services';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from './shared/services/auth-interceptor.service';
import { ErrorComponent } from './components/auth/error/error.component';
import { LoginComponent } from './components/auth/login/login.component';
import { RegisterComponent } from './components/auth/register/register.component';
import { AccessComponent } from './components/auth/access/access.component';
import { UserTableComponent } from './components/users/user-table/user-table.component';
import { UserInfoComponent } from './components/users/user-info/user-info.component';
import { UserMachineTableComponent } from './components/user-machines/user-machines-table/user-machines-table.component';
import { UserMachineInfoComponent } from './components/user-machines/user-machines-info/user-machines-info.component';
import { UserMachineExecuteCommandComponent } from './components/user-machines/user-machines-execute-command/user-machines-execute-command.component';

@NgModule({
  declarations: [
    AppComponent,
    AccessComponent,
    NotfoundComponent,
    ErrorComponent,
    LoginComponent,
    RegisterComponent,
    UserTableComponent,
    UserInfoComponent,
    UserMachineTableComponent,
    UserMachineInfoComponent,
    UserMachineExecuteCommandComponent
  ],
  imports: [
    AppRoutingModule,
    AppLayoutModule,
    PrimeNgComponentsModule
  ],
  providers: [
    services,
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    { provide: LocationStrategy, useClass: HashLocationStrategy }
  ],
  bootstrap: [AppComponent]
})

export class AppModule { }

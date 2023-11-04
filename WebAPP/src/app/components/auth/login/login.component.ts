import jwt_decode from 'jwt-decode';
import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { MessageService } from 'primeng/api';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { UserModel } from 'src/app/shared/models/user.model';
import { AuthService } from 'src/app/shared/services/auth.service';
import { TokenService } from 'src/app/shared/services/core/token.service';
import { UserLoginModel } from 'src/app/shared/models/userLogin.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styles: [`
        :host ::ng-deep .pi-eye,
        :host ::ng-deep .pi-eye-slash {
            transform:scale(1.6);
            margin-right: 1rem;
            color: var(--primary-color) !important;
        }
    `]
})

export class LoginComponent {
  username: string;
  password!: string;

  constructor(
    public layoutService: LayoutService,
    private authService: AuthService,
    private messageService: MessageService,
    private tokenService: TokenService,
    private router: Router) { }

  ngOnInit(): void { }

  public login() {
    this.authService.login(new UserLoginModel(this.username, this.password)).subscribe(
      loginInfo => {
        this.decodeToken(loginInfo.token);
        this.tokenService.setToken(loginInfo.token);
        this.router.navigate(['/']);
      },
      error => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: String((error as HttpErrorResponse).error).split('\n')[0] });
      });
  }

  decodeToken(token: string) {
    try {
      this.tokenService.setUserInfo(jwt_decode(token));
    } catch (error) {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: String((error as HttpErrorResponse).error).split('\n')[0] });
    }
  }
}

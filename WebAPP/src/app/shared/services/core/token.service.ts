import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { UserModel } from '../../models/user.model';

@Injectable({
  providedIn: 'root'
})

export class TokenService {

  constructor(private cookieService: CookieService) { }

  setUserInfo(info: UserModel) {
    this.cookieService.delete('userName');
    this.cookieService.delete('roles');
    this.cookieService.set('userName', info.UserName);
    this.cookieService.set('roles', JSON.stringify(info.Roles));
  }

  setToken(token: string) {
    //this.cookieService.deleteAll()
    this.cookieService.delete('token');
    this.cookieService.set('token', token);
  }

  getUserName(): string {
    return this.cookieService.get('userName');
  }

  getUserRoles(): string[] {
    return JSON.parse(this.cookieService.get('roles'));
  }

  getToken(): string {
    return this.cookieService.get('token');
  }
}

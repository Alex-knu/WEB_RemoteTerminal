import { Injectable } from "@angular/core";
import { AuthService } from "../auth.service";

@Injectable()
export class ApiService {
  constructor(
    public auth: AuthService) { }
}

import { HttpService } from "./core/http.service";
import { ClientConfigurationService } from "./core/client-configuration.service";
import { ApiService } from "./api/api.service";
import { UserService } from "./api/user.service";
import { RoleService } from "./api/role.service";

export const services = [
  HttpService,
  ClientConfigurationService,
  ApiService,
  UserService,
  RoleService
]

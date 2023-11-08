import { HttpService } from "./core/http.service";
import { ClientConfigurationService } from "./core/client-configuration.service";
import { ApiService } from "./api/api.service";
import { UserService } from "./api/user.service";
import { RoleService } from "./api/role.service";
import { MachineUserService } from "./api/machineUser.service";
import { SystemUserToMachineUserService } from "./api/systemUserToMachineUserService.service";
import { RemoteService } from "./api/remoteService.service";
import { CommandHistoryService } from "./api/commandHistoryService.service";

export const services = [
  HttpService,
  ClientConfigurationService,
  ApiService,
  UserService,
  RoleService,
  MachineUserService,
  SystemUserToMachineUserService,
  RemoteService,
  CommandHistoryService
]

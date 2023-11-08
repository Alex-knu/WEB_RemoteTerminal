import { Injectable } from "@angular/core";
import { HttpService } from "../core/http.service";
import { BaseService } from "../core/base.service";
import { ClientConfigurationService } from "../core/client-configuration.service";
import { ServiceType } from "../core/serviceType";
import { MachineUserModel } from "../../models/machineUser.model";

@Injectable()
export class MachineUserService extends BaseService<any> {
  constructor(
    httpService: HttpService,
    configService: ClientConfigurationService) {
    super(httpService, 'MachineUser', configService, MachineUserModel, ServiceType.route);
  }
}

import { Injectable } from "@angular/core";
import { HttpService } from "../core/http.service";
import { BaseService } from "../core/base.service";
import { ClientConfigurationService } from "../core/client-configuration.service";
import { ServiceType } from "../core/serviceType";
import { RunCommandModel } from "../../models/runCommandModel.model";

@Injectable()
export class RemoteService extends BaseService<any> {
  constructor(
    httpService: HttpService,
    configService: ClientConfigurationService) {
    super(httpService, 'Remote', configService, RunCommandModel, ServiceType.route);
  }
}

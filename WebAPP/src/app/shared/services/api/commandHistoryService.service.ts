import { Injectable } from "@angular/core";
import { HttpService } from "../core/http.service";
import { BaseService } from "../core/base.service";
import { ClientConfigurationService } from "../core/client-configuration.service";
import { ServiceType } from "../core/serviceType";
import { CommandHistoryModel } from "../../models/commandHistory.model";

@Injectable()
export class CommandHistoryService extends BaseService<any> {
  constructor(
    httpService: HttpService,
    configService: ClientConfigurationService) {
    super(httpService, 'CommandHistory', configService, CommandHistoryModel, ServiceType.route);
  }
}

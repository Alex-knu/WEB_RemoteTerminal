import { ClientConfigurationService } from "./client-configuration.service";
import { HttpService } from "./http.service";
import { ServiceType } from "./serviceType";

export abstract class CoreHttpService {
  public static onHandleError: (error: any) => void;

  protected constructor(
    protected httpService: HttpService,
    protected controllerName: string,
    protected configService: ClientConfigurationService,
    protected serviceType: ServiceType = ServiceType.web) { }

  public get baseUrl() {
    const baseHost = this.configService.getEnvSetting(this.serviceType);
    if (baseHost) {
      return `${baseHost}${this.configService.getEnvSetting('webserverApiPath')}`;
    } else {
      return this.configService.getEnvSetting('webserverApiPath');
    }
  }

  protected handleError(error: any): never {
    if (CoreHttpService.onHandleError) {
      CoreHttpService.onHandleError(error);
    }
    throw error;
  }
}

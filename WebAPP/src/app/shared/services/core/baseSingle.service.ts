import { Observable, map, catchError } from "rxjs";
import { BaseModel } from "../../models/base.model";
import { ClientConfigurationService } from "./client-configuration.service";
import { CoreHttpService } from "./coreHttp.service";
import { HttpService } from "./http.service";
import { ServiceType } from "./serviceType";

export class BaseSingleService<TModel extends BaseModel> extends CoreHttpService {
  public constructor(
    protected override httpService: HttpService,
    protected override controllerName: string,
    protected override configService: ClientConfigurationService,
    protected createModel: new (id: string | null) => TModel,
    protected override serviceType: ServiceType = ServiceType.web) {
    super(httpService, controllerName, configService, serviceType);
  }

  private mapModel(payload: any): TModel {
    const model = new this.createModel(payload.id ?? null);
    Object.assign(model, payload);
    return model;
  }

  get(): Observable<TModel> {
    return this.httpService.get(`${this.baseUrl}${this.controllerName}`)
      .pipe(
        map((payload: any) => this.mapModel(payload)),
        catchError(this.handleError)
      );
  }

  create(record: TModel): Observable<TModel> {
    return this.httpService.post(`${this.baseUrl}${this.controllerName}`, record)
      .pipe(
        map((payload: any) => this.mapModel(payload)),
        catchError(this.handleError),
      );
  }

  update(record: TModel): Observable<TModel> {
    return this.httpService.put(`${this.baseUrl}${this.controllerName}`, record)
      .pipe(
        map((payload: any) => this.mapModel(payload)),
        catchError(this.handleError),
      );
  }

  getById(id: string): Observable<TModel> {
    return this.httpService.get(`${this.baseUrl}${this.controllerName}/${id}`)
      .pipe(
        map((payload: any) => this.mapModel(payload)),
        catchError(this.handleError),
      );
  }

  deleteById(id: string): Observable<TModel> {
    return this.httpService.delete(`${this.baseUrl}${this.controllerName}/${id}`)
      .pipe(
        map((payload: any) => this.mapModel(payload)),
        catchError(this.handleError),
      );
  }
}

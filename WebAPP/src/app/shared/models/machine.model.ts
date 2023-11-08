import { BaseModel } from "./base.model";

export class MachineModel  extends BaseModel {

  constructor(id: string | null = null) {
    super(id);
  }

  name: string;
  host: string;
  port: number;
}

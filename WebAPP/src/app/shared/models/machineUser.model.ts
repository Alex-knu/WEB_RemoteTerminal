import { BaseModel } from "./base.model";
import { MachineModel } from "./machine.model";

export class MachineUserModel  extends BaseModel {

  constructor(id: string | null = null) {
    super(id);
  }

  username: string;
  password: string;
  machine: MachineModel;
  machineId: string;
}

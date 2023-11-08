import { BaseModel } from "./base.model";
import { MachineUserModel } from "./machineUser.model";

export class SystemUserToMachineUserModel  extends BaseModel {

  constructor(id: string | null = null) {
    super(id);
  }

  systemUserId: string;
  MachineUserId: string;
  machineUser: MachineUserModel;
}

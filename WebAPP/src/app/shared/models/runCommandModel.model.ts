import { BaseModel } from "./base.model";

export class RunCommandModel  extends BaseModel {

  constructor(id: string | null = null) {
    super(id);
  }

  host: string;
  port: number;
  password: string;
  username: string;
  useSSHKey: boolean | null;
  rootPassword: string | null;
  command: string;
  machineUserId: string | null;
}

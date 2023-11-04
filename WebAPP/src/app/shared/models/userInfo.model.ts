import { BaseModel } from "./base.model";
import { RoleModel } from "./role.model";

export class UserInfoModel  extends BaseModel {

  constructor(id: string | null = null) {
    super(id);
  }

  name: string;
  roles: RoleModel[];
}

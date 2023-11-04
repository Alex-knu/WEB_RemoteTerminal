import { BaseModel } from "./base.model";

export class RoleModel  extends BaseModel {

  constructor(id: string | null = null) {
    super(id);
  }

  name: string;
}

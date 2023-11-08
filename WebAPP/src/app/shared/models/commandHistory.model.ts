import { BaseModel } from "./base.model";

export class CommandHistoryModel  extends BaseModel {

  constructor(id: string | null = null) {
    super(id);
  }

  command: string;
  time: string;
}

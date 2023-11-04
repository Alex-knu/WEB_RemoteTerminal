export abstract class BaseModel {
  constructor(
    public id: string | null) {
  }

  public get _id(): string | null {
    return this.id;
  }

  public set _id(value: string | null) {
    this.id = value;
  }
}

import { ComponentFixture, TestBed } from '@angular/core/testing';
import { UserMachineExecuteCommandComponent } from './user-machines-execute-command.component';

describe('UserMachineExecuteCommandComponent', () => {
  let component: UserMachineExecuteCommandComponent;
  let fixture: ComponentFixture<UserMachineExecuteCommandComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserMachineExecuteCommandComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserMachineExecuteCommandComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

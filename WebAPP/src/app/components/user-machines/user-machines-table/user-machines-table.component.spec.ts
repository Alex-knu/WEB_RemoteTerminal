import { ComponentFixture, TestBed } from '@angular/core/testing';
import { UserMachineTableComponent } from './user-machines-table.component';

describe('UserMachineTableComponent', () => {
  let component: UserMachineTableComponent;
  let fixture: ComponentFixture<UserMachineTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserMachineTableComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserMachineTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

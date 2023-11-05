import { ComponentFixture, TestBed } from '@angular/core/testing';
import { UserMachineInfoComponent } from './user-machines-info.component';

describe('UserMachineInfoComponent', () => {
  let component: UserMachineInfoComponent;
  let fixture: ComponentFixture<UserMachineInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserMachineInfoComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserMachineInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

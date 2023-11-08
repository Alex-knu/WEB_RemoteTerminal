import { Component } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { UUID } from 'angular2-uuid';
import { MessageService } from 'primeng/api';
import { DialogService, DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { MachineUserModel } from 'src/app/shared/models/machineUser.model';
import { SystemUserToMachineUserService } from 'src/app/shared/services/api/systemUserToMachineUserService.service';
import { MachineModel } from 'src/app/shared/models/machine.model';
import { MachineUserService } from 'src/app/shared/services/api/machineUser.service';

@Component({
  selector: 'app-user-machines-info',
  templateUrl: './user-machines-info.component.html',
  styleUrls: ['./user-machines-info.component.scss']
})

export class UserMachineInfoComponent {
  submitted: boolean;
  machineUser: MachineUserModel;

  constructor(
    private systemUserToMachineUserService: SystemUserToMachineUserService,
    private machineUserService: MachineUserService,
    private messageService: MessageService,
    public dialogService: DialogService,
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig) { }

  ngOnInit(): void {
    this.submitted = false;

    if (this.config.data != null) {
      this.machineUser = this.config.data;
    }
    else {
      this.machineUser = new MachineUserModel;
      this.machineUser.machine = new MachineModel;
      this.machineUser.machine.port = 22;
    }
  }

  saveMachine() {
    this.submitted = true;

    if (this.machineUser.id) {
      this.machineUserService.single.update(this.machineUser).subscribe(
        machineUser => {
          this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'З`єднання оновлено' });
          this.ref.close(machineUser);
        },
        error => {
          this.messageService.add({ severity: 'error', summary: 'Error', detail: String((error as HttpErrorResponse).error).split('\n')[0] });
        })
    }
    else {
      this.machineUser.id = UUID.UUID();
      this.machineUser.machine.id = UUID.UUID();
      this.systemUserToMachineUserService.single.create(this.machineUser).subscribe(
        machineUser => {
          this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'З`єднання створено' });
          this.ref.close(machineUser);
        },
        error => {
          this.machineUser.id = null;
          this.messageService.add({ severity: 'error', summary: 'Error', detail: String((error as HttpErrorResponse).error).split('\n')[0] });
        })
    }
  }
}

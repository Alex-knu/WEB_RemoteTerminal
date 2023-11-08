import { Component } from '@angular/core';
import { MessageService } from 'primeng/api';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { UserMachineInfoComponent } from '../user-machines-info/user-machines-info.component';
import { UserMachineExecuteCommandComponent } from '../user-machines-execute-command/user-machines-execute-command.component';
import { MachineUserService } from 'src/app/shared/services/api/machineUser.service';
import { SystemUserToMachineUserService } from 'src/app/shared/services/api/systemUserToMachineUserService.service';
import { MachineUserModel } from 'src/app/shared/models/machineUser.model';
import { CommandHistoryService } from 'src/app/shared/services/api/commandHistoryService.service';

@Component({
  selector: 'app-user-machines-table',
  templateUrl: './user-machines-table.component.html',
  styleUrls: ['./user-machines-table.component.scss']
})

export class UserMachineTableComponent {
  machines: MachineUserModel[] = [];
  ref: DynamicDialogRef;
  loading: boolean = false;
  machine: any;

  constructor(
    private commandHistoryService: CommandHistoryService,
    private systemUserToMachineUserService: SystemUserToMachineUserService,
    private messageService: MessageService,
    private dialogService: DialogService) { }

  ngOnInit() {
    this.loading = true;
    setTimeout(() => {
      this.systemUserToMachineUserService.collection.getAll()
        .subscribe(
          (machineUsers) => {
            this.machines = machineUsers;
          });

      this.loading = false;
    });
  }

  onClick(event: any) {
    this.commandHistoryService.collection.getListById(event.id)
      .subscribe(
        (history) => {
          event.changeValues = history;
        });
  }

  editMachine(machine: any) {
    this.machine = machine;

    this.ref = this.dialogService.open(UserMachineInfoComponent, {
      header: 'Деталі хоста',
      data: machine,
      contentStyle: { overflow: 'auto' },
      baseZIndex: 10000,
      maximizable: true
    });

    this.ref.onClose.subscribe((machine: MachineUserModel) => {
      if (machine && machine.id) {
        this.systemUserToMachineUserService.collection.getAll()
          .subscribe(
            (machineUsers) => {
              this.machines = machineUsers;
            });
        this.messageService.add({ severity: 'info', summary: 'Список оновлено' });
      }
    });
  }


  executeCommand(machine: any) {
    this.machine = machine;

    this.ref = this.dialogService.open(UserMachineExecuteCommandComponent, {
      header: 'Виконати команду',
      data: machine,
      contentStyle: { overflow: 'auto' },
      baseZIndex: 10000,
      maximizable: true
    });
  }

  openNew() {
    this.ref = this.dialogService.open(UserMachineInfoComponent, {
      header: 'Деталі хоста',
      contentStyle: { overflow: 'auto' },
      baseZIndex: 10000,
      maximizable: true
    });

    this.ref.onClose.subscribe((machine: MachineUserModel) => {
      if (machine && machine.id) {
        this.systemUserToMachineUserService.collection.getAll()
          .subscribe(
            (machines) => {
              this.machines = machines;
            });

        this.messageService.add({ severity: 'info', summary: 'Список оновлено' });
      }
    });
  }
}

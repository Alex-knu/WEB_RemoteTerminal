import { Component } from '@angular/core';
import { MessageService } from 'primeng/api';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { UserMachineInfoComponent } from '../user-machines-info/user-machines-info.component';
import { UserMachineExecuteCommandComponent } from '../user-machines-execute-command/user-machines-execute-command.component';

@Component({
  selector: 'app-user-machines-table',
  templateUrl: './user-machines-table.component.html',
  styleUrls: ['./user-machines-table.component.scss']
})

export class UserMachineTableComponent {
  machines: any[] = [];
  ref: DynamicDialogRef;
  loading: boolean = false;
  machine: any;

  constructor(
    private messageService: MessageService,
    private dialogService: DialogService) { }

  ngOnInit() {
    this.machines = [
      {
        name: 'Home',
        host: '10.10.1.152',
        port: 22,
        changeValues: [
          {
            command: 'ls',
            time: '01.01.2000',
          }
        ]
      }
    ];
  }

  onClick(event: any) {
    event.changeValues = this.machines[0].changeValues;
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

    // this.ref.onClose.subscribe((application: BaseApplication) => {
    //   if (application && application.id) {
    //     this.baseApplicationService.collection.getAll()
    //       .subscribe(
    //         (applications) => {
    //           this.applications = applications;
    //         });

    //     this.messageService.add({ severity: 'info', summary: 'Список оновлено', detail: application.name });
    //   }
    // });
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

    // this.ref.onClose.subscribe((application: BaseApplication) => {
    //   if (application && application.id) {
    //     this.baseApplicationService.collection.getAll()
    //       .subscribe(
    //         (applications) => {
    //           this.applications = applications;
    //         });

    //     this.messageService.add({ severity: 'info', summary: 'Список оновлено', detail: application.name });
    //   }
    // });
  }
}

import { Component } from '@angular/core';
import { MessageService } from 'primeng/api';
import { HttpErrorResponse } from '@angular/common/http';
import { DialogService, DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { MachineUserModel } from 'src/app/shared/models/machineUser.model';
import { RunCommandModel } from 'src/app/shared/models/runCommandModel.model';
import { RemoteService } from 'src/app/shared/services/api/remoteService.service';

@Component({
  selector: 'app-user-machines-execute-command',
  templateUrl: './user-machines-execute-command.component.html',
  styleUrls: ['./user-machines-execute-command.component.scss']
})

export class UserMachineExecuteCommandComponent {
  submitted: boolean;
  machineUser: MachineUserModel;
  runComman = new RunCommandModel;
  result: string;
  machine: any;

  constructor(
    private remoteService: RemoteService,
    private messageService: MessageService,
    public dialogService: DialogService,
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig) { }

  ngOnInit(): void {
    this.submitted = false;

    if (this.config.data != null) {
      this.machineUser = this.config.data;

      this.runComman.machineUserId = this.machineUser.id;
      this.runComman.password = this.machineUser.password;
      this.runComman.username = this.machineUser.username;
      this.runComman.host = this.machineUser.machine.host;
      this.runComman.port = this.machineUser.machine.port;
    }
  }

  execute() {
    this.remoteService.single.create(this.runComman).subscribe(
      (result)  => {
        this.result = JSON.parse(JSON.stringify(result)).result;
      },
      error => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: String((error as HttpErrorResponse).error).split('\n')[0] });
      })
  }
}

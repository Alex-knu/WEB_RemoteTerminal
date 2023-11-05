import { Component } from '@angular/core';
import { UUID } from 'angular2-uuid';
import { MessageService } from 'primeng/api';
import { DialogService, DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';

@Component({
  selector: 'app-user-machines-execute-command',
  templateUrl: './user-machines-execute-command.component.html',
  styleUrls: ['./user-machines-execute-command.component.scss']
})

export class UserMachineExecuteCommandComponent {
  command: string;
  machine: any;

  constructor(
    private messageService: MessageService,
    public dialogService: DialogService,
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig) { }

  ngOnInit(): void {
  }

  execute() {

    if (this.machine.id) {
      // this.baseApplicationService.single.update(this.application).subscribe(
      //   application => {
      //     this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Заявку оновлено' });
      //     this.ref.close(application);
      //   },
      //   error => {
      //     this.messageService.add({ severity: 'error', summary: 'Error', detail: String((error as HttpErrorResponse).error).split('\n')[0] });
      //   })
    }
    else {
      this.machine.id = UUID.UUID();
      // this.baseApplicationService.single.create(this.application).subscribe(
      //   application => {
      //     this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Заявку створено' });
      //     this.ref.close(application);
      //   },
      //   error => {
      //     this.application.id = null;
      //     this.messageService.add({ severity: 'error', summary: 'Error', detail: String((error as HttpErrorResponse).error).split('\n')[0] });
      //   })
    }
  }
}

import { Component } from '@angular/core';
import { UUID } from 'angular2-uuid';
import { MessageService } from 'primeng/api';
import { DialogService, DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';

@Component({
  selector: 'app-user-machines-info',
  templateUrl: './user-machines-info.component.html',
  styleUrls: ['./user-machines-info.component.scss']
})

export class UserMachineInfoComponent {
  submitted: boolean;
  machine: any;

  constructor(
    private messageService: MessageService,
    public dialogService: DialogService,
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig) { }

  ngOnInit(): void {
    this.submitted = false;

    if (this.config.data != null) {
      this.machine = this.config.data;
    }
    else {
      this.machine = {
        name: 'Home',
        host: '10.10.1.152',
        port: 22
      };
    }
  }

  saveMachine() {
    this.submitted = true;

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

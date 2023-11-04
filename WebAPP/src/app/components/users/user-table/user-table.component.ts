import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MessageService, ConfirmationService } from 'primeng/api';
import { DynamicDialogRef, DialogService } from 'primeng/dynamicdialog';
import { UserInfoModel } from 'src/app/shared/models/userInfo.model';
import { UserService } from 'src/app/shared/services/api/user.service';
import { AuthService } from 'src/app/shared/services/auth.service';
import { UserInfoComponent } from '../user-info/user-info.component';

@Component({
  selector: 'app-user-table',
  templateUrl: './user-table.component.html',
  styleUrls: ['./user-table.component.scss']
})

export class UserTableComponent {
  loading: boolean = false;
  users: UserInfoModel[];
  user: UserInfoModel;
  ref: DynamicDialogRef;

  constructor(
    private router: Router,
    private messageService: MessageService,
    private confirmationService: ConfirmationService,
    private userService: UserService,
    private dialogService: DialogService,
    private authService: AuthService) { }

  ngOnInit() {
    this.loading = true;
    setTimeout(() => {
      this.userService.collection.getAll()
        .subscribe(
          (users) => {
            this.users = users;
          });

      this.loading = false;
    });
  }

  ngOnDestroy() {
    if (this.ref) {
      this.ref.close();
    }
  }

  editUser(user: UserInfoModel) {
    this.user = user;

    this.ref = this.dialogService.open(UserInfoComponent, {
      header: 'Деталі про користувача',
      data: user,
      contentStyle: { overflow: 'auto' },
      baseZIndex: 10000,
      maximizable: true
    });

    this.ref.onClose.subscribe((user: UserInfoModel) => {
      if (user && user.id) {
        this.userService.collection.getAll()
          .subscribe(
            (users) => {
              this.users = users;
            });
        this.messageService.add({ severity: 'info', summary: 'Список оновлено', detail: user.name });
      }
    });
  }

  deleteUser(user: UserInfoModel) {
    this.confirmationService.confirm({
      message: 'Are you sure you want to delete ' + user.name + '?',
      header: 'Confirm',
      icon: 'pi pi-exclamation-triangle',
      // accept: () => {
      //   if (user.id) {
      //     this.userService.single.deleteById(user.id).subscribe(
      //       user => {
      //         this.applications = this.applications.filter(val => val.id !== user.id);
      //         this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Заявку видалено' });
      //       },
      //       error => {
      //         this.messageService.add({ severity: 'error', summary: 'Error', detail: String((error as HttpErrorResponse).error).split('\n')[0] });
      //       })
      //   }
      // }
    });
  }
}

import { Component } from '@angular/core';
import { MessageService } from 'primeng/api';
import { DynamicDialogRef, DynamicDialogConfig } from 'primeng/dynamicdialog';
import { RoleModel } from 'src/app/shared/models/role.model';
import { UserInfoModel } from 'src/app/shared/models/userInfo.model';
import { RoleService } from 'src/app/shared/services/api/role.service';
import { UserService } from 'src/app/shared/services/api/user.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-user-info',
  templateUrl: './user-info.component.html',
  styleUrls: ['./user-info.component.scss']
})
export class UserInfoComponent {
  loading: boolean = false;
  roles: RoleModel[];
  selectedRoles: RoleModel[];
  user: UserInfoModel;

  constructor(
    private messageService: MessageService,
    private userService: UserService,
    private roleService: RoleService,
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig) { }

  ngOnInit() {
    this.loading = true;
    this.user = this.config.data;

    setTimeout(() => {
      this.roleService.collection.getAll()
        .subscribe(
          (roles) => {
            this.roles = roles;
          });
      this.loading = false;
    });
  }

  saveRoles(){
    this.userService.single.update(this.user).subscribe(
      user => {
        this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Користувача оновлено' });
        this.ref.close(user);
      },
      error => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: String((error as HttpErrorResponse).error).split('\n')[0] });
      });
  };
}

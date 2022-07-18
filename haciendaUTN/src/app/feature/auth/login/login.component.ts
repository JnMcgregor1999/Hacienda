import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LoginModel } from '@core/model/login.model';
import { CommonService } from '@shared/services/common.service';
import { Subject } from 'rxjs';
import { LoginService } from './service/login.service';
import { takeUntil } from "rxjs/operators";
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { ModalErrorComponent } from '../../../shared/modal/modal-error/modal-error.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  /******************************************************
   * Author: Johan McGregor
   * Creation date: 19/06/2022
   * Description: Variable that contains all subscriptions
   *******************************************************/
  private _unsubscribe$ = new Subject<void>();

  /******************************************************
   * Author: Johan McGregor
   * Creation date: 19/06/2022
   * Description: Variable that contains information and validators for the inputs
   *******************************************************/
  public loginForm: FormGroup = this.form.group({
    password: ["", Validators.compose([Validators.required])],
    identification: ["", Validators.compose([Validators.required])],
  });

  /******************************************************
   * Author: Johan McGregor
   * Creation date: 19/06/2022
   * Description: Variable that manage the form error's 
   *******************************************************/
  public submitted: boolean = false;

  constructor(
    private form: FormBuilder,
    private _router: Router,
    private _commonService: CommonService,
    private _loginService: LoginService,
    public dialog: MatDialog) { }


  ngOnInit(): void {
  }

  /******************************************************
   * Author: Johan McGregor
   * Creation date: 19/06/2022
   * Description: Method that makes login
   *******************************************************/
  login() {
    if (this.loginForm.invalid) {
      this.submitted = true;
    } else {
      this._commonService._setLoading(true); // this line call/show the loading
      let model: LoginModel = {
        Identification: this.loginForm.get("identification")?.value,
        Password: this.loginForm.get("password")?.value,
        Fk_Catalog_Identification_Type: 0,
        Full_Name: '',
        Email: '',
        Active: true
      }
      this._loginService
        .login(model)
        .pipe(takeUntil(this._unsubscribe$))
        .subscribe({
          next: (response: any) => {
            this._commonService._setLoading(false);// this line hidden the loading
            if (response) {
              this._router.navigate(
                ['dashboard']
              );
            }
          },
          error: (response: any) => { this._commonService._setLoading(false); console.log(`e => ${response}`); this.openDialog(response.message) },
          complete: () => {
            this._commonService._setLoading(false);
          }
        });
    }
  }

  openDialog(messageError: string): void {
    const datainfo = {
      labelTitile: "Error",
      icon: "cancel",
      textDescription: messageError,
      status: "error",
    };


    const dialogRef = this.dialog.open(ModalErrorComponent, {
      data: { datainfo: datainfo },
      minWidth: "525px",
      maxWidth: "478px",
      maxHeight: "277px",
      minHeight: "277px",
    });

    // setTimeout(() => dialogRef.close(), 3000);
  }



  /******************************************************
   * Author: Johan McGregor
   * Creation date: 19/06/2022
   * Description: Method that cancels subscriptions
   *******************************************************/
  ngOnDestroy() {
    this._unsubscribe$.next();
    this._unsubscribe$.complete();
  }

}

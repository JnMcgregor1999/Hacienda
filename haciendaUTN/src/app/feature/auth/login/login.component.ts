import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LoginModel } from '@core/model/login.model';
import { CommonService } from '@shared/services/common.service';
import { Subject } from 'rxjs';
import { LoginService } from './service/login.service';
import { takeUntil } from "rxjs/operators";
import { Router } from '@angular/router';

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
  private unsubscribe$ = new Subject<void>();

  /******************************************************
   * Author: Johan McGregor
   * Creation date: 19/06/2022
   * Description: Variable that contains information and validators for the inputs
   *******************************************************/
  public loginForm: FormGroup = this.form.group({
    password: ["", Validators.compose([Validators.required])],
    username: ["", Validators.compose([Validators.required])],
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
    private _loginService: LoginService) { }

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
        userName: this.loginForm.get("username")?.value,
        password: this.loginForm.get("password")?.value,
        isLogin: false
      }
      this._loginService
        .login(model)
        .pipe(takeUntil(this.unsubscribe$))
        .subscribe({
          next: (response: any) => {
            this._commonService._setLoading(false);// this line hidden the loading
            if (response) {
              this._router.navigate(
                ['dashboard']
              );
            }
          },
          error: (response: any) => { this._commonService._setLoading(false); console.log(`e => ${response}`) },
          complete: () => {
            this._commonService._setLoading(false);
          }
        });
    }
  }


  /******************************************************
   * Author: Johan McGregor
   * Creation date: 19/06/2022
   * Description: Method that cancels subscriptions
   *******************************************************/
  ngOnDestroy() {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

}

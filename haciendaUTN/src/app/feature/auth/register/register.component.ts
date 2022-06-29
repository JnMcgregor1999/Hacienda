import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { RegisterModel } from '@core/model/register.model';
import { Type_CatalogModel } from '@core/model/type_Catalog.module';
import { CommonService } from '@shared/services/common.service';
import { Subject } from 'rxjs';
import { takeUntil } from "rxjs/operators";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit, OnDestroy {

  /******************************************************
  * Author: Johan McGregor
  * Creation date: 27/06/2022
  * Description: Variable that contains all subscriptions
  *******************************************************/
  private unsubscribe$ = new Subject<void>();

  /******************************************************
   * Author: Johan McGregor
   * Creation date: 27/06/2022
   * Description: Variable that contains information and validators for the inputs
   *******************************************************/
  public registerForm: FormGroup = this.form.group({
    fk_Catalog_Identification_Type: ["", Validators.compose([Validators.required])],
    identification: ["", Validators.compose([Validators.required])],
    full_Name: ["", Validators.compose([Validators.required])],
    email: ["", Validators.compose([Validators.required])],
    password: ["", Validators.compose([Validators.required])],
    confirm_Password: ["", Validators.compose([Validators.required])],
  });

  /******************************************************
  * Author: Johan McGregor
  * Creation date: 27/06/2022
  * Description: Variable that manage the form error's 
  *******************************************************/
  public submitted: boolean = false;


  /******************************************************
  * Author: Johan McGregor
  * Creation date: 27/06/2022
  * Description: 
  *******************************************************/
  public identification_type: Array<any> = new Array();

  constructor(private form: FormBuilder,
    private _router: Router,
    private _commonService: CommonService,) { }

  ngOnInit(): void {
    this.getCatalogTypeIdentification();
  }




  getCatalogTypeIdentification() {
    this._commonService._setLoading(true);
    let model: Type_CatalogModel = {
      search_Key: 'identification_type'
    }

    this._commonService
      .getTypeCatalog(model)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe({
        next: (response: any) => {
          this.listCatalogIdentification(response.pk_Gbl_Type_Catalog);
        },
        error: (response: any) => { this._commonService._setLoading(false); console.log(`e => ${response}`) },
        complete: () => {
          this._commonService._setLoading(false);
        }
      });
  }



  listCatalogIdentification(pk_Gbl_Type_Catalog: number) {
    this._commonService._setLoading(true);
    let model = {
      fk_Gbl_Type_Catalog: pk_Gbl_Type_Catalog
    }

    this._commonService
      .listCatalog(model)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe({
        next: (response: any) => {
          this._commonService._setLoading(false);
          this.identification_type = response;
        },
        error: (response: any) => { this._commonService._setLoading(false); console.log(`e => ${response}`) },
        complete: () => {
          this._commonService._setLoading(false);
        }
      });
  }



  save() {
    if (this.registerForm.invalid) {
      this.submitted = true;
    } else {
      this._commonService._setLoading(true); // this line call/show the loading
      let model: RegisterModel = {
        fk_Catalog_Identification_Type: this.registerForm.get("fk_Catalog_Identification_Type")?.value,
        identification: this.registerForm.get("identification")?.value,
        full_Name: this.registerForm.get("full_Name")?.value,
        email: this.registerForm.get("email")?.value,
        password: this.registerForm.get("password")?.value,
        active: true
      }
      console.log(model);

      // this._loginService
      //   .login(model)
      //   .pipe(takeUntil(this.unsubscribe$))
      //   .subscribe({
      //     next: (response: any) => {
      //       this._commonService._setLoading(false);// this line hidden the loading
      //       if (response) {
      //         this._router.navigate(
      //           ['dashboard']
      //         );
      //       }
      //     },
      //     error: (response: any) => { this._commonService._setLoading(false); console.log(`e => ${response}`) },
      //     complete: () => {
      //       this._commonService._setLoading(false);
      //     }
      //   });
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

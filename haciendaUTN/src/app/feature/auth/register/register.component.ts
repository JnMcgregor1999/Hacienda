import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { RegisterModel } from '@core/model/register.model';
import { Type_CatalogModel } from '@core/model/type_Catalog.module';
import { CommonService } from '@shared/services/common.service';
import { Subject } from 'rxjs';
import { takeUntil } from "rxjs/operators";
import { RegisterService } from './services/register.services';

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


  public identificationTypeSelected: number = 0;

  constructor(private form: FormBuilder,
    private _router: Router,
    private _commonService: CommonService,
    private _registerService: RegisterService) { }

  ngOnInit(): void {
    this.getCatalogTypeIdentification();
  }

  /******************************************************
   * Author: Johan McGregor
   * Creation date: 02/07/2022
   * Description: Method that get type catalog of identification type
   *******************************************************/
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

  /******************************************************
   * Author: Johan McGregor
   * Creation date: 02/07/2022
   * Description: Method that get catalog of identification type
   *******************************************************/
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


  /******************************************************
     * Author: Johan McGregor
     * Creation date: 02/07/2022
     * Description: Method that save user information
     *******************************************************/
  save() {
    if (this.registerForm.invalid) {
      this.submitted = true;
    } else {
      this._commonService._setLoading(true); // this line call/show the loading
      let model: RegisterModel = {
        fk_Catalog_Identification_Type: 1,
        identification: this.registerForm.get("identification")?.value,
        full_Name: this.registerForm.get("full_Name")?.value,
        email: this.registerForm.get("email")?.value,
        password: this.registerForm.get("password")?.value,
        active: true
      }

      this._registerService
        .save(model)
        .pipe(takeUntil(this.unsubscribe$))
        .subscribe({
          next: (response: any) => {
            this._commonService._setLoading(false);// this line hidden the loading
            this._router.navigate(
              ['login']
            );
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

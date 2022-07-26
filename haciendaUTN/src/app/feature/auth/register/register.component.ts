import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { RegisterModel } from '@core/model/register.model';
import { Type_CatalogModel } from '@core/model/type_Catalog.module';
import { environment } from '@env/environment';
import { CommonService } from '@shared/services/common.service';
import { dropzoneConfig } from '@util/dropzoneConfig';
import { ModalErrorComponent } from 'app/shared/modal/modal-error/modal-error.component';
import { DropzoneComponent, DropzoneConfigInterface, DropzoneDirective } from 'ngx-dropzone-wrapper';
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


  @ViewChild(DropzoneComponent) componentRef?: DropzoneComponent;
  @ViewChild(DropzoneDirective) directiveRef?: DropzoneDirective;

  dropzone: any;
  files: Array<any> = [];
  userInfo: RegisterModel;


  // public config: DropzoneConfigInterface = {
  //   paramName: "file",
  //   clickable: true,
  //   url: environment.apiURL + 'api/Document/Upload',
  //   method: 'POST',
  //   maxFilesize: 10,
  //   maxFiles: 5,
  //   dictResponseError: 'Ha ocurrido un error en el servidor',
  //   acceptedFiles: '.json',
  //   autoProcessQueue: false,
  //   // parallelUploads: 5,
  //   uploadMultiple: false,
  //   chunking: false,
  //   addRemoveLinks: true,
  //   dictRemoveFile: "Borrar archivo",
  //   dictFileTooBig: "El archivo es muy grande ({{filesize}}) para cargarlo en el sistema. Capacidad maxima {{maxFilesize}}MB",
  //   dictUploadCanceled: "La carga de archivos ha sido cancelada.",
  //   timeout: 1200000000
  // };


  constructor(private form: FormBuilder,
    private _router: Router,
    private _commonService: CommonService,
    private _registerService: RegisterService,
    public dialog: MatDialog) { }

  ngOnInit(): void {
    this.getCatalogTypeIdentification();
  }

  ngAfterViewInit() {
    this.dropzone = this.componentRef.directiveRef.dropzone();

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
    } else if (this.files.length == 0) {
      this.openModalMessage("No se ha seleccionado ningun archivos")
    } else {
      this._commonService._setLoading(true); // this line call/show the loading
      let model: RegisterModel = {
        fk_Catalog_Identification_Type: Number(this.registerForm.get("fk_Catalog_Identification_Type")?.value),
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
            this.userInfo = response;
            // this._commonService._setLoading(false);// this line hidden the loading
            this.prossesInformation();
          },
          error: (response: any) => { console.log(`${response}`) },
          complete: () => {
            this._commonService._setLoading(false);
          }
        });
    }
  }

  /******************************************************
  * Author: Johan McGregor
  * Creation date: 16/07/2022
  * Description: method that dropzone configuration
  *******************************************************/
  get config() {
    dropzoneConfig.url = environment.apiURL + "api/BlobFile/UploadFile";
    return dropzoneConfig
  }

  /******************************************************
  * Author: Johan McGregor
  * Creation date: 16/07/2022
  * Description: method that opens the error modal
  *******************************************************/
  onUploadError(args: any): void {
    this._commonService._setLoading(false);
    var error = "";
    if (args[1] == "You can't upload files of this type.") {
      error =
        "El archivo " + args[0].name + " contiene una extensión no permitida.";
    }

    const datainfo = {
      labelTitile: "Error",
      icon: "cancel",
      textDescription: error != "" ? error : args[1],
      status: "error",
    };

    const dialogRef = this.dialog.open(ModalErrorComponent, {
      data: { datainfo: datainfo },
      minWidth: "478px",
      maxWidth: "478px",
      maxHeight: "277px",
      minHeight: "277px",
    });
    setTimeout(() => dialogRef.close(), 3000);
  }



  /******************************************************
  * Author: Johan McGregor
  * Creation date: 16/07/2022
  * Description: method that dropzone configuration
  *******************************************************/
  onRemove(file: any) {
    this.dropzone.removeAllFiles(true);
    this.files.splice(this.files.indexOf(file), 1);
  }




  /******************************************************
  * Author: Johan McGregor
  * Creation date: 16/07/2022
  * Description: method that shows the modal of success
  *******************************************************/
  onUploadSuccess(args: any): void {
    this._router.navigate(
      ['login']
    );
  }

  prossesInformation() {
    if (this.files.length == 0) {
      this.openModalMessage("No hay archivos que procesar");
    } else {
      this._commonService._setLoading(true);
      this.dropzone.processQueue();
    }
  }

  openModalMessage(messageError: string): void {
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

    setTimeout(() => dialogRef.close(), 3000);
  }

  /***********************************************************************************
  * Author:JnMcGregor
  * Creation date: 29/01/2019
  * Description:add new file
  * ***********************************************************************************/
  onAddedFile(file) {
    let data = {
      file_Name: file.name,
      mime_Type: file.type,
      file_original: file.name
    }
    this.files.push(data);
  }

  /***********************************************************************************
 * Author: JnMcGregor
 * Creation date: 29/01/2019
 * Description:on event send files
 * ***********************************************************************************/
  onSending(file) {
    file[2].append("pk_Mtr_User", this.userInfo.pk_Mtr_User);
  }

  deleteDocument(file) {
    this.files = this.files.filter(item => item.file_Name !== file.file_Name);
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

import { Component, OnDestroy, OnInit } from '@angular/core';
import { DashboardModel } from '@core/model/dashboard.model';
import { CommonService } from '@shared/services/common.service';
import { utiles } from '@util/utiles';
import { Subject, takeUntil } from 'rxjs';
import { DashboardService } from './service/dashboard.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit, OnDestroy {

  /******************************************************
   * Author: Johan McGregor
   * Creation date: 02/07/2022
   * Description: Variable that contains all subscriptions
   *******************************************************/
  private _unsubscribe$ = new Subject<void>();


  public allInvoiceData : Array<any> = new Array<any>();


  constructor(private _commonService: CommonService,
    private _dashboardService: DashboardService) { }

  ngOnInit(): void {
    this.listAllInvoice();
  }


  /******************************************************
   * Author: Johan McGregor
   * Creation date: 02/07/2022
   * Description: Method that list all invoice
   *******************************************************/
  listAllInvoice() {
    this._commonService._setLoading(true); // this line call/show the loading
    let userData = utiles.getCacheObject("userData");
 
    let model: DashboardModel = {
      Pk_Mtr_Invice: 0,
      Creation_User: "",
      Modification_User: "",
      Fk_Mtr_Customer: 0,
      Fk_Mtr_User: userData.pk_Mtr_User,
      Reference_Number: "",
      Invoice_Url: "",
      Active: true
    }
    this._dashboardService
      .listInvoices(model)
      .pipe(takeUntil(this._unsubscribe$))
      .subscribe({
        next: (response: any) => {
          this.allInvoiceData = response;
          this._commonService._setLoading(false);// this line hidden the loading
          console.log(this.allInvoiceData );
          
        },
        error: (response: any) => { debugger; this._commonService._setLoading(false); console.log(`e => ${response}`) },
        complete: () => {
          this._commonService._setLoading(false);
        }
      });
  }

  /******************************************************
   * Author: Johan McGregor
   * Creation date: 02/07/2022
   * Description: Method that cancels subscriptions
   *******************************************************/
  ngOnDestroy() {
    this._unsubscribe$.next();
    this._unsubscribe$.complete();
  }

}

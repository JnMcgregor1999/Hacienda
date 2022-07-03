import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { DashboardModel } from "@core/model/dashboard.model";
import { environment } from "@env/environment";
import { map } from "rxjs";

/******************************************************
   * Author: Johan McGregor
   * Creation date: 02/07/2022
   * Description: Variable that contains the settings http headers
   *******************************************************/
const httpOptions = {
    headers: new HttpHeaders({ "Content-Type": "application/json" }),
};

const methodListInvoice = 'api/Mtr_Invoice/List'

@Injectable({
    providedIn: 'root'
})
export class DashboardService {

    constructor(private _http: HttpClient) { }

    /******************************************************
       * Author: Johan McGregor
       * Creation date: 02/07/2022
       * Description: Method that list all invoice
       *******************************************************/
    listInvoices(data: DashboardModel) {
        const url = environment.apiURL + methodListInvoice;
        return this._http.post<any>(url, data, httpOptions).pipe(
            map((user: any) => {
                return user;
            })
        );
    }
}
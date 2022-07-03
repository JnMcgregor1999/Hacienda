import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "@env/environment";
import { utiles } from "@util/utiles";
import { map } from "rxjs";



/******************************************************
   * Author: Johan McGregor
   * Creation date: 27/06/2022
   * Description: Variable that contains the settings http headers
   *******************************************************/
 const httpOptions = {
    headers: new HttpHeaders({ "Content-Type": "application/json" }),
};

const methodSave= 'api/Mtr_User/Save'

@Injectable({
    providedIn: 'root'
})
export class RegisterService {

    constructor(private _http: HttpClient) { }

    /******************************************************
       * Author: Johan McGregor
       * Creation date: 27/06/2022
       * Description: Method that save new user´s
       *******************************************************/
    save(data: any) {
        const url = environment.apiURL + methodSave;
        return this._http.post<any>(url, data, httpOptions).pipe(
            map((user: any) => {
                utiles.createCacheObject("userData", user);
                return user;
            })
        );
    }
}
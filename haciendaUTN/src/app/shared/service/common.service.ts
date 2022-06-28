import { Injectable } from '@angular/core'
import { HttpHeaders, HttpClient } from '@angular/common/http'
import { Router } from '@angular/router'
import { environment } from '@env/environment'
import { utiles } from '@util/utiles'
import { BehaviorSubject, map } from 'rxjs'

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
}

const methodGetTypeCatalog = 'api/Gbl_Type_Catalog/Get'
const methodListCatalog = 'api/Gbl_Catalog/List'


@Injectable({
    providedIn: 'root'
})

export class CommonService {
    /*------------------------------------------------------------------
    Autor: Johan McGregor
    Fecha de creación: 19/06/2022
    Descripción: Declaration for loading observable
    --------------------------------------------------------------------*/
    private loading = new BehaviorSubject(false)
    loadingService = this.loading.asObservable()


    constructor(private router: Router, private _http: HttpClient) { }

    _setLoading(item: any) {
        setTimeout(() => {
            this.loading.next(item)
        });
    }

    getTypeCatalog(model: any) {
        const url = environment.apiURL + methodGetTypeCatalog;
        return this._http.post<any>(url, model, httpOptions).pipe(
            map((user: any) => {
                utiles.createCacheObject("userData", user);
                return user;
            })
        );
    }


    listCatalog(model: any) {
        const url = environment.apiURL + methodListCatalog;
        return this._http.post<any>(url, model, httpOptions).pipe(
            map((user: any) => {
                utiles.createCacheObject("userData", user);
                return user;
            })
        );
    }

}

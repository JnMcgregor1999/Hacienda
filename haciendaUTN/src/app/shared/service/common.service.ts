import { Injectable } from '@angular/core'
import { HttpHeaders, HttpClient } from '@angular/common/http'
import { Router } from '@angular/router'
import { environment } from '@env/environment'
import { utiles } from '@util/utiles'
import { BehaviorSubject } from 'rxjs'

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
}

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


    constructor(private router: Router, public http: HttpClient) { }

    _setLoading(item: any) {
        setTimeout(() => {
            this.loading.next(item)
        });
    }


}

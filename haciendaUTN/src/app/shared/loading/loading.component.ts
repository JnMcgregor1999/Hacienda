import { Component, OnInit } from '@angular/core';
import { CommonService } from '@shared/services/common.service';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-loading',
  templateUrl: './loading.component.html',
  styleUrls: ['./loading.component.scss']
})
export class LoadingComponent implements OnInit {
  /******************************************************
   * Author: Johan McGregor
   * Creation date: 19/06/2022
   * Description: Variable that contains all subscriptions
   *******************************************************/
  private unsubscribe$ = new Subject<void>();

  /******************************************************
   * Author: Johan McGregor
   * Creation date: 19/06/2022
   * Description: Variable that manage loading
   *******************************************************/
  public loading: boolean = true;

  constructor(private _common: CommonService) { }

  ngOnInit(): void {
    setTimeout(() => {
      this._common.loadingService
        .pipe(takeUntil(this.unsubscribe$))
        .subscribe(data => {
          this.loading = data;
        });
    });
  }

}

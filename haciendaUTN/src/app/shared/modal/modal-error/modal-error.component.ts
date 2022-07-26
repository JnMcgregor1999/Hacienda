import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-modal-error',
  templateUrl: './modal-error.component.html',
  styleUrls: ['./modal-error.component.scss']
})
export class ModalErrorComponent implements OnInit {
  message: string = "";

  icon: any;
  labelTitile: string;
  textDescription: any;
  status: any;


  constructor(
    public dialogRef: MatDialogRef<ModalErrorComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    if (this.data !== null && this.data !== undefined) {
      this.labelTitile = this.data.datainfo.labelTitile;
      this.icon = this.data.datainfo.icon;
      this.textDescription = this.data.datainfo.textDescription;
      this.status = this.data.datainfo.status;
    } else {
      this.labelTitile = 'Lo sentimos parece que algo anda mal';
    }
  }

  ngOnInit(): void {
  }

}

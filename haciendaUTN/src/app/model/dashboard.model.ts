/*******************************************************
  * Author: Johan McGregor
  * Creation date: 02/07/2022
  * Description: Model to invoice object
*********************************************************/
export class DashboardModel {
    Pk_Mtr_Invice: number;
    Creation_User: string;
    Modification_User: string;
    Fk_Mtr_Customer: number;
    Fk_Mtr_User: number;
    Reference_Number: string;
    Invoice_Url: string;
    Active: boolean;

    constructor() {
        this.Pk_Mtr_Invice = 0;
        this.Creation_User = "";
        this.Modification_User = "";
        this.Fk_Mtr_Customer = 0;
        this.Fk_Mtr_User = 0;
        this.Reference_Number = "";
        this.Invoice_Url = "";
        this.Active = true;
    }
}
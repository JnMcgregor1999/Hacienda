/*******************************************************
  * Author: Johan McGregor
  * Creation date: 27/06/2022
  * Description: Model to user register object
*********************************************************/

export class RegisterModel {
  pk_Mtr_User?: number;
  fk_Catalog_Identification_Type: number;
  identification: string;
  full_Name: string;
  email: string;
  password: string;
  active: boolean;

  constructor() {
    this.pk_Mtr_User = 0;
    this.fk_Catalog_Identification_Type = 0;
    this.identification = "";
    this.full_Name = "";
    this.email = "";
    this.active = false;
    this.password = "";
  }
}
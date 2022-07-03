/*******************************************************
  * Author: Johan McGregor
  * Creation date: 19/06/2022
  * Description: Model to login object
*********************************************************/

export class LoginModel {
  Fk_Catalog_Identification_Type: number;
  Identification: string;
  Full_Name: string;
  Email: string;
  Password: string;
  Active: boolean;

  constructor() {
    this.Fk_Catalog_Identification_Type = 0;
    this.Identification = "";
    this.Full_Name = "";
    this.Email = "";
    this.Password = "";
    this.Active = true;
  }
}
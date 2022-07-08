/*******************************************************
  * Author: Johan McGregor
  * Creation date: 27/06/2022
  * Description: Model to user type catalog object
*********************************************************/

export class Type_CatalogModel {
  pk_Gbl_Type_Catalog?: number;
  creation_User?: string;
  modification_User?: string;
  search_Key?: string;
  name?: string;
  description?: string;
  active?: boolean;

  constructor() {
    this.pk_Gbl_Type_Catalog = 0;
    this.creation_User = "";
    this.modification_User = "";
    this.search_Key = "";
    this.name = "";
    this.description = "";
    this.active = false;
  }
}


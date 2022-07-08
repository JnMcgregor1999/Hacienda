
export class utiles {
    constructor() { }

    /*-------------------------------------------------------------------------------------
      Name: Johan McGregor
      Date: 19/06/2022
      Description: Method that create a specific local storage
      Params: Name of the localStorage you want to delete
      --------------------------------------------------------------------------------------*/
    static deleteCache(name: string) {
        localStorage.removeItem(name);
    }

    /*-------------------------------------------------------------------------------------
      Name: Johan McGregor
      Date: 19/06/2022 
      Description: Method that clear all local storage
      Params: 
      --------------------------------------------------------------------------------------*/
    static clearCache() {
        localStorage.removeItem("userData");
    }

    /*-------------------------------------------------------------------------------------
      Name: Johan McGregor
      Date: 19/06/2022
      Description: Method that create a specific local storage
      Params: Name of the localStorage you want to create and the information you want to store
      --------------------------------------------------------------------------------------*/
    static createCacheObject(name: string, data: any) {
        localStorage.setItem(name, JSON.stringify(data));
    };

    /*-------------------------------------------------------------------------------------
     Name: Johan McGregor
     Date: 19/06/2022
     Description: Method that obtains a specific local storage
     Params: Name of the localStorage you want to get
     --------------------------------------------------------------------------------------*/
    static getCacheObject(name: string) {
        let data: any = localStorage.getItem(name);
        let dataObject = JSON.parse(data);

        return dataObject;
    };
}
/*******************************************************
  * Author: Johan McGregor
  * Creation date: 19/06/2022
  * Description: Model to login object
*********************************************************/

export class LoginModel {
    password: string;
    userName: string;
    isLogin: boolean;

    constructor() {
        this.password = "";
        this.userName = "";
        this.isLogin = false;
    }
}
import {LoginInformacije} from "./login-informacije";

export class AutentifikacijaHelper {

  static setLoginInfo(x: LoginInformacije | null):void
  {
    if (x==null)
      x = new LoginInformacije();
    localStorage.setItem("autentifikacija-token", JSON.stringify(x));
  }

  static getLoginInfo():LoginInformacije
  {
    let x = localStorage.getItem("autentifikacija-token");
    if (x==="")
      return new LoginInformacije();

    try {
      let loginInformacije:LoginInformacije = JSON.parse(x as string);
      if (loginInformacije==null)
        return new LoginInformacije();
      return loginInformacije;
    }
    catch (e)
    {
      return new LoginInformacije();
    }
  }
}
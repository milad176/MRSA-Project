import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor() { }

  authUser(user: any){
    let userArray=[];
    if(localStorage.getItem("users")){
      userArray= JSON.parse(localStorage.getItem("users"));
    }
    return userArray.find(u=> u.userName === user.userName && u.password === user.password);
  }
}

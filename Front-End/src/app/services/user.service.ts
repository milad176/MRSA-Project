import { Injectable } from '@angular/core';
import { IUser } from '../model/IUser';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor() { }


  addUser(user: IUser){
    let users=[];
    if(localStorage.getItem("users")){
      users= JSON.parse(localStorage.getItem("users"));
      users=[user, ...users];
    }else{
      users=[user];
    }
    localStorage.setItem("users", JSON.stringify(users));
  }
}

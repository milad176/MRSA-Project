import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { IUser } from 'src/app/model/IUser';
import { UserService } from 'src/app/services/user.service';
import { AlertifyService } from 'src/app/services/alertify.service';

@Component({
  selector: 'app-user-register',
  templateUrl: './user-register.component.html',
  styleUrls: ['./user-register.component.css'],
})
export class UserRegisterComponent implements OnInit {
  registrationForm: FormGroup;
  user:IUser;
  userSubmitted:boolean=false;
  constructor( private userService: UserService, private alertify: AlertifyService) {}

  ngOnInit(): void {
    this.registrationForm = new FormGroup({
      userName: new FormControl(null, [Validators.required, Validators.minLength(5)]),
      email: new FormControl(null, [Validators.required, Validators.email]),
      password: new FormControl(null, [Validators.required, Validators.minLength(8)]),
      confirmPassword: new FormControl(null, Validators.required),
      mobile: new FormControl(null, [Validators.required, Validators.maxLength(10)])
    }, this.passwordMatchingValidator);
  }

  passwordMatchingValidator(fg: FormGroup): Validators{
    return fg.get("password").value === fg.get("confirmPassword").value ? null :
    {notmatched:true}
  }

  //Getter method for all form controls
  get userName(){
     return this.registrationForm.get('userName') as FormControl;
  }
  get email(){
     return this.registrationForm.get('email') as FormControl;
  }
  get password(){
     return this.registrationForm.get('password') as FormControl;
  }
  get confirmPassword(){
     return this.registrationForm.get('confirmPassword') as FormControl;
  }
  get mobile(){
     return this.registrationForm.get('mobile') as FormControl;
  }


  onSubmit() {
    this.userSubmitted = true;
    if(this.registrationForm.valid){
      // this.user= Object.assign(this.user, this.registrationForm.value);
      console.log("user:  ",this.user);
      this.userService.addUser(this.userData());
      this.registrationForm.reset();
      this.userSubmitted = false;
      this.alertify.success('congratulation, You are successfully registered');
    }else{
      this.alertify.error('Please provide the required felids');
    }
  }

  userData(): IUser{
    return this.user = {
      userName: this.userName.value,
      email: this.email.value,
      password: this.password.value,
      mobile: this.mobile.value
    };
  }

}

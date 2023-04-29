import { Component } from '@angular/core';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  constructor(private authService: AuthService) { }

  username: any;
  password: any;
  confirmpassword: any;

register() {
  this.authService.register( {
    username: this.username,
    password: this.password,
    confirmpassword:  this.confirmpassword
  })
}
}

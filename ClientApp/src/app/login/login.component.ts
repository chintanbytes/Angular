import { Component } from '@angular/core';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  constructor(private authService: AuthService) {
  }

  username : string ="";
  password : string ="";
  show: boolean= false;

  login() {
    return this.authService.login({
      username: this.username,
      password: this.password
    });
  }
}

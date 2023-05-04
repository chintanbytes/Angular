import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) {
   }

  user: any = null;

  async loadUser(){
    const user = await firstValueFrom(
    this.http.get<any>('/api/users')
    )

    if('user_id' in user){
      this.user = user;
    }

    return user;
 }

  login(loginForm: any){
    return this.http.post<any>('/api/users/login', loginForm, {withCredentials: true})
    .subscribe(_ => {
      this.loadUser();
    });
  }

  register(registerForm: any){
    return this.http.post<any>('/api/users/register', registerForm, {withCredentials: true})
    .subscribe(_ => {
    });
  }

  logout(){
    return this.http.get<any>('/api/users/logout')
    .subscribe(_ => this.user = null );
  }
}

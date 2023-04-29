import { Component } from '@angular/core';
import { AuthService } from './auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [AuthService]
})
export class AppComponent {
  title = 'ClientApp';

  constructor(private authService: AuthService) {

  }

  username: any = "";

  async ngOnInit() {
    const user = await this.authService.loadUser();
    this.username = user["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"]
  }

  logout(){
    return this.authService.logout();
  }
}



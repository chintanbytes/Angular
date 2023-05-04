import { Component } from '@angular/core';
import { AuthService } from './auth.service';
import { Observable, interval } from 'rxjs';
import { switchMap,map } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'ClientApp';

  constructor(private authService: AuthService) {

  }

  username$!: Observable<string>;

  async ngOnInit() {

    this.username$ = interval(5000).pipe(
      switchMap(async () => {
        const user = await this.authService.loadUser();
        return user["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
      })
    );
  }

  logout(){
    return this.authService.logout();
  }
}



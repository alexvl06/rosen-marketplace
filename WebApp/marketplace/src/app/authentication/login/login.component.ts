import { Component, OnInit} from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  constructor(
    private router:Router
  ) { }


  onSubmit(){
    let input:HTMLInputElement = document.getElementById('user-name') as HTMLInputElement
    sessionStorage.setItem('username', input.value)
    this.router.navigate(['offer/list'])

  }
}

import { Component, OnInit} from '@angular/core';
import { Router } from '@angular/router';
import {FormControl, Validators} from '@angular/forms'

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  input = new FormControl('', [Validators.required])
  constructor(
    private router:Router
  ) { }


  onSubmit(){
    sessionStorage.setItem('username',this.input.value)
    console.log(sessionStorage.getItem('username'))
    //this.router.navigate(['offer/list'])

  }
}

import { Component, OnInit} from '@angular/core';
import { Router } from '@angular/router';
import {FormControl, Validators} from '@angular/forms'

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  input = new FormControl('',[Validators.required])
  constructor(
    private router:Router
  ) { }

  ngOnInit(): void {
  }

  onSubmit(){
    this.router.navigate(['offer/list'])
  }
}

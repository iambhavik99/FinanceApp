import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm!: FormGroup;

  constructor(private router: Router) {

  }

  ngOnInit(): void {
    this.initializeLoginForm();
  }

  initializeLoginForm() {
    this.loginForm = new FormGroup({
      username: new FormControl("", Validators.required),
      email: new FormControl("", Validators.required),
      password: new FormControl("", Validators.required)
    });
  }

  isLoginFormValid() {
    return this.loginForm.invalid
      || !this.loginForm.get("username")?.value?.trim()
      || !this.loginForm.get("email")?.value?.trim()
      || !this.loginForm.get("password")?.value?.trim();
  }

  login() {
    console.log(this.loginForm.value);
    localStorage.setItem('isLoggedInUser', 'true');
    this.router.navigate(['/home']);
  }





}

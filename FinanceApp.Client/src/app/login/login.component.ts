import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CryptoService } from '../services/crypto/crypto.service';
import { Base64 } from 'js-base64';
import { ApiService } from '../services/api/api.service';
import { UserLoginRequestMedia, UserRequestMedia } from '../common/models/users/user.model';
import { lastValueFrom } from 'rxjs';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  state: 'LOGIN' | 'SIGNUP' = 'LOGIN';
  loginForm!: FormGroup;

  constructor(private router: Router,
    private cryptoService: CryptoService,
    private apiService: ApiService
  ) {

  }

  ngOnInit(): void {
    this.initializeLoginForm();
  }

  initializeLoginForm() {
    this.loginForm = new FormGroup({
      username: new FormControl("", Validators.required),
      email: new FormControl(""),
      password: new FormControl("", Validators.required)
    });
  }

  onChangeStatus() {
    this.state = this.state === 'LOGIN' ? 'SIGNUP' : 'LOGIN';
    this.loginForm.reset();
  }


  isLoginFormValid() {

    if (this.state == 'LOGIN') {
      return !this.loginForm.get("username")?.value?.trim()
        || !this.loginForm.get("password")?.value?.trim();
    }

    return !this.loginForm.get("username")?.value?.trim()
      || !this.loginForm.get("email")?.value?.trim()
      || !this.loginForm.get("password")?.value?.trim();
  }


  async onSubmit() {

    try {
      let requestMedia;
      let endpointURL;

      const username = this.loginForm.get("username")?.value?.trim();
      const email = this.loginForm.get("email")?.value?.trim()
      const password = this.loginForm.get("password")?.value;

      const aesKey = await this.cryptoService.importKey(environment.Key);
      const encryptedPassword = await this.cryptoService.encrypt(aesKey, environment.IV, password);

      if (this.state === 'LOGIN') {
        requestMedia = { username, password: encryptedPassword } as UserLoginRequestMedia;
        endpointURL = 'api/users/login';
      }
      else {
        requestMedia = { username, email, password: encryptedPassword } as UserRequestMedia;
        endpointURL = 'api/users/signup';
      }

      const response = await lastValueFrom(this.apiService.post(endpointURL, requestMedia));

      if (this.state === 'LOGIN') {
        localStorage.setItem('userId', response.id)
        this.router.navigate(['/home']);
      }
      else {
        this.loginForm.reset();
        this.state = 'LOGIN';
      }

    }
    catch (error: any) {
      console.error(error);
    }

  }




}

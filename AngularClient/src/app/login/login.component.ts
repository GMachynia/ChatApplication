import { ToastrService } from 'ngx-toastr';
import { UserService } from '../shared/user.service';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  formModel = {
    UserName: '',
    Password: ''
  }
  constructor(private userService: UserService, private router: Router, private toastr: ToastrService) { }

  ngOnInit() {
    if (localStorage.getItem('token') != null)
      this.router.navigateByUrl('/chat');
  }

  onSubmit(form: NgForm) {
    this.userService.login(form.value).subscribe(
      (response: any) => {
        localStorage.setItem('userName', this.formModel.UserName);
        localStorage.setItem('token', response.token);
        this.router.navigateByUrl('/chat');
        this.toastr.success(this.formModel.UserName, 'Welcome');
      },
      error => {
        if (error.status == 400)
          this.toastr.error('Incorrect username or password.', 'Authentication failed.');
        else
          console.log(error);
      }
    );
  }
}
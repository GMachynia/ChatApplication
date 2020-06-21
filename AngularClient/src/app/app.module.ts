import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RegistrationComponent } from './registration/registration.component';
import { UserService } from './shared/user.service';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';
import { ChatComponent } from './chat/chat.component';
import { MatButtonModule, MatToolbarModule} from '@angular/material';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { LoginComponent } from './login/login.component';
import { ChatService } from './shared/chat.service';
import { AuthInterceptor } from './shared/auth/auth.interceptor';
import { LocalizationInterceptor } from './shared/localization/localization.interceptor';


@NgModule({
  declarations: [
    AppComponent,
    RegistrationComponent,
    ChatComponent,
    LoginComponent, 
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      progressBar: true
    }),
    MatButtonModule,   
    MatToolbarModule,
    NoopAnimationsModule,
    FormsModule

       
  ],
  providers: [
    UserService, 
    {
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true
  }, 
  {
    provide: HTTP_INTERCEPTORS,
    useClass: LocalizationInterceptor,
    multi: true
  },
  ChatService],
  bootstrap: [AppComponent]
})
export class AppModule { }

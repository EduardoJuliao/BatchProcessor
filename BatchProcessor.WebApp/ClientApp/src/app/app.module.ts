import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { ProcessorService } from 'src/app/services/processor.service';

import { ProcessComponent } from './components/process/process.component';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';

import { NotificationService } from './services/notification.service';
import { LastProcessPageComponent } from './pages/last-process/last-process.component';
import { ProcessPageComponent } from './pages/process/process.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    ProcessComponent,
    ProcessPageComponent,
    LastProcessPageComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: ProcessPageComponent, pathMatch: 'full' },
      { path: 'last-process', component: LastProcessPageComponent },
    ]),
  ],
  providers: [NotificationService, ProcessorService],
  bootstrap: [AppComponent]
})
export class AppModule { }

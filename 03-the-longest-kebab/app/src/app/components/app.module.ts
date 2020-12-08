import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { LongestKebabComponent } from './longest-kebab/longest-kebab.component';

@NgModule({
  declarations: [
    AppComponent,
    LongestKebabComponent
  ],
  imports: [
    BrowserModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

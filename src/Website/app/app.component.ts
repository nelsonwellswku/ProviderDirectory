import { Component } from '@angular/core';

@Component({
  selector: 'my-app',
  template: `
  <div class='container'>
    <site-navigation></site-navigation>
    <router-outlet></router-outlet>  
  </div>
`,
})
export class AppComponent  { name = 'Angular'; }

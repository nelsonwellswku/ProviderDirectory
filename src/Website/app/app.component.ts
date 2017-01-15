import { Component } from '@angular/core';

@Component({
  selector: 'my-app',
  template: `
  <h1>Provider Directory</h1>
  <router-outlet></router-outlet>  
`,
})
export class AppComponent  { name = 'Angular'; }

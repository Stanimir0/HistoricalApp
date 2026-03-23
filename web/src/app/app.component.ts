import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  readonly appName = 'Historical App';
  readonly version = 'v1.0';

  // Replace these with your real hosted files/pages.
  readonly downloads = {
    androidApk: '#',
    windows: '#',
    iosInfo: '#'
  };
}

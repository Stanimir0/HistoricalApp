import { Component, OnInit } from '@angular/core';

type ReleaseLinks = {
  version: string;
  androidApk: string;
  windows: string;
  iosInfo: string;
};

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  readonly appName = 'Historical App';
  version = 'v1.0';

  // Fallback values used if releases.json cannot be loaded.
  downloads: ReleaseLinks = {
    version: this.version,
    androidApk: '#',
    windows: '#',
    iosInfo: '#'
  };

  async ngOnInit(): Promise<void> {
    try {
      const response = await fetch('/releases.json', { cache: 'no-store' });
      if (!response.ok) {
        return;
      }

      const data = (await response.json()) as Partial<ReleaseLinks>;
      this.version = data.version ?? this.version;
      this.downloads = {
        version: this.version,
        androidApk: data.androidApk ?? '#',
        windows: data.windows ?? '#',
        iosInfo: data.iosInfo ?? '#'
      };
    } catch {
      // Keep fallback links if config file is unavailable.
    }
  }
}

import { bootstrapApplication } from '@angular/platform-browser';
import { App } from './app/app'; // ✅ matches app.ts
import { routes } from './app/app.routes'; // ✅ matches app.routes.ts
import { provideRouter } from '@angular/router';
import { provideHttpClient } from '@angular/common/http';

bootstrapApplication(App, {
  providers: [
    provideRouter(routes),
    provideHttpClient()
  ]
}).catch((err) => console.error(err));

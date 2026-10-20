import { ApplicationConfig, provideBrowserGlobalErrorListeners } from '@angular/core';

// Zoneless change detection is the default since Angular v21, so no zone provider is needed
export const appConfig: ApplicationConfig = {
  providers: [provideBrowserGlobalErrorListeners()]
};

import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl: 'http://localhost:4200/',
    name: 'ModuleTest',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44393/',
    redirectUri: baseUrl,
    clientId: 'ModuleTest_App',
    responseType: 'code',
    scope: 'offline_access ModuleTest',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:44393',
      rootNamespace: 'ModuleTest',
    },
    ModuleTest: {
      url: 'https://localhost:44330',
      rootNamespace: 'ModuleTest',
    },
  },
} as Environment;

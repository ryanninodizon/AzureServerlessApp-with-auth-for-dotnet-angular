export const environment = {
    production: false,
    msalConfig: {
        auth: {
            clientId: '<client-id>',
            authority: 'https://login.microsoftonline.com/<tenant-id>'
        }
    },
    apiConfig: {
        scopes: ['user.read'],//This scopes should be added in "EntraID > App Registrations > API Permissions"
        uri: 'https://graph.microsoft.com/v1.0/me',
        backendapi:''
    }
};

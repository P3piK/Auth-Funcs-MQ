# Auth-Funcs-MQ
Auth-Funcs-MQ is a repository of Auth-Funcs Notification Service. 
The App utilizes `Azure Service Bus` to receive messages from API-Project which converts into _EmailMessage_'s and sends utilizing `Azure Communication Services`.
`Azure WebJobs` allows Auth-Funcs-MQ to run as a _Background Service_ for the API process.
All of the Azure components is connected and authorized using `Azure Key Vault`

# Key components
- Azure Key Vault
- Azure App Service with WebJobs
- Azure Service Bus
- Azure Communication Services

# Email Template
Email template was brought by open source HTML email template on GitHub made by leemunroe
https://github.com/leemunroe/responsive-html-email-template

# Main Repository
https://github.com/P3piK/Auth-Funcs

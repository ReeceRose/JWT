# Secrets

All of the secrets needed for this project will be stored in [Azure Key Vault](https://azure.microsoft.com/en-ca/services/key-vault/). It provides great security, reduces complexity during deployment with docker, and comes at a very low cost.

Before we start, make sure you have a Azure account. You can get one [here](https://azure.microsoft.com/en-us/free/)

#### Step 1. Active Directory and app Registrations
1. From your Azure portal, search for and open Azure Active Directory

    <img src="https://static.reecerose.com/images/projects/JWTExample/Secrets/ad-search.png" alt="Searching for Azure Active Directory" height="300px">
 
2. Click on App registrations

    <img src="https://static.reecerose.com/images/projects/JWTExample/Secrets/app-registrations.png" alt="App registrations" height="300px">
    
3. Create a new app, I used the name JWT. *__Note: Sign-on URL does not matter__*

    <img src="https://static.reecerose.com/images/projects/JWTExample/Secrets/app-registrations-new.png" alt="New app registration" height="300px">
    
4. Click on create, then settings, and then keys
5. Create a new key with a description of JWT and set to never expire
6. Once you create the key, a value will appear. **COPY THIS, THIS IS THE ONLY TIME YOU WILL BE ABLE TO SEE THIS VALUE**
    
    <img src="https://static.reecerose.com/images/projects/JWTExample/Secrets/app-registration-created.png" alt="App registration created" height="300px">

#### Step 2. Azure Key Vault
1. From your Azure portal, search for and open Key Vault
    
    <img src="https://static.reecerose.com/images/projects/JWTExample/Secrets/key-vault-search.png" alt="Key vault search" height="300px">
    
2. Click create key vault and fill in the required information
    
    <img src="https://static.reecerose.com/images/projects/JWTExample/Secrets/key-vault-create.png" alt="Key vault created" height="300px">
    
3. Click on Access Policy, then add new
    
    <img src="https://static.reecerose.com/images/projects/JWTExample/Secrets/key-vault-policy-new.png" alt="New access policy" height="300px">
    
4. Change Select Principal to the App registration you had previosuly created
    
    <img src="https://static.reecerose.com/images/projects/JWTExample/Secrets/key-vault-policy-new-principal.png" alt="New principal" height="300px">
    
5. For all permissions, select Get and List
    
    <img src="https://static.reecerose.com/images/projects/JWTExample/Secrets/key-vault-policy-new-permissions.png" alt="Set permissions" height="300px">

#### Step 3. Add all secrets
1. Click on Secrets
    
    <img src="https://static.reecerose.com/images/projects/JWTExample/Secrets/key-vault-secrets.png" alt="Secrets" height="300px">
2. Create a new key
    
    <img src="https://static.reecerose.com/images/projects/JWTExample/Secrets/key-vault-secrets-create.png" alt="Secrets" height="300px">
3. Repeat step two until you have the following keys created. *__Note: ConnectionStrings--Postgres must be a valid connection string. All other secrets can be random if you wish__*
    
    <img src="https://static.reecerose.com/images/projects/JWTExample/Secrets/key-vault-secrets-list.png" alt="Secrets" height="300px">

---

You should be all setup and ready to run the web API. If you have any questions or this is not working, please feel free to contact me [here](https://reecerose.com/)

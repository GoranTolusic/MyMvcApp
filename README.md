# MyMvcApp (Simple minimalistic mvc crud app on top of asp.net core framework)

1. Create .env file and fill it with environment variables (see .env.example)
2. Setup connection strings in appsettings.json (make sure db creds are valid and corresponding to those in .env)
3. Make sure you have docker installed
4. Run local-setup.sh

local-setup.sh will

1. Pull and run postgreSql database

2. Build Docker image for mvc application

3. Run mvc application (asp.net mvc)



When the application starts, the database is automatically created with the name created in the appsettings.json file and the migration files are started to create the tables in the database.

Application should be available at port 8080 on localhost
# Commands

## Setup
* Create API project
```
dotnet new webapi -o api
```

* Run API
```
dotnet watch run
```

* Migrations
```
dotnet tool install --global dotnet-ef --version 8.0.0 
dotnet ef migrations add Init
dotnet ef database update
```

* Remove Migrations
```
ef migrations remove
```

## DataBase
* Database update
```
dotnet ef database update
```

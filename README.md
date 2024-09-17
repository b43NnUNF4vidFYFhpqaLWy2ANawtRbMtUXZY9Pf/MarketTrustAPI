# MarketTrustAPI

## Table of Contents

- [MarketTrustAPI](#markettrustapi)
  - [Table of Contents](#table-of-contents)
  - [Development](#development)
    - [Database](#database)

## Development

```sh
$ cd src/
$ dotnet watch run
```

### Database

```sh
$ cd src/
$ dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost,1433;Database=markettrust;User Id=SA;Password=${devPassword};Encrypt=false;TrustServerCertificate=True"
$ dotnet ef migrations add init
$ dotnet ef database update
$ dotnet ef database drop
$ dotnet ef migrations remove
```

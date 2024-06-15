dotnet ef migrations add init --context AuthDbContext -s .\AuthService.Main\ -p .\AuthService.DataAccess\
 
dotnet ef database update --context AuthDbContext -s .\AuthService.Main\ -p .\AuthService.DataAccess\
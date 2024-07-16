$imageName = "postgres:latest"

$userName = "postgres"
$password = "1"

docker run -d --name shavaPostgres -p 5432:5432 -e POSTGRES_USER=$userName -e POSTGRES_PASSWORD=$password $imageName

Start-Sleep -Seconds 1

dotnet ef database update -s .\OrderService\OrderService.Api\ -p .\OrderService\OrderService.DataAccess\

dotnet ef database update -s .\AuthService\AuthService.Main\   -p .\AuthService\AuthService.DataAccess\

dotnet ef database update -s .\CourierService\CourierService.API\   -p .\CourierService\CourierService.DataAccess\

dotnet ef database update -s .\StoreService\StoreService.Main\  -p .\StoreService\StoreService.DataAccess\

dotnet ef database update -s .\UserService\UserService.Main\   -p .\UserService\UserService.DataAccess\

cd .\Kafka
docker-compose up
cd ..
 
docker ps
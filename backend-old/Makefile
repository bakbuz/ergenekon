.PHONY: m efi up down

efi:
	dotnet tool uninstall --global dotnet-ef
	dotnet tool install --global dotnet-ef

m:
	del /s/q C:\Dev\ergenekon\src\Ergenekon.Infrastructure\Persistence\Migrations\*
	cd C:\Dev\ergenekon\src\Ergenekon.Infrastructure
	dotnet ef migrations add InitialCreate --project src\Ergenekon.Infrastructure\Ergenekon.Infrastructure.csproj
	dotnet ef database drop -f
	dotnet ef database update

up:
	docker container start sql-server
	docker container start minio1
	docker container start redis
	docker-compose up

down:
	docker-compose down
	docker image remove ergenekon.host
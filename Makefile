.PHONY: m

m:
	del /s/q C:\Dev\ergenekon\src\Ergenekon.Infrastructure\Persistence\Migrations\*
	cd C:\Dev\ergenekon\src\Ergenekon.Infrastructure
	dotnet ef migrations add InitialCreate
	dotnet ef database drop -f
	dotnet ef database update

dotnet new ca-usecase --name CreateTodoList --feature-name TodoLists --usecase-type command --return-type int

dotnet new ca-usecase -n GetTodos -fn TodoLists -ut query -rt TodosVm


dotnet new ca-usecase -n GetCurrentUser -fn User -ut query -rt UserVm
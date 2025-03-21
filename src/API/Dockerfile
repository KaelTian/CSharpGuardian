# 构建阶段
FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy AS build
WORKDIR /src
COPY . .
RUN dotnet restore "CSharpGuardian.sln"
RUN dotnet publish "src/API/CSharpGuardian.API.csproj" -c Release -o /app/publish

# 运行阶段
FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CSharpGuardian.API.dll"]
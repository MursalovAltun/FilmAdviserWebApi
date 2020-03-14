FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 5007

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Common/Common.WebApiCore/Common.WebApiCore.csproj", "src/Common/Common.WebApiCore/"]
COPY ["src/Common/Common.DTO/Common.DTO.csproj", "src/Common/Common.DTO/"]
COPY ["src/Common/Common.IdentityManagementCore/Common.IdentityManagementCore.csproj", "src/Common/Common.IdentityManagementCore/"]
COPY ["src/Common/Common.Services.Infrastructure/Common.Services.Infrastructure.csproj", "src/Common/Common.Services.Infrastructure/"]
COPY ["src/Common/Common.Entities/Common.Entities.csproj", "src/Common/Common.Entities/"]
COPY ["src/Common/Common.DIContainerCore/Common.DIContainerCore.csproj", "src/Common/Common.DIContainerCore/"]
COPY ["src/Common/Common.DataAccess.EFCore/Common.DataAccess.EFCore.csproj", "src/Common/Common.DataAccess.EFCore/"]
COPY ["src/Common/Common.Services/Common.Services.csproj", "src/Common/Common.Services/"]
COPY ["src/Common/Common.Exceptions/Common.Exceptions.csproj", "src/Common/Common.Exceptions/"]
COPY ["src/Common/Common.Constants/Common.Constants.csproj", "src/Common/Common.Constants/"]
RUN dotnet restore "src/Common/Common.WebApiCore/Common.WebApiCore.csproj"
COPY . .
WORKDIR "/src/src/Common/Common.WebApiCore"
RUN dotnet build "Common.WebApiCore.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Common.WebApiCore.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Common.WebApiCore.dll"]
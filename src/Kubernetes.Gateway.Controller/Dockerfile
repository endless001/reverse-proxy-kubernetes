FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Kubernetes.Gateway.Controller/Kubernetes.Gateway.Controller.csproj", "Kubernetes.Gateway.Controller/"]
COPY ["src/Kubernetes.Gateway/Kubernetes.Gateway.csproj", "Kubernetes.Gateway/"]
RUN dotnet restore "src/Kubernetes.Gateway.Controller/Kubernetes.Gateway.Controller.csproj"
COPY . .
WORKDIR "/src/Kubernetes.Gateway.Controller"
RUN dotnet build "Kubernetes.Gateway.Controller.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kubernetes.Gateway.Controller.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kubernetes.Gateway.Controller.dll"]

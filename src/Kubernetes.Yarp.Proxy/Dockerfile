FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Kubernetes.Yarp.Proxy/Kubernetes.Yarp.Proxy.csproj", "Kubernetes.Yarp.Proxy/"]
COPY ["src/Kubernetes.Gateway/Kubernetes.Gateway.csproj", "Kubernetes.Gateway/"]
RUN dotnet restore "src/Kubernetes.Yarp.Proxy/Kubernetes.Yarp.Proxy.csproj"
COPY . .
WORKDIR "/src/Kubernetes.Yarp.Proxy"
RUN dotnet build "Kubernetes.Yarp.Proxy.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kubernetes.Yarp.Proxy.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kubernetes.Yarp.Proxy.dll"]

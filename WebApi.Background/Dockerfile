#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
#FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
#WORKDIR /src
#COPY ["WebApi.Background/WebApi.Background.csproj", "WebApi.Background/"]
#COPY ["WebApi/WebApi.csproj", "WebApi/"]
#COPY ["Basic/Basic.csproj", "Basic/"]
#COPY ["Adai.Standard/Adai.Standard.csproj", "Adai.Standard/"]
#COPY ["Adai.Core/Adai.Core.csproj", "Adai.Core/"]
#RUN dotnet restore "WebApi.Background/WebApi.Background.csproj"
#COPY . .
#WORKDIR "/src/WebApi.Background"
#RUN dotnet build "WebApi.Background.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "WebApi.Background.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "WebApi.Background.dll"]

#���뾵��
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
#�趨����Ŀ¼
WORKDIR /app
#�������г���ʹ�õĶ˿ڣ�һ��Ҫ�ͳ�������ʹ�õĶ˿ڶ�Ӧ��
EXPOSE 80
EXPOSE 443
#�����ļ�������Ŀ¼
COPY . .
#��������
ENTRYPOINT ["dotnet", "WebApi.Background.dll"]

#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
#FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
#WORKDIR /src
#COPY ["WebApi.Common/WebApi.Common.csproj", "WebApi.Common/"]
#COPY ["WebApi/WebApi.csproj", "WebApi/"]
#COPY ["Basic/Basic.csproj", "Basic/"]
#COPY ["Adai.Standard/Adai.Standard.csproj", "Adai.Standard/"]
#COPY ["Adai.Core/Adai.Core.csproj", "Adai.Core/"]
#RUN dotnet restore "WebApi.Common/WebApi.Common.csproj"
#COPY . .
#WORKDIR "/src/WebApi.Common"
#RUN dotnet build "WebApi.Common.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "WebApi.Common.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "WebApi.Common.dll"]

#���뾵��
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
RUN apt-get update && apt-get install -y libgdiplus
#�趨����Ŀ¼
WORKDIR /app
#�������г���ʹ�õĶ˿ڣ�һ��Ҫ�ͳ�������ʹ�õĶ˿ڶ�Ӧ��
EXPOSE 80
EXPOSE 443
#�����ļ�������Ŀ¼
COPY . .
#��������
ENTRYPOINT ["dotnet", "WebApi.Common.dll"]
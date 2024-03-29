#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

#设置时区
RUN /bin/cp /usr/share/zoneinfo/Asia/Shanghai /etc/localtime && echo 'Asia/Shanghai' >/etc/timezone

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["WebApi.Background/WebApi.Background.csproj", "WebApi.Background/"]
COPY ["WebApi/WebApi.csproj", "WebApi/"]
COPY ["Basic/Basic.csproj", "Basic/"]
COPY ["Adai.Standard/Adai.Standard.csproj", "Adai.Standard/"]
COPY ["Adai.Core/Adai.Core.csproj", "Adai.Core/"]
RUN dotnet restore "WebApi.Background/WebApi.Background.csproj"
COPY . .
WORKDIR "/src/WebApi.Background"
RUN dotnet build "WebApi.Background.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApi.Background.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi.Background.dll"]

##如果是使用发布后的文件创建镜像
##复制文件到工作目录
#COPY . /app
##启动程序
#ENTRYPOINT ["dotnet", "WebApi.Background.dll"]

#buildnode=====================================
FROM node:latest as buildnode
WORKDIR /app/MvcFrameworkWeb
# add `/app/MvcFrameworkWeb/node_modules/.bin` to $PATH
ENV PATH /app/MvcFrameworkWeb/node_modules/.bin:$PATH
# install and cache app dependencies
COPY MvcFrameworkWeb/package.json /app/MvcFrameworkWeb/package.json
RUN npm install
#RUN npm install @vue/cli@3.7.0 -g
COPY . .

#builddotnet=====================================
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS builddotnet
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY MvcFrameworkWeb/*.csproj ./MvcFrameworkWeb/
COPY MvcFrameworkDbl/*.csproj ./MvcFrameworkDbl/
COPY MvcFrameworkBll/*.csproj ./MvcFrameworkBll/
COPY MvcFrameworkCml/*.csproj ./MvcFrameworkCml/
RUN dotnet restore

# copy everything else and build app
COPY MvcFrameworkWeb/. ./MvcFrameworkWeb/
COPY MvcFrameworkDbl/. ./MvcFrameworkDbl/
COPY MvcFrameworkBll/. ./MvcFrameworkBll/
COPY MvcFrameworkCml/. ./MvcFrameworkCml/
WORKDIR /app/MvcFrameworkWeb
RUN dotnet publish -c Release -o out


# App configuration
ENV ASPNETCORE_URLS="http://*:80"
ENV LANG=C
EXPOSE 80


FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
COPY --from=builddotnet /app/MvcFrameworkWeb/out ./
CMD ["./MvcFrameworkWeb"]
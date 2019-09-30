# base image
FROM node:12.2.0-alpine

# set working directory
WORKDIR /app/Profiler

# add `/app/Profiler/node_modules/.bin` to $PATH
ENV PATH /app/Profiler/node_modules/.bin:$PATH

# install and cache app dependencies
COPY Profiler/package.json /app/Profiler/package.json
RUN npm install
RUN npm install @vue/cli@3.7.0 -g


FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY Profiler/*.csproj ./Profiler/
COPY ProfilerDatabase/*.csproj ./ProfilerDatabase/
COPY ProfilerLogic/*.csproj ./ProfilerLogic/
COPY ProfilerModels/*.csproj ./ProfilerModels/
RUN dotnet restore

# copy everything else and build app
COPY Profiler/. ./Profiler/
COPY ProfilerDatabase/. ./ProfilerDatabase/
COPY ProfilerLogic/. ./ProfilerLogic/
COPY ProfilerModels/. ./ProfilerModels/
WORKDIR /app/Profiler
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime
WORKDIR /app
COPY --from=build /app/Profiler/out ./
ENTRYPOINT ["dotnet", "Profiler.dll"]
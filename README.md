# Customer Management

This Web API project is used to perform CRUD operations for parent-child entities.

## How to build

- [Install](https://dotnet.microsoft.com/en-us/download/dotnet/7.0) the latest .NET Core 7.0 SDK
- Install Git
- Clone this repo

## Integration Testing packages

`dotnet add package xunit`
`dotnet add package Microsoft.EntityFrameworkCore.InMemory`

## Deploy as Lambda function

- Add below package to convert web api code in lambda function
  `dotnet add package Amazon.Lambda.AspNetCoreServer.Hosting --version 1.5.0`

- Add `aws-lambda-tools-defaults.json` file for configuration of Lambda function

- Below commands to deploy lambda function and
  `dotnet lambda deploy-function`
  `aws lambda list-functions`

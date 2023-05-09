# Use the official Microsoft ASP.NET Core runtime image as base
FROM mcr.microsoft.com/dotnet/aspnet:7.0

# Set the working directory
WORKDIR /app

# Copy the published output
COPY ./bin/Release/net7.0/publish/ ./

# Expose the port the app runs on
EXPOSE 80

# Start the app
ENTRYPOINT ["dotnet", "DadJokeGenerator.dll"]

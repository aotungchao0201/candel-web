# syntax=docker/dockerfile:1

# Comments are provided throughout this file to help you get started.
# If you need more help, visit the Dockerfile reference guide at
# https://docs.docker.com/go/dockerfile-reference/

# Create a stage for building the application.
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build

# Copy the source files into the container.
COPY . /source
WORKDIR /source/Candle_Web

# Set the architecture argument.
ARG TARGETARCH

# Build the application.
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish -a ${TARGETARCH/amd64/x64} --use-current-runtime --self-contained false -o /app

################################################################################
# Create a new stage for running the application with runtime dependencies only.
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS final

# Install ICU libraries to enable full globalization support.
RUN apk add --no-cache icu-libs

# Set globalization mode to full mode (not invariant).
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# Set the working directory and copy the build output.
WORKDIR /app
COPY --from=build /app .

# Create a non-privileged user that the app will run under.
ARG UID=10001
RUN adduser \
    --disabled-password \
    --gecos "" \
    --home "/nonexistent" \
    --shell "/sbin/nologin" \
    --no-create-home \
    --uid "${UID}" \
    appuser
USER appuser

# Expose port 80 and set the entry point.
EXPOSE 80
ENTRYPOINT ["dotnet", "Candle_Web.dll"]

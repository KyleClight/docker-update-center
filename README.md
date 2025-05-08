# Docker Update Center

A web interface for managing Docker containers, built with ASP.NET Razor Pages.

## 🚀 Features

- Display a list of running containers:
  - Container ID
  - Name
  - Image
  - Ports
  - Status
  - Creation time
- Restart containers via a single click
- View container logs (last output)
- Download full logs (planned)

## Technologies Used

- ASP.NET Core Razor Pages (.NET 8.0)
- Docker CLI (used internally)
- C#
- HTML + Razor
- System.Diagnostics (to run shell commands)

## Deployment

This application is designed to run on **Unix-based systems** such as:
- Ubuntu
- Debian
- CentOS
- Astra Linux

> It uses `zsh` or `bash` under the hood, so make sure your system has a Unix shell available.

## How to Run

1. Make sure Docker is installed and available to your user:
   ```bash
   docker ps
   ```

2. Clone this repository:
    ```bash
    git clone https://github.com/KyleClight/docker-update-center.git
    cd docker-update-center
    ```

3. Run the app:
    ```bash
    dotnet run
    ```

4. Open in browser:
    ```
    http://localhost:5003
    ```

## Premissions

If you get a Docker permission error:
```bash
Got permission denied while trying to connect to the Docker daemon socket
```
Run this to add your user to the docker group:
```bash
sudo usermod -aG docker $USER
newgrp docker
```
## Future Improvments

- Export container logs as downloadable files 
- Add live container stats (CPU, RAM)
- Support for stopping/removing containers
- Authentication and access control


# License

MIT License
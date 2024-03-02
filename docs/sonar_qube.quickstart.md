# SonarQube Quickstart for .NET app using Windows

### Preparation

1. Download and install Java JDK.
- [Download JDK](https://www.oracle.com/java/technologies/downloads/)
- Install it locally

2. Add `JAVA_HOME` environment variable.
- Find where Java were installed. By default should be in `C:\Program Files\Java`
- Open the Start Search, type in "env", and choose "Edit the system environment variables".
- In the System Properties window, click on the "Environment Variables..." button.
- Under System Variables, click "New" and add a new variable. As variable name add: `JAVA_HOME`,
  and as variable value set the path to your JDK (e.g., `C:\Program Files\Java\jdk-<installed_version>`)
- Save changes

3. Add Java to the `PATH`.
- In the same Environment Variables window, locate the Path variable under System Variables and choose "Edit…".
- Click "New" and add the path to the JDK bin directory, e.g., `C:\Program Files\Java\jdk-<installed_version>\bin`.
- Save changes

4. Verify if the configuration was saved correctly.
- Open a new command prompt and type
```shell
java --version
```

5. Run sonarqube server in docker container.
```shell
docker run -d --name sonarqube -p 9000:9000 -p 9092:9092 sonarqube
```

6. Install `dotnet-tool`.
```shell
dotnet tool install --global dotnet-sonarscanner
```

7. Open sonarqube UI in browser on [localhost:9000](localhost:9000).

8. Create new local project.
9. Generate report use commands showed in last step on creating project.
10. Analyze report.

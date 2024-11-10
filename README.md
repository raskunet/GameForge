# GameForge 

## How to setup the project

### Prerequisites
- [.Net 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [PostgreSQL](https://www.postgresql.org/)
- [git](https://git-scm.com)

Knowledge of git is required for contributing to project
##### Optional
- [Github Client](https://desktop.github.com/download/)

---

## Steps
- ### Getting started
1. Make a fork of this project on your github account
1. Clone the repo by running the following command in your terminal
```bash
git clone https://github.com/your-username/GameForge.git
```
2. Cd into the project directory
```bash
cd GameForge
```
3. Install the dependencies
```ps
dotnet restore
```
- ### Contributing
1. Make a branch with your username
```bash
git checkout -B your-username
```
2. Make the necessary changes onto your local branch
3. Push your changes to your branch
```bash
git push -u origin your-username
```
4. Make a Pull request on github
5. Wait for the reviewers to accept your pull request and merge your changes with main branch
---
### Project Structure
The project structure should look like this (TODO)

---
## How to run the project
1. To run the project you can either use the cli or vscode/vs run command. Before that make sure you have Postgres Installed locally. 
2. Make a `.env` file in Project root and add connection string paramaters. You can look at the `.env.example` for reference
```bash
    dotnet ef database update
    dotnet build
    dotnet run
```
3. For hot-reloading
```bash
    dotnet watch
```
---
## Further Reading
- [Fork and Pull request Model](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/getting-started/about-collaborative-development-models#fork-and-pull-model)
### Contributors

| Full name  | Email address | GitHub |
| ------------- | ------------- | ------------- |
| **Shaheer Rashad**  | [raskunet@proton.me](mailto:epolamik@proton.me)  | [@raskunet](https://github.com/raskunet) |

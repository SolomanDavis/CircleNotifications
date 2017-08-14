# CircleNotifications
Windows system tray tool that tracks [CircleCI](www.circleci.com) build status by polling their API (v1.1).

This tool allows you to keep track of your builds running on CircleCI.

Currently there is no working program that can do this for you (their [cc.xml setup](https://circleci.com/docs/1.0/polling-project-status/) is completely broken). The only way to be notified that your build passed or failed is to get an email which is often not enough as you get many emails throughout the day and ignore most of them when you are working. This tool exists only in your system tray and allows you to find out the latest build status of your project with a quick glance at the icon.

## Setup
On initial startup, you will get two popup forms asking you for necessary information to run the program:
### Set CircleCI API Token
This is a user-level CircleCI API that is required in order to poll build status through their API. In order to generate this token, go to User Settings -> Personal API Tokens -> Create New Token.

**NOTE:** This will give CircleNotifications full read/write permissions. Currently CircleNotifications only reads.

### Set Project Details
In order to request certain builds, the following information is required by CircleNotifications:
  - Username: Username that owns the project from CircleCI's perspective (if you are using a bitbucket team structure, this could be the team).
  - VCS Type: Type of version control (the choices are `bitbucket` or `github`).
  - Project Name: Name of your project.
    - *Note: CircleNotifications currently only polls one project. You would have to run two instances of the program to poll multiple projects (which is ugly). I plan to support multiple projects. See TODOs.*

A good way to find out what these values are is to go to one of your builds in CircleCI and look at the URL: `https://circleci.com/:vcstype/:username/:project/buildnum`

## TODO
Things I feel will make this tool a lot more useful and even give some much needed functionality to CircleCI.
  - Ability to poll multiple projects. This could take the form of section separators in the ContextMenuStrip per project that the user sets.
  - Ability to specify what branches to subscribe to. Users would only receive notifications for branches they care about. The different branches would be represented as a ToolStripMenuItem under each project section and mousing over them will show the latest builds for that branch on that project.
  - Ability to trigger a new build on a specific commit on a specific branch (probably through a form). This feature currently doesn't exist in CircleCI at all and would be super useful.
    - Also ability to quickly trigger a new build on the head of a given branch.
  - For each build, option to look at build artifacts produced by the build straight from the menu using the artifact URL. This would be very useful for users that export coverage results to build artifacts (like using jacoco).
  - For each build, option to retry the build.
  - For each running build, option to cancel the build.

There's actually quite a lot we can do with the API. Feel free to write a feature that you think would be helpful and create a pull request or a feature ticket.

## Project Details
This is a Visual C# WinForms solution that only runs on Windows. I built it with Visual Studio 2017.

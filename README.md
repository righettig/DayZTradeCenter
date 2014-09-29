DayZTradeCenter
===============

The official repository of the DayZTradeCenter project.

## Installation notes

1. Open Visual Studio
2. Clone the repository
3. Open the solution DayZTradeCenter.sln
4. Open the Package Manager Console
5. Restore missing Nuget packages
6. Make sure DayZTradeCenter.UI.Web is set as Startup Project
7. Make sure DayZTradeCenter.Domain.Model.Identity is the default project in the Package Manager Console
8. Run Update-Database from the Package Manager Console. This will create the dev database with a bunch of entities already configured for you

### Known issues

Sometimes it may happen that "Update-Database" is not recognized as the name of a cmdlet inside the Package Manager Console. Try closing and reopening VS. This should do the trick. More info here: http://stackoverflow.com/questions/9674983/the-term-update-database-is-not-recognized-as-the-name-of-a-cmdlet


## Release notes

- v0.2.5380.19796:
  - The admin can create, list, edit, delete and see the details of a specific item.
  - Trade management:
    1. Users can create trades (H+W, only 1 item).
    2. Users can make (and withdraw) offers. 
    3. The owner of a trade can list the pending offers for a specific trade.
    4. The owner of a trade can choose the winner of a trade.
    3. The profile page shows the trade/offers of the current user.
  - Exchange management:
    1. users can exchange private information for the actual exchange of items (i.e.: Steam id, server, location).
    2. (basic) feedback support and some sort of (not 100% working..) messaging system.

- v0.1.5376.27928 (refactoring):
  - Moved the code that deals with auth and identity management in DomainModel.Identity

- v0.1.5376.24670:
  - Steam login
  - Debug (= quicker) login with default accounts (Administrator, TestUser)
    1. Switch to the "DebugFakeLogin" project configuration.
    2. Select the current user by using DefaultUsers.Admin/TestsUser inside AccountController.ExternalLoginCallback
  - Update profile: username, email
  - Logoff

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
7. Make sure DayZTradeCenter.DomainModel is the default project in the Package Manager Console
8. Run Update-Database from the Package Manager Console. This will create the dev database with a bunch of entities already configured for you

### Known issues

Sometimes it may happen that "Update-Database" is not recognized as the name of a cmdlet inside the Package Manager Console. Try closing and reopening VS. This should do the trick. More info here: http://stackoverflow.com/questions/9674983/the-term-update-database-is-not-recognized-as-the-name-of-a-cmdlet

### Testing

It is possible to test the web application using preconfigured accounts without the need to log in with Steam. Selected the "DebugFakeLogin" configuration.

- Administrator
- Test1, a test user with a trade already created that has received 2 offers from test user #2, #3
- Test2, a test user who has offered to the trade created by test user #1
- Test3, a test user who has offered to the trade created by test user #1
- Test4, a user who has not yet done anything

## Release notes

- v0.3.5389.32644:
  - Trade management:
    1. Users can create trades (H+W) with multiple items and quantity
    2. Users can delete unclosed trades
    3. Quick offer
  - Account management:
    - (basic) reputation score based on the actual received feedback
  - Search:
    - H & W item queries
    - Reset
  - Trends & statistics:
    - Most wanted/offered items
  - Profile management:
    - History
  - Landing page revamp (latest & hot trades)
  - Notifications
  - UI/UX improvements
    1. Updated Trades.Details page
    2. (stubbed) "Complete History" page
    3. Tweaked design (landing/dashboard)
    4. Quicker login process
  - Testing
    - 5 pre-configured users available

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

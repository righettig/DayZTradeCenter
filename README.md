DayZTradeCenter
===============

The official repository of the DayZTradeCenter project.

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

WHY THE PROJECT IS NOT A PORTABLE CLASS LIBRARY?
------------------------------------------------
Prior to Visual Studio Update 2 you could create a PCL project that targets .Net 4 or .Net 4.5 and Windows 8, 
which would allow you to install the EntityFramework nuget package. 
After this update you cannot target Windows 8 without Windows Phone 8.1 being automatically targeted.

This auto targeting of Windows Phone seems to prevent adding EF package to PCL that targets Windows 8.

http://stackoverflow.com/questions/22879578/entity-framework-for-portable-class-library
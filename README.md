# OrderPlacementLibrary

Custom simple library created as homework for a .net interview.

To make it easier to use, i have published it as a NuGet package, which is publicly available in the nuget.org source in visual studio.

You can search for it by "OrderPlacementHomeWorkSample" in the nuget package manager and install it, or use <br>
- .NET CLI - dotnet add package OrderPlacementHomeWorkSample --version 1.0.0<br>
- Package manager - NuGet\Install-Package OrderPlacementHomeWorkSample -Version 1.0.0

To acces its public methods:<br> 
<b>PlaceOrder(int customerId, DateTime expectedDeliveryDate, int desiredAmount, int kitType)<br>
and <br>
GetAllCustomerOrders(int customerId)</b>

Just create a new instance of the class and you are good to go.

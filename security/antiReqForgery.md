# Undgå request forgery
- brug taghelperen `@Html.AntiForgeryToken()` på dine razorpage hvor der er en formular.
- eller brug `asp.antiforgery="true"` inde i `<form>`-tagget
- din i controller tilføjes `[ValidateAntiForgeryToken]` over relante metoder. (De metoder er håndterer Postrequests)

[mere info](https://docs.microsoft.com/da-dk/aspnet/core/security/anti-request-forgery?view=aspnetcore-2.2)

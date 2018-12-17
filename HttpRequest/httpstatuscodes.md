# Http statuskoder
## HTTP 1xx
Bruges typisk ikke af API'er


## HTTP 2xx
Alle disse statuskoder indikerer at en forespørgsel er udført succesfuldt.

| Kode | Type | Beskrivelse | Metode i ASP.net Core | HttpVerb |
| ---| --- | --- | --- | --- |
| 200 | Success | Returnes ved en HttpGet | Ok() | __HttpGet__ , __HttpPatch__|
| 201 | Created | returneres typisk ved en HttpPost, oprettelse af ny ressource  | Created(),  CreatedAtAction(...),  CreatedAtRoute(..) | __HttpPost__ |
| 204 | No content | returneres ved en succesfuld request som ikke skal returnere noget, eksempelvis Delete |  NoContent() | __HttpPut__, __HttpPatch__, __HttpDelete__ |

## HTTP 3xx
Bruges typisk ikke af API'er, men kan dog bruges til at fortælle at en ressource har flyttet sig.

## HTTP 4xx
Alle disse statuskoder indikerer at en forespørgsel ikke er udført succesfuldt.

| Kode | Type | Beskrivelse | Metode i ASP.net Core |
| ---| --- | --- | --- |
| 400 | Bad Request | Du sender noget som serveren ikke kan forstå | BadRequest() |
| 401 | Unauthorized | Ingen eller forkerte authentificeringsoplysninger sendt | Unauthorized() | 
| 403 | Forbidden | Authentication er iorden, men bruger har ikke adgang til forespurgte ressource) | Forbid() |
| 404 | Not Found | Den forespurgte ressource findes ikke) | NotFound() |
| 409 | Conflict  | Bruges ved samtidighedsproblemer | |

## HTTP 5xx
| Kode | Type | Beskrivelse | Metode i ASP.net Core |
| ---| --- | --- | --- |
| 500 | Internal Server Error | Serveren oplever en fejl som brugeren af website/webapi ikke kan gøre noget ved. Brugeren kan ikke gøre andet end at prøve igen senere ||

## Se desuden
 - [ControllerBase](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.controllerbase?view=aspnetcore-2.1)
   - MS dokumentation om klassen som indeholder ovenstående metoder

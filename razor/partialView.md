# Partial View
- Et partial view filnavn starter typisk med en underscore, og har endelsen ".cshtml" præcis som views. Eksempel: `_PartialView.cshtml`
- partial view kan ligge i samme mappe som det view der kalder den.
- hvis vores partial view skal genbruges i flere forskellige views, skal det placeres i `/Views/Shared` mappen, for at følge konventionen. _se iøvrigt [Partial view discovery](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/partial?view=aspnetcore-2.1#partial-view-discovery)_
- et partial view har ikke en reference til `_Layout`, dette klares af det view som kalder partial viewet.

## do's and dont's
- __DO:__ bruges til at bryde store og komplekse markup filer ud i mere overskuelige dele.
- __DON'T:__ bør dog ikke blive brugt til fælles layout delt af hele sitet, de skal ligge i `_Layout.cshtml`
- __DO:__ giver mulighed for at genbruge ensartede layout-dele over hele sitet.
- __DO:__ Et partial view skal kun bruges til at præsentere data
- __DON'T:__ Det er ikke til at udføre kompleks renderingslogik eller udførsel af kode 
  - se istedet [view component](./viewComponent.md) hvis du skal bruge dette.

## Konsumer et partial view
I ASP.NET Core 2.1 og senere anbefales det at bruge en _[Partial Tag Helper](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/tag-helpers/built-in/partial-tag-helper?view=aspnetcore-2.1)._
Den renderer indholdet asynkront og bruger en html-lignende syntaks:
```html
<partial name="_PartialView" />
```
Bruger man hele filnavnet (dvs. filnavn + endelse), så skal filen ligge i samme mappe som det view der kalder den. 

Alternativ skal man angive hele stien til filen inkl. filendelse, eller den relative sti.

## Tilgå data fra partial view
Du skal bruge `model` attributten til at overføre data til dit partial view.
```CSHTML 
@foreach (var product in Model.Products)
{
    <partial name="_ProductPartial" model="product" />
}
```
Læs mere om attributterne `for` og `view-data`: _[Partial Tag Helper](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/tag-helpers/built-in/partial-tag-helper?view=aspnetcore-2.1)._

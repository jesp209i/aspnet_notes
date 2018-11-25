# Genbrug af Markup og layout

## _Layout.cshtml
- brug "_Layout.cshtml" til at skabe en grundlæggende skabelon alle views skal bruge.
  - denne fil skal ligge i "/Pages"
### @RenderBody()
- tilføj ```@RenderBody()``` det sted i "_Layout.cshtml" hvor det varierende indhold skal indsættes.
  - I views der skal bruge layout-filen, tilføjes ```@{ Layout= "_Layout"; (...) }``` i toppen af filen
  - alt indhold som ligger i layout-filen kan nu fjernes fra alle views som bruger layoutfilen.
### @RenderSection()
- tilføj ```@RenderSection("SektionsNavn")``` det sted i "_Layout.cshtml" hvor der skal tilføjes en sektion
  - bemærk at der skal bruges en parameter i metoden, nemlig navnet på den sektion der skal renderes.
  - I views som bruger layout-filen, tilføjes: ```@section SektionsNavn { (HTML-syntaks) }```
    - Bemærk at "SektionsNavn" er et eksempel og kan være mere beskrivende for den sektion du vil tilføje.
    - Som ovenstående er skrevet nu, kræves det af alle views der bruger layout-filen at de definerer en sektion med navnet "SektionsNavn".
- Man kan lave gøre sektionen valgfri ved at skrive: ```@RenderSection("SektionsNavn", required: false )```
- en mere elegant løsning, som indsætter noget prædefineret indhold, hvis sektionen ikke findes:
```
@if (IsSectionDefined("SektionsNavn"))
{
  @RenderSection("SektionsNavn", required: false)
} else {
  <h2 class="css-klasse">Prædefineret indhold</h2>
}
```
- ved hjælp af ViewData-dictionary kan du nemt dele data mellem layout og views.
  - I viewet kan du definere et ViewData-ord på følgende måde: ```@{(...) ViewData["SektionsTekst"] = modelInstans.TekstProperty; } ```
  - og i layout'et bruges det på følgende måde:
```
@if (IsSectionDefined("SektionsNavn"))
{
  @RenderSection("SektionsNavn", required: false)
} else {
  <h2 class="css-klasse">@ViewData["SektionsTekst"]</h2>
}
```
## ViewStart og ViewImports
- ligger i "/Pages"
- skal hedde "_ViewStart.cshtml" og "_ViewImports.cshtml"
- filen bliver altid udført ved et kald til en razor-page.
  - __Vedrørende ViewStart__
  - Layout filen kan refereres herfra, så skal den ikke tilføjes til alle views.
  - __Vedrørende ViewImports__
  - Alle Using, Namesspaces og Taghelpers som er fælles for samtlige views, kan tilføles her.

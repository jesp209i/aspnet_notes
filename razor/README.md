# Razor-pages
_kilde: [Lynda: ASP.NET Core : Razor Pages](https://www.lynda.com/ASP-NET-tutorials/ASP-NET-Core-Razor-Pages/630622-2.html)_

## Grundlæggende
- Skal bruge en mappe med navnet "Pages" i roden af projektet.
- Filerne skal bruge endelsen ".cshtml".
- alle razor-filers indhold starter med "@page" øverst
- Namespaces kan tilføjes i toppen af filerne (under "@page") på samme måde som vi er vant til, bare med @ foran
  - ```@using Microsoft.NameSpace```
- al kode i razor starter med @ direkte efterfulgt af kode
  - ```@DateTime.Now```
  - hvis du skal bruget et @ i en mail, kan du escape med et ekstra @
    - eksempel: ```jesper@@fakeEmailadresse.dk``` renderes som en normal email med kun et 
- kodeblokke kan tilføjes ved at skrive ``` @{ ... } ```
  - inde i kodeblokken gælder de samme syntaksregler som ved normal C#
  - det er kotume at lægge mest mulig c#/programkode i toppen af en razorfil, så der er en nogenlunde pæn adskillelse af kode og præsentation.
- Overfør parametre vha URL. [afsnit 6 i kap.2](https://www.lynda.com/ASP-NET-tutorials/Passing-parameters-URL/630622/679592-4.html)

## Genbrug af Markup og layout
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
### ViewStart og ViewImports
- ligger i "/Pages"
- skal hedde "_ViewStart.cshtml" og "_ViewImports.cshtml"
- filen bliver altid udført ved et kald til en razor-page.
  - __Vedrørende ViewStart__
  - Layout filen kan refereres herfra, så skal det ikke tilføjes til alle views.
  - __Vedrørende ViewImports__
  - Using, Namesspaces og Taghelpers som er fælles for samtlige views kan tilføles her, så skal de ikke tøjes til hver fil.

## Byg vedligeholdbare applikationer

## Arbejd med data

## Valider brugerinput og sikr din applikation fra uautoriserede brugere

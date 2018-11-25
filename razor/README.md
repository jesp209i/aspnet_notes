# Razor-pages
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

## Flere emner
- [Genbrug af Markup og layout](/reuse/README.md)
- [Byg vedligeholdbare applikationer](/maintainable/README.md)
- [Arbejd med data](/data/README.md)
- [Valider brugerinput og sikr din applikation fra uautoriserede brugere](/secure/README.md)

Alt på denne og efterfølgende sider er baseret på _[Lynda: ASP.NET Core : Razor Pages](https://www.lynda.com/ASP-NET-tutorials/ASP-NET-Core-Razor-Pages/630622-2.html)_ med mindre andet er angivet.

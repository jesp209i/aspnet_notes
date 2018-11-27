# View Component
En View component:
- renderer en klump i stedet for hele responset.
- har de samme separation-of-concerns and testbare fordele som man får ved controller og view
- kan have parametre og business logik.
- bliver typisk påkaldt fra en layout-side.

View Components skal bruges alle de steder hvor du har genbrugelig renderingslogik som er for kompleks til et partial view, såsom:
- Dynamiske navigationsmenuer
- tag clouds (som laver forespørgsler mod en database)
- login panel
- indkøbskurv
- nyligt udgivede artikler
- indhold på et sidepanel på en typisk blog.
- et login panel som typisk bliver renderet på samtlige sider og viser enten linket til at logge ind eller ud, afhængigt af brugerens login status.

View Component består af to dele: en klasse (som typisk nedarver fra `ViewComponent`) og det resultat som den returnerer (typsik et view). Ligesom controllerne, kan en view component være en POCO, men de fleste udviklere vil bruge fordelene ved at tage metoderne og propertiesne i brug hvis der nedarved fra `ViewComponent`.

[nået hertil](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/view-components?view=aspnetcore-2.1#creating-a-view-component)

# Http Request
Et Http request er bygget op af to dele:

En header og en payload/body.

Payloaden er nogle gange undladt i visse typer af requests (HttpHead)

## Http Verber
Listen er længere, men her er de mest gængse:

| Navn | attribut i ASP.net | Beskrivelse | HttpSuccesKode |
|---|---|---|---|
| Get | `[HttpGet]` | Henter ressource | 200 |
| Post | `[HttpPost]` | opretter ressource | 201 |
| Put | `[HttpPut]` | opdaterer ressource, kræver at hele den opdaterede ressource sendes med i requestet | 204 |
| Patch | `[HttpPatch]` | opdaterer ressource, kun de angivne dele af ressourcen bliver opdateret | 200, 204 |
| Delete | `[HttpDelete]` | sletter ressourcen | 204 |

Se desuden [statuskoder](httpstatuscodes.md) for mere information om Https statuskoder, blandt andet fejlkoder.

Andre verber:
- Option
- Head
- ...

## Header
Headeren indeholde metainformation om requestet. Denne information er lagt i såkaldte headers, der hver især fortæller modtageren noget om hvordan requestet skal behandles. nogle headers fortæller også noget om hvordan et eventuelt svar skal udformes.

### udvalgte Headers

| Header | beskrivelse | brug |  |
|---|---|---|---|
| Content-Type | Fortæller modtageren hvilket format payloaden er i | [mime-type](https://developer.mozilla.org/en-US/docs/Web/HTTP/Basics_of_HTTP/MIME_types) | * |
| Accept | Fortæller modtageren hvilket format man gerne vil have svaret i. (Der er dog ingen garanti for at det kan returneres i det angivne format) - Dette kaldes __Content Negotiation__ ([Se outputformatters](outputformatter.md)). | Se ovenstående link. __"application/json"__ og __"application/xml"__ er typiske muligheder.  | * |

(* Man bør forme sine requests så de indeholder disse headere som minimum.)

## Payload

# Http Request
Et Http request er bygget op af to dele:

En header og en payload/body.

Payloaden er nogle gange undladt i visse typer af requests (HttpHead)

## Header
Headeren indeholde metainformation om requestet. Denne information er lagt i såkaldte headers, der hver især fortæller modtageren noget om hvordan requestet skal behandles. nogle headers fortæller også noget om hvordan et eventuelt svar skal udformes.

### udvalgte Headers

| Header | beskrivelse | brug |
|---|---|---|
| Content-Type | Fortæller modtageren hvilket format payloaden er i | [mime-type](https://developer.mozilla.org/en-US/docs/Web/HTTP/Basics_of_HTTP/MIME_types) |
| Accept | Fortæller modtageren hvilket format man gerne vil have svaret i. (Der er dog ingen garanti for at det kan returneres i det angivne format) - Dette kaldes __Content Negotiation__ ([Se outputformatters](outputformatter.md)). | Se ovenstående link. __"application/json"__ og __"application/xml"__ er typiske muligheder.  |

## Payload

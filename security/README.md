# Sikkerhed
Kilde [Pluralsight](https://app.pluralsight.com/player?course=aspdotnetcore-implementing-securing-api&author=shawn-wildermuth&name=aspdotnetcore-implementing-securing-api-m7&clip=1&mode=live)

| hvis du skal | Implementer Sikkerhed? |
| --- | --- |
| bruge private eller personaliserede data? | Ja |
| sende følsomme data over nettet? | Ja |
| bruge en eller anden form for credentials? | Ja |
| beskytte din server mod overforbrug? | Ja |

## Beskyt API
- Beskyt din server infrastruktur (fysik)
- Sikker forsendelse af data
  - [SSL](ssl.md) skal næsten altid bruges
- Sikr selve API'et
  - Cross Origin Sikkerhed
  - Authorization/Authentication

# Sikre mod misbrug
 - [overhold HTTPS](ssl.md)
 - [request forgery](antiReqForgery.md)
 - [open redirect attacks](https://docs.microsoft.com/da-dk/aspnet/core/security/preventing-open-redirects?view=aspnetcore-2.2)
 - [XSS](https://docs.microsoft.com/da-dk/aspnet/core/security/cross-site-scripting?view=aspnetcore-2.2)
 - [CORS](cors.md)
# Sikre brugere
- [opsætning af Identity](aspnetidentity.md)
- [Authentication](authentication.md)
- [Authorization](authorization.md)

# SSL forklaring
## Symmetrisk kryptering
 - shared key / secret - begge sider bruger samme nøgle, den kan både bruges til at kryptere og dekryptere beskeder

## Asymmetrisk kryptering
- public key - denne nøgle kan kryptere en besked (men kan ikke dekryptere!!)
- private key - denne nøgle kan dekryptere en besked

## SSL handshake

| Step | Klient | Server |
|---|---|---|
| 1 | Sender et request | |
| 2 | | returnerer et `certifikat` |
| 3 | sikrer sig at `certifikatet` er iorden |  |
| 4 | finder den `public key` der blev send med i responset |  |
| 5 | laver en ny `symmetrisk shared key` som bliver krypteret med `public key` |  |
| 6 | returnerer den krypterede `shared key` |  |
| 7 |  | Resten af kommunikationen er krypteret med symmetrisk kryptering |

- punkt 1 og 2 - ingen kryptering, men kontakt etableres
- punkt 4 - klienten kender nu serverens `public key` som den kan bruge til `asymmetrisk krypteret` kommunikation i punkt 6.
- punkt 5 - klienten laver en `shared key` som både klient og server kan bruge til den fortsatte `symmetrisk krypterede` kommunikation fra punkt 7 (og frem).

### Hvorfor bruger man ikke asymmetrisk kryptering til al kommunikation?
Det er meget ressourcekrævende at opretholde asymmetrisk kryptering, hvorfor man kun bruger det til at sende en `shared key`. Denne `shared key` bruges efterfølgende til `symmetrisk kryptering` mellem parterne. Dette er mindre ressourcekrævende og derfor hurtigere.

## Implementer SSL i dit projekt
### RequireHttpsAttribute
Her kan du redirecte et Http-kald om til et Https-kald:
```c#
public void ConfigureServices(IServiceCollection)
{
 // udelagt kode
 services.AddMvc(opt =>
 {
   if (!_env.IsProduction()) { 
     opt.SslPort = 44388;   // skriv porten der bruges til development
   }
   opt.Filters.Add(new RequireHttpsAttribute()); // laver et redirect fra http til https
 });
}
```
- [Yderligere information](https://docs.microsoft.com/da-dk/aspnet/core/security/enforcing-ssl?view=aspnetcore-2.2)
  - Linket har desuden alternativ implementering og en advarsel om at bruge redirects i API'er
  
### HSTS
https://docs.microsoft.com/da-dk/aspnet/core/security/enforcing-ssl?view=aspnetcore-2.2&tabs=visual-studio#http-strict-transport-security-protocol-hsts

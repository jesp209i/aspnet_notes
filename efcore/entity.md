# Entity klasser
Se et kodeeksempel i mappen code.

Man kan bruge fluent API og Data annotations til at configurere en model. Her fokuseres på __Data Annotations__.

Per konvention vil properties blive inkluderet i modellen. Ved at tilføje attributten `[NotMapped]` over en property, vil propertyen ikke komme med i modellen til databasen. 
```c#
public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }

    [NotMapped] // der oprettes ikke feltet LoadedFromDatabase i databasen.
    public DateTime LoadedFromDatabase { get; set; }
}
```
## Data Annotations
Per konvention vil properties med navnet `Id` eller `<type navn>Id` blive konfigureret som primær nøgle i en entity. 
Med annotations tilføjes attributten `[Key]` over propertien, du vil gøre til nøgle.

Nøgler af typerne `int`, `long`, `Guid` vil automatisk få værdier genereret når entityen bliver tilføjet til databasen. Alle andre typer har ikke automatisk værdi-generering.

Dette kan påvirkes med følgende attributter:
- `[DatabaseGenerated(DatabaseGeneratedOption.None)]`
  - ingen autogenereret værdi
- `[DatabaseGenerated(DatabaseGeneratedOption.Identity)]`
  - hvis typen er understøttet tilføjes en værdi ved indsæt i database
- `[DatabaseGenerated(DatabaseGeneratedOption.Computed)]`
  - hvis typen er understøttet tilføjes/opdateres en værdi ved indsæt i databsen eller ved opdatering af række.

### `not null` og  `null` værdier
Hvis en property har en CLR type, der kan indeholde værdien NULL (`string`, `int?`, `byte[]`), bliver feltet automatisk konfigureret til at tillade NULL-værdier.
Ved CLR typer som ikke tillader Null, vil feltet blive konfigureret til ikke at tillade Null-værdier.

Hvis du vil tvinge et felt til at være påkrævet bruges attributten `[Required]` over propertien.

### Størrelse på felter
Brug MaxLength attributten til at angive en størrelse på et felt (dvs. hvor "meget data feltet" må indeholde).

`[MaxLength(#)]` udskift # med et tal.

attributten virker kun med array-typer som `string` og `byte[]`. Hvis man ikke angiver en størrelse på felterne vil databasen automatisk bruge så store felter som muligt, eksempelvis `nvarchar(max)` for `string`.

### Concurrency Tokens
kilde: https://docs.microsoft.com/da-dk/ef/core/modeling/concurrency

Properties bliver aldrig konfigureret som concurrency tokens automatisk. Du kan tilføje en `[ConcurrencyCheck]` over en property som du vil bruge som token.

Hvis du vil have databasen til at håndtere mere af dette for dig, skal du lave en speciel property:
```c#
[Timestamp]
public byte[] Timestamp { get; set; }
```
læs mere om at [håndtere concurrency konflikter](https://docs.microsoft.com/da-dk/ef/core/saving/concurrency)

---

kilde: https://docs.microsoft.com/da-dk/ef/core/modeling/ 

# Arbejd med data
## TagHelpers
- kræver at du bruger `addTagHelper` direktivet på de sider der skal gøre brug af dem.
  - linjen `addTagHelper *,Microsoft.AspNetCore.Mvc.TagHelpers` kan alternativt tilføjes til `_ViewImports.cshtml` så er taghelpers tilgængelige overalt
## Formularer med Tag-Helpers
Man kan tilføje tagHelpers til HTML-elementer for at asp kan hjælpe med at generere disse.
- eksempelvis `asp-for="PropertyNavn"`
### RazorPages OnGet()
Pages bliver oprettet med en tom metode `OnGet()` som bruges til at hente værdier til din page.

I nedenstående eksempel fra videoen er OnGet() lavet om til OnGetAsync(), begge er valide og understøttes af frameworket. 
```C# 
// kode udeladt...
private readonly IMinService recipiesService;
public Recipe Recipe { get; set; }

public HestModel(IMinService minService) // constructor med DI
{
  this.recipesService = minService;
}

public async Task OnGetAsync()
{
  // forsøger at hente, ellers oprettes en ny tom instans af Recipe.
  Recipe = await recipesService.FindAsync(Id.GetValueOrDefault()) ?? new Recipe();
}
// kode udeladt...
```
### RazorPages OnPost()
Bruges til at opdatere en entity hvis der opdates igennem den samme page, via en HttpPost-request/html-formular.

Her gælder også at både `OnPost()` og `OnPostAsync()` er valide og understøttes af frameworket.
```C#
// kode udeladt...
public async Task<IActionResult> OnPostAsync()
{
  // udfører kun et redirect i dette tilfælde
  return RedirectToPage("/Recipe", new { id = Id });
}
// kode udeladt...
```
### ModelBinding
- brug `asp-for=...` tagHelperne i view-delen.
- tilføj attributten `[BindProperty]` til den property du vil koble på viewet.
- tilbage til OnPost():
```C#
// kode udeladt...
public async Task<IActionResult> OnPostAsync()
{
  Recipe.Id = Id.GetValueOrDefault();
  await recipesService.SaveAsync(Recipe);
  return RedirectToPage("/Recipe", new { id = Recipe.Id });
}
// kode udeladt...
```
### tilføj tilpassede handlers
i klassen:
```C#
public async Task<IActionResult> OnPostDelete() { // kode udeladt... }
```
i pagemodel-viewet tilføjes `asp-page-handler="Delete"` til en "submit"-knap.
```C#
<button type="submit" asp-page-handler="Delete" class="btn ...">Delete</button>
```
Bemærk at OnPost __Delete__ og asp-page-handler="__Delete__" skal være skrevet ens.

På denne måde kan man tilpasse formularens post-action til egne tilpassede formål.

## Upload af billede
Punkt 1:
- i pagemodellen tilføjes en property:
```c#
[BindProperty]
public IFormFile Image {get; set;}
```
Punkt 2:
- i page view:
  - formularen skal have encoding type "multipart/form-data"
```html
<form method="post" class="css" enctype="multipart/form-data">
...
```
  - Der tilføjes et felt i formularen til billedet:
```html
<label asp-for="Image" class="css"></label>
<input type="file" asp-for="Image" />
```

Punkt 3, tilbage i pagemodellen:
```C#
// kode udeladt...
public async Task<IActionResult> OnPostAsync()
{
// henter den der svarer til det der redigeres og forsøges at opdatere... 
  var recipe = await recipesService.FindAsync(Id.GetValueOrDefault()) ?? new Recipe(); 
  recipe.Name = Recipe.Name;
  recipe.Description = Recipe.Description;
  recipe.Ingredients = Recipe.Ingredients;
  recipe.Directions = Recipe.Directions;
  
  if(Image != null){
    using(var stream = new System.IO.MemoryStream())
    {
      await Image.CopyToAsync(stream);
      
      recipe.Image = stream.ToArray();
      recipe.ImageContentType = Image.ContentType;
    }
  }
  
  await recipesService.SaveAsync(recipe); // dette er tager ikke højde for concurrency umiddelbart.
  return RedirectToPage("/Recipe", new { id = recipe.Id });
}
// kode udeladt...
```

### Hent uploaded billede
I modellen som billedet hører til skal der bruges følgende:
```C#
    public byte[] Image { get; set; }

        public string ImageContentType { get; set; }

        public string GetInlineImageSrc ()
        {
            if (Image == null || ImageContentType == null)
                return null;

            var base64Image = System.Convert.ToBase64String(Image);
            return $"data:{ImageContentType};base64,{base64Image}";
        }

        public void SetImage(Microsoft.AspNetCore.Http.IFormFile file)
        {
            if (file == null)
                return;

            ImageContentType = file.ContentType;

            using (var stream = new System.IO.MemoryStream())
            {
                file.CopyTo(stream);
                Image = stream.ToArray();
            }
        }
```
og i html/cshtml-filen hentes billedet på denne måde:
```html
        <img class="img img-thumbnail" src="@recipe.GetInlineImageSrc()" />
```

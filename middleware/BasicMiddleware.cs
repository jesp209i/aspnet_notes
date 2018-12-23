public class BasicMiddleware
{
  private readonly RequestDelegate _next;
  
  public BasicMiddleware(RequestDelegate next)
  {
    _next = next;
  }
  
  public async Task Invoke(HttpContext context) 
  {
    // kode før næste middleware kaldes
    
    await next(context); 
    
    // kode der udføres når kontrollen kommer tilbage hertil
  }
}

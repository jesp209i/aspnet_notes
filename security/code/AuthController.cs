public class AuthController : Controller
{
  private DBContextKlasse _context;
  private SignInManager<UserKlasse> _signInMgr;
  private UserManager<UserKlasse> _userMgr;
  private IPasswordHasher _hasher;
  private ILogger<AuthController> _logger;
  
  public AuthController(DBContextKlasse context, SignInManager<UserKlasse> signInMgr, 
    UserManager<UserKlasse> userMgr, IPasswordHasher hasher, ILogger<AuthController> logger)
  {
    _context = context;
    _signInMgr = signInMgr;
    _userMgr = userMgr;
    _hasher = hasher;
    _logger = logger;
  }
  
  // cookie
  [HttpPost("api/auth/login")]
  public async Task<IActionResult> Login([FromBody]CredentialModel model)
  {
    try 
    {
      var result = await _signInMgr.PasswordSignInAsync(model.UserName, model.Password, false, false)
      if (result.Succeded)
      {
        return Ok();
      }
    }
    catch (Exception ex)
    {
      _logger.LogError($"Exception thrown while logging in: {ex}");
    }
    // af hensyn til sikkerhed fortæller man ikke hvad der gik galt, blot at login ikke lykkedes
    return BadRequest("Failed to login");
  }
  
  // token
  [HttpPost("api/auth/token")]
  public async Task<IActionResult> CreateToken([FromBody]CredentialModel model)
  {
    try
    {
      var user = await _userMgr.FindByNameAsync(model.UserName);
      if (user != null)
      {
        if (_hasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) == PasswordVerificationResult.Success)
        {
          var userClaims = await _userMgr.GetClaimsAsync(user);
        
          var claims = new []
          {
            new claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
            new claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
            new claim(JwtRegisteredClaimNames.Email, user.Email),
          }.Union(userClaims);
          
          var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("VERYLONGKEYVALUETHATISSECURE"));
          var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
          
          var token = new JwtSecurityToken(
            issuer: "http://ourwebsite.com",
            audience: "http://ourwebsite.com",
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(15),
            signingCredentials: creds
          )
          
          return Ok(new
          {
            token = new JwtSecurityTokenHandler().WriteToken(token),
            expiration = token.ValidTo
          })
        }
      }
    }
    catch (Exception ex)
    {
      _logger.LogError($"Exception thrown while creating JWT: {ex}");
    }
    Return BadRequest("Failed to generate token");
  }
}

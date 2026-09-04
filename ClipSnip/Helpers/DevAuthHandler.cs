using System.Security.Claims;
using System.Text.Encodings.Web;
using ClipSnip.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace ClipSnip.Helpers;

public class DevAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private const string SeedEmail = "user@example.com";

    public DevAuthHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        UserManager<ApplicationUser> userManager) // 👈 Injiser UserManager
        : base(options, logger, encoder)
    {
        _userManager = userManager;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // 1. Hent den seedede brukeren fra databasen
        var user = await _userManager.FindByEmailAsync(SeedEmail);
        if (user == null)
        {
            // Hvis databasen ikke er seedet ennå, feiler vi grasiøst
            return AuthenticateResult.Fail("Seed user not found in database yet.");
        }

        // 2. Bygg claims som matcher Identity standarden
        var claims = new List<Claim>
        {
            // Identity krever NameIdentifier (Bruker-ID) for å koble forespørselen til tabellen
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName ?? SeedEmail),
            new Claim(ClaimTypes.Email, user.Email ?? SeedEmail),
        };

        // 3. Hent rollene brukeren har i databasen og legg dem til (f.eks. "User")
        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        // 4. Opprett identiteten (IdentityConstants.ApplicationScheme sørger for at appen tror det er en vanlig login)
        var identity = new ClaimsIdentity(claims, IdentityConstants.ApplicationScheme);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, IdentityConstants.ApplicationScheme);

        return AuthenticateResult.Success(ticket);
    }
}

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using pract5;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

builder.Services.AddAuthorization();
builder.Services.AddMvc();
builder.Services.AddControllers();
builder.Services.AddAuthentication(authOptions =>
{
    authOptions.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    authOptions.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    authOptions.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    authOptions.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
}).AddOpenIdConnect(oidOptions =>
{
    oidOptions.ClientId = builder.Configuration["Okta:Clientid"];
    oidOptions.ClientSecret = builder.Configuration["Okta:ClientSecret"];
    oidOptions.CallbackPath = "/authorization-code/callback";
    oidOptions.Authority = builder.Configuration["Okta:Issuer"];
    oidOptions.ResponseType = "code";
    oidOptions.SaveTokens = true;
    oidOptions.Scope.Add("openid");
    oidOptions.Scope.Add("profile");
    oidOptions.TokenValidationParameters.ValidateIssuer = false;
    oidOptions.TokenValidationParameters.NameClaimType = "name";
}).AddCookie();
builder.Services.AddHttpClient<OktaApiService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

/*
 * 
 * o =>
{
    var policy = new AuthorizationPolicyBuilder()
       .RequireAuthenticatedUser()
       .Build();
    o.Filters.Add(new AuthorizeFilter(policy));
}


 * builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.Always;
})
   .AddAuthentication(options =>
   {
       options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
       options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
   })
   .AddCookie()
   .AddOktaMvc(new OktaMvcOptions
   {
       OktaDomain = builder.Configuration["Okta:Issuer"],
       AuthorizationServerId = builder.Configuration["Okta:AuthorizationServerId"],
       ClientId = builder.Configuration["Okta:ClientId"],
       ClientSecret = builder.Configuration["Okta:ClientSecret"],
       Scope = new List<string> { "openid", "profile", "email" },
   });

if (builder.Environment.IsDevelopment())
{
    IdentityModelEventSource.ShowPII = true;
}

builder.Services.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = builder.Configuration["Auth0:Domain"];
    options.ClientId = builder.Configuration["Auth0:ClientId"];
});
builder.Services.AddAuthentication("Bearer")
           .AddJwtBearer("Bearer", options =>
           {
               options.Authority = "https://localhost:5005";
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateAudience = false
               };
           });

 */
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = "cookie";
        options.DefaultChallengeScheme = "oidc";
    })
    .AddCookie("cookie", options =>
    {
    })
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = "https://developer.api.autodesk.com";
        
        options.ClientId = "<CLIENT_ID>";
        options.ClientSecret = "<CLIENT_SECRET>";
        options.UsePkce = false;
        options.ResponseType = "code";
        options.ResponseMode = "query";
        
        options.Scope.Clear();
        options.Scope.Add("openid");

        options.GetClaimsFromUserInfoEndpoint = true;
        options.SaveTokens = true;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
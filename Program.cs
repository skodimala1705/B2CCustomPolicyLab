using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews()
    .AddMicrosoftIdentityUI();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "B2CSignInCookie";
    options.DefaultChallengeScheme = "B2CSignIn";
})
.AddMicrosoftIdentityWebApp(
    options =>
    {
        builder.Configuration.Bind("AzureAdB2C", options);
    },
    openIdConnectScheme: "B2CSignIn",
    cookieScheme: "B2CSignInCookie");

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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

var actionProvider =
    app.Services.GetRequiredService<IActionDescriptorCollectionProvider>();

Console.WriteLine("===== CONTROLLERS =====");

foreach (var action in actionProvider.ActionDescriptors.Items)
{
    Console.WriteLine(action.DisplayName);
}

Console.WriteLine("=======================");

app.Run();
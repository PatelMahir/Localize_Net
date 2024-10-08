using Localize_Net;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddMvc().AddViewLocalization
    (LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();
builder.Services.AddMvc().AddDataAnnotationsLocalization(options =>
{
    options.DataAnnotationLocalizerProvider = (type, factory) =>
    factory.Create(typeof(SharedResource));
});
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { "en-US", "fr" };
    options.SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures)
    .AddInitialRequestCultureProvider(new CustomRequestCultureProvider
    (async context =>
    {
        return await Task.FromResult(new ProviderCultureResult("en"));
    }));
});
//builder.Services.AddPortableObjectLocalization();
builder.Services.AddRazorPages().AddViewLocalization();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseRequestLocalization(new RequestLocalizationOptions
{
    ApplyCurrentCultureToResponseHeaders = true
});
app.UseRequestLocalization(options =>
{
    var questStringCultureProvider = options.RequestCultureProviders[0];
    options.RequestCultureProviders.RemoveAt(0);
    options.CultureInfoUseUserOverride = false;
    options.AddInitialRequestCultureProvider(new CustomRequestCultureProvider
        (async context =>
        {
            var currentCulture = "en";
            var segments = context.Request.Path.Value.Split(new char[] { '/' },
                StringSplitOptions.RemoveEmptyEntries);
            if (segments.Length > 1 && segments[0].Length == 2)
            {
                currentCulture = segments[0];
            }
            var requestCulture = new ProviderCultureResult(currentCulture);
            await Task.FromResult(requestCulture);
            return requestCulture;
        }));
    options.RequestCultureProviders.Insert(1, questStringCultureProvider);
});
app.MapControllers();
app.Run();
using Microsoft.AspNetCore.Localization;

namespace Localize_Net
{
    public class AppSettingsRequestCultureProvider : RequestCultureProvider
    {
        public string CultureKey { get; set; } = "culture";
        public string UICultureKey { get; set; } = "ui-culture";
        public override Task<ProviderCultureResult>DetermineProviderCultureResult(HttpContext httpContext)
        {
            if(httpContext == null)
            {
                throw new ArgumentException();
            }
            var configuration=httpContext.RequestServices.
                GetService<IConfigurationRoot>();
            var culture = configuration[CultureKey];
            var uiCulture= configuration[UICultureKey];
            if(culture == null && uiCulture == null)
            {
                return Task.FromResult((ProviderCultureResult)null);
            }
            if(culture!=null && uiCulture == null)
            {
                uiCulture = culture;
            }
            if(culture == null && uiCulture != null)
            {
                culture = uiCulture;
            }
            var providerResultCulture = new ProviderCultureResult(culture, uiCulture);
            return Task.FromResult(providerResultCulture);
        }
    }
}
using DGA.RelyingParty.OpenID_Connect.MVC.Utilities;
using DGA.RelyingParty.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DGA.RelyingParty.OpenID_Connect.MVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();

            var builder = services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(options => {
                options.LoginPath = "/Account/Login";

            });

            builder.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
            {
                options.Authority = "https://connect.dga.or.th";
                options.ClientId = "WS Consumer-Key";
                options.ClientSecret = SecretUtility.encodeSecret("WS Consumer-Secret");
                options.ResponseType = "code";

                //Required scope
                options.Scope.Add(DGAScope.OpenId);
                options.Scope.Add(DGAScope.Profile);

                //Optional scope
                options.Scope.Add(DGAScope.UserId);
                options.Scope.Add(DGAScope.UserName);
                options.Scope.Add(DGAScope.FirstName);
                options.Scope.Add(DGAScope.LastName);
                options.Scope.Add(DGAScope.MiddleName);
                options.Scope.Add(DGAScope.CitizenId);
                options.Scope.Add(DGAScope.CitizenIdVerified);
                options.Scope.Add(DGAScope.Mobile);
                options.Scope.Add(DGAScope.MobileVerified);
                options.Scope.Add(DGAScope.Email);
                options.Scope.Add(DGAScope.EmailVerified);
                options.Scope.Add(DGAScope.IALLevel);
                options.Scope.Add(DGAScope.PersonalToken);

                //Map to local claim
                options.GetClaimsFromUserInfoEndpoint = true;
                options.ClaimActions.MapJsonKey(DGAScope.CitizenId, DGAScope.CitizenId);
                options.ClaimActions.MapJsonKey(DGAScope.CitizenIdVerified, DGAScope.CitizenIdVerified);
                options.ClaimActions.MapJsonKey(DGAScope.IALLevel, DGAScope.IALLevel);
                options.ClaimActions.MapJsonKey(DGAScope.Mobile, DGAScope.Mobile);
                options.ClaimActions.MapJsonKey(DGAScope.MobileVerified, DGAScope.MobileVerified);
                options.ClaimActions.MapJsonKey(DGAScope.Email, DGAScope.Email);
                options.ClaimActions.MapJsonKey(DGAScope.EmailVerified, DGAScope.EmailVerified);
                options.ClaimActions.MapJsonKey(DGAScope.PersonalToken, DGAScope.PersonalToken);
                options.ClaimActions.MapJsonKey(DGAScope.UserId, DGAScope.UserId);
                options.ClaimActions.MapJsonKey(DGAScope.UserName, DGAScope.UserName);

                options.Events = new OpenIdConnectEvents
                {
                    OnRemoteFailure = context =>
                    {
                        context.Response.Redirect("Account/RemoteFailure");
                        context.HandleResponse();

                        return Task.FromResult(0);
                    },
                    OnAccessDenied = context =>
                    {
                        context.Response.Redirect("Account/AccessDenied");
                        context.HandleResponse();

                        return Task.FromResult(0);
                    }
                };
                options.SaveTokens = true;
            });

            services.AddScoped<SignInUtility>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

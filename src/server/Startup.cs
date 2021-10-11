using Server.GraphQL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Server.Repository;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Server.Security;
using Microsoft.Extensions.Configuration;

namespace Server
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddGraphQLServer()
                .AddErrorFilter<GeneralErrorFilter>()
                .AddAuthorization()
                //.AddProjections() // not needed when only using explicit GraphQL/hotchocolate types since we set up projection logic manually there
                .AddFiltering()
                .AddSorting()
                .AddQueryType<Query>()
                .AddMutationType<Mutations>()
                .AddType<AuthorType>()
                .AddType<BookType>()
                .AddType<SystemUserType>();

            services.AddSingleton<IUserAuthenticationService, UserAuthenticationService>();
            services.AddSingleton<IInMemDataRepo, InMemDataRepo>();
            services.AddSingleton<IConfigureOptions<JwtBearerOptions>, ConfigureJwtBearerOptions>();
            services.Configure<SecurityConfiguration>(Configuration.GetSection(SecurityConfiguration.Identifier));

            ConfigureSecurity(services);
        }

        private static void ConfigureSecurity(IServiceCollection services)
        {
            // Configure jwt authentication
            // See: https://github.com/dotnet/aspnetcore/issues/21491
            // And: https://stackoverflow.com/questions/61186836/jwt-bearer-and-dependency-injection
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(); // This will be configured via ConfigureJwtBearerOptions

            services.AddAuthorization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
                endpoints.MapGraphQLVoyager("voyager");
            });
        }
    }
}

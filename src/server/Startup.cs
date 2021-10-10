using Server.GraphQL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Server.Repository;

namespace Server
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddGraphQLServer()
                //.AddProjections() // not needed when only using explicit GraphQL/hotchocolate types since we set up projection logic manually there
                .AddFiltering()
                .AddSorting()
                .AddQueryType<Query>()
                .AddType<AuthorType>()
                .AddType<BookType>();
                //.AddMutationType<Mutations>();

            services.AddSingleton<IInMemDataRepo, InMemDataRepo>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapGet("/", async context => await context.Response.WriteAsync("Hello World!"));
                endpoints.MapGraphQL();
                endpoints.MapGraphQLVoyager("voyager");
            });
        }
    }
}

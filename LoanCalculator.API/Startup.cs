using LoanCalculator.Application;
using LoanCalculator.Application.Customer.Interfaces;
using LoanCalculator.Infrastructure;
using LoanCalculator.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using System.Threading.Tasks;

namespace LoanCalculator.API
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
            services.AddMediatR(typeof(Startup));
            services.RegisterInfrastructerServices(Configuration);
            services.RegisterApplicationServices(Configuration);
            services.RegisterDatabaseContext(Configuration);

            var corsQuoteSubmissionPolicy = Configuration.GetSection("CorsPolicies").GetValue<string>("UserPolicy");
            var allowedOrigins = Configuration.GetSection("AllowedOrigins").Value.Split(',');

            services.AddCors(options =>
            {
                options.AddPolicy(name: corsQuoteSubmissionPolicy,
                                  builder =>
                                  {
                                      builder.WithOrigins(allowedOrigins)
                                             .AllowAnyHeader()
                                             .AllowAnyMethod();
                                  });
            });

            services.AddControllers();
            services.AddAuthorization();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMvc(options => options.EnableEndpointRouting = false);

            // configure jwt authentication
            var key = Encoding.ASCII.GetBytes(APIKeys.ApiKey.QuoteSubmissionApiKey);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userService = context.HttpContext.RequestServices.GetRequiredService<ICustomerService>();
                        var userId = int.Parse(context.Principal.Identity.Name);
                        var user = userService.GetCustomerByIdAsync(userId);
                        if (user == null)
                        {
                            // return unauthorized if user no longer exists
                            context.Fail("Unauthorized");
                        }
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo { Title = "LoanCalculator.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("./swagger/v1/swagger.json", "LoanCalculator.Api v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            // global cors policy
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}

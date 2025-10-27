using FluentValidation.AspNetCore;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SurveyBasket.api.Authentication;
using SurveyBasket.api.Persistence;
using SurveyBasket.api.Services;
using SurveyBasket.Services;
using System.Reflection;
using System.Text;

namespace SurveyBasket.api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services,IConfiguration configuration)
        {


            services.AddControllers();
            services.AddAuthConfig(configuration);
            #region Datebase  Configration
            // DataBase
            var connectionstring = configuration.GetConnectionString("DefultConnections") ?? throw new InvalidOperationException("Connections string 'defultConnecting' not found");

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(connectionstring)
                );

            #endregion

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });

            // Add services to the container.


            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            //services.AddOpenApi();
            //services.AddSwaggerGen();
            services.AddSwaggerGen();

            //AddMapster : mapping using Mapster
            services.AddMapsterConf();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPollService, PollService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IVoteService, VoteService>();
            services.AddScoped<IResultService, ResultService>();
            //validation using fluentvalidtion 
            services.AddFluentValidationConf();
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();

            services .AddHybridCache();
            return services;
        }



        public static IServiceCollection AddswaggerServices(this IServiceCollection services)
        {

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            services.AddOpenApi();
            services.AddSwaggerGen();
            return services;
        }

        public static IServiceCollection AddMapsterConf(this IServiceCollection services)
        {

            //AddMapster : mapping using Mapster

            var mappingConfig = TypeAdapterConfig.GlobalSettings;
            mappingConfig.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton<IMapper>(new Mapper(mappingConfig));


            return services;
        }

        public static IServiceCollection AddFluentValidationConf(this IServiceCollection services)
        {

            services.AddFluentValidationAutoValidation()
     .AddValidatorsFromAssemblyContaining<Program>();

            return services;
        }


        /// user Manger 
        private static IServiceCollection AddAuthConfig(this IServiceCollection services,
          IConfiguration configuration)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
              .AddEntityFrameworkStores<ApplicationDbContext>();


            services.AddSingleton<IJwtProvider, JwtProvider>();

            //services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
            services.AddOptions<JwtOptions>()
                .BindConfiguration(JwtOptions.SectionName)
                .ValidateDataAnnotations()
                .ValidateOnStart();

            var jwtSettings = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings?.Key!)),
                    ValidIssuer = jwtSettings?.Issuer,
                    ValidAudience = jwtSettings?.Audience
                };
            });

            return services;
        }


    }


      
}

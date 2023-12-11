using MySqlConnector;
using projeto.databaseAdapters;
using projeto.servicebusAdapter;

namespace projeto.service.Extensions;
public static class ServicesColletionExtensions
{

    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo { Title = "Api para projetos", Version = "v1" });
            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Adicione o token para logar",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
            });
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            option.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
        });
        services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
        return services;
    }

    public static IServiceCollection AddJwtToken(this IServiceCollection services, IConfiguration config)
    {
        services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
        ).AddJwtBearer(opt =>
        {
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = config["Jwt:Issuer"],
                ValidAudience = config["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(config["Jwt:SecretKey"]))
            };
        });
        return services;
    }

    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IRepoProjetos, RepoProjetos>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IServiceBusAdapter, ServiceBusAdapter>();
        services.AddTransient<IDatabaseAdapter, DatabaseAdapter>();
        services.AddAutoMapper(typeof(Program).Assembly);
        return services;
    }
}

using Microsoft.EntityFrameworkCore;
using PoS.Abstractions;
using PoS.Data;
using PoS.Extensions;

namespace PoS
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                var filePath = Path.Combine(AppContext.BaseDirectory, "PoS.xml");
                c.IncludeXmlComments(filePath);
            });

            services.AddCors(p =>
                p.AddPolicy("corsapp", builder => {
                    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
                }));

            services.AddControllers().AddNewtonsoftJson(x =>
             x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


            services.AddControllers();

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddDbContext<ApplicationDbContext>(options => { options.UseSqlServer(connectionString); });

            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            services.AddDependencies(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "T2AD PoS API V1");
                    c.DocumentTitle = "T2AD PoS API";
                });
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Quartz.AspNetCore;
using System.Security.Cryptography;
using Water.Web;

internal class Program
{
    private static void Main(string[] args)
    {
        //Console.Write("Enter a password: ");
        //string? password = Console.ReadLine();

        //// Generate a 128-bit salt using a sequence of
        //// cryptographically strong random bytes.
        //byte[] salt = RandomNumberGenerator.GetBytes(128 / 8); // divide by 8 to convert bits to bytes
        //Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");

        //// derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
        //string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
        //    password: password!,
        //    salt: salt,
        //    prf: KeyDerivationPrf.HMACSHA256,
        //    iterationCount: 100000,
        //    numBytesRequested: 256 / 8));

        //Console.WriteLine($"Hashed: {hashed}");
        //return;
        var builder = WebApplication.CreateSlimBuilder(args);
        builder.Services.AddGrpc();
        builder.Services.AddQuartz(); 
        builder.Services.AddQuartzServer(options =>
        {
            options.WaitForJobsToComplete = true;
        });
        builder.Services.AddGrpcReflection();
        builder.Services.AddDbContextPool<WaterDbContext>((provider, builder) =>
        {
            var cfg = provider.GetRequiredService<IConfiguration>();
            var connStr = cfg["ConnectionStrings:PgSql"];
            builder.UseNpgsql(connStr);
        });
        var app = builder.Build();
        app.UseStaticFiles();
        var apiGroup = app.MapGroup("/api");
        app.UseGrpcWeb();
        apiGroup.MapGrpcService<CharServiceImpl>().EnableGrpcWeb();
        apiGroup.MapGrpcReflectionService();
#if DEBUG
        app.MapWhen(ctx => !ctx.Request.Path.StartsWithSegments("/api"), b =>
        {
            b.UseSpa(spaB =>
            {
                spaB.UseProxyToSpaDevelopmentServer("http://localhost:8080");
            });
        });
#else
        app.MapFallbackToFile("index.html");
#endif
        using (var scope = app.Services.CreateScope())
        {
            var dbc = scope.ServiceProvider.GetRequiredService<WaterDbContext>();
            dbc.Database.EnsureCreated();
            var schedulerFactory=scope.ServiceProvider.GetRequiredService<ISchedulerFactory>();
            var scheduler=schedulerFactory.GetScheduler().GetAwaiter().GetResult();
            scheduler.Start().GetAwaiter().GetResult();
        }
        app.Run();
    }
}
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prock.Core;
using System.IO;
using System.Linq;

namespace Prock.Sample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var mocks = Configuration.GetSection(nameof(Mock)).Get<Mock[]>();
            foreach (var mock in mocks.Where(m => m.FilePath != null))
            {
                mock.Json = File.ReadAllText(mock.FilePath);
            }

            app.UseMocks(mocks);
        }
    }
}
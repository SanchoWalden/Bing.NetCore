using Bing.Core.Modularity;
using Bing.DependencyInjection;
using Bing.Logging;
using Bing.Modularity;
using Bing.Modularity.PlugIns;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace Bing.Tests.Modularity
{
    public class ModuleLoaderTest
    {
        [Fact]
        public void Should_Load_Modules_By_Dependency_Order()
        {
            var moduleLoader = new ModuleLoader();
            var modules = moduleLoader.LoadModules(
                new ServiceCollection()
                    .AddSingleton<IInitLoggerFactory>(new DefaultInitLoggerFactory()),
                typeof(MyStartupModule),
                new PlugInSourceList()
            );
            modules.Length.ShouldBe(2);
            modules[0].Type.ShouldBe(typeof(DependencyModule));
            modules[1].Type.ShouldBe(typeof(MyStartupModule));
        }

        [DependsOn(typeof(DependencyModule))]
        public class MyStartupModule : BingModule
        {

        }
    }
}

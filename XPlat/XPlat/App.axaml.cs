using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReactiveUI;
using Splat;
using Splat.Microsoft.Extensions.DependencyInjection;
using XPlat.ViewModels;
using XPlat.Views;

namespace XPlat
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

    
            public IServiceProvider Container { get; private set; }
            public IHost host { get; set; }
   

            public override void OnFrameworkInitializationCompleted()
            {
                host = Host.CreateDefaultBuilder()
                    .ConfigureServices((_, services) =>
                    {
                        services.UseMicrosoftDependencyResolver();
                        var resolver = Locator.CurrentMutable;
                        resolver.InitializeSplat();
                        resolver.InitializeReactiveUI();

                        services.AddHttpClient();
         

                        services.AddTransient<MainWindow>();
      
                    })
                    .Build();
                Container = host.Services;
                Container.UseMicrosoftDependencyResolver();
                if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                {
                    desktop.MainWindow =
                        host.Services.GetRequiredService<MainWindow>();
                }

                base.OnFrameworkInitializationCompleted();
                
            }
        
    }
}
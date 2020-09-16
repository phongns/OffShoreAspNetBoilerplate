﻿using System;
using Abp.Authorization;
using Abp.Dependency;
using JetBrains.Annotations;

namespace Abp
{
    /// <summary>
    /// This is the main class that is responsible to start entire ABP system.
    /// Prepares dependency injection and registers core components needed for startup.
    /// It must be instantiated and initialized (see <see cref="Initialize"/>) first in an application.
    /// </summary>
    public class AbpBootstrapper : IDisposable
    {
        /// <summary>
        /// Gets IIocManager object used by this class.
        /// </summary>
        public IIocManager IocManager { get; }


        /// <summary>
        /// Creates a new <see cref="AbpBootstrapper"/> instance.
        /// </summary>
        /// <param name="startupModule">Startup module of the application which depends on other used modules. Should be derived from <see cref="AbpModule"/>.</param>
        /// <param name="optionsAction">An action to set options</param>
        private AbpBootstrapper([NotNull] Type startupModule, [CanBeNull] Action<AbpBootstrapperOptions> optionsAction = null)
        {
            var options = new AbpBootstrapperOptions();
            optionsAction?.Invoke(options);

            //if (!typeof(AbpModule).GetTypeInfo().IsAssignableFrom(startupModule))
            //{
            //    throw new ArgumentException($"{nameof(startupModule)} should be derived from {nameof(AbpModule)}.");
            //}

            //StartupModule = startupModule;

            IocManager = options.IocManager;
            //PlugInSources = options.PlugInSources;

            if (!options.DisableAllInterceptors)
            {
                AddInterceptorRegistrars();
            }
        }

        /// <summary>
        /// Creates a new <see cref="AbpBootstrapper"/> instance.
        /// </summary>
        /// <typeparam name="TStartupModule">Startup module of the application which depends on other used modules. Should be derived from <see cref="AbpModule"/>.</typeparam>
        /// <param name="optionsAction">An action to set options</param>
        public static AbpBootstrapper Create<TStartupModule>([CanBeNull] Action<AbpBootstrapperOptions> optionsAction = null)
        //where TStartupModule : AbpModule
        {
            return new AbpBootstrapper(typeof(TStartupModule), optionsAction);
        }

        /// <summary>
        /// Creates a new <see cref="AbpBootstrapper"/> instance.
        /// </summary>
        /// <param name="startupModule">Startup module of the application which depends on other used modules. Should be derived from <see cref="AbpModule"/>.</param>
        /// <param name="optionsAction">An action to set options</param>
        public static AbpBootstrapper Create([NotNull] Type startupModule, [CanBeNull] Action<AbpBootstrapperOptions> optionsAction = null)
        {
            return new AbpBootstrapper(startupModule, optionsAction);
        }

        private void AddInterceptorRegistrars()
        {
            //ValidationInterceptorRegistrar.Initialize(IocManager);
            //AuditingInterceptorRegistrar.Initialize(IocManager);
            //EntityHistoryInterceptorRegistrar.Initialize(IocManager);
            //UnitOfWorkRegistrar.Initialize(IocManager);
            AuthorizationInterceptorRegistrar.Initialize(IocManager);
        }

        /// <summary>
        /// Initializes the ABP system.
        /// </summary>
        public virtual void Initialize()
        {
            //ResolveLogger();

            //try
            //{
            //    RegisterBootstrapper();
            //    IocManager.IocContainer.Install(new AbpCoreInstaller());

            //    IocManager.Resolve<AbpPlugInManager>().PlugInSources.AddRange(PlugInSources);
            //    IocManager.Resolve<AbpStartupConfiguration>().Initialize();

            //    _moduleManager = IocManager.Resolve<AbpModuleManager>();
            //    _moduleManager.Initialize(StartupModule);
            //    _moduleManager.StartModules();
            //}
            //catch (Exception ex)
            //{
            //    _logger.Fatal(ex.ToString(), ex);
            //    throw;
            //}

            //AuthorizationInterceptorRegistrar.Initialize(IocManager);
        }

        /// <summary>
        /// Disposes the ABP system.
        /// </summary>
        public virtual void Dispose()
        {
            //if (IsDisposed)
            //{
            //    return;
            //}

            //IsDisposed = true;

            //_moduleManager?.ShutdownModules();
        }
    }
}

namespace simple_cms.App_Start
{
    using Autofac;
    using Autofac.Integration.WebApi;
    using simple_cms.Controllers;
    using simple_cms.Manager;
    using simple_cms.Repository;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Http;

    public static class AutofacConfig
    {
        public static void Register(HttpConfiguration config)
        {
            IContainer container = RegisterContentController();
            config.DependencyResolver= new AutofacWebApiDependencyResolver(container);
        }

        public static  IContainer RegisterContentController()
        {
            ContainerBuilder builder= new ContainerBuilder();
            builder.RegisterType<ContentController>();
            builder.RegisterType<ContentManager>().As<IContentManager>();
            builder.RegisterType<CloudSettings>().As<ISettingRepository>();
            builder.RegisterType<CloudRepository>().As<IContentRepository>();
            return builder.Build();
        }
    }
}
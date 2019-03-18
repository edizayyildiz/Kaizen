using Autofac;
using Autofac.Integration.Mvc;
using Kaizen.Data;
using Kaizen.Model;
using Kaizen.Service;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Kaizen.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var builder = new ContainerBuilder();

            // Register your MVC controllers. (MvcApplication is the name of
            // the class in Global.asax.)
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            builder.RegisterType<ApplicationDbContext>().InstancePerRequest();
            builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).InstancePerRequest();
            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication.User.Identity).As<System.Security.Principal.IIdentity>();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerDependency();
            builder.RegisterType(typeof(CityService)).As(typeof(ICityService)).InstancePerDependency();
            builder.RegisterType(typeof(CompanyService)).As(typeof(ICompanyService)).InstancePerDependency();
            builder.RegisterType(typeof(CountryService)).As(typeof(ICountryService)).InstancePerDependency();
            builder.RegisterType(typeof(CountyService)).As(typeof(ICountyService)).InstancePerDependency();
            builder.RegisterType(typeof(DepartmentService)).As(typeof(IDepartmentService)).InstancePerDependency();
            builder.RegisterType(typeof(EmployeeService)).As(typeof(IEmployeeService)).InstancePerDependency();
            builder.RegisterType(typeof(SuggestionService)).As(typeof(ISuggestionService)).InstancePerDependency();
            builder.RegisterType(typeof(BranchService)).As(typeof(IBranchService)).InstancePerDependency();

            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();
            builder.Register(c => new UserStore<ApplicationUser>(c.Resolve<ApplicationDbContext>())).AsImplementedInterfaces().InstancePerRequest();
            //builder.Register(c => new RoleStore<IdentityRole>(c.Resolve<ApplicationDbContext>())).InstancePerRequest();
            //builder.RegisterType<ApplicationRoleManager>().AsSelf().InstancePerRequest();
            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).As<IAuthenticationManager>();
            builder.Register(c => new IdentityFactoryOptions<ApplicationUserManager>
            {
                DataProtectionProvider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("Application​")
            });

            //Set the dependency resolver to be Autofac.

           var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            new AutoMapperConfig().Initialize();
        }
    }
}

using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using OAuth2;
using OnlinerNotifier.BLL.Services.Implementations;
using OnlinerNotifier.BLL.Mappers.Implementations;
using OnlinerNotifier.BLL.Mappers.Interfaces;
using OnlinerNotifier.BLL.Services.Implementations.EmailServices;
using OnlinerNotifier.BLL.Services.Implementations.NotificationDataSevices;
using OnlinerNotifier.BLL.Services.Implementations.PriceChangesServices;
using OnlinerNotifier.BLL.Services.Implementations.UserProductServices;
using OnlinerNotifier.BLL.Services.Implementations.UserServices;
using OnlinerNotifier.BLL.Services.Interfaces;
using OnlinerNotifier.BLL.Services.Interfaces.EmailServices;
using OnlinerNotifier.BLL.Services.Interfaces.NotificationDataSevices;
using OnlinerNotifier.BLL.Services.Interfaces.PriceChangesServices;
using OnlinerNotifier.BLL.Services.Interfaces.RedisEmailServices;
using OnlinerNotifier.BLL.Services.Interfaces.UserProductServices;
using OnlinerNotifier.BLL.Services.Interfaces.UserServices;
using OnlinerNotifier.BLL.SmtpClients;
using OnlinerNotifier.BLL.Templates.Builders;
using OnlinerNotifier.BLL.Templates.TemplatePathProvider;
using OnlinerNotifier.BLL.Validators;
using OnlinerNotifier.DAL;
using OnlinerNotifier.Scheduler.Jobs;

namespace OnlinerNotifier
{
    public class AutofacConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            RegisterControllers(builder);
            RegisterApiControllers(builder);
            RegisterModelBinders(builder);
            RegisterModule(builder);
            RegisterSource(builder);
            RegisterFilterProvider(builder);
            RegisterTypes(builder);

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }

        private static void RegisterControllers(ContainerBuilder builder)
        {
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
        }

        private static void RegisterApiControllers(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
        }

        private static void RegisterModelBinders(ContainerBuilder builder)
        {
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();
        }

        private static void RegisterModule(ContainerBuilder builder)
        {
            builder.RegisterModule<AutofacWebTypesModule>();
        }

        private static void RegisterSource(ContainerBuilder builder)
        {
            builder.RegisterSource(new ViewRegistrationSource());
        }

        private static void RegisterFilterProvider(ContainerBuilder builder)
        {
            builder.RegisterFilterProvider();
        }

        private static void RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterType<AuthorizationRoot>().AsSelf().SingleInstance();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerDependency();
            RegisterServices(builder);
            RegisterMappers(builder);
            RegisterJobs(builder);
            builder.RegisterType<EmailValidator>().As<IEmailValidator>().SingleInstance();
            builder.RegisterType<SmtpClientWrapper>().As<ISmtpClient>();
            builder.RegisterType<TemplatePathProvider>().As<ITemplatePathProvider>();
            builder.RegisterType<RazorPriceChangesEmailBuilder>().As<IPriceChangesEmailBuilder>();
            ToastNotificationsConfig.Setup.RegisterDependencies(builder);
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<UserDataService>().As<IUserDataService>().InstancePerDependency();
            builder.RegisterType<UserManageService>().As<IUserManageService>().InstancePerDependency();
            builder.RegisterType<UserNotificationsService>().As<IUserNotificationsService>().InstancePerDependency();
            builder.RegisterType<NotifiableUsersService>().As<INotifiableUsersProvider>().InstancePerDependency();
            builder.RegisterType<UserNotificationsService>().As<IUserNotificationsService>().InstancePerDependency();
            builder.RegisterType<UserProductAddService>().As<IUserProductAddService>().InstancePerDependency();
            builder.RegisterType<UserProductGetService>().As<IUserProductGetService>().InstancePerDependency();
            builder.RegisterType<UserProductRemoveService>().As<IUserProductRemoveService>().InstancePerDependency();
            builder.RegisterType<UserProductService>().As<IUserProductService>().InstancePerDependency();
            builder.RegisterType<OnlinerSearchService>().As<IOnlinerSearchService>().InstancePerDependency();
            builder.RegisterType<PricesChangesInfoService>().As<IPricesChangesInfoService>().InstancePerDependency();
            builder.RegisterType<PriceChangesService>().As<IPriceChangesService>().InstancePerDependency();
            builder.RegisterType<NotificationDataDataService>().As<INotificationDataService>().InstancePerDependency();
            builder.RegisterType<UserProductChangesService>().As<IUserProductChangesService>().InstancePerDependency();
            builder.RegisterType<EmailModelSender>().As<IEmailModelSender>().InstancePerDependency();
            builder.RegisterType<EmailBuildingService>().As<IEmailBuildingService>().InstancePerDependency();
            builder.RegisterType<TrackingService>().As<ITrackingService>().InstancePerDependency();
            builder.RegisterType<TimeCalculationService>().As<ITimeCalculationService>().InstancePerDependency();
            builder.RegisterType<RedisEmailService>().As<IRedisEmailPublisher>().InstancePerDependency();
            builder.RegisterType<RedisEmailService>().As<IRedisEmailGetter>().InstancePerDependency();
        }

        private static void RegisterMappers(ContainerBuilder builder)
        {
            builder.RegisterType<UserMapper>().As<IUserMapper>().SingleInstance();
            builder.RegisterType<ProductMapper>().As<IProductMapper>().SingleInstance();
            builder.RegisterType<PriceChangesMapper>().As<IPriceChangesMapper>().SingleInstance();
            builder.RegisterType<UserProductsMapper>().As<IUserProductsMapper>().SingleInstance();
            builder.RegisterType<EmailMapper>().As<IEmailMapper>().SingleInstance();
            builder.RegisterType<NotificationProductChangesModelMapper>()
                .As<INotificationProductChangesModelMapper>().SingleInstance();
            builder.RegisterType<NotificationDataModelMapper>()
                .As<INotificationDataModelMapper>().SingleInstance();
        }

        private static void RegisterJobs(ContainerBuilder builder)
        {
            builder.RegisterType<CheckPricesJob>().AsSelf().InstancePerDependency();
            builder.RegisterType<SendEmailJob>().AsSelf().InstancePerDependency();
            builder.RegisterType<PushEmailsJob>().AsSelf().InstancePerDependency();
        }
    }
}
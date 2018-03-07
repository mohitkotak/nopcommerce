using Autofac;
using Autofac.Core;
using Nop.Core.Configuration;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Nop.Plugin.Widgets.Testimonial.Data;
using Nop.Plugin.Widgets.Testimonial.Domain;
using Nop.Plugin.Widgets.Testimonial.Services;
using Nop.Web.Framework.Mvc;

namespace Nop.Plugin.Widgets.Testimonial
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int Order
        {
            get
            {
                return 1;
            }
        }

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            builder.RegisterType<TestimonialService>().As<ITestimonialService>().InstancePerRequest();

            this.RegisterPluginDataContext<TestimonialObjectContext>(builder, "nop_object_context_testimonial");

            //override required repository with our custom context
            builder.RegisterType<EfRepository<TestimonialRecord>>()
                .As<IRepository<TestimonialRecord>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("nop_object_context_testimonial"))
                .InstancePerRequest();

        }
    }
}

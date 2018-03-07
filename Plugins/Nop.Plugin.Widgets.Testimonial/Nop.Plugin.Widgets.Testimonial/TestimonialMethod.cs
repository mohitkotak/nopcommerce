using Nop.Core;
using Nop.Core.Plugins;
using Nop.Plugin.Widgets.Testimonial.Data;
using Nop.Plugin.Widgets.Testimonial.Services;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace Nop.Plugin.Widgets.Testimonial
{
    public class TestimonialMethod : BasePlugin, IWidgetPlugin
    {
        #region Fields

        private readonly ISettingService _settingService;
        private readonly ITestimonialService _testimonialService;
        private readonly TestimonialSettings _testimonialSettings;
        private readonly IStoreContext _storeContext;
        private readonly TestimonialObjectContext _objectContext;

        #endregion

        #region Ctor

        public TestimonialMethod(ISettingService settingService,
            ITestimonialService testimonialService,
            TestimonialSettings testimonialSetting,
            IStoreContext storeContext,
            TestimonialObjectContext objectContext)
        {
            this._settingService = settingService;
            this._testimonialService = testimonialService;
            this._testimonialSettings = testimonialSetting;
            this._storeContext = storeContext;
            this._objectContext = objectContext;
        }



        #endregion

        #region Methods

        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "Testimonial";
            routeValues = new RouteValueDictionary { { "Namespaces", "Nop.Plugin.Widgets.Testimonial.Controllers" }, { "area", null } };
        }

        /// <summary>
        /// Install plugin
        /// </summary>
        public override void Install()
        {
            //settings
            var settings = new TestimonialSettings
            {
                NumberTestimonial = 10,
                RequiredApprovider = false
            };

            _settingService.SaveSetting(settings);

            //locales
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.Testimonial.Fields.AddNewRecordTitle", "Add new testimonial");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.Testimonial.Fields.DisplayOrder", "Display Order");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.Testimonial.Fields.TitleTestimonial", "Title Testimonial");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.Testimonial.Fields.CustomerName", "Customer Name");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.Testimonial.Fields.Store", "Store");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.Testimonial.Fields.LimitMethodsToCreated", "Limit shipping methods to configured ones");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.Testimonial.Fields.CustomerGuid", "Guid");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.Testimonial.Fields.DescriptionTestimonial", "Description");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.Testimonial.Fields.TitleVisibility", "Show Title");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.Testimonial.Fields.Visibility", "To show");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.Testimonial.Fields.AddNewTestimonial", "Add new testimonial");

            //database objects
            _objectContext.Install();

            //locales

            base.Install();
        }

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override void Uninstall()
        {
            //settings
            _settingService.DeleteSetting<TestimonialSettings>();


            //locales
            this.DeletePluginLocaleResource("Plugins.Widgets.Testimonial.Fields.AddNewRecordTitle");
            this.DeletePluginLocaleResource("Plugins.Widgets.Testimonial.Fields.CustomerName");
            this.DeletePluginLocaleResource("Plugins.Widgets.Testimonial.Fields.DisplayOrder");
            this.DeletePluginLocaleResource("Plugins.Widgets.Testimonial.Fields.TitleTestimonial");
            this.DeletePluginLocaleResource("Plugins.Widgets.Testimonial.Fields.Fields.LimitMethodsToCreated");
            this.DeletePluginLocaleResource("Plugins.Widgets.Testimonial.Fields.Store");
            this.DeletePluginLocaleResource("Plugins.Widgets.Testimonial.Fields.CustomerGuid");
            this.DeletePluginLocaleResource("Plugins.Widgets.Testimonial.Fields.DescriptionTestimonial");
            this.DeletePluginLocaleResource("Plugins.Widgets.Testimonial.Fields.TitleVisibility");
            this.DeletePluginLocaleResource("Plugins.Widgets.Testimonial.Fields.Visibility");
            this.DeletePluginLocaleResource("Plugins.Widgets.Testimonial.Fields.AddNewTestimonial");

            //database objects
            _objectContext.Uninstall();

            //locales


            base.Uninstall();
        }

        public IList<string> GetWidgetZones()
        {
            return new List<string> { "page_testimonial" };
        }

        public void GetDisplayWidgetRoute(string widgetZone, out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "PublicInfo";
            controllerName = "Testimonial";
            routeValues = new RouteValueDictionary
            {
                {"Namespaces", "Nop.Plugin.Widgets.Testimonial.Controllers"},
                {"area", null},
                {"widgetZone", widgetZone}
            };
        }

        #endregion

    }
}

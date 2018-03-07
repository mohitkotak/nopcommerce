using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.Testimonial.Models
{
    public class ConfigurationModel: BaseNopModel
    {
        [NopResourceDisplayName("Plugins.Widgets.Testimonial.Fields.LimitMethodsToCreated")]
        public bool LimitMethodsToCreated { get; set; }

        public bool TestimonialIsEnabled { get; set; }

        public int ActiveStoreScopeConfiguration { get; set; }
    }
}

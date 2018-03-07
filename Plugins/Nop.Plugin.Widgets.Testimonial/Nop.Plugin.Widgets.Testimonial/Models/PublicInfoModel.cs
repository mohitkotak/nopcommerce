using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.Testimonial.Models
{
    public class PublicInfoModel : BaseNopModel
    {
        public int NumberTestimonial { get; set; }
        public List<TestimonialModel> items { get; set; }
    }
}

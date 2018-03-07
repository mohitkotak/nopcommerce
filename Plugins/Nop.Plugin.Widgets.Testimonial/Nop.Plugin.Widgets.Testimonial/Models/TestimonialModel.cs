using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Nop.Plugin.Widgets.Testimonial.Models
{
    public class TestimonialModel : BaseNopEntityModel
    {
        public TestimonialModel()
        {
            AvailableStores = new List<SelectListItem>();
            AvailableCustomer = new List<SelectListItem>();
        }

        [NopResourceDisplayName("Plugins.Widgets.Testimonial.Fields.Store")]
        public int StoreId { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.Testimonial.Fields.CustomerGuid")]
        public string CustomerGuid { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.Testimonial.Fields.DisplayOrder")]
        public int DisplayOrder { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.Testimonial.Fields.CustomerName")]
        public string CustomerName { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.Testimonial.Fields.CustomerName")]
        public string CustomerCustomerName { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.Testimonial.Fields.TitleTestimonial")]
        public string TitleTestimonial { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.Testimonial.Fields.DescriptionTestimonial")]
        public string DescriptionTestimonial { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.Testimonial.Fields.TitleVisibility")]
        public bool TitleVisibility { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.Testimonial.Fields.Visibility")]
        public bool Visibility { get; set; }



        public SelectListItem Customer { get; set; }
        public IList<SelectListItem> AvailableStores { get; set; }

        public IList<SelectListItem> AvailableCustomer { get; set; }
    }
}

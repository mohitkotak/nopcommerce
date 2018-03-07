using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.Testimonial
{
    public class TestimonialSettings: ISettings
    {
        public int NumberTestimonial { get; set; }
        public bool RequiredApprovider { get; set; }
    }
}
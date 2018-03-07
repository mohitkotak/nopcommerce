using Nop.Data.Mapping;
using Nop.Plugin.Widgets.Testimonial.Domain;

namespace Nop.Plugin.Widgets.Testimonial.Data
{
    public partial class TestimonialRecordMap: NopEntityTypeConfiguration<TestimonialRecord>
    {
        public TestimonialRecordMap()
        {
            this.ToTable("CustomerTestimony");
            this.HasKey(x => x.Id);
        }
    }
}

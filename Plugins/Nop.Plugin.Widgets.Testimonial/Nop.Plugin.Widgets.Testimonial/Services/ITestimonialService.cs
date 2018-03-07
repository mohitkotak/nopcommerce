using Nop.Plugin.Widgets.Testimonial.Domain;
using Nop.Core;
using System.Collections.Generic;

namespace Nop.Plugin.Widgets.Testimonial.Services
{
   public partial interface ITestimonialService
    {
        IPagedList<TestimonialRecord> GetAllPage(int pageIndex = 0, int pageSize = int.MaxValue);
        TestimonialRecord GetById(int Id);

        IList<TestimonialRecord> GetAll();

        void InsertTestimonialRecord(TestimonialRecord testimonialRecord);

        void UpdateTestimonialRecord(TestimonialRecord testimonialRecord);
        void DeleteTestimonial(TestimonialRecord testimonialRecord);

        IList<TestimonialRecord> GetTestimonialByStore(int storeId);
        IList<TestimonialRecord> GetTestimonialPublicInfo(int storeId);
    }
}

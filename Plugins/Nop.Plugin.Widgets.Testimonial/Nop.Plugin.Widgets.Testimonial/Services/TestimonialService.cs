using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Plugin.Widgets.Testimonial.Domain;
using Nop.Core.Data;
using Nop.Core.Caching;

namespace Nop.Plugin.Widgets.Testimonial.Services
{
    public class TestimonialService : ITestimonialService
    {
        #region Constants

        private const string TESTIMONIAL_ALL_KEY = "Nop.testimonial.all-{0}-{1}";
        private const string TESTIMONIAL_PARRTERN_KEY = "Nop.testimonial.";

        #endregion

        #region Fields

        private readonly IRepository<TestimonialRecord> _tsmRepository;

        private readonly ICacheManager _cacheManager;

        #endregion

        public TestimonialService(IRepository<TestimonialRecord> tsmRepository, ICacheManager cacheManager)
        {
            this._tsmRepository = tsmRepository;
            this._cacheManager = cacheManager;
        }

        public IPagedList<TestimonialRecord> GetAllPage(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            string key = string.Format(TESTIMONIAL_ALL_KEY, pageIndex, pageSize);
            return _cacheManager.Get(key, () =>
            {
                var query = from t in _tsmRepository.Table
                            orderby t.StoreId, t.TitleTestimonial, t.CustomerGuid
                            select t;
                var records = new PagedList<TestimonialRecord>(query, pageIndex, pageSize);
                return records;
            });
        }

        public TestimonialRecord GetById(int Id)
        {
            if (Id == 0)
                return null;

            return _tsmRepository.GetById(Id);
        }

        public IList<TestimonialRecord> GetAll()
        {
            var query = from ts in _tsmRepository.Table
                        orderby ts.Id
                        select ts;

            var tsMethods = query.ToList();
            return tsMethods;
        }

        public void InsertTestimonialRecord(TestimonialRecord testimonialRecord)
        {
            if (testimonialRecord == null)
            {
                throw new ArgumentNullException(nameof(testimonialRecord));
            }

            _tsmRepository.Insert(testimonialRecord);

            _cacheManager.RemoveByPattern(TESTIMONIAL_PARRTERN_KEY);
        }

        public void UpdateTestimonialRecord(TestimonialRecord testimonialRecord)
        {
            if (testimonialRecord == null)
            {
                throw new ArgumentNullException(nameof(testimonialRecord));
            }

            _tsmRepository.Update(testimonialRecord);
            _cacheManager.RemoveByPattern(TESTIMONIAL_PARRTERN_KEY);
        }

        public void DeleteTestimonial(TestimonialRecord testimonialRecord)
        {
            if (testimonialRecord == null)
            {
                throw new ArgumentNullException(nameof(testimonialRecord));
            }
            _tsmRepository.Delete(testimonialRecord);
            _cacheManager.RemoveByPattern(TESTIMONIAL_PARRTERN_KEY);
        }

        public IList<TestimonialRecord> GetTestimonialByStore(int storeId)
        {
            var query = from ts in _tsmRepository.Table
                        where ts.StoreId == storeId
                        select ts;

            var tsMethods = query.ToList();
            return tsMethods;
        }

        public IList<TestimonialRecord> GetTestimonialPublicInfo(int storeId)
        {
            var query = from ts in _tsmRepository.Table
                        where ts.StoreId == storeId || ts.StoreId == 0 && ts.Visibility == true
                        orderby ts.DisplayOrder ascending
                        select ts;

            var tsMethods = query.ToList();
            return tsMethods;
        }
    }
}

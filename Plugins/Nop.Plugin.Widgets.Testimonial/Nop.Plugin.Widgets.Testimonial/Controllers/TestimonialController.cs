using Nop.Core;
using Nop.Core.Caching;
using Nop.Plugin.Widgets.Testimonial.Domain;
using Nop.Plugin.Widgets.Testimonial.Models;
using Nop.Plugin.Widgets.Testimonial.Services;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Localization;
using Nop.Services.Security;
using Nop.Services.Stores;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Kendoui;
using Nop.Web.Framework.Mvc;
using Nop.Web.Framework.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Nop.Plugin.Widgets.Testimonial.Controllers
{
  
    public class TestimonialController : BasePluginController
    {
        private readonly TestimonialSettings _testimonialSettings;
        private readonly ITestimonialService _testimonialService;
        private readonly ILocalizationService _localizationService;
        private readonly ISettingService _settingService;
        private readonly IStoreService _storeService;
        private readonly ICustomerService _customerService;
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly ICacheManager _cacheManager;
        private readonly IPermissionService _permissionService;

        public TestimonialController(TestimonialSettings testimonialSettings,
            ITestimonialService testiomonialService,
            ILocalizationService localizationService,
            ISettingService settingsService,
            IStoreService storeService,
            IWorkContext workContext,
            IStoreContext storeContext,
            ICacheManager cacheManager,
            IPermissionService permissionService,
            ICustomerService customerService)
        {
            this._testimonialSettings = testimonialSettings;
            this._testimonialService = testiomonialService;
            this._localizationService = localizationService;
            this._settingService = settingsService;
            this._storeService = storeService;
            this._workContext = workContext;
            this._storeContext = storeContext;
            this._cacheManager = cacheManager;
            this._permissionService = permissionService;
            this._customerService = customerService;
        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            //little hack here
            //always set culture to 'en-US' (Telerik has a bug related to editing decimal values in other cultures). Like currently it's done for admin area in Global.asax.cs
            CommonHelper.SetTelerikCulture();

            base.Initialize(requestContext);
        }

        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure()
        {

            //var scoreScope = this.GetActiveStoreScopeConfiguration(_storeService, _workContext);
            //var model = new TestimonialListModel
            //{
            //    LimitMethodsToCreated = _testimonialSettings.LimitMethodsToCreated,
            //    TestimonialIsEnabled = _testimonialSettings.TestimonialIsEnabled,
            //    ActiveStoreScopeConfiguration = scoreScope
            //};


            return View("~/Plugins/Widgets.Testimonial/Views/Configure.cshtml");
        }
        [ChildActionOnly]
        public ActionResult PublicInfo(string widgetZone, object additionaldata = null)
        {
            //var testimonial = 
            //var testimonial = _testimonialService.GetTestimonialByStore(_storeContext.CurrentStore.Id);
            var testimonialSettings = _settingService.LoadSetting<TestimonialSettings>(_storeContext.CurrentStore.Id);

            var model = new PublicInfoModel()
            {
                items = new List<TestimonialModel>(),
                NumberTestimonial = testimonialSettings.NumberTestimonial
            };
            var testimonial = _testimonialService.GetTestimonialPublicInfo(_storeContext.CurrentStore.Id);
            foreach (var ts in testimonial)
            {
                model.items.Add(new TestimonialModel()
                {
                    CustomerName = ts.CustomerName,
                    TitleVisibility = ts.TitleVisibility,
                    TitleTestimonial = ts.TitleTestimonial,
                    DescriptionTestimonial = ts.DescriptionTestimonial
                });
            }
            return View("~/Plugins/Widgets.Testimonial/Views/PublicInfo.cshtml", model);
        }

        public ActionResult Create()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return Content("Access denied");

            var scoreScope = this.GetActiveStoreScopeConfiguration(_storeService, _workContext);
            var model = new TestimonialModel();
            //var model = new TestimonialModel
            //{
            //    LimitMethodsToCreated = _testimonialSettings.LimitMethodsToCreated,
            //    TestimonialIsEnabled = _testimonialSettings.TestimonialIsEnabled,
            //    ActiveStoreScopeConfiguration = scoreScope
            //};

            model.AvailableStores.Add(new SelectListItem() { Text = "*", Value = "0" });
            model.AvailableCustomer.Add(new SelectListItem() { Text = "*", Value = null });

            foreach (var store in _storeService.GetAllStores())
            {
                model.AvailableStores.Add(new SelectListItem() { Text = store.Name, Value = store.Id.ToString() });
            }

            foreach (var customer in _customerService.GetAllCustomers())
            {
                model.AvailableCustomer.Add(new SelectListItem() { Text = customer.GetFullName(), Value = customer.CustomerGuid.ToString() });
            }

            model.Visibility = true;
            return View("~/Plugins/Widgets.Testimonial/Views/Create.cshtml", model);
        }

        [HttpPost]
        [AdminAntiForgery]
        public ActionResult Create(string btnId, string formId, TestimonialModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
            {
                return Json(new { Result = false, Message = _localizationService.GetResource("Plugins.Shipping.ByTotal.ManageShippingSettings.AccessDenied") });
            }

            if (model.CustomerGuid != "*")
            {
                var customer = _customerService.GetCustomerByGuid(Guid.Parse(model.CustomerGuid));
                model.Customer = new SelectListItem() { Text = customer.GetFullName(), Value = customer.CustomerGuid.ToString() };
            }

            var testimonialRecord = new TestimonialRecord
            {
                CustomerGuid = (model.Customer != null ? Guid.Parse(model.Customer.Value.ToString()) : (Guid?)null),
                CustomerName = (model.Customer != null ? model.Customer.Text : model.CustomerCustomerName),
                DescriptionTestimonial = model.DescriptionTestimonial,
                DisplayOrder = model.DisplayOrder,
                StoreId = model.StoreId,
                TitleTestimonial = model.TitleTestimonial,
                TitleVisibility = model.TitleVisibility,
                Visibility = model.Visibility,

            };
            _testimonialService.InsertTestimonialRecord(testimonialRecord);

            ViewBag.RefreshPage = true;
            ViewBag.btnId = btnId;
            ViewBag.formId = formId;

            return View("~/Plugins/Widgets.Testimonial/Views/Create.cshtml", model);
        }
        public ActionResult Edit(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
            {
                return Content("Access denied");
            }

            var testimonial = _testimonialService.GetById(id);
            if (testimonial == null)
                return RedirectToAction("Configure");

            var model = new TestimonialModel
            {
                TitleTestimonial = testimonial.TitleTestimonial,
                DisplayOrder = testimonial.DisplayOrder,
                Id = testimonial.Id,
                CustomerCustomerName = (testimonial.CustomerGuid == null ? testimonial.CustomerName : ""),
                DescriptionTestimonial = testimonial.DescriptionTestimonial,
                CustomerName = testimonial.CustomerName,
                CustomerGuid = testimonial.CustomerGuid.ToString(),
                StoreId = testimonial.StoreId,
                TitleVisibility = testimonial.TitleVisibility,
                Visibility = testimonial.Visibility

            };
            model.AvailableStores.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Configuration.Settings.StoreScope.AllStores"), Value = "0" });
            foreach (var store in _storeService.GetAllStores())
                model.AvailableStores.Add(new SelectListItem { Text = store.Name, Value = store.Id.ToString(), Selected = store.Id == model.StoreId });
            model.AvailableCustomer.Add(new SelectListItem { Text = "*" });
            foreach (var customer in _customerService.GetAllCustomers())
                model.AvailableCustomer.Add(new SelectListItem { Text = customer.GetFullName(), Value = customer.CustomerGuid.ToString(), Selected = customer.CustomerGuid.ToString() == model.CustomerGuid });

            return View("~/Plugins/Widgets.Testimonial/Views/Edit.cshtml", model);
        }

        [HttpPost]
        [AdminAntiForgery]
        public ActionResult Edit(string btnId, string formId, TestimonialModel model)
        {

            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
            {
                return Json(new { Result = false, Message = _localizationService.GetResource("Plugins.Shipping.ByTotal.ManageShippingSettings.AccessDenied") });
            }

            if (model.CustomerGuid != "*")
            {
                var customer = _customerService.GetCustomerByGuid(Guid.Parse(model.CustomerGuid));
                model.Customer = new SelectListItem() { Text = customer.GetFullName(), Value = customer.CustomerGuid.ToString() };
            }
            var testimonialRecord = _testimonialService.GetById(model.Id);



            testimonialRecord.CustomerGuid = (model.Customer != null ? Guid.Parse(model.Customer.Value.ToString()) : (Guid?)null);
            testimonialRecord.CustomerName = (model.Customer != null ? model.Customer.Text : model.CustomerCustomerName);
            testimonialRecord.DescriptionTestimonial = model.DescriptionTestimonial;
            testimonialRecord.DisplayOrder = model.DisplayOrder;
            testimonialRecord.StoreId = model.StoreId;
            testimonialRecord.TitleTestimonial = model.TitleTestimonial;
            testimonialRecord.TitleVisibility = model.TitleVisibility;
            testimonialRecord.Visibility = model.Visibility;


            _testimonialService.UpdateTestimonialRecord(testimonialRecord);

            ViewBag.RefreshPage = true;
            ViewBag.btnId = btnId;
            ViewBag.formId = formId;


            return View("~/Plugins/Widgets.Testimonial/Views/Edit.cshtml", model);

        }

        [HttpPost]
        [AdminAntiForgery]
        public ActionResult TestimonialDelete(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return Content("Access denied");

            var tsm = _testimonialService.GetById(id);
            if (tsm != null)
                _testimonialService.DeleteTestimonial(tsm);

            return new NullJsonResult();
        }


        #region Methods

        [HttpPost]
        public ActionResult TestimonialList(DataSourceRequest command)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return ErrorForKendoGridJson("Access denied");

            var testimonialModel = new List<TestimonialModel>();

            var records = _testimonialService.GetAllPage(command.Page - 1, command.PageSize);

            var sbtModel = records.Select(x =>
            {
                var m = new TestimonialModel
                {
                    CustomerGuid = x.CustomerGuid.ToString(),
                    CustomerName = x.CustomerName,
                    DescriptionTestimonial = x.DescriptionTestimonial,
                    Id = x.Id,
                    StoreId = x.StoreId,
                    TitleTestimonial = x.TitleTestimonial,
                    TitleVisibility = x.TitleVisibility,
                    Visibility = x.Visibility,
                    DisplayOrder = x.DisplayOrder
                };
                return m;
            }).ToList();


            //foreach (var testimonial in _testimonialService.GetAllPage
            //{
            //    testimonialModel.Add(new TestimonialModel()
            //    {
            //        CustomerGuid = testimonial.CustomerGuid,
            //        CustomerName = testimonial.CustomerName,
            //        DescriptionTestimonial = testimonial.DescriptionTestimonial,
            //        TitleTestimonial = testimonial.TitleTestimonial,
            //        Id = testimonial.Id,
            //        StoreId = testimonial.StoreId
            //    });
            //}

            var gridModel = new DataSourceResult
            {
                Data = sbtModel,
                Total = records.TotalCount
            };

            return Json(gridModel);
        }


        #endregion
    }
}

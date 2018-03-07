using Nop.Core;
using System;

namespace Nop.Plugin.Widgets.Testimonial.Domain
{
    public class TestimonialRecord: BaseEntity
    {
        /// <summary>
        /// Gets or sets the store identifier
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// Gets or sets the Customer identifier
        /// </summary>
        public Guid? CustomerGuid { get; set; }

        /// <summary>
        /// Gets or sets the Customer name
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Gets or sets the Title Testimonial
        /// </summary>
        public string TitleTestimonial { get; set; }

        /// <summary>
        /// Gets or sets the Description Testimonial
        /// </summary>
        public string DescriptionTestimonial { get; set; }


        /// <summary>
        /// Gets or sets the Title Visibility
        /// </summary>
        public bool TitleVisibility { get; set; }
        public int DisplayOrder { get;  set; }
        public bool Visibility { get;  set; }
    }
}

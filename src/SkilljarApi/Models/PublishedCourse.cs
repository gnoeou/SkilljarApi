using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkilljarApi.Models
{

    public class PublishedCourse
    {
        public string id { get; set; }
        public Course course { get; set; }
        public string slug { get; set; }
        public string course_url { get; set; }
        public bool hidden { get; set; }
        public bool registration_required { get; set; }
        public DateTime? registration_starts_at { get; set; }
        public DateTime? registration_ends_at { get; set; }
        public string timezone { get; set; }
        public Offer offer { get; set; }
    }

    public class Offer
    {
        public string id { get; set; }
        public bool registration_open { get; set; }
        public string registration_url { get; set; }
        public Price price { get; set; }
    }

    public class Price
    {
        public string amount { get; set; }
        public string currency_code { get; set; }
    }




}

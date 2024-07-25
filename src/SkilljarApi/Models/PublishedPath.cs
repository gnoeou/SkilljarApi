using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkilljarApi.Models
{

    public class PublishedPathList : ListResponse<List<PublishedPath>>
    {
    }

    public class PublishedPath
    {
        public string id { get; set; }
        public Path path { get; set; }
        public string path_url { get; set; }
        public Offer offer { get; set; }
    }

    public class Path
    {
        public string id { get; set; }
        public string title { get; set; }
        public int path_item_count { get; set; }
    }
}

using System.Collections.Generic;


namespace TwitterAdsAPIProject.Models
{

    public class TwitterAdsResponse
    {
        public List<LineItem> data { get; set; }
        //public MetaData meta { get; set; }
    }

    public class LineItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        // Diğer özellikler
    }

    //public class MetaData
    //{
    //    public string? request_id { get; set; }
    //    public int rate_limit { get; set; }
    //    public int rate_limit_remaining { get; set; }
    //    // Diğer özellikler
    //}

}

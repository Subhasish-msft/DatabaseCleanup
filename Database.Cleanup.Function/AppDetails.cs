using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Cleanup.Function
{
    public class AppDetails
    {
        public string AppName { get; set; }
        public string Url { get; set; }
        public string AuthKey { get; set; }
    }
    public class AppDetil {
        public AppDetails[] AppDetails { get; set; }
    }
}

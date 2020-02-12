using System;
using System.Collections.Generic;
using System.Text;

namespace CleanUpCore.Models
{
    class CosmosDb : DbBase
    {
        public string endpoint { get; set; }
        public string authKey { get; set; }
        public string database { get; set; }
        public string collection { get; set; }
    }
}

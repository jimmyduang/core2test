using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowWebApi.Entities
{
    public class Material
    {
        public int Id { set; get; }
        public int FlowId { get; set; }
        public string Name { set; get; }
        public Flow flow{ get; set; }
    }
}

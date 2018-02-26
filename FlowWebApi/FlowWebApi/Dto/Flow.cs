using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowWebApi.Dto
{
    public class Flow
    {

        public Flow()
        {
            Materials = new List<Material>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string des { get; set; }
        public ICollection<Material> Materials { get; set; }

        public int MaterialCount => Materials.Count;
    }
}

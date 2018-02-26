using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowWebApi.Dto
{
    public class ProductWithoutMaterialDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Des { get; set; }
    }
}

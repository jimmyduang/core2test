﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowWebApi.Entities
{
    public class Flow
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string des { get; set; }
        public ICollection<Material> Materials { get; set; }
    }
}

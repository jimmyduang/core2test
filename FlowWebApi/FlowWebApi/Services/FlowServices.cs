using FlowWebApi.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowWebApi.Services
{
    public class FlowServices
    {
        public static FlowServices Current { get; } = new FlowServices();

        public List<Flow> list { get; }

        private FlowServices(){
            list = new List<Flow>{
                new Flow
                {
                    Id = 1,
                    Name = "牛奶",
                    Price = 2.5M,
                    Materials=new List<Material>{
                         new Material
                        {
                            Id = 1,
                            Name = "水"
                        },
                        new Material
                        {
                            Id = 2,
                            Name = "奶粉"
                        }
                    }
                },
                new Flow
                {
                    Id = 2,
                    Name = "面包",
                    Price = 4.5M,
                    Materials = new List<Material>
                    {
                        new Material
                        {
                            Id = 3,
                            Name = "面粉"
                        },
                        new Material
                        {
                            Id = 4,
                            Name = "糖"
                        }
                    }
                },
                new Flow
                {
                    Id = 3,
                    Name = "啤酒",
                    Price = 7.5M,
                    Materials = new List<Material>
                    {
                        new Material
                        {
                            Id = 5,
                            Name = "麦芽"
                        },
                        new Material
                        {
                            Id = 6,
                            Name = "地下水"
                        }
                    }
                }
            };
        }
    }
}

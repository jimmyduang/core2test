using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlowWebApi.Dto;
using FlowWebApi.Repositories;
using FlowWebApi.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FlowWebApi.Controllers
{
    [Route("api/flow")]
    public class MaterialController : Controller
    {
        private readonly IFlowRepository _flowRepository;

        public MaterialController(IFlowRepository flowRepository) {
            _flowRepository = flowRepository;
        }

        // GET api/<controller>/5
        [HttpGet("{id}/materials")]
        public IActionResult Get(int id)
        {
            var result = _flowRepository.GetMaterials(id);
            if (result==null)
            {
                return NotFound();
            }
            var re = result.Select(x => new Material
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
            return Ok(new JsonResult(re));
        }

        // GET api/<controller>/5
        [HttpGet("{id}/materials/{mid}")]
        public IActionResult Get(int id,int mid)
        {
            if (!_flowRepository.FlowExist(id))
            {
                return NotFound();
            }
            var result = _flowRepository.GetMaterialForFlow(id,mid);
            if (result == null)
            {
                return NotFound();
            }
            //var m = result.Materials.SingleOrDefault(x => x.Id == mid);
            //if (m == null)
            //{
            //    return NotFound();
            //}
            var re = new Material {
                Id=result.Id,
                Name=result.Name
            };
            return Ok(new JsonResult(re));
        }


       
    }
}

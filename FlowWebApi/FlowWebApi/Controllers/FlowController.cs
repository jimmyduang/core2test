using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FlowWebApi.Dto;
using FlowWebApi.DtoPut;
using FlowWebApi.Dtos;
using FlowWebApi.Repositories;
using FlowWebApi.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FlowWebApi.Controllers
{
    [Route("api/flow")]
    public class FlowController : Controller
    {

        private readonly ILogger<FlowController> _log;
        private readonly IMailService _mailService;
        private readonly IFlowRepository _flowRepository;

        public FlowController(ILogger<FlowController> logger, IMailService mailService,IFlowRepository flowRepository)
        {
            _log = logger;
            _mailService = mailService;
            _flowRepository = flowRepository;
        }
        // GET: api/<controller>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
        [HttpGet]
        public IActionResult GetProducts()
        {
            var flow = _flowRepository.GetFlows();
            var result = Mapper.Map<IEnumerable<ProductWithoutMaterialDto>>(flow);
          
            return Ok(result);
        }

        // GET api/<controller>/5
        //[HttpGet("{id}", Name = "GetProduct")]
        [Route("{id}",Name = "GetProduct")]
        public IActionResult Get(int id,bool includeMaterial = false)
        {
            var product = _flowRepository.GetFlow(id, includeMaterial);
            if (product == null)
            {
                return NotFound();
            }
            if (includeMaterial)
            {
                var hasMa = Mapper.Map<Flow>(product);
                return Ok(hasMa);
            }
            var onlyProductResult = Mapper.Map<ProductWithoutMaterialDto>(product);
            return Ok(onlyProductResult);
            //try
            //{
            //    throw new Exception();
            //    var result = FlowServices.Current.list.FirstOrDefault(x => x.Id == id);

            //    if (result == null)
            //    {
            //        _log.LogInformation($"id为{id}的产品没有被找到");
            //        return NotFound();
            //    }
            //    return Ok(new JsonResult(result));
            //}
            //catch (Exception)
            //{
            //    _log.LogCritical($"{id}异常走起");
            //    return StatusCode(500, "内部错误");
            //}

        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]FlowCreation flow)
        {
            if (flow==null)
            {
                return BadRequest();
            }

            if (flow.Name=="产品")
            {
                ModelState.AddModelError("Name","产品名称不能有“产品”两个字");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
            var flownew = Mapper.Map<Entities.Flow>(flow);
           
            _flowRepository.AddFlow(flownew);
            if (!_flowRepository.save())
            {
                return StatusCode(500, "保存产品的时候出错");
            }
            var dto = Mapper.Map<Flow>(flownew);
            return CreatedAtRoute("GetProduct", new { id = dto.Id }, dto);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]FlowModification flow)
        {
            if (flow==null)
            {
                return BadRequest();
            }
            if (ModelState.IsValid==false)
            {
                return BadRequest(ModelState);
            }
            if (!_flowRepository.FlowExist(id))
            {
                return StatusCode(500, "不存在");
            }
            var result = _flowRepository.GetFlow(id);
            if (result==null)
            {
                return NotFound();
            }
            Mapper.Map(flow, result);
            if (!_flowRepository.save())
            {
                return StatusCode(500, "保存产品的时候出错");
            }
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id,[FromBody]JsonPatchDocument<FlowModification> patchDoc)
        {
            if (patchDoc==null)
            {
                return BadRequest();
            }
            var model = _flowRepository.GetFlow(id);
            if (model==null)
            {
                return NotFound();
            }
            var topatch= Mapper.Map<FlowModification>(model);
            patchDoc.ApplyTo(topatch, ModelState);

            TryValidateModel(topatch);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Mapper.Map(topatch,model);

            if (!_flowRepository.save())
            {
                return StatusCode(500, "更新的时候出错");
            }
            return NoContent();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var model = _flowRepository.GetFlow(id);
            if (model==null)
            {
                return NotFound();
            }

            _flowRepository.DeleteFlow(model);
            if (!_flowRepository.save())
            {
                return StatusCode(500,"删除出错！");
            }
            _mailService.send("flow delete",$"id为{id}的产品被干掉了！");
            return Ok();
        }
    }
}

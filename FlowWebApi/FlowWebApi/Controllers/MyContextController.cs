using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlowWebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlowWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/MyContext")]
    public class MyContextController : Controller
    {
        private MyContext _myContext;

        public MyContextController(MyContext myContext)
        {
            _myContext = myContext;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
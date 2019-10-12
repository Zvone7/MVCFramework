﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcFrameworkWeb.Services;

namespace MvcFrameworkWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : BaseController
    {
        public TestController(ControllerHelper controllerHelper) : base(controllerHelper) { }

        [HttpGet]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Int32> Testing()
        {
            return 5;
        }
    }
}
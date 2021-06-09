﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("crud")]
    public class CrudController : ControllerBase
    {
        private readonly IValuesHolder _holder;
        public IValuesHolder nHolder = new ValuesHolder();
        
        public CrudController(IValuesHolder holder)
        {
            _holder = holder;
            nHolder.Values.Add("1234");
        }

        [HttpPost("create")]
        public IActionResult Create([FromQuery] string input)
        {
            _holder.Add(input);
            return Ok();
        }

        [HttpGet]
        public string Test()
        {
            return "Test is good";
        }

        [HttpGet("read")]
        public IActionResult Read()
        {
            return Ok();//_holder.Get()
        }

        [HttpPut("update")]
        public IActionResult Update([FromQuery] string stringsToUpdate, [FromQuery] string newValue)
        {
            for (int i = 0; i < _holder.Values.Count; i++)
            {
                if (_holder.Values[i] == stringsToUpdate)
                    _holder.Values[i] = newValue;
            }

            return Ok();
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] string stringsToDelete)
        {
            _holder.Values = _holder.Values.Where(w => w != stringsToDelete).ToList();
            return Ok();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RestServer.Services;
using RestServer.Models;

namespace RestServer.Controllers
{
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private CustomersService svc;

        public CustomersController() {
            this.svc = new CustomersService();
        }
        // customer
        [HttpGet]
        public IActionResult Get()
        {
            return Json(this.svc.All());
        }

        // GET api/customers/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var res = this.svc.Get(id);
            if (res != null) {
                return Json(res);
            }
            return NotFound();
        }

        // POST api/customers
        [HttpPost]
        public IActionResult Post([FromBody]Customer value)
        {
            int new_id = this.svc.NextId();
            value.ID = new_id;
            this.svc.Add(value);
            return Created(String.Format("api/customers/{0}", new_id.ToString()), value);
        }

        // PUT api/customers/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Customer value)
        {
            var exists = this.svc.Exists(id);
            if (exists) {
                this.svc.Update(id, value);
                return Ok();
            }
            return NotFound();
        }

        // DELETE api/customers/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var exists = this.svc.Exists(id);
            if (exists) {
                this.svc.Remove(id);
                return Ok();
            }
            return NotFound();
        }
    }
}

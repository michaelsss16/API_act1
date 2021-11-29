using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Domain.Entities;
using Domain.Services;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        [HttpGet]
        //public async IEnumerable<Cliente> Get() //Ainda estou implementando e vou colocar o DI
        public string Get()
        {
            return "Teste";
        }
    }
}

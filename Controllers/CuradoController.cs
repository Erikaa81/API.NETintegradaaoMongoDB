using Api.Data.Collections;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Api.Controllers
{[ApiController]
    [Route("[controller]")]
    public class CuradoController : ControllerBase
    {
        Api.Data.MongoDB _mongoDB;
        IMongoCollection<Curado> _curadoCollection;

        public CuradoController(Api.Data.MongoDB mongoDB)
        {
            _mongoDB = mongoDB;
            _curadoCollection = _mongoDB.DB.GetCollection<Curado>(typeof(Curado).Name.ToLower());
        }

        [HttpPost]
        public ActionResult SalvarInfectado([FromBody] CuradoDto dto)
        {
            var curado = new Curado(dto.DataNascimento, dto.Sexo, dto.Latitude, dto.Longitude);

            _curadoCollection.InsertOne(curado);
            
            return StatusCode(201, "Curado adicionado com sucesso");
        }

        [HttpGet]
        public ActionResult ObterInfectados()
        {
            var curados = _curadoCollection.Find(Builders<Curado>.Filter.Empty).ToList();
            
            return Ok(curados);
        }
    }
}
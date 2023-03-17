using ApiRuleta.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
//using System.Web.Http.Cors;

namespace ApiRuleta.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RuletaController : ApiController
    {
        // GET api/values
        [HttpGet]
        [Route("api/numero-color-aleatorio")]
        public IHttpActionResult ObtenerNumeroYColor()
        {
            var data = new { 
                numero = ObtenerNumeroAleatorio(),
                color = ObtenerColorAleatorio()
            };

            return Json(data);
        }

        private int ObtenerNumeroAleatorio()
        { 
            Random random = new Random();
            return random.Next(0, 32);
        }

        private string ObtenerColorAleatorio()
        {
            var colores = new[] { "#C34A36", "#4B4453" };
            Random random = new Random();

            return colores[random.Next(0, 2)];
        }

        [HttpGet]
        [Route("api/jugadores")]
        public IHttpActionResult ObtenerJugadores()
        {
            using (RuletaJuegoDBEntities db = new RuletaJuegoDBEntities())
            {
                return Json(db.Jugador.ToList());
            };
        }

        [HttpGet]
        [Route("api/jugador/{nombre}")]
        public IHttpActionResult ObtenerJugadores(string nombre)
        {
            using (RuletaJuegoDBEntities db = new RuletaJuegoDBEntities())
            {
                return Json(db.Jugador.FirstOrDefault(x => x.Nombre.ToLower() == nombre.ToLower()));
            };
        }

        [HttpPost]
        [Route("api/jugador")]
        public IHttpActionResult InsertarNuevoJugador([FromBody] JugadoMontoDTO jugador)
        {
            using (RuletaJuegoDBEntities db = new RuletaJuegoDBEntities())
            {
                var jugandorDB = new Jugador
                {
                    Nombre = jugador.Nombre,
                    Monto = jugador.Monto
                };
                db.Jugador.Add(jugandorDB);

                db.SaveChanges();

                return Json(jugandorDB);
            };
        }

        [HttpPut]
        [Route("api/jugador")]
        public IHttpActionResult ActualizarMontoJugador([FromBody] JugadoEditarMontoDTO jugador)
        {
            using (RuletaJuegoDBEntities db = new RuletaJuegoDBEntities())
            {
                var jugadorDB = db.Jugador.FirstOrDefault(x => x.Id == jugador.Id);
                jugadorDB.Monto = jugador.Monto;

                db.Jugador.AddOrUpdate(jugadorDB);
                db.SaveChanges();

                return Json(jugadorDB);
            };
        }
    }
}

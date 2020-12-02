﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using EventosCore.Data;
using EventosCore.Data.Entities;
using EventosCore.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

using VuelosCore.Models.DTOs;
using VuelosCore.Models.Responses;
using static VuelosCore.Models.DTOs.Consulta4;

namespace EventosCore.Controllers
{
    [Route("api/v1/Eventos")]
    [ApiController]
    public class EventosController : ControllerBase
    {
        private readonly ILogger<EventosController> Logger;
        private readonly ApplicationDbContext _db;
        public EventosController(ILogger<EventosController> logger, ApplicationDbContext context)
        {
            Logger = logger;
            _db = context;
        }
        [HttpGet]
        [Route("GetCiudades")]
        [EnableCors("AllowAll")]
        public IActionResult GetCiudades()
        {
            try
            {
                Logger.LogInformation("Inicia obtencion de aeropuertos");
                var eventtosCiudades = _db.Eventos.ToList();
                var aeropuertos = eventtosCiudades.Where(c => !string.IsNullOrEmpty(c.Aeropuertos.Lata)).OrderBy(c => c.Aeropuertos.CiudadUbicacin).ToList();
                List<ResponseCiudades> responseAeropuertos = new List<ResponseCiudades>();
                foreach (var item in aeropuertos)
                {
                    if (responseAeropuertos.FirstOrDefault(c => c.CiudadUbicacion == item.Aeropuertos.CiudadUbicacin) == null)
                    {
                        responseAeropuertos.Add(new ResponseCiudades
                        {
                            CiudadUbicacion = item.Aeropuertos.CiudadUbicacin,
                            Iata = item.Aeropuertos.Lata,
                            Id = item.Id,
                            Concatenado = $"{item.Aeropuertos.CiudadUbicacin}[{item.Aeropuertos.Lata}]"
                        });
                    }
                }
                Logger.LogInformation(responseAeropuertos.ToString());
                return Ok(responseAeropuertos);
            }
            catch (Exception ex)
            {
                Logger.LogError("Excepcion generada en GetAeropuertos: " + ex.Message);
                return StatusCode(500, "Ocurrio un error");
                throw;
            }
        }

        [HttpGet]
        [Route("GetEventos")]
        [EnableCors("AllowAll")]
        public IActionResult GetEventos(string ciudad)
        {
            try
            {
                Logger.LogInformation("Inicia obtencion de eventos");
                if (string.IsNullOrEmpty(ciudad))
                {
                    var eventos = _db.Eventos.OrderBy(c => c.FechaEvento).ToList();
                    List<ResponceEventosCiudades> responceEventosCiudades = new List<ResponceEventosCiudades>();
                    foreach (var item in eventos)
                    {
                        responceEventosCiudades.Add(new ResponceEventosCiudades
                        {
                            tourEventId = item.Id,
                            description = item.Descripcion,
                            eventCity = item.Aeropuertos.CiudadUbicacin,
                            eventDate = item.FechaEvento,
                            imgLocal = item.ImgLocal,
                            imgVisitante = item.ImgVisitante,
                            shortDescription = item.DescripcionCorta,
                            value = item.Precio,
                            concatenated = $"{item.Aeropuertos.CiudadUbicacin}[{item.Aeropuertos.Lata}]"

                        });
                    }
                    Logger.LogInformation(responceEventosCiudades.ToString());
                    return Ok(responceEventosCiudades);
                }
                else
                {
                    var eventos = _db.Eventos.Where(c => c.Aeropuertos.CiudadUbicacin == ciudad).ToList();
                    List<ResponceEventosCiudades> responceEventosCiudades = new List<ResponceEventosCiudades>();
                    foreach (var item in eventos)
                    {
                        responceEventosCiudades.Add(new ResponceEventosCiudades
                        {
                            tourEventId = item.Id,
                            description = item.Descripcion,
                            eventCity = item.Aeropuertos.CiudadUbicacin,
                            eventDate = item.FechaEvento,
                            imgLocal = item.ImgLocal,
                            imgVisitante = item.ImgVisitante,
                            shortDescription = item.DescripcionCorta,
                            value = item.Precio,
                            concatenated = $"{item.Aeropuertos.CiudadUbicacin}[{item.Aeropuertos.Lata}]"
                        });
                    }
                    Logger.LogInformation(responceEventosCiudades.ToString());
                    return Ok(responceEventosCiudades);
                }

            }
            catch (Exception ex)
            {
                Logger.LogError("Excepcion generada en GetAeropuertos: " + ex.Message);
                return StatusCode(500, "Ocurrio un error");
                throw;
            }
        }

        [HttpGet]
        [Route("GetEventosTop10")]
        [EnableCors("AllowAll")]
        public IActionResult GetEventosTop10()
        {
            try
            {
                Logger.LogInformation("Inicia obtencion de eventos");

                var eventos = _db.Eventos.OrderBy(c => c.FechaEvento).Take(10).ToList();
                List<ResponceEventosCiudades> responceEventosCiudades = new List<ResponceEventosCiudades>();
                foreach (var item in eventos)
                {
                    responceEventosCiudades.Add(new ResponceEventosCiudades
                    {
                        tourEventId = item.Id,
                        description = item.Descripcion,
                        eventCity = item.Aeropuertos.CiudadUbicacin,
                        eventDate = item.FechaEvento,
                        imgLocal = item.ImgLocal,
                        imgVisitante = item.ImgVisitante,
                        shortDescription = item.DescripcionCorta,
                        value = item.Precio,
                        concatenated = $"{item.Aeropuertos.CiudadUbicacin}[{item.Aeropuertos.Lata}]"

                    });
                }
                Logger.LogInformation(responceEventosCiudades.ToString());
                return Ok(responceEventosCiudades);
            }
            catch (Exception ex)
            {
                Logger.LogError("Excepcion generada en GetAeropuertos: " + ex.Message);
                return StatusCode(500, "Ocurrio un error");
                throw;
            }
        }

        [HttpGet]
        [Route("{eventoId:int}", Name = "GetEvento")]
        [EnableCors("AllowAll")]
        public IActionResult GetEvento(int eventoId)
        {
            try
            {
                var evento = _db.Eventos.FirstOrDefault(c => c.Id == eventoId);
                if (evento != null)
                {
                    return Ok(new ResponceEventosCiudades
                    {
                        tourEventId = evento.Id,
                        description = evento.Descripcion,
                        eventCity = evento.Aeropuertos.CiudadUbicacin,
                        eventDate = evento.FechaEvento,
                        imgLocal = evento.ImgLocal,
                        imgVisitante = evento.ImgVisitante,
                        shortDescription = evento.DescripcionCorta,
                        value = evento.Precio,
                        concatenated = $"{evento.Aeropuertos.CiudadUbicacin}[{evento.Aeropuertos.Lata}]"
                    });
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                Logger.LogError("Excepcion generada en GetAeropuertos: " + ex.Message);
                return StatusCode(500, "Ocurrio un error");
                throw;
            }
        }

        [HttpGet]
        [Route("Healty")]
        public IActionResult Healty()
        {
            return Ok("Todo Bien");
        }
    }
}

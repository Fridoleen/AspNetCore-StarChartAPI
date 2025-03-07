﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;
using StarChart.Models;

namespace StarChart.Controllers
{
    [ApiController]
    [Route("")]
    public class CelestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("{id:int}", Name = "GetById")]        
        public IActionResult GetById(int id)
        {
            var celObject = _context.CelestialObjects.Find(id);
            if (celObject == null) return NotFound();

            celObject.Satellites = _context.CelestialObjects.Where(e => e.OrbitedObjectId == id).ToList();
            return Ok(celObject);
        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var celestialObjects = _context.CelestialObjects.Where(e => e.Name == name).ToList();
            if (! celestialObjects.Any()) return NotFound();
            foreach(var celestialObject in celestialObjects)
            {
                celestialObject.Satellites = _context.CelestialObjects.Where(e => e.OrbitedObjectId == celestialObject.Id).ToList();
            }
            return Ok(celestialObjects);
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var celestialObjects = _context.CelestialObjects.ToList();
            foreach(var celestialObject in celestialObjects)
            {
                celestialObject.Satellites = _context.CelestialObjects.Where(e => e.OrbitedObjectId == celestialObject.Id).ToList();
            }
            return Ok(celestialObjects);
        }
        public IActionResult Index()
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public IActionResult Create([FromBody]CelestialObject celestial)
        {
            _context.CelestialObjects.Add(celestial);
            _context.SaveChanges();

            return CreatedAtRoute("GetById", new { id = celestial.Id }, celestial);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CelestialObject celestialObject)
        {
            var celestialObjects = _context.CelestialObjects.Where(e => e.Id == id).ToList();
            if (!celestialObjects.Any()) return NotFound();
            else
            {
                celestialObjects.First().Name = celestialObject.Name;
                celestialObjects.First().OrbitalPeriod = celestialObject.OrbitalPeriod;
                celestialObjects.First().OrbitedObjectId = celestialObject.OrbitedObjectId;
                _context.CelestialObjects.Update(celestialObjects.First());
                _context.SaveChanges();
            }

            return NoContent();
        }

        [HttpPatch("{id}/{name}")]
        public IActionResult RenameObject(int id, string name)
        {
            var celestialObjects = _context.CelestialObjects.Where(e => e.Id == id ).ToList();
            if (!celestialObjects.Any()) return NotFound();
            else
            {
                celestialObjects.First().Name = name;
                _context.CelestialObjects.Update(celestialObjects.First());
                _context.SaveChanges();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var celestialObjects = _context.CelestialObjects.Where(e => e.Id == id || e.OrbitedObjectId == id).ToList();
            if (!celestialObjects.Any()) return NotFound();
            else
            {
                _context.CelestialObjects.RemoveRange(celestialObjects);
                _context.SaveChanges();
            }

            return NoContent();
        }
    }
}

﻿using Beautiful_Villa_API.Controllers.Data;
using Beautiful_Villa_API.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Beautiful_Villa_API.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        public VillaAPIController()
        {
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas() 
        {
            return Ok(VillaStore.villaList);
        } 
        [HttpGet("{id:int}", Name ="GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // [ProducesResponseType(200, Type =Tyoeof(VillaDTO))]
        public ActionResult<VillaDTO> GetVilla(int id) 
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            return Ok(villa);
        } 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDTO> CreateVilla([FromBody]VillaDTO villaDTO)
        {
            if (VillaStore.villaList.FirstOrDefault(u=>u.Name.ToLower() == villaDTO.Name.ToLower())!=null)  
            {
                ModelState.AddModelError("CustomError", "Villa already Exists!");
                return BadRequest(ModelState);
            }
            if (villaDTO == null)
            { return BadRequest(villaDTO); }
            if (villaDTO.Id > 0)    
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            villaDTO.Id = VillaStore.villaList.OrderByDescending(u => u.Id).FirstOrDefault().Id+1;
            VillaStore.villaList.Add(villaDTO);
            return CreatedAtRoute("GetVilla", new {villaDTO.Id},villaDTO);
        }   
        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}", Name ="DeleteVilla")]
        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(u=> u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            VillaStore.villaList.Remove(villa);
            return NoContent();
        }

        [HttpPut("{id:int}", Name ="UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateVilla(int id,[FromBody] VillaDTO villaDTO)
        {
            if(villaDTO == null || id != villaDTO.Id)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(u=> u.Id == id);
            villa.Name = villaDTO.Name;
            villa.Sqft = villaDTO.Sqft;
            villa.Occupancy = villaDTO.Occupancy;

            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialVilla(int id,JsonPatchDocument<VillaDTO> patchDTO)
        {
            if(patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(u=> u.Id == id);
            if(villa == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(villa,ModelState);
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}

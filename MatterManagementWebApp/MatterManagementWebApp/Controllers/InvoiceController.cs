﻿using MatterManagementWebApp.Services.Data.DTOs;
using MatterManagementWebApp.Services.Models;
using MatterManagementWebApp.Services.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MatterManagementWebApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceController(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        [HttpPost("/api/Invoices")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Post(InvoiceDto invoice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _invoiceRepository.Add(invoice);

            return CreatedAtAction(nameof(GetById), new { id = invoice.InvoiceId }, invoice);
        }

        /// <summary>
        /// Get a invoice By AttorneyId.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /api/invoice/{id}
        ///
        /// </remarks>
        /// <response code="200">successfully received invoice values</response>
        /// <response code="404">invoice with this id not found</response>
        /// <response code="500">Internal server Error</response>
        [HttpGet("/api/Invoice/{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult GetById(int id)
        {
            var invoice = _invoiceRepository.GetById(id);

            if (invoice == null)
            {
                return NotFound();
            }

            return Ok(invoice);
        }

        [HttpGet]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll()
        {
            var invoices = _invoiceRepository.GetAll();

            return Ok(invoices);
        }

        [HttpPut("/api/Invoice/{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Update(int id, [FromBody] InvoiceDto invoice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != invoice.InvoiceId)
            {
                return BadRequest();
            }
            _invoiceRepository.Update(id,invoice);

            return NoContent();
        }

        [HttpDelete("/api/Invoice/{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id)
        {
            var invoice = _invoiceRepository.GetById(id);

            if (invoice == null)
            {
                return NotFound();
            }

            _invoiceRepository.Delete(id);

            return NoContent();
        }
    }

}

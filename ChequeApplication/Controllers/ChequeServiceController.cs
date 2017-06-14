using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ChequeApplication.Models;

namespace ChequeApplication.Controllers
{
    public class ChequeServiceController : ApiController
    {
        private List<Cheque> GetChequeList()
        {
            List<Cheque> chequeList = new List<Cheque> { 
                            new Cheque() { Id = 1, Name = "John", Date = DateTime.Now.AddDays(-1), Amount = 1000m},
                            new Cheque() { Id = 2, Name = "Steve",  Date = DateTime.Now.AddDays(-2), Amount = 2000m },
                            new Cheque() { Id = 3, Name = "Bill",  Date = DateTime.Now.AddDays(-3), Amount = 3000m },
                            new Cheque() { Id = 4, Name = "Ram" , Date = DateTime.Now.AddDays(-4), Amount = 4000m },
                            new Cheque() { Id = 5, Name = "Ron" , Date = DateTime.Now.AddDays(-5), Amount = 5000m },
                            new Cheque() { Id = 6, Name = "Chris" , Date = DateTime.Now.AddDays(-6), Amount = 6000m },
                            new Cheque() { Id = 7, Name = "Rob" , Date = DateTime.Now.AddDays(-7), Amount = 7000m } 
                        };
            return chequeList;
        }


        public IHttpActionResult GetAllCheques()
        {
            List<Cheque> cheques = GetChequeList();

            if (cheques.Count == 0)
                return NotFound();

            return Ok(cheques);
        }

        public IHttpActionResult GetChequeById(int id)
        {
            List<Cheque> cheques = GetChequeList();
            var cheque = cheques.Where(c => c.Id == id).FirstOrDefault();

            if (cheque == null)
            {
                return NotFound();
            }

            return Ok(cheque);
        }

        public IHttpActionResult Put(Cheque cheque)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid data");

            List<Cheque> cheques = GetChequeList();
            var chequeToUpdate = cheques.Where(c => c.Id == cheque.Id).FirstOrDefault();

            if (chequeToUpdate == null)
            {
                return NotFound();
            }

            chequeToUpdate.Name = cheque.Name;
            chequeToUpdate.Date = cheque.Date;
            chequeToUpdate.Amount = cheque.Amount;

            //Update the cheques
            return Ok();
        }

    }
}

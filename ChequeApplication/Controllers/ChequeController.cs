using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChequeApplication.Models;
using System.Net.Http;

namespace ChequeApplication.Controllers
{
    public class ChequeController : Controller
    {
        // GET: Cheque
        public ActionResult Index()
        {
            IEnumerable<Cheque> cheques = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61932/api/");

                var responseTask = client.GetAsync("ChequeService");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Cheque>>();
                    readTask.Wait();

                    cheques = readTask.Result;
                }
                else
                {
                    //log response status here..
                    cheques = Enumerable.Empty<Cheque>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            return View(cheques);
        }

        // GET Cheque/Details/id
        public ActionResult Details(int id)
        {
            Cheque cheque = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61932/api/");

                var responseTask = client.GetAsync("ChequeService?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Cheque>();
                    readTask.Wait();

                    cheque = readTask.Result;
                }
            }

            return View(cheque);
        }

        // GET Cheque/Edit/id
        public ActionResult Edit(int id)
        {
            Cheque cheque = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61932/api/");

                var responseTask = client.GetAsync("ChequeService?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Cheque>();
                    readTask.Wait();

                    cheque = readTask.Result;
                }
            }

            return View(cheque);
        }

        [HttpPost]
        public ActionResult Edit(Cheque cheque)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61932/api/values");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<Cheque>("ChequeService", cheque);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(cheque);
        }

    }
}
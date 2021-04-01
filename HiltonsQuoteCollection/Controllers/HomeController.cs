using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HiltonsQuoteCollection.Models;
using System.Net.Http;

namespace HiltonsQuoteCollection.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private QuotesDbContext context { get; set; }

        public HomeController(ILogger<HomeController> logger, QuotesDbContext ctx)
        {
            _logger = logger;
            context = ctx;
        }

        //normal index page showing all quotes
        [HttpGet]
        public IActionResult Index()
        {
            return View(context.Quotes.ToList());
        }

        //this is the method for deleting a quote. i used the post index for simplicity
        [HttpPost]
        public IActionResult Index(int quoteId)
        {
            Quotes quoteToDelete = context.Quotes.FirstOrDefault(q => q.QuoteId == quoteId);

            context.Quotes.Remove(quoteToDelete);
            context.SaveChanges();

            return View(context.Quotes.ToList());
        }

        //this redirects to a new page for adding a quote
        [HttpGet]
        public IActionResult AddQuote()
        {
            return View();
        }

        //this performs the add, and returns to the home index view
        // it checks if the model is a valid input
        [HttpPost]
        public IActionResult AddQuote(Quotes newQuote)
        {
            if (ModelState.IsValid)
            {
                context.Quotes.Add(newQuote);
                context.SaveChanges();

                return View("Index", context.Quotes.ToList());
            }
            else
            {
                return View();
            }
        }

        //this method takes the user to the page for editing quotes
        [HttpGet]
        public IActionResult EditQuote(int quoteId)
        {
            Quotes Quote = context.Quotes.FirstOrDefault(q => q.QuoteId == quoteId);

            return View(Quote);
        }

        //this method performs the edit, and returns the view to the index
        [HttpPost]
        public IActionResult EditQuote(Quotes newQuote)
        {
            context.Quotes.Update(newQuote);
            context.SaveChanges();

            return View("Index", context.Quotes.ToList());
        }

        //this method takes the user to a page that randomly shows a quote everytime it is loaded. 
        public IActionResult RandomQuote()
        {
            Quotes randQuote = context.Quotes.AsEnumerable().OrderBy(q => Guid.NewGuid()).FirstOrDefault();

            return View(randQuote);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

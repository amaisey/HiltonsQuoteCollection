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

        [HttpGet]
        public IActionResult Index()
        {
            return View(context.Quotes.ToList());
        }

        [HttpPost]
        public IActionResult Index(int quoteId)
        {
            Quotes quoteToDelete = context.Quotes.FirstOrDefault(q => q.QuoteId == quoteId);

            context.Quotes.Remove(quoteToDelete);
            context.SaveChanges();

            return View(context.Quotes.ToList());
        }

        [HttpGet]
        public IActionResult AddQuote()
        {
            return View();
        }

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

        [HttpGet]
        public IActionResult EditQuote(int quoteId)
        {
            Quotes Quote = context.Quotes.FirstOrDefault(q => q.QuoteId == quoteId);

            return View(Quote);
        }

        [HttpPost]
        public IActionResult EditQuote(Quotes newQuote)
        {
            context.Quotes.Update(newQuote);
            context.SaveChanges();

            return View("Index", context.Quotes.ToList());
        }

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

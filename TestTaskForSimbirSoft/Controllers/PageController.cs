using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NLog;
using TestTaskForSimbirSoft.Infrastructure;
using TestTaskForSimbirSoft.Models;

namespace TestTaskForSimbirSoft.Controllers
{
    public class PageController : Controller
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        private readonly IWordRepository wordRepository;
        private readonly IPageRepository pageRepository;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly IWordsetHandler wordsetHandler;

        /// <summary>
        /// Конструктор контроллера при инициализации запрашивает экземпляры репозиториев Word и Page
        /// </summary>
        public PageController(
            IWordsetHandler wordsetHandler,
            IWordRepository wordRepository,
            IPageRepository pageRepository,
            IWebHostEnvironment appEnvironment)
        {
            this.wordsetHandler = wordsetHandler;
            this.wordRepository = wordRepository;
            this.pageRepository = pageRepository;
            _appEnvironment = appEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("Page", pageRepository.Pages);
        }

        /// <summary>
        /// Метод возврщает представление Words и передает данные слов в пренадлежащие страницы.
        /// </summary>
        [HttpGet("Words/{id}")]
        public IActionResult GetWords(int? id)
        {
            return View("Words", wordRepository?.Words?.Where(w => w.PageId == id));
        }

        /// <summary>
        /// Метод принимает экземпляр объекта Page, далее делает запрос на поиск в базе данных страницы с адресом 
        /// </summary>
        [HttpPost]
        public IActionResult PageProcessing(Page page)
        {
            if (ParserPage.CheckingPageForExistence(page.PageAddress))
            {
                try
                {
                    List<string> wordsFromPage;
                    if (pageRepository.FindPageByAddress(page?.PageAddress) == null)
                    {
                        pageRepository.AddPage(page);
                        pageRepository.Save();
                        wordsFromPage = ParserPage.PageFormatting(page?.PageAddress);
                        wordsetHandler.DictionaryAnalyzer(page, wordsFromPage);
                        return RedirectToAction("Index");
                    }
                    else
                        return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    logger.Debug(ex.ToString);
                    TempData["Message"] = ex.Message;
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["Message"] = "Некорректный адрес страницы.";
                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// Метод принимает id страницы, данные которой стоит удалить из таблицы базы данных.
        /// </summary>
        [HttpGet("DeletePage/{id}")]
        public IActionResult DeletePage(Int32? id)
        {
            try
            {
                pageRepository.DeletePage(id);
                pageRepository.Save();
            }
            catch (Exception ex)
            {
                logger.Debug(ex.ToString);
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetPage/{id}")]
        public IActionResult GetWebPage(int? id)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFile(pageRepository.FinedPagById(id).PageAddress, "HTML-Page.html");
                return PhysicalFile(Path.Combine(_appEnvironment.ContentRootPath, "HTML-Page.html"), "application/html", "HTML-Page.html");
            }
            catch(Exception ex)
            {
                logger.Debug(ex.ToString);
            }
            return RedirectToAction("Index");
        }
    }
}

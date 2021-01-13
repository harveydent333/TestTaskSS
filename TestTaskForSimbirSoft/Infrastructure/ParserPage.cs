using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Net;
using NLog;

namespace TestTaskForSimbirSoft.Infrastructure
{
    /// <summary>
    /// Класс обрабатывает веб страницу и получает содержимое ввиде слов.
    /// </summary>
    public class ParserPage
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Метод получает содержимое веб страницы и удаляет все лешнее, оставляя только содержащиеся слова.
        /// </summary>
        /// <returns>Возвращает список слов содержащихся на странице.</returns>
        public static List<string> PageFormatting(string pageAddress)
        {
            WebClient client = new WebClient();
            client.Headers.Add("user-agent", "Only a test!");
            string contentPage;

            contentPage = DeleteScriptTags(client.DownloadString(pageAddress));
            contentPage = DeleteStyleTags(contentPage);
            
            contentPage = Regex.Replace(contentPage, "<[^>]+>", string.Empty);
            contentPage = Regex.Replace(contentPage, @"\d+", string.Empty);
            contentPage = Regex.Replace(contentPage, "nbsp", string.Empty);
            contentPage = Regex.Replace(contentPage, @"\W", " ");
            return Regex.Replace(contentPage, @"\s+", " ").Trim(' ').ToLower().Split(' ', '.', '?', '!', '-', '(', ')', '[', ']').ToList();
        }

        /// <summary>
        /// Метод удаляет все теги script из содержимого страницы. 
        /// </summary>
        /// <param name="contentPage">Строковое содержимое страницы.</param>
        /// <returns>Возвращает содержимое страницы без тегов script.</returns>
        public static string DeleteScriptTags(string contentPage)
        {
            try
            {
                while (contentPage.IndexOf("<script") > 0)
                    contentPage = contentPage.Remove(contentPage.IndexOf("<script"), contentPage.IndexOf("</script>") - contentPage.IndexOf("<script") + 9); //  remove(f, l-f+9);
                return contentPage;
            }
            catch(Exception ex)
            {
                logger.Debug(ex.ToString());
                return contentPage;
            }
        }

        /// <summary>
        /// Метод удаляет все теги style из содержимого страницы. 
        /// </summary>
        /// <param name="contentPage">Строковое содержимое страницы.</param>
        /// <returns>Возвращает содержимое страницы без тегов style.</returns>
        public static string DeleteStyleTags(string contentPage)
        {
            try
            {
                while (contentPage.IndexOf("<style") > 0)
                    contentPage = contentPage.Remove(contentPage.IndexOf("<style"), contentPage.IndexOf("</style>") - contentPage.IndexOf("<style") + 8);
                return contentPage;
            }
            catch(Exception ex)
            {
                logger.Debug(ex.ToString());
                return contentPage;
            }
        }

        /// <summary>
        /// Метод отправляет запрос странице методом HEAD и получая ответ, тем самым, проверяет сущуствует ли страница.
        /// </summary>
        public static bool CheckingPageForExistence(string pageAddress)
        {
            try
            {
                WebRequest webRequest = HttpWebRequest.Create(pageAddress);
                webRequest.Method = "GET";
                webRequest.Headers.Add("user-agent", "Only a test!");
                using (WebResponse webResponse = webRequest.GetResponse())
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                logger.Debug(ex.ToString());
                return false;
            }
        }
    }
}
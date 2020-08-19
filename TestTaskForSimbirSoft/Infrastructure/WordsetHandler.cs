using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskForSimbirSoft.Models;

namespace TestTaskForSimbirSoft.Infrastructure
{
    /// <summary>
    /// Класс обрабатывает коллекцию слов с веб страницы.
    /// </summary>
    public class WordsetHandler
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Данный метод принимает набор слов обрабатываемой страницы и перебирает каждое слово. Если слово уже встречалось ранее на странице сайта, то метод обновляет счетчик.
        /// Иначе добавляет информацию о слове в таблицу базы данных.
        /// </summary>
        public static void DictionaryAnalyzer(IWordRepository wordRepository, IPageRepository pageRepository, Page page, List<string> wordsFromPage)
        {
            foreach (String word in wordsFromPage)
            {
                if (wordRepository.FindWordByName(word) != null)
                {
                    wordRepository.FindWordByName(word).WordCount++;
                    wordRepository.UpdateWord(wordRepository.FindWordByName(word));
                    wordRepository.Save();
                }
                else
                {
                    try
                    {
                        wordRepository.AddWord(
                            new Word
                            {
                                WordName = word,
                                WordCount = 1,
                                PageId = pageRepository.FindPageByAddress(page.PageAddress).Id
                            }); 
                        wordRepository.Save();
                    }
                    catch(Exception ex)
                    {
                        logger.Debug(ex.ToString());
                    }
                }
            }
            try
            {
                pageRepository.FindPageByAddress(page.PageAddress).CountWords = wordRepository.Words.Where(w => w.PageId == page.Id).LongCount();
                pageRepository.UpdatePage(pageRepository.FindPageByAddress(page.PageAddress));
                pageRepository.Save();
            }
            catch(Exception ex)
            {
                logger.Debug(ex.ToString());
            }
        } 
    }
}

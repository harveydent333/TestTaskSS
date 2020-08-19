using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskForSimbirSoft.Models
{
    /// <summary>
    /// Класс для управления взаимодействием модели Page и базой данных.
    /// </summary>
    public class PageRepository : IPageRepository
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private ApplicationDbContext context;

        /// <summary>
        /// Метод связывает репозиторий с базой данных
        /// </summary>
        /// <param name="cont"></param>
        public PageRepository(ApplicationDbContext cont)
        {
            this.context = cont;
        }

        /// <summary>
        /// Свойство возвращает все записи страниц из таблицы базы данных
        /// </summary>
        public IEnumerable<Page> Pages => context.Pages;

        /// <summary>
        /// Метод добавляет данные страницы в таблицу базы данных.
        /// </summary>
        /// <param name="page"></param>
        public void AddPage(Page page)
        {
            context.Add(page);
        }

        /// <summary>
        /// Редактирует запись страницы в таблице базы данных
        /// </summary>
        public void UpdatePage(Page page)
        {
            context.Update(page);
        }

        /// <summary>
        /// Метод удаляет запись страницы из базы данных с указанным ID.
        /// </summary>
        public void DeletePage(Int32? id)
        {
            try
            {
                context.Pages.Remove(context.Pages.Where(p => p.Id == id).FirstOrDefault());
            }
            catch(Exception ex)
            {
                logger.Debug(ex.ToString());
            }
        }

        /// <summary>
        /// Метод возвращает запись страницы с интересующим адресом.
        /// </summary>
        public Page FindPageByAddress(String pageAddress)
        {
            try
            {
                return Pages.Where(p => p.PageAddress == pageAddress).FirstOrDefault();
            }
            catch(Exception ex)
            {
                logger.Debug(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Метод возвращает запись страницы с интересующим Id.
        /// </summary>
        public Page FinedPagById(Int32? id)
        {
            try
            {
                return Pages.Where(p => p.Id == id).FirstOrDefault();
            }
            catch(Exception ex)
            {
                logger.Debug(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Метод сохраняет изменения в базе данных
        /// </summary>
        public void Save()
        {
            context.SaveChanges();
        }
    }
}

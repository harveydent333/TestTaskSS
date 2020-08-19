using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskForSimbirSoft.Models
{
    /// <summary>
    /// Интерфейс работы с модели Page с базой данных
    /// </summary>
    public interface IPageRepository
    {
        /// <summary>
        /// Свойство возвращает записи страниц.
        /// </summary>
        public IEnumerable<Page> Pages { get; }

        /// <summary>
        /// Метод осуществляет поиск и возврат страницы по id.
        /// </summary>
        public Page FinedPagById(Int32? id);

        /// <summary>
        /// Метод осуществляет поиск и возврат страницы по адресу.
        /// </summary>
        public Page FindPageByAddress(String pageAddress);
        
        /// <summary>
        /// Метод добавляет запись в таблицу базы данных.
        /// </summary>
        public void AddPage(Page page);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        public void UpdatePage(Page page);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void DeletePage(Int32? id);

        /// <summary>
        /// Метод сохраняет изменения в базе данных.
        /// </summary>
        public void Save();
    }
}

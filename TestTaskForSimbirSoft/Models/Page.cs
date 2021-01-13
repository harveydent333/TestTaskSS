using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestTaskForSimbirSoft.Models
{
    /// <summary>
    /// Модель страницы сайта
    /// </summary>
    public class Page
    {
        public int Id { get; set; }

        /// <summary>
        /// URL Адрес страницы.
        /// </summary>
        [StringLength(600)]
        [Required(ErrorMessage = "Необходимо ввести адрес страницы.")]
        public string PageAddress { get; set; }

        /// <summary>
        /// Количество уникальных слов на странице.
        /// </summary>
        public long? CountWords { get; set; }

        /// <summary>
        /// Коллеция уникальных слов на странице.
        /// </summary>
        public List<Word> Words { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskForSimbirSoft.Models
{
    /// <summary>
    /// Модель страницы сайта
    /// </summary>
    public class Page
    {
        public Int32 Id { get; set; }

        /// <summary>
        /// URL Адрес страницы.
        /// </summary>
        [StringLength(600)]
        [Required(ErrorMessage = "Необходимо ввести адрес страницы.")]
        public String PageAddress { get; set; }

        /// <summary>
        /// Количество уникальных слов на странице.
        /// </summary>
        public Int64? CountWords { get; set; }

        /// <summary>
        /// Коллеция уникальных слов на странице.
        /// </summary>
        public List<Word> Words { get; set; }
    }
}

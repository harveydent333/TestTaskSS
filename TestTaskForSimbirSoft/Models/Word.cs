using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace TestTaskForSimbirSoft.Models
{
    public class Word
    {
        public Int32 Id { get; set; }

        /// <summary>
        /// Наименование слова
        /// </summary>
        [Required]
        public String WordName { get; set; }

        /// <summary>
        /// Количество, сколько раз слово встречается на странице
        /// </summary>
        [Required]
        public Int32 WordCount { get; set; }

        /// <summary>
        /// Id страницы, на которой встречается слово
        /// </summary>
        [Required]
        public Int32 PageId { get; set; }

        /// <summary>
        /// Объект страницы
        /// </summary>
        public Page PageSite { get; set; }
    }
}

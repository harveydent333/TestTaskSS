using System.ComponentModel.DataAnnotations;

namespace TestTaskForSimbirSoft.Models
{
    public class Word
    {
        public int Id { get; set; }

        /// <summary>
        /// Наименование слова
        /// </summary>
        [Required]
        public string WordName { get; set; }

        /// <summary>
        /// Количество, сколько раз слово встречается на странице
        /// </summary>
        [Required]
        public int WordCount { get; set; }

        /// <summary>
        /// Id страницы, на которой встречается слово
        /// </summary>
        [Required]
        public int PageId { get; set; }

        /// <summary>
        /// Объект страницы
        /// </summary>
        public Page PageSite { get; set; }
    }
}

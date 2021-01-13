using System.Linq;

namespace TestTaskForSimbirSoft.Models
{
    /// <summary>
    /// Класс для управления взаимодействием модели Word и базой данных.
    /// </summary>
    public class WordRepository : IWordRepository
    {
        private ApplicationDbContext context;

        /// <summary>
        /// Конструктор класса, при его инициализации запрашивается экземпляр ApplicationDbContext для связи с базой данных
        /// </summary>
        public WordRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Свойство возвращает набор данных таблицы из таблицы Words.
        /// </summary>
        public IQueryable<Word> Words => context.Words;

        /// <summary>
        /// Добавляет данные слова в таблицу базы данных
        /// </summary>
        public void AddWord(Word word)
        {
            context.Add(word);
        }

        /// <summary>
        /// Метод обновляет данные в таблице базы данных.
        /// </summary>
        /// <param name="word"></param>
        public void UpdateWord(Word word)
        {
            context.Update(word);
        }

        /// <summary>
        /// Метод ищет данные слова в таблице по его наименованию.
        /// </summary>
        public Word FindWordByName(string wordName)
        {
            return context.Words
                .Where(w => w.WordName == wordName)
                .FirstOrDefault();
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

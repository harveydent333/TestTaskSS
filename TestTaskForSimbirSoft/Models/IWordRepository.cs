using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskForSimbirSoft.Models
{
    /// <summary>
    /// 
    /// </summary>
    public interface IWordRepository
    {
        /// <summary>
        /// 
        /// </summary>
        public IQueryable<Word> Words { get; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="word"></param>
        public void AddWord(Word word);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="word"></param>
        public void UpdateWord(Word word);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="WordName"></param>
        /// <returns></returns>
        public Word FindWordByName(String WordName);
        
        /// <summary>
        /// 
        /// </summary>
        public void Save();
    }
}

using System.Collections.Generic;
using TestTaskForSimbirSoft.Models;

namespace TestTaskForSimbirSoft.Infrastructure
{
    public interface IWordsetHandler
    {
        void DictionaryAnalyzer(Page page, List<string> wordsFromPage);
    }
}

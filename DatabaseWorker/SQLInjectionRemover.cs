using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DatabaseWorker.PreventSQLInjection
{
    //TODO: Впихнуть цей клас в шаблони для перевірки значень типу стрінг від ін'єкцій, або перенести в юай, або зробити і те і те
    internal static class SQLInjectionRemover
    {
        internal static string TryClean(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return query;
            }

            // Замінюємо небезпечні символи на безпечні еквіваленти
            query = Regex.Replace(query, @"[-;,\\/*+=@%&!#|{}()<>?$]", "");

            // Перевіряємо наявність ключових слів SQL та видаляємо їх з запиту
            string[] keywords = { "DELETE", "DROP", "UPDATE", "INSERT", "TRUNCATE", "ALTER", "CREATE", "SELECT" };
            foreach (string keyword in keywords)
            {
                query = query.Replace(keyword, string.Empty);
            }

            return query;
        }
    }
}
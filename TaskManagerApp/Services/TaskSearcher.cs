using System;
using System.Collections.Generic;
using System.Linq;
using TaskManagerApp.Models;

namespace TaskManagerApp.Services
{
    /**
     * @class TaskSearcher
     * @brief A generic utility class for searching through collections of tasks or task-like objects.
     * @tparam T The type of the item, must be a TaskItem or derived from it.
     */
    public static class TaskSearcher<T> where T : TaskItem 
    {
        /**
         * @brief Finds all items containing a specific search term in the title or description.
         * @param collection The collection to search.
         * @param searchTerm The term to find.
         * @return A list of matching items.
         */
        public static List<T> SearchByKeyword(IEnumerable<T> collection, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return collection.ToList();

            string searchLower = searchTerm.ToLower();
            
            return collection
                .Where(item => 
                    item.Title.ToLower().Contains(searchLower) || 
                    item.Description.ToLower().Contains(searchLower))
                .ToList();
        }
        
        /**
         * @brief Counts all items that are overdue, assuming they are TimedTask.
         * @param collection The collection to check.
         * @return The count of overdue tasks.
         */
        public static int CountOverdue(IEnumerable<T> collection)
        {
            return collection.Count(item => item is TimedTask tt && tt.IsOverdue());
        }
    }
}
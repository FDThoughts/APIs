using System;
using System.Collections.Generic;

namespace ToDo.Repository
{
    /// <summary>
    /// Columns map: mapp SQL columns to Entities
    /// </summary>
    internal class ColumnMap
    {
        /// <summary>
        /// forward dictionary
        /// </summary>
        private readonly Dictionary<string, string> forward =
            new Dictionary<string, string>();
        /// <summary>
        /// reverse dictionary
        /// </summary>
        private readonly Dictionary<string, string> reverse =
            new Dictionary<string, string>();

        /// <summary>
        /// Add column to map
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        public void Add(string t1, string t2)
        {
            forward.Add(t1, t2);
            reverse.Add(t2, t1);
        }

        /// <summary>
        /// get column of index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string this[string index]
        {
            get
            {
                if (forward.ContainsKey(index)) return forward[index];
                if (reverse.ContainsKey(index)) return reverse[index];
                return index;
            }
        }
    }
}

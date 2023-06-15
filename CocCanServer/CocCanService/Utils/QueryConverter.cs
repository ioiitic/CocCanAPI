using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Repository.Utils
{
    public static class QueryConverter
    {
        public static string CheckQuery(string filter, string range, string sort)
        {
            Regex regexFilter = new Regex("^{((\"\\S{1,}\":\"(\\S|\\s){1,}\",)*(\"\\S{1,}\":\"(\\S|\\s){1,}\"))*}$");
            Regex rangeFilter = new Regex("^[[]([0-9]{1,},[0-9]{1,})*[]]$");
            Regex sortFilter = new Regex("^[[](\"\\S{1,}\",\"(DESC|ASC)\")*[]]$");

            if (!regexFilter.IsMatch(filter)) return "Filter is wrong format!";
            if (!rangeFilter.IsMatch(range)) return "Range is wrong format!";
            if (!sortFilter.IsMatch(sort)) return "Sort is wrong format!";

            return "Valid";
        }

        public static Dictionary<string, string> getFilter(string filter)
        {
            Dictionary<string, string> _filter = new Dictionary<string, string>();
            int from = filter.IndexOf("\"") + 1;
            int to = filter.IndexOf("\"", from);
            while (from != 0)
            {
                try
                {
                    string key = filter.Substring(from, to - from);
                    from = filter.IndexOf("\"", to + 1) + 1;
                    to = filter.IndexOf("\"", from);
                    string value = filter.Substring(from, to - from);
                    _filter.Add(key, value);
                    from = filter.IndexOf("\"", to + 1) + 1;
                    to = filter.IndexOf("\"", from);
                }
                catch
                {
                    throw new Exception("Filter is wrong format!");
                }
            }
            return _filter;
        }
        public static List<int> getRange(string range)
        {
            List<int> _range = new List<int>() { -1, -1 };
            int mid = range.IndexOf(",");
            if (mid == -1) return _range;

            _range[0] = int.Parse(range.Substring(1, mid-1));
            _range[1] = int.Parse(range.Substring(mid + 1, range.Length - mid - 2));

            if (_range[0] > _range[1]) throw new Exception("Range is wrong format!");

            return _range;
        }
        public static List<string> getSort(string sort)
        {
            List<string> _sort = new List<string>() { null, null };

            int mid = sort.IndexOf(",");
            if (mid == -1) return _sort;

            int from = sort.IndexOf("\"") + 1;
            int to = sort.IndexOf("\"", from);
            _sort[0] = sort.Substring(from, to - from);

            from = sort.IndexOf("\"", to + 1) + 1;
            to = sort.IndexOf("\"", from);
            _sort[1] = sort.Substring(from, to - from);

            return _sort;
        }
    }
}

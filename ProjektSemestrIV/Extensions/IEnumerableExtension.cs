using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ProjektSemestrIV.Extensions
{
    static class IEnumerableExtension
    {
        public static ObservableCollection<T> Convert<T>(this IEnumerable<T> enumerable)
        => new ObservableCollection<T>(enumerable);
    }
}

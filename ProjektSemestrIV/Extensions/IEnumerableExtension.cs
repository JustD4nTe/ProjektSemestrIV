using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ProjektSemestrIV.Extensions
{
    static class IEnumerableExtension
    {
        // Shorter way to convert from IEnumerable to ObservableCollection
        public static ObservableCollection<T> Convert<T>(this IEnumerable<T> enumerable)
        => new ObservableCollection<T>(enumerable);
    }
}

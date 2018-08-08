using System.Collections.ObjectModel;
using MVVMFramevork;

namespace Game
{
    public class FieldRow : NotifyObject
    {
        public ObservableCollection<MapField> Items { get; } = new ObservableCollection<MapField>();
    }
}

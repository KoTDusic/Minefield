using MVVMFramevork;

namespace Game
{
    public class MapField : NotifyObject
    {
        private FieldType _type;
        private int _number;
        private bool _isOpen=true;
        public int X { get; set; }
        public int Y { get; set; }

        public int Number
        {
            get => _number;
            set
            {
                if (value == _number)
                {
                    return;
                }

                _number = value;
                OnPropertyChanged();
            }
        }

        public FieldType Type
        {
            get => _type;
            set
            {
                if (value == _type) return;
                {
                    _type = value;
                }

                OnPropertyChanged();
            }
        }

        public bool IsOpen
        {
            get => _isOpen;
            set
            {
                if (value == _isOpen)
                {
                    return;
                }

                _isOpen = value;
                OnPropertyChanged();
            }
        }

        public override string ToString()
        {
            return $"type = {Type} x = {X}, y = {Y}";
        }
    }
}
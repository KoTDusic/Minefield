using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MVVMFramevork;

namespace Game
{
    public class GameViewModel : NotifyObject
    {
        private int _fieldWidth;
        private int _fieldHeight;
        private int _allMines;
        private bool _initialized;
        private List<MapField> _mines = new List<MapField>();
        private MapField _focusedField;

        public int FieldWidth
        {
            get => _fieldWidth;
            set
            {
                if (value == _fieldWidth)
                {
                    return;
                }

                _fieldWidth = value;
                OnPropertyChanged();
            }
        }

        public int FieldHeight
        {
            get => _fieldHeight;
            set
            {
                if (value == _fieldHeight)
                {
                    return;
                }

                _fieldHeight = value;
                OnPropertyChanged();
            }
        }

        public int AllMines
        {
            get => _allMines;
            set
            {
                if (value == _allMines)
                {
                    return;
                }

                _allMines = value;
                OnPropertyChanged();
            }
        }

        public bool Initialized
        {
            get => _initialized;
            set
            {
                if (value == _initialized)
                {
                    return;
                }

                _initialized = value;
                OnPropertyChanged();
            }
        }

        public MapField FocusedField
        {
            get => _focusedField;
            set
            {
                if (Equals(value, _focusedField))
                {
                    return;
                }

                _focusedField = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenFieldCommand { get; private set; }
        public ICommand ChangeFocusCommand { get; private set; }
        public ICommand ClearFocusCommand { get; private set; }

        public ObservableCollection<FieldRow> Rows { get; }
            = new ObservableCollection<FieldRow>();

        public GameViewModel(GameSettings settings)
        {
            InitCommands();
            Initialized = false;
            InitField(settings, false).Forget();
        }

        private void InitCommands()
        {
            OpenFieldCommand = new RelayCommand(OpenFieldExecute);
            ChangeFocusCommand = new RelayCommand(ChangeFocusExecute);
            ClearFocusCommand = new RelayCommand(ClearFocusExecute);
        }

        private void ClearFocusExecute()
        {
            FocusedField = null;
        }

        private void ChangeFocusExecute(object obj)
        {
            if (!(obj is MapField field))
            {
                return;
            }

            FocusedField = field;
        }

        private void OpenFieldExecute(object obj)
        {
            if (!(obj is MapField field))
            {
                return;
            }
        }

        private async Task InitField(GameSettings settings, bool withAnumation = true)
        {
            try
            {
                AllMines = settings.Mines;
                FieldHeight = settings.Height;
                FieldWidth = settings.Width;
                await Task.Yield();
                var map = GenerateField(FieldWidth, FieldHeight, AllMines);
                var animationDelimiter = FieldWidth * FieldHeight / 500 + 1;
                for (var i = 0; i < FieldHeight; i++)
                {
                    var row = new FieldRow();
                    Rows.Add(row);
                    for (var j = 0; j < FieldWidth; j++)
                    {
                        var index = i * FieldWidth + j;
                        var item = map[index];
                        item.X = j;
                        item.Y = i;
                        row.Items.Add(item);
                        if (item.Type == FieldType.Mine)
                        {
                            _mines.Add(item);
                        }

                        if (withAnumation && index % animationDelimiter == 0)
                        {
                            await Task.Delay(1);
                        }
                    }
                }

                foreach (var mine in _mines)
                {
                    UpdateArroundMine(mine);
                }

                Initialized = true;
            }
            catch (Exception e)
            {
                Debugger.Break();
            }
        }

        private void UpdateArroundMine(MapField field)
        {
            foreach (var mapField in GetNeibors(field))
            {
                UpdateFieldNumber(mapField);
            }
        }

        private void UpdateFieldNumber(MapField field)
        {
            if (field.Type == FieldType.Mine)
            {
                return;
            }

            var minesArround = GetNeibors(field).Count(cell => cell.Type == FieldType.Mine);
            field.Number = minesArround;
            if (minesArround > 0)
            {
                field.Type = FieldType.Number;
            }
        }

        private IEnumerable<MapField> GetNeibors(MapField field)
        {
            var x = field.X;
            var y = field.Y;
            var neibors = new List<MapField>();
            for (var i = x - 1; i <= x + 1; i++)
            {
                for (var j = y - 1; j <= y + 1; j++)
                {
                    if (i == x && j == y)
                    {
                        continue;
                    }

                    neibors.Add(TryGetField(i, j));
                }
            }

            return neibors.Where(mapField => mapField != null);
        }

        private MapField TryGetField(int x, int y)
        {
            return Rows.ElementAtOrDefault(y)?.Items.ElementAtOrDefault(x);
        }

        public MapField[] GenerateField(int width, int height, int mines)
        {
            var length = width * height;
            var result = new MapField[length];
            try
            {
                for (var i = 0; i < length; i++)
                {
                    result[i] = new MapField();
                    if (i < mines)
                    {
                        result[i].Type = FieldType.Mine;
                    }
                }

                var rnd = new Random();
                rnd.Shuffle(result);
            }
            catch (Exception e)
            {
                Debugger.Break();
            }

            return result;
        }
    }
}
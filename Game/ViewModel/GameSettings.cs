using MVVMFramevork;

namespace Game
{
    public class GameSettings : NotifyObject
    {
        public int Width { get; set; } = 16;
        public int Height { get; set; } = 16;
        public int Mines { get; set; } = 10;
        public bool IsValid => Width * Height > Mines;
    }
}

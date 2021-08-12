using OpenTK.Input;

namespace OneEngine.Windows.Presets
{
    class KeyDetectorOpenTK : KeyDetector
    {
        public override KeyboardState GetKeyboard()
        {
            return Keyboard.GetState();
        }
    }
}

namespace Sources.Runtime.Input
{
    public class InputRouter
    {
        public InputRouter()
        {
            Input = new PlayerInput();
        }

        public PlayerInput Input { get; }

        public void OnEnable() => Input.Enable();

        public void OnDisable() => Input.Disable();
    }
}
namespace Code.Gameplay.Input
{
    public interface IInputService
    {
        float GetHorizontalAxis();
        float GetVerticalAxis();
        bool HasAxisInput();
        bool GetActionButton(string actionName);
    }
}
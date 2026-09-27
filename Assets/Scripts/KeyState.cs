public enum KeyState
{
    Pressed,
    Held,
    Released,
    Up
}

public static class KeyStateHelpers
{
    public static bool IsKeyDown(this KeyState keyState) =>
        keyState is KeyState.Held or KeyState.Pressed;
}
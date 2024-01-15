using UnityEngine;
using UnityEngine.Events;

public abstract class Tevent
{
    public static UnityAction NextT;
}
internal static class Key
{
    public static KeyCode HardDrop1 = KeyCode.LeftShift;
    public static KeyCode HardDrop3 = KeyCode.DownArrow;
    public static KeyCode HardDrop2 = KeyCode.RightShift;

    public static KeyCode FastDrop = KeyCode.Space;
    
    public static KeyCode Right = KeyCode.RightArrow;
    public static KeyCode Left = KeyCode.LeftArrow;
    
    public static KeyCode RotateR1 = KeyCode.UpArrow;
    public static KeyCode RotateR2 = KeyCode.X;
    
    public static KeyCode RotateL1 = KeyCode.RightControl;
    public static KeyCode RotateL2 = KeyCode.Z;
}
public enum TStates
{
    Wait,
    Fall,
    LockDelay,
    DelLine,
    RowDon,
    FallSuccess,
    GameOver
}
public static class TState
{
    private static TStates _state = TStates.Wait;

    public static TStates State
    {
        get => _state;
        set => _state = value;
    }

    public static bool Check(TStates s)
    {
        if (_state == s) return true;
        return false;
    }
    public static bool Checks(params TStates[] s)
    {
        foreach (var t in s)
        {
            if (_state == t) return true;
        }
        return false;
    }
    public static void Change(TStates s)
    {
        _state = s;
    }
}
public enum GameStates
{
    UI,
    Start,
    GameOver
}
public static class GameState
{
    private static GameStates _state = GameStates.UI;

    public static GameStates State
    {
        get => _state;
        set => _state = value;
    }

    public static bool Check(GameStates s)
    {
        if (_state == s) return true;
        return false;
    }
    public static bool Checks(params GameStates[] s)
    {
        foreach (var t in s)
        {
            if (_state == t) return true;
        }
        return false;
    }
    public static void Change(GameStates s)
    {
        _state = s;
    }
}
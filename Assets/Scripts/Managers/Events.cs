using UnityEngine.Events;

/// <summary>
/// Events templates definition
/// </summary>
public class Events
{

    [System.Serializable] public class EventGameState : UnityEvent<GameManager.GameState, GameManager.GameState> { }
}

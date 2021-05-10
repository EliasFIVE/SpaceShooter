using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Events templates definition
/// </summary>
public class Events
{
    [System.Serializable] public class EventGameState : UnityEvent<GameManager.GameState, GameManager.GameState> { }
    [System.Serializable] public class EventIntegerEvent : UnityEvent<int> { }
    [System.Serializable] public class EventWeaponTypeEvent : UnityEvent<WeaponType> { }
    //[System.Serializable] public class EventEnemyShipDeath : UnityEvent<Vector3> { }
}

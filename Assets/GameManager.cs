using System;
using UnityEngine;

public static class GameManager
{
    [RuntimeInitializeOnLoadMethod]
    public static void Init()
    {
        EventManager.Init();
    }
}

public static class EventManager
{
    public static GameEvent<int> OnHealthChanged;
    public static GameEvent<int> OnDamageTaken;
    public static GameEvent<int> OnDamageDealt;
    public static GameEvent<Vector3> OnFire;
    public static GameEvent<Item> OnItemAdded;
    public static GameEvent<Item> OnItemUsed;
    public static GameEvent<Item> OnItemRemoved;

    public static void Init()
    {
        OnDamageTaken.Subscribe(OnHealthChanged);
    }
}

public struct GameEvent<T>
{
    private Action<T> _event;
    public void Invoke(T value) => _event?.Invoke(value);
    public void Unsubscribe(Action<T> gameEvent) => _event -= gameEvent.Invoke;
    public void Unsubscribe(GameEvent<T> gameEvent) => _event -= gameEvent.Invoke;
    public void Subscribe(Action<T> gameEvent) => _event += gameEvent.Invoke;
    public void Subscribe(GameEvent<T> gameEvent) => _event += gameEvent.Invoke;
}

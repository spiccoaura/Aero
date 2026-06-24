// ====================================
// <copyright file="EventRegistry.cs" company="Spicco D'Aura">
// Copyright (c) Spicco D'Aura. All rights reserved.
// Licensed under the CC BY-SA 1.0 License.
// </copyright>
// ====================================

using System.Collections.Concurrent;

namespace Aero.Event;

public static class EventRegistry
{
    public static readonly ConcurrentDictionary<Type, int> Registry = new();

    public static void Register<T>() where T : struct, IEvent
    {
        if (!Registry.ContainsKey(typeof(T)))
            Registry[typeof(T)] = Registry.Count;
    }
    
    public static int GetId<T>() => Registry[typeof(T)];
}
// ====================================
// <copyright file="Dispatcher.cs" company="Spicco D'Aura">
// Copyright (c) Spicco D'Aura. All rights reserved.
// Licensed under the CC BY-SA 1.0 License.
// </copyright>
// ====================================

using Avian.Core;

namespace Avian.Event;

public class Dispatcher
{
    private readonly RingBuffer<IEvent>[] _eventBuffers;
    private readonly List<Action<IEvent>>[] _handlers;
    
    public Dispatcher(int numberOfEventTypes, int bufferSize)
    {
        _eventBuffers = new RingBuffer<IEvent>[numberOfEventTypes];
        for (int i = 0; i < numberOfEventTypes; i++)
        {
            _eventBuffers[i] = new RingBuffer<IEvent>(bufferSize);
        }
        _handlers = new List<Action<IEvent>>[numberOfEventTypes];
        for (int i = 0; i < numberOfEventTypes; i++)
        {
            _handlers[i] = new List<Action<IEvent>>();
        }
    }

    public void Publish<T>(T eventData) where T : struct, IEvent
    {
        int id = EventRegistry.GetId<T>();
        _eventBuffers[id].TryPush(eventData);
    }

    public void Subscribe<T>(IHandler<T> handler) where T : struct, IEvent
    {
        Subscribe<T>(handler.Handle);
    }

    private void Subscribe<T>(Action<T> handler) where T : struct, IEvent
    {
        int id = EventRegistry.GetId<T>();
        _handlers[id].Add(e => handler((T)e));
    }
    
    public void Execute()
    {
        for (int i = 0; i < _eventBuffers.Length; i++)
        {
            while (_eventBuffers[i].TryPop(out IEvent @eventData))
            {
                foreach (var handler in _handlers[i])
                {
                    handler(eventData);
                }
            }
        }
    }
}
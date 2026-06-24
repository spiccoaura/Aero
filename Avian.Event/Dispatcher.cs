// ====================================
// <copyright file="Dispatcher.cs" company="Spicco D'Aura">
// Copyright (c) Spicco D'Aura. All rights reserved.
// Licensed under the MIT License.
// </copyright>
// ====================================

using Avian.Core;

namespace Avian.Event;

public class Dispatcher
{
    private readonly RingBuffer<IEvent>[] _eventBuffers;
    private readonly List<object>[] _handlers;
    
    public Dispatcher(int numberOfEventTypes, int bufferSize)
    {
        _eventBuffers = new RingBuffer<IEvent>[numberOfEventTypes];
        _handlers = new List<object>[numberOfEventTypes];
        
        for (int i = 0; i < numberOfEventTypes; i++)
        {
            _eventBuffers[i] = new RingBuffer<IEvent>(bufferSize);
            _handlers[i] = new List<object>();
        }
    }
    
    public void Publish<T>(T eventData) where T : struct, IEvent
    {
        int id = EventRegistry.GetId<T>();
        _eventBuffers[id].TryPush(eventData);
    }
    
    public void PublishImmediate<T>(ref T eventData) where T : struct, IEvent
    {
        int id = EventRegistry.GetId<T>();
        var list = _handlers[id];

        for (int i = 0; i < list.Count; i++)
        {
            ((IHandler<T>)list[i]).Handle(ref eventData);
        }
    }
    
    public void Subscribe<T>(IHandler<T> handler) where T : struct, IEvent
    {
        int id = EventRegistry.GetId<T>();
        _handlers[id].Add(handler);
    }
    
    public void Execute()
    {
        for (int i = 0; i < _eventBuffers.Length; i++)
        {
            while (_eventBuffers[i].TryPop(out IEvent eventData))
            {
                var list = _handlers[i];
                for (int j = 0; j < list.Count; j++)
                {
                    if (list[j] is IHandlerConcreteProxy proxy)
                    {
                        proxy.HandleDynamic(ref eventData);
                    }
                }
            }
        }
    }
}
// ====================================
// <copyright file="RingBuffer.cs" company="Spicco D'Aura">
// Copyright (c) Spicco D'Aura. All rights reserved.
// Licensed under the CC BY-SA 1.0 License.
// </copyright>
// ====================================

namespace Aero.Core;

public class RingBuffer<T>(int size)
{
    private readonly T[] _buffer = new T[size];
    private int _head;
    private int _tail;
    private int _count;

    public bool TryPush(T item)
    {
        if (_count == size)
            return false;
        
        _buffer[_head] = item;
        _head = (_head + 1) % size;
        
        _count++;
        return true;
    }

    public bool TryPop(out T item)
    {
        if (_count == 0)
        {
            item = default!;
            return false;
        }

        item = _buffer[_tail];
        _tail = (_tail + 1) % size;
        
        _count--;
        return true;
    }
}
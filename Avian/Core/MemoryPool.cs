// // -----------------------------------------------------------------------
// // <copyright file="MemoryPool.cs" company="ThetaRP">
// // Copyright (c) Spicco D'Aura. All rights reserved.
// // Licensed under the CC BY-SA 3.0 license.
// // </copyright>
// // -----------------------------------------------------------------------

namespace Avian.Core;

public class MemoryPool<T> where T : new()
{
    public readonly Stack<T> Pool = new();

    public T Rent()
    {
        if(Pool.Count > 0)
            return Pool.Pop();
        return new T();
    }
    
    public void Return(T item) => Pool.Push(item);
    
}
// ====================================
// <copyright file="IHandlerConcreteProxy.cs" company="Spicco D'Aura">
// Copyright (c) Spicco D'Aura. All rights reserved.
// Licensed under the CC BY-SA 1.0 License.
// </copyright>
// ====================================

namespace Avian.Event;

public interface IHandlerConcreteProxy
{
    void HandleDynamic(ref IEvent ev);
}
﻿namespace BuildingBlocks.RabbitMq.Events;

public abstract class IntegrationEvent
{
    public Guid Id => Guid.NewGuid();
    
    public DateTime OccurredOn => DateTime.Now;
    
    public string EventType => GetType().AssemblyQualifiedName;
}
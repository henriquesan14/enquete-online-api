﻿using MediatR;

namespace EnqueteOnline.Domain.Abstractions
{
    public interface IDomainEvent : INotification
    {
        Guid EventId => Guid.NewGuid();
        public DateTime OccurredOn => DateTime.Now;
        public string EventType => GetType().AssemblyQualifiedName;
    }
}

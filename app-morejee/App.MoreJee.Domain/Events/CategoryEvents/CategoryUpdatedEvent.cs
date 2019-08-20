using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.MoreJee.Domain.Events.CategoryEvents
{
    public class CategoryUpdatedEvent : INotification
    {
        public string Id { get; protected set; }
        public string Name { get; protected set; }
        public string Resource { get; protected set; }
        public string Description { get; protected set; }
        public string Icon { get; protected set; }
        public CategoryUpdatedEvent(string id, string name, string resource, string description, string icon)
        {
            Id = id;
            Name = name;
            Resource = resource;
            Description = description;
            Icon = icon;
        }
    }
}

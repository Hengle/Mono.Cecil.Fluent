﻿
// ReSharper disable once CheckNamespace
namespace Mono.Cecil.Fluent.Attributes
{
    public interface IEventAttribute
    {
        EventAttributes EventAttributesValue { get; }
    }

    public static partial class EventDefinitionExtensions
    {
        public static EventDefinition UnsetEventAttributes(this EventDefinition @event, params EventAttributes[] attributes)
        {
            foreach (var attribute in attributes)
                @event.Attributes &= ~attribute;
            return @event;
        }
        public static EventDefinition UnsetAllEventAttributes(this EventDefinition @event)
        {
            @event.Attributes = 0;
            return @event;
        }
    }

    public static partial class EventDefinitionExtensions
	{
		public static EventDefinition SetEventAttributes(this EventDefinition @event, params EventAttributes[] attributes)
		{
			foreach (var attribute in attributes)
				@event.Attributes |= attribute;
			return @event;
		}

		public static EventDefinition SetEventAttributes<TAttr>(this EventDefinition @event) 
			where TAttr : struct, IEventAttribute
		{
			@event.Attributes |= default(TAttr).EventAttributesValue;
			return @event;
		}

		public static EventDefinition SetEventAttributes<TAttr1, TAttr2>(this EventDefinition field)
			where TAttr1 : struct, IEventAttribute
			where TAttr2 : struct, IEventAttribute
		{
			return field.SetEventAttributes<TAttr1>()
				.SetEventAttributes<TAttr2>();
		}
	}
}

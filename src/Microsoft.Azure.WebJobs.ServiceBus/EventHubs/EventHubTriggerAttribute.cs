﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using Microsoft.Azure.WebJobs.Description;

namespace Microsoft.Azure.WebJobs.ServiceBus
{
    /// <summary>
    /// Setup an 'trigger' on a parameter to listen on events from an event hub. 
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    [Binding]
    public sealed class EventHubTriggerAttribute : Attribute, IConnectionProvider
    {
        /// <summary>
        /// Create an instance of this attribute.
        /// </summary>
        /// <param name="eventHubName">Event hub to listen on for messages. </param>
        public EventHubTriggerAttribute(string eventHubName)
        {
            this.EventHubName = eventHubName;
        }

        /// <summary>
        /// Name of the event hub. 
        /// </summary>
        public string EventHubName { get; private set; }

        /// <summary>
        /// Optional Name of the consumer group. If missing, then use the default name, "$Default"
        /// </summary>
        public string ConsumerGroup { get; set; }

        /// <summary>
        /// Optional connection name. If missing, tries to use a registered event hub receiver.
        /// </summary>
        [AppSetting]
        public string Connection { get; set; }
    }
}
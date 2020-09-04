// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

namespace Microsoft.UI.Notifications
{
    [NotificationXmlElement("header")]
    internal sealed class Element_ToastHeader : IElement_ToastActivatable
    {
        [NotificationXmlAttribute("id")]
        public string Id { get; set; }

        [NotificationXmlAttribute("title")]
        public string Title { get; set; }

        [NotificationXmlAttribute("arguments")]
        public string Arguments { get; set; }

        [NotificationXmlAttribute("activationType", Element_ToastActivationType.Foreground)]
        public Element_ToastActivationType ActivationType { get; set; } = Element_ToastActivationType.Foreground;

        [NotificationXmlAttribute("protocolActivationTargetApplicationPfn")]
        public string ProtocolActivationTargetApplicationPfn { get; set; }

        [NotificationXmlAttribute("afterActivationBehavior", NotificationAfterActivationBehavior.Default)]
        public NotificationAfterActivationBehavior AfterActivationBehavior
        {
            get
            {
                return NotificationAfterActivationBehavior.Default;
            }

            set
            {
                if (value != NotificationAfterActivationBehavior.Default)
                {
                    throw new InvalidOperationException("AfterActivationBehavior on ToastHeader only supports the Default value.");
                }
            }
        }
    }
}

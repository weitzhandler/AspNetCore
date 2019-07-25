// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components.Test.Helpers
{
    public static class IComponentExtensions
    {
        public static void SetParameters(
            this IComponent component,
            Dictionary<string, object> parameters)
        {
            component.SetParametersAsync(DictionaryToParameterCollection(parameters));
        }

        private static ParameterView DictionaryToParameterCollection(
            IDictionary<string, object> parameters)
        {
            var builder = new RenderTreeBuilder(new TestRenderer());
            builder.OpenComponent<AbstractComponent>(0);
            foreach (var pair in parameters)
            {
                builder.AddAttribute(0, pair.Key, pair.Value);
            }
            builder.CloseElement();

            return new ParameterView(builder.GetFrames().Array, 0);
        }

        private abstract class AbstractComponent : IComponent
        {
            public abstract void Attach(RenderHandle renderHandle);
            public abstract Task SetParametersAsync(ParameterView parameters);
        }
    }
}

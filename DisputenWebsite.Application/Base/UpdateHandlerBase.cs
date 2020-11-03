using DisputenPWA.Domain.Hierarchy;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Application.Base
{
    public class UpdateHandlerBase
    {
        protected Dictionary<string, object> GetUpdateProperties<T>(T request)
        {
            var updateProperties = new Dictionary<string, object>();
            var propertyInfos = request.GetType().GetProperties();
            foreach (var propertyInfo in propertyInfos)
            {
                if (propertyInfo.Name == nameof(IdModelBase.Id)) continue;
                var value = propertyInfo.GetValue(request);
                if (value != null)
                {
                    updateProperties.Add(propertyInfo.Name, value);
                }
            }
            return updateProperties;
        }
    }
}

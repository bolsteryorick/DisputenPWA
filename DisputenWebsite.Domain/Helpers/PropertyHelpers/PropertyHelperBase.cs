using GraphQL.Language.AST;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace DisputenPWA.Domain.Helpers.PropertyHelpers
{
    public class PropertyHelperBase
    {
        protected bool Equals(string name, string propertyName)
        {
            return name.ToLower() == propertyName.ToLower();
        }

        protected IList<string> GetPropertyNames(IEnumerable<Field> fields)
        {
            return fields.Select(x => x.Name).ToImmutableList();
        }
        
        protected IList<Field> GetSubFields(Field field)
        {
            if(field != null)
            {
                return field.SelectionSet.Children.Select(x => (Field)x).ToList();
            }
            return new List<Field>();
        }
    }
}

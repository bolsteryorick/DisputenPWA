using GraphQL.Language.AST;
using System;
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

        public GroupPropertyHelper GetGroupPropertyHelper(
            IEnumerable<Field> fields, 
            string groupFieldName,
            DateTime? lowestEndDate,
            DateTime? highestStartDate
            )
        {
            var groupFields = GetSubFields(fields.FirstOrDefault(x => x.Name.ToLower() == groupFieldName.ToLower()));
            if (groupFields.Count > 0)
            {
                return new GroupPropertyHelper(groupFields, lowestEndDate, highestStartDate);
            }
            return null;
        }

        public AppEventPropertyHelper GetAppEventPropertyHelper(
            IEnumerable<Field> fields,
            string appEventFieldName,
            DateTime? lowestEndDate,
            DateTime? highestStartDate
            )
        {
            var appEventFields = GetSubFields(fields.FirstOrDefault(x => x.Name.ToLower() == appEventFieldName.ToLower()));
            if (appEventFields.Count > 0)
            {
                return new AppEventPropertyHelper(
                    appEventFields,
                    lowestEndDate,
                    highestStartDate
                );
            }
            return null;
        }

        public MemberPropertyHelper GetMemberPropertyHelper(
            IEnumerable<Field> fields,
            string memberFieldName
            )
        {
            var memberFields = GetSubFields(fields.FirstOrDefault(x => x.Name.ToLower() == memberFieldName.ToLower()));
            if (memberFields.Count > 0)
            {
                return new MemberPropertyHelper(
                    memberFields
                );
            }
            return null;
        }

        public UserPropertyHelper GetUserPropertyHelper(
            IEnumerable<Field> fields,
            string userFieldName
            )
        {
            var userFields = GetSubFields(fields.FirstOrDefault(x => x.Name.ToLower() == userFieldName.ToLower()));
            if (userFields.Count > 0)
            {
                return new UserPropertyHelper(
                    userFields
                );
            }
            return null;
        }
    }
}

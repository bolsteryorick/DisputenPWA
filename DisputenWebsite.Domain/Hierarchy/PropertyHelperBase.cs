using DisputenPWA.Domain.AttendeeAggregate;
using DisputenPWA.Domain.EventAggregate;
using DisputenPWA.Domain.GroupAggregate;
using DisputenPWA.Domain.MemberAggregate;
using DisputenPWA.Domain.UserAggregate;
using GraphQL.Language.AST;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace DisputenPWA.Domain.Hierarchy
{
    public class PropertyHelperBase
    {
        protected const int MaxDepth = 3;

        protected bool CanGoDeeper(int depth)
        {
            return depth <= MaxDepth;
        }

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
            if (field != null)
            {
                return field.SelectionSet.Children.Select(x => (Field)x).ToList();
            }
            return new List<Field>();
        }

        public GroupPropertyHelper GetGroupPropertyHelper(
            IEnumerable<Field> fields,
            string groupFieldName,
            DateTime? lowestEndDate,
            DateTime? highestStartDate,
            int depth
            )
        {
            var groupFields = GetSubFields(fields.FirstOrDefault(x => x.Name.ToLower() == groupFieldName.ToLower()));
            if (groupFields.Count > 0)
            {
                return new GroupPropertyHelper(groupFields, lowestEndDate, highestStartDate, depth + 1);
            }
            return null;
        }

        public AppEventPropertyHelper GetAppEventPropertyHelper(
            IEnumerable<Field> fields,
            string appEventFieldName,
            DateTime? lowestEndDate,
            DateTime? highestStartDate,
            int depth
            )
        {
            var appEventFields = GetSubFields(fields.FirstOrDefault(x => x.Name.ToLower() == appEventFieldName.ToLower()));
            if (appEventFields.Count > 0)
            {
                return new AppEventPropertyHelper(
                    appEventFields,
                    lowestEndDate,
                    highestStartDate,
                    depth + 1
                );
            }
            return null;
        }

        public MemberPropertyHelper GetMemberPropertyHelper(
            IEnumerable<Field> fields,
            string memberFieldName,
            int depth
            )
        {
            var memberFields = GetSubFields(fields.FirstOrDefault(x => x.Name.ToLower() == memberFieldName.ToLower()));
            if (memberFields.Count > 0)
            {
                return new MemberPropertyHelper(
                    memberFields,
                    depth + 1
                );
            }
            return null;
        }

        public UserPropertyHelper GetUserPropertyHelper(
            IEnumerable<Field> fields,
            string userFieldName,
            int depth
            )
        {
            var userFields = GetSubFields(fields.FirstOrDefault(x => x.Name.ToLower() == userFieldName.ToLower()));
            if (userFields.Count > 0)
            {
                return new UserPropertyHelper(
                    userFields,
                    depth + 1
                );
            }
            return null;
        }

        public AttendeePropertyHelper GetAttendeePropertyHelper(
            IEnumerable<Field> fields,
            string userFieldName,
            int depth
            )
        {
            var userFields = GetSubFields(fields.FirstOrDefault(x => x.Name.ToLower() == userFieldName.ToLower()));
            if (userFields.Count > 0)
            {
                return new AttendeePropertyHelper(
                    userFields,
                    depth + 1
                );
            }
            return null;
        }
    }
}

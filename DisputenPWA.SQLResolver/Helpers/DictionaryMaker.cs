using System;
using System.Collections.Generic;

namespace DisputenPWA.SQLResolver.Helpers
{
    public static class DictionaryMaker
    {
        public static Dictionary<KeyType, List<ItemType>> MakeDictionary<KeyType, ItemType>(string keyName, IReadOnlyCollection<ItemType> items)
        {
            var dict = new Dictionary<KeyType, List<ItemType>>();

            var getter = (Func<ItemType, KeyType>)Delegate.CreateDelegate(
                    typeof(Func<ItemType, KeyType>), 
                    null, 
                    typeof(ItemType).GetProperty(keyName).GetGetMethod()
                    );

            foreach (var item in items)
            {
                var groupId = getter(item);
                if (!dict.ContainsKey(groupId))
                {
                    dict[groupId] = new List<ItemType>();
                }
                dict[groupId].Add(item);
            }
            return dict;
        }
    }
}

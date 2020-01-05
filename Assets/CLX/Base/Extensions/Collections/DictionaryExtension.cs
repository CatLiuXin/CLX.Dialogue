using System.Collections.Generic;

namespace CLX.Extensions.Collections
{
    public static class DictionaryExtension
    {
        /// <summary>
        /// 获取key所对应的value，如果不存在的话返回defaultValue
        /// </summary>
        public static TValue GetValue<TKey,TValue>(this Dictionary<TKey,TValue> dict,
            TKey key,TValue defaultValue=default(TValue))
        {
            if (dict.ContainsKey(key))
            {
                return dict[key];
            }
            return defaultValue;
        }

        /// <summary>
        /// 尝试将键和值添加到字典中：如果不存在，才添加；存在，不添加也不抛导常
        /// </summary>
        public static Dictionary<TKey, TValue> TryAdd<TKey, TValue>(this Dictionary<TKey, TValue> dict,
            TKey key, TValue value)
        {
            if (dict.ContainsKey(key) == false) dict.Add(key, value);
            return dict;
        }

        /// <summary>
        /// 将键和值添加或替换到字典中：如果不存在，则添加；存在，则替换
        /// </summary>
        public static Dictionary<TKey, TValue> AddOrReplace<TKey, TValue>(this Dictionary<TKey, TValue> dict,
            TKey key, TValue value)
        {
            dict[key] = value;
            return dict;
        }

        /// <summary>
        /// 将键和值添加或替换到字典中.
        /// 如果不存在，则添加为addValue值；
        /// 存在，则根据repalceFunc替换为新的值
        /// </summary>
        public static Dictionary<TKey, TValue> AddOrReplace<TKey, TValue>(this Dictionary<TKey, TValue> dict,
            TKey key, TValue addValue, System.Func<TValue,TValue> repalceFunc)
        {
            if (dict.ContainsKey(key))
            {
                dict[key] = repalceFunc(dict[key]);
            }
            else
            {
                dict[key] = addValue;
            }
            return dict;
        }

        /// <summary>
        /// 向字典中批量添加键值对
        /// </summary>
        /// <param name="replaceExisted">如果已存在，是否替换</param>
        public static Dictionary<TKey, TValue> AddRange<TKey, TValue>(this Dictionary<TKey, TValue> dict,
            IEnumerable<KeyValuePair<TKey, TValue>> values,bool replaceExisted)
        {
            foreach (var item in values)
            {
                if (dict.ContainsKey(item.Key) == false || replaceExisted)
                    dict[item.Key] = item.Value;
            }
            return dict;
        }


    }
}

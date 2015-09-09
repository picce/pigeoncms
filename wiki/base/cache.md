#Caching contents

You can cache almost everything using `PigeonCms.Core.Helpers.CacheManager<T>` class.

Here an example of how to use cache to store a list of items:
```C#
const string CACHE_KEY = "Rows_Items";
var cachedList = new CacheManager<List<Item>>("ACME.CacheSample");
List<Item> list;

if (cachedList.IsEmpty(CACHE_KEY))
{
    //data from db
    list = getList();
    cachedList.Insert(CACHE_KEY, list);
}
else
{
    //data from cache
    list = cachedList.GetValue(CACHE_KEY);
}

//..do stuff with list..
```

Here an example of how to clear or invalidate cache:
```C#
//clear cache
cachedList.Remove(CACHE_KEY);
```

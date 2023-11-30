using Framework;
using System.Collections.Generic;

public static class ResourceExtension
{
    public static void AddResource(this ResourceType resourceType, int value)
    {
        ResourceUnit.resources[resourceType].Add(value);
    }
    public static void ReduceResource(this ResourceType resourceType, int value)
    {
        ResourceUnit.resources[resourceType].Minus(value);
    }
    public static bool IsAffordable(this ResourceType resourceType, int value)
    {
        return ResourceUnit.resources[resourceType].IsAffordable(value);
    }
    public static bool IsAffordable(this ResourceType resourceType, IEnumerable<int> values)
    {
        return ResourceUnit.resources[resourceType].IsAffordable(values);
    }
}

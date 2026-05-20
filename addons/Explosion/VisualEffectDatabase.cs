using Godot;
using Godot.Collections;

#nullable enable

public partial class VisualEffectDatabase : Node
{
    [Export] public Dictionary<VisualEffectKey, VisualEffectResource> visualEffectResources = [];
    [Export] public MaterialType FallbackMaterial = MaterialType.General;

    public VisualEffectResource? GetVisualEffectResource(VisualEffectKey visualEffectKey)
    {
        // if (visualEffectResources.TryGetValue(visualEffectKey, out var visualEffectResource))
        //     return visualEffectResource;

        foreach (var kvp in visualEffectResources)
        {
            if (kvp.Key.Type == visualEffectKey.Type && kvp.Key.Material == visualEffectKey.Material)
                return kvp.Value;
        }

        var fallbackKey = new VisualEffectKey { Type = VisualEffectType.Hit, Material = FallbackMaterial };
        if (visualEffectResources.TryGetValue(fallbackKey, out var fallbackResource))
            return fallbackResource;

        Logger.LogWarning($"No visual effect resource found for type '{visualEffectKey.Type}' and material '{visualEffectKey.Material}'.", Logger.LogTypeEnum.Audio);
        return null;
    }
}

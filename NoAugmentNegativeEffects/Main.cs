using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Augments;
using Kingmaker.Blueprints.Items.Components;
using Kingmaker.Blueprints.JsonSystem;
using System.Reflection;
using UnityModManagerNet;

namespace NoAugmentNegativeEffects;

public static class Main
{
    internal static Harmony HarmonyInstance;
    internal static UnityModManager.ModEntry.ModLogger Log;

    public static bool Load(UnityModManager.ModEntry modEntry)
    {
        Log = modEntry.Logger;
        modEntry.OnGUI = OnGUI;
        HarmonyInstance = new Harmony(modEntry.Info.Id);
        try
        {
            HarmonyInstance.PatchAll(Assembly.GetExecutingAssembly());
        }
        catch
        {
            HarmonyInstance.UnpatchAll(HarmonyInstance.Id);
            throw;
        }
        return true;
    }

    public static void OnGUI(UnityModManager.ModEntry modEntry)
    {

    }

    [HarmonyPatch(typeof(BlueprintsCache))]
    public static class BlueprintsCaches_Patch
    {
        private static bool Initialized = false;

        [HarmonyPriority(Priority.First)]
        [HarmonyPatch(nameof(BlueprintsCache.Init)), HarmonyPostfix]
        public static void Init_Postfix(BlueprintsCache __instance)
        {
            try
            {
                if (Initialized)
                {
                    Log.Log("Already initialized blueprints cache.");
                    return;
                }
                Initialized = true;

                Log.Log("Patching blueprints.");
                // Take a snapshot of the keys first to avoid enumeration-modified errors
                var allKeys = __instance.m_LoadedBlueprints.Keys.ToArray();
                var guids = new List<string>();
                foreach (var g in allKeys)
                {
                    if (__instance.Load(g) is BlueprintItemAugment)
                        guids.Add(g);
                }

                foreach (var guid in guids)
                {
                    BlueprintItemAugment augment = (BlueprintItemAugment)__instance.Load(guid);
                    //Log.Log($"Checking augment: {augment.name} ({guid})");
                    // For each component of the augment, if it is an AddFactToEquipmentWielder, remove it if its BlueprintFeature's AssetName has the text "negative" in it
                    var components = augment.ComponentsArray?.Where(c =>
                    {
                        if (c is AddFactToEquipmentWielder addFact)
                        {
                            if (addFact.Fact != null)
                            {
                                //Log.Log($"Checking component: {addFact.name} ({addFact.Fact.name})");
                                return addFact.Fact.name.IndexOf("negative", StringComparison.OrdinalIgnoreCase) < 0;
                            }
                        }
                        return true;
                    }).ToList() ?? new List<BlueprintComponent>();

                    if ((augment.ComponentsArray?.Length ?? 0) != components.Count)
                    {
                        augment.ComponentsArray = components.ToArray();
                        //Log.Log($"Removed negative effect from augment: {augment.name} ({guid})");
                        __instance.AddCachedBlueprint(guid, augment);
                    }
                }

            }
            catch (Exception e)
            {
                Log.Log(string.Concat("Failed to initialize.", e));
            }
        }
    }
}

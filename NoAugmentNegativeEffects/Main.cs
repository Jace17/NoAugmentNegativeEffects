using HarmonyLib;
using Kingmaker.Blueprints.Items.Augments;
using Kingmaker.Blueprints.Items.Components;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Code.UI.MVVM.VM.Tooltip.Bricks;
using Kingmaker.Code.UI.MVVM.VM.Tooltip.Templates.TooltipTemplateItemParts;
using Owlcat.Runtime.UI.Tooltips;
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
                // Log all guids
                //var guids = __instance.m_LoadedBlueprints.Keys;
                //foreach (var g in guids)
                //{
                //    if (__instance.Load(g) is BlueprintItemAugment)
                //        Log.Log($"\"{g}\",");
                //}

                String[] guids = [
                    "fa87fe5faab24943928f8c7e0301c55a",
                    "2727a81b0a404ceb9f699e36c9a899bb",
                    "c9801dbe50a545a784e650b7e4aaaac5",
                    "5ab98bb0ac3c4c3388f6eddad4bf6c36",
                    "28e0eb91889b4ddca59830abb90208b9",
                    "eeeb639d5ccb450cb038aba28fdc05e3",
                    "2a9570c7d6af4eaf8bc395438e30cfd2",
                    "130ca69e986f414296669e7fff6a5d4a",
                    "2ae3fda28f264db681817408c6353c3f",
                    "f6dd66c8534a4bb89dd1a086a7de55f5",
                    "d298fed4a4664d238fbb28e385ff3100",
                    "1556658994b140309895c706929d40f6",
                    "cde1d446416549228ec8ea008a871645",
                    "d0876b1bcdf14c559fd38ccfa53e498b",
                    "88ad53e2e54b46ef834571294b07aeee",
                    "39b7f180bb524f1bae8216fc221b46d9",
                    "569b3c3bb17441bb8361e09a92bbeac5",
                    "aec3b818b1aa465c82392e50ee7aa538",
                    "a064f41321eb4a50b7e9eeccff42e7e8",
                    "2eb45d79c74c4164aa8c5adf34865543",
                    "d11c093e97cf4ba9babc3e6d42863d81",
                    "582399431cdf4f18a7370d4569917399",
                    "be8c47096c594e65aee606ad8bc397dc",
                    "20d1534dbcaf47f694df5de997502ff4",
                    "591e7e034e5045faa31a0c9e5786da08",
                    "18b727b71c794b558d229a7831ef7bee",
                    "587c9d09efc4481282271ee0db9df7c1",
                    "8d598bfec7e042029ab0c2a2ec8c478b",
                    "e807b19418564d1a8dedcc8b7def2d5b",
                    "fd1da14cd1f54ef0a280ce1160dde3e6",
                    "8320bba9ff8a4870a10256f51226ca4c",
                    "f60c2f316af34bac93e95638ad366ca8",
                    "e1231239b8f842eabafd22b2c2e40ded",
                    "2fd9849654ed4556aca8ea0ca6ed8188",
                    "756e0384483540e8a160b1b5584440c9",
                    "04fbf60fe58d4da289a55f645969bb52",
                    "dc5d0e7b109e42a6959f509756fdbdc2",
                    "d7d4a4aecb944d6aad5880b0b1f3bdc7",
                    "9574a2028a5640b181202068c88759f8",
                    "b986d0399c6e4165a43596b8101abf8c",
                    "8cb545ebdd1f4a1aabb04edd8a876251",
                    "ada7f3aa826a411d97fe3715e9c33cf5",
                    "8a7112899e934f6d82bec38e70bc597f",
                    "41ce38591ba14c159a7167e4d0aec3ca",
                    "6c06809a1cf84f2bb7938e7f5d6f0e7f",
                    "9d9ffeda704e468b8e92df6f5d2ff75b",
                    "1d272b97be0849f6a8ea665d3f030df4",
                    "8cd258b21e974216bbbe2b2bf943a48e",
                    "7656626a2e23485eb8c2fe1017e4ae46",
                    "3abc1cf2a493471b938dc1b7106dc481",
                    "c6e22aec78c641e1abe24f16d231ef72",
                    "13757f3863ec47929fd914b7f306fa31",
                    "87c3dd3ad50045a39d09009867c1d605",
                    "2e102d4fdebf429ab51fcf015aafe50a",
                    "2f1b9ffcdd494a31ad99f609b4d5921d",
                    "a19c3bdd7dac4ac2aa4a67dd1dc9a930",
                    "8411f5688dbd4a08a8722cd9f6f80f20",
                    "32063e3c746f4e2e9d517f4282378f98",
                    "14990b3ff8da4539aa80aacde449efb7",
                    "2b610eeac01643419d9636949f74c297",
                    "3c1c3f3fb1444f3e96541b356ca64ab2",
                    "c899fdec8bd345f999de3708046e1ef7",
                    "8c99543a3af9405ca8c42e5d3e07b1a3",
                    "3ee99c8bed704c04affaf706eaee785e",
                    "7e2b91b8cc944f4c951fb2058fcb2f22",
                    "d16dbc47d0fc46c0833b3549f9be82f0",
                    "fe6060602c6d4b71be16808f05368242",
                    "fa121ed0f18c4017a53c7adb6036a552",
                    "372c3403a9a34805847fa3ab038e4c3e",
                    "dc7ac18ad7d04b848db1ff9b195c1d98",
                    "968f392e57a644b0b7954ac0f61a028c",
                    "a6ff1903856a4fa48589e54307d226f7",
                    "96481ea4098f4235b7b4af38cfbd70a1",
                    "b1b20071f47f450bb613bfb8033f8338",
                    "1cdc65695faa43b09a004e4fbcae92c1",
                    "3f66756b1f8a485ca16c11c12bf137c8",
                    "abb367ffd0f944a1819d719e8eff8142",
                    "068cc4c34bcd483ebd018b206ba9a7a8",
                    "80a9a29d43f04f98a57525d5e2ea57ff",
                    "efa9af54dbcb4ac095130abf827b0cbf",
                    "633672743a5944a9bc5a84f0c2545476",
                    "1a28a49f8d4343059260e44558da27bd",
                    "561e38ab4f56415280ad9b57712cff4b",
                    "c08512df303f42ab9b0c633a1882db24",
                    "710ecc018ff14207842910fcd0f9f466",
                    "a8007fd72eec4934b171cdfe11e4dbcd",
                    "a7a11bf442474497a0b9a5e636f579f6",
                    "19bfb0611c484e1e9e3dd816f4558f32",
                    "dd6e72190375445087673166301c750a",
                    "1aae0d6c1234436985385492b1a93774",
                    "5ec54f6924364701a83b9abcfdb2062c",
                    "9c2b22c47cb7485ea4d398de671250d6",
                    "f21829b4155847c194a1728ee2e84a90",
                    "450c26d133e44b06a56ea149ae6de5c5",
                    "1f37a299125648d8863b1c4ce322ca60",
                    "4107123e59564920aa0bb4a3e6072fd2",
                    "308b2e1c0cd34c82a33cd6d2738d7fa8",
                    "33a49bd301814a2c803bb82650cea118",
                    "e4b8860c9bb74bdbada651ced73aa27e",
                    "016338c050c54da48f91da00152b0d93",
                    "f9e771d6f94f4601b7e6c955c40d2307",
                    "8425a219082f42fa9b9c24ee8ae99151",
                    "0acf2c80e17541e79914eb8ebadcfb3a",
                    "f7446c0113a74a5d84a1e88ad16845b2",
                    "d09371309b4c4a699bee849c0274c744",
                    "678abb7603bf4376ad336cd113eefdb0",
                    "0a139ddffc6348dab4b3ed54d9051d8f",
                    "084dc0a72fa4465e96b1cb3c7047ac2b",
                    "ac2ffb65772340b48d4397e73429cf3a",
                    "f0107cd687d440bdaeea6ad3738cf813",
                    "d28e29e5e1d142249dc703f60243d681",
                    "c1eb664220d2495382a252b330303d92",
                    "3335d782627d4a6d8d1ee2b6e67d4ae6",
                    "c83dd7547a8f4a1187077a3ba3bb694c",
                    "632ea44429a746bab86b68a62a762708",
                    "1ddbc2556b094e9dbeacb332e9270da9",
                    "98326c617a0144d0886b5c74e60bd907",
                    "846a2ae351a34cfd921d5f15d9c2445c",
                    "d058f19f96054225a970c449f47b5a13",
                    "5b46732f51a9413bbc94491ac63c807f",
                    "7d9af936e7ca4f229f8a381e28032a04",
                    "5e2c0549e040480e98ff71749b5ae4b3",
                    "988433a10b7a477c8f189af3370a1ed9",
                    "c767aa7d4310426c97ea7f6057f1c9ff",
                    "e8dc5248bfa943cf891127ce05317417",
                    "57b92510d4214b3d86ef5f945b10ea60",
                    "40f568daef6149e89563b0aac66e3c3b",
                    "1b87942bf9614182979a3aaae3bb8bca",
                    "3f181e1770cc4e0ca282e0d90d60bacf",
                    "e8236066706b49b38294da45c9851020",
                    "bb9b4c043e6d40cc84f06d83dffc40a7",
                    "b49b545775cf41838af10f88ec9af6e1",
                    "5c3d611fb933420b9509dd515444726f",
                    "089d898d13a14733bd5acebb5e7dd84c",
                    "5c4b11d6e6384737956de054967e4f91"];

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
                    }).ToList() ?? [];

                    if ((augment.ComponentsArray?.Length ?? 0) != components.Count)
                    {
                        augment.ComponentsArray = [.. components];
                        //Log.Log($"Removed negative effect from augment: {augment.name} ({guid})");
                    }
                }

            }
            catch (Exception e)
            {
                Log.Log(string.Concat("Failed to initialize.", e));
            }
        }
    }

    [HarmonyPatch(typeof(AugmentItemPart))]
    public static class AugmentItemPart_Patch
    {
        [HarmonyPriority(Priority.First)]
        [HarmonyPatch(nameof(AugmentItemPart.AddEffectsDescription)), HarmonyPostfix]
        public static void AddEffectsDescription_Postfix(ref List<ITooltipBrick> result)
        {
            result.RemoveAll(t => t is TooltipBrickTitle);
        }
    }
}

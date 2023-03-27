using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using Trainworks.Managers;

namespace ProjectUmbra.Upgrades
{
    class Largestone
    {
        public static void Modify()
        {
            CardUpgradeData upgradeData =
                ProviderManager.SaveManager.GetAllGameData().FindCardUpgradeData("64500555-8a22-4328-bbc8-3220b2d17293");
            Traverse.Create(upgradeData).Field("bonusDamage").SetValue(15);
            Traverse.Create(upgradeData).Field("bonusHP").SetValue(30);

            StatusEffectStackData trampleData = new StatusEffectStackData
            {
                statusId = "trample",
                count = 1
            };

            upgradeData.GetStatusEffectUpgrades().Add(trampleData);

            string key = ProjectUmbra.GUID + "UpgradeData_DescriptionKey_Largestone";
            CustomLocalizationManager.ImportSingleLocalization(key, "Text", "", "", "EnhancerData_UnitUpgradeBiggerUmbra", "",
                "<nobr>+1[capacity]</nobr>, +15[attack], +30[health], [trample].");
            Traverse.Create(upgradeData).Field("upgradeDescriptionKey").SetValue(key);

            EnhancerData enhancerData =
                ProviderManager.SaveManager.GetAllGameData().FindEnhancerData("e1646fd5-9245-431a-a07f-580c3a033347");
            key = ProjectUmbra.GUID + "EnhancerData_DescriptionKey_Largestone";
            CustomLocalizationManager.ImportSingleLocalization(key, "Text", "", "", "EnhancerData_UnitUpgradeBiggerUmbra", "",
                "Upgrade a unit with <nobr>+1[capacity]</nobr>, +15[attack], +30[health], and [trample].");
            Traverse.Create(enhancerData).Field("descriptionKey").SetValue(key);



        }
    }
}

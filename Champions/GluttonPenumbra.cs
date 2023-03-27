using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;

namespace ProjectUmbra.Champions
{
    class GluttonPenumbra
    {
        public static void Modify()
        {
            ModifyGluttonPenumbraUpgrade("5a85e2f8-8197-4cc0-a4b8-02cd8ed0a20e", 0, 0, 0, 0);
            ModifyGluttonPenumbraUpgrade("c37e7693-e0f7-4149-be80-7091715be579", 0, 0, 20, 1);
            ModifyGluttonPenumbraUpgrade("bd4485de-039e-41f9-b174-9212f8fc2ca7", 0, 0, 40, 2);
        }

        private static void ModifyGluttonPenumbraUpgrade(string upgradeID, int bonusSize, int bonusDamage, int bonusHP, int bonusGorge)
        {
            CardUpgradeData upgradeData =
                ProviderManager.SaveManager.GetAllGameData().FindCardUpgradeData(upgradeID);
            Traverse.Create(upgradeData).Field("bonusSize").SetValue(bonusSize);
            Traverse.Create(upgradeData).Field("bonusDamage").SetValue(bonusDamage);
            Traverse.Create(upgradeData).Field("bonusHP").SetValue(bonusHP);

            upgradeData.GetStatusEffectUpgrades().Clear();
            upgradeData.GetTriggerUpgrades().Clear();

            CharacterTriggerData hungerData = new CharacterTriggerDataBuilder
            {
                Trigger = CharacterTriggerData.Trigger.OnFoodSpawn,
                DescriptionKey = "CharacterData_descriptionKey-c178d2cd3adb49c4-6c9046852d9050b45883c23f7cfd5795-v2",
                AdditionalTextOnTrigger = "EmptyString-0000000000000000-00000000000000000000000000000000-v2",
                HideTriggerTooltip = false,
                DisplayEffectHintText = false,
                EffectBuilders = new List<CardEffectDataBuilder>()
                {
                    new CardEffectDataBuilder
                    {
                        EffectStateType = VanillaCardEffectTypes.CardEffectFeederRules,
                        TargetMode = TargetMode.Room,
                        TargetTeamType = Team.Type.Monsters | Team.Type.Heroes,
                    }
                }
            }.Build();

            upgradeData.GetTriggerUpgrades().Add(hungerData);

            // Only do the gorge upgrade text if we have bonus damage.
            if (bonusGorge > 0)
            {
                string key = ProjectUmbra.GUID + "CardUpgradeData_GluttonPenumbraGorge" + bonusGorge.ToString();
                string description = "+[effect0.upgrade.bonusdamage][attack] and +[effect0.upgrade.bonushp][health].";
                Utilities.AddLocalizationKey(key, "", description);

                CharacterTriggerData gorgeData = new CharacterTriggerDataBuilder
                {
                    Trigger = CharacterTriggerData.Trigger.OnFeed,
                    DescriptionKey = key,
                    AdditionalTextOnTrigger = "EmptyString-0000000000000000-00000000000000000000000000000000-v2",
                    HideTriggerTooltip = false,
                    DisplayEffectHintText = false,
                    EffectBuilders = new List<CardEffectDataBuilder>()
                    {
                        new CardEffectDataBuilder
                        {
                            EffectStateType = VanillaCardEffectTypes.CardEffectAddTempCardUpgradeToUnits,
                            TargetMode = TargetMode.Self,
                            TargetTeamType = Team.Type.None,
                            ParamCardUpgradeData = new CardUpgradeDataBuilder
                            {
                                BonusDamage = bonusGorge,
                                BonusHP = bonusGorge
                            }.Build()
                        }
                    }
                }.Build();

                upgradeData.GetTriggerUpgrades().Add(gorgeData);
            }


        }
    }
}

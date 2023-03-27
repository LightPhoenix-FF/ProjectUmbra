using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;

namespace ProjectUmbra.Champions
{
    class MonstrousPenumbra
    {
        public static void Modify()
        {
            ModifyMonstrousPenumbraUpgrade("52fcb0d7-2a46-44ef-8ea6-7cafa44b25df", 2, 15, 15, 3);
            ModifyMonstrousPenumbraUpgrade("208f3fb4-72cc-4eda-b970-fb5b5cd7b194", 2, 45, 45, 6);
            ModifyMonstrousPenumbraUpgrade("6e0b7f50-ed86-4658-bad4-c353421f5444", 2, 115, 115, 9);
        }
        private static void ModifyMonstrousPenumbraUpgrade(string upgradeID, int bonusSize, int bonusDamage, int bonusHP, int scaleStat)
        {
            CardUpgradeData upgradeData =
                ProviderManager.SaveManager.GetAllGameData().FindCardUpgradeData(upgradeID);
            Traverse.Create(upgradeData).Field("bonusSize").SetValue(bonusSize);
            Traverse.Create(upgradeData).Field("bonusDamage").SetValue(bonusDamage);
            Traverse.Create(upgradeData).Field("bonusHP").SetValue(bonusHP);

            string key = ProjectUmbra.GUID + "CardUpgradeData_MonsterousPenumbra" + scaleStat.ToString();
            string description = "+[effect0.upgrade.bonusdamage][attack] and +[effect0.upgrade.bonushp][health].";
            Utilities.AddLocalizationKey(key, "", description);

            CharacterTriggerData triggerData = new CharacterTriggerDataBuilder
            {
                Trigger = CharacterTriggerData.Trigger.PostCombat,
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
                            BonusDamage = scaleStat,
                            BonusHP = scaleStat
                        }.Build()
                    }
                }
            }.Build();
            upgradeData.GetTriggerUpgrades().Add(triggerData);
        }

    }
}

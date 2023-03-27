using HarmonyLib;
using System;
using System.Collections.Generic;
using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;

namespace ProjectUmbra.Units
{
    class CrucibleWarden
    {
        public static void Modify()
        {
            string charID = VanillaCharacterIDs.CrucibleWarden;
            CharacterData charData = CustomCharacterManager.GetCharacterDataByID(charID);
            Traverse.Create(charData).Field("health").SetValue(30);
            Traverse.Create(charData).Field("attackDamage").SetValue(20);

            string key = ProjectUmbra.GUID + "CardData_DescriptionKey_CrucibleWarden_OnGorge";
            CustomLocalizationManager.ImportSingleLocalization(key, "Text", "", "", "CardData_CrucibleWarden", "", "Gain <b>Damage Shield [effect0.status0.power]</b>.  Apply <b>Dazed [effect1.status0.power]</b> to the front unit.");

            CardEffectData cardEffectData = new CardEffectDataBuilder
            {
                EffectStateType = VanillaCardEffectTypes.CardEffectAddStatusEffect,
                TargetMode = TargetMode.FrontInRoom,
                TargetTeamType = Team.Type.Heroes,
                ParamStatusEffects = new StatusEffectStackData[]
                {
                    new StatusEffectStackData
                    {
                        count = 1,
                        statusId = VanillaStatusEffectIDs.Dazed
                    }
                }
            }.Build();
            charData.GetTriggers()[0].GetEffects().Add(cardEffectData);

            CharacterTriggerData triggerData = charData.GetTriggers()[0];
            Traverse.Create(triggerData).Field("descriptionKey").SetValue(key);

            string infusionID = "a3ca372b-dbad-4d37-8728-9c284e8ef2dd";
            CardUpgradeData infusionData =
                ProviderManager.SaveManager.GetAllGameData().FindCardUpgradeData(infusionID);
            infusionData.GetStatusEffectUpgrades().Clear();

            string infusionKey = ProjectUmbra.GUID + "CardUpgradeData_UpgradeDescriptionKey_CrucibleWarden";
            CustomLocalizationManager.ImportSingleLocalization(infusionKey, "Text", "", "", "CardData_CrucibleWarden", "", "[feed]: Gain [damageshield] 1.");
            Traverse.Create(infusionData).Field("upgradeDescriptionKey").SetValue(infusionKey);

            var infusionTriggerData = new CharacterTriggerDataBuilder
            {
                Trigger = CharacterTriggerData.Trigger.OnFeed,
                AdditionalTextOnTrigger = "EmptyString-0000000000000000-00000000000000000000000000000000-v2",
                HideTriggerTooltip = false,
                DisplayEffectHintText = false,
                EffectBuilders = new List<CardEffectDataBuilder>
                {
                    // Spikes Effect
                    new CardEffectDataBuilder
                    {
                        EffectStateType = VanillaCardEffectTypes.CardEffectAddStatusEffect,
                        TargetMode = TargetMode.Self,
                        TargetTeamType = Team.Type.Monsters,
                        ParamStatusEffects = new StatusEffectStackData[]
                        {
                            new StatusEffectStackData
                            {
                                count = 1,
                                statusId = VanillaStatusEffectIDs.DamageShield
                            }
                        }
                    }
                }
            }.Build();
            infusionData.GetTriggerUpgrades().Add(infusionTriggerData);

        }
    }

}

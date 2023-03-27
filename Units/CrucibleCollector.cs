using HarmonyLib;
using System;
using System.Collections.Generic;
using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;

namespace ProjectUmbra.Units
{
    class CrucibleCollector
    {
        public static void Modify()
        {
            string charID = VanillaCharacterIDs.CrucibleCollector;
            CharacterData charData = CustomCharacterManager.GetCharacterDataByID(charID);
            Traverse.Create(charData).Field("health").SetValue(20);
            Traverse.Create(charData).Field("attackDamage").SetValue(30);

            StatusEffectStackData statusData = new StatusEffectStackData();
            statusData.statusId = "trample";
            statusData.count = 1;
            var paramStatusEffects = new StatusEffectStackData[] { statusData };
            Traverse.Create(charData).Field("startingStatusEffects").SetValue(paramStatusEffects);

            string infusionID = "b222be77-5b3c-43aa-8800-8b3ae2cb0da8";
            CardUpgradeData infusionData = 
                ProviderManager.SaveManager.GetAllGameData().FindCardUpgradeData(infusionID);
            infusionData.GetStatusEffectUpgrades().Clear();

            string infusionKey = ProjectUmbra.GUID + "CardUpgradeData_UpgradeDescriptionKey_CrucibleCollector";
            CustomLocalizationManager.ImportSingleLocalization(infusionKey, "Text", "", "", "CardData_CrucibleCollector", "", "[feed]: Gain [lifesteal] 1.");
            Traverse.Create(infusionData).Field("upgradeDescriptionKey").SetValue(infusionKey);

            var triggerData = new CharacterTriggerDataBuilder
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
                                statusId = VanillaStatusEffectIDs.Lifesteal
                            }
                        }
                    }
                }
            }.Build();
            infusionData.GetTriggerUpgrades().Add(triggerData);
        }
    }

}

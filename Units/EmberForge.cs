using HarmonyLib;
using System;
using System.Collections.Generic;
using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;

namespace ProjectUmbra.Units
{
    class EmberForge
    {
        public static void Modify()
        {
            string charID = VanillaCharacterIDs.EmberForge;
            CharacterData charData = CustomCharacterManager.GetCharacterDataByID(charID);
            Traverse.Create(charData).Field("size").SetValue(3);
            Traverse.Create(charData).Field("health").SetValue(30);

            Traverse.Create(charData).Field("roomModifiers").SetValue(new List<RoomModifierData>());

            string key = ProjectUmbra.GUID + "CardData_DescriptionKey_EmberForge_OnGorge";
            CustomLocalizationManager.ImportSingleLocalization(key, "Text", "", "", "CardData_EmberForge", "", "Gain [spikes] [effect0.status0.power] and [effect1.power][ember].");

            var triggerData = new CharacterTriggerDataBuilder
            {
                Trigger = CharacterTriggerData.Trigger.OnFeed,
                DescriptionKey = key,
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
                                count = 2,
                                statusId = VanillaStatusEffectIDs.Spikes
                            }
                        }
                    }, 
                    // Ember Gain Effect
                    new CardEffectDataBuilder
                    {
                        EffectStateType = VanillaCardEffectTypes.CardEffectGainEnergy,
                        TargetMode = TargetMode.Self,
                        TargetTeamType = Team.Type.Monsters,
                        ParamInt = 1
                    }
                }
            }.Build();
            charData.GetTriggers().Add(triggerData);

        }
    }

}

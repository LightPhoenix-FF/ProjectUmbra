using HarmonyLib;
using System;
using System.Collections.Generic;
using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;

namespace ProjectUmbra.Units
{
    class Shadowsiege
    {
        public static void Modify()
        {
            string cardID = VanillaCardIDs.Shadowsiege;
            CardData cardData = CustomCardManager.GetCardDataByID(cardID);
            Traverse.Create(cardData).Field("cost").SetValue(3);

            string charID = VanillaCharacterIDs.Shadowsiege;
            CharacterData charData = CustomCharacterManager.GetCharacterDataByID(charID);
            Traverse.Create(charData).Field("size").SetValue(5);
            Traverse.Create(charData).Field("health").SetValue(75);
            Traverse.Create(charData).Field("attackDamage").SetValue(75);

            StatusEffectStackData trampleData = new StatusEffectStackData();
            trampleData.statusId = "trample";
            trampleData.count = 1;

            StatusEffectStackData emberdrainData = new StatusEffectStackData();
            emberdrainData.statusId = "scorch";
            emberdrainData.count = 2;

            var paramStatusEffects = new StatusEffectStackData[] { trampleData, emberdrainData };
            Traverse.Create(charData).Field("startingStatusEffects").SetValue(paramStatusEffects);

            string key = ProjectUmbra.GUID + "CardData_DescriptionKey_Shadowsiege_OnGorge";
            CustomLocalizationManager.ImportSingleLocalization(key, "Text", "", "", "CardData_Shadowsiege", "", "+50[attack], +50[health], and +1[capacity].");

            var triggerData = new CharacterTriggerDataBuilder
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
                        TargetTeamType = Team.Type.Monsters,
                        ParamCardUpgradeData = new CardUpgradeDataBuilder()
                        {
                            BonusHP = 50,
                            BonusDamage = 50,
                            BonusSize = 1
                        }.Build()

                    }
                }
            }.Build();
            charData.GetTriggers().Add(triggerData);

        }
    }

}

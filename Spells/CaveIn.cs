using HarmonyLib;
using System;
using System.Collections.Generic;
using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;

namespace ProjectUmbra.Spells
{
    class CaveIn
    {
        public static void Modify()
        {
            string cardID = VanillaCardIDs.CaveIn;
            CardData cardData = CustomCardManager.GetCardDataByID(cardID);

            cardData.GetEffects().Clear();

            Traverse.Create(cardData).Field("cost").SetValue(1);

            string key = ProjectUmbra.GUID + "CardData_DescriptionKey_CaveIn";
            CustomLocalizationManager.ImportSingleLocalization(key, "Text", "", "", "CardData_CaveIn", "",
                "[effect0.power][capacity] on this floor. [halfbreak][descend] enemy units on this floor and apply [dazed] [effect1.status0.power].");
            Traverse.Create(cardData).Field("overrideDescriptionKey").SetValue(key);

            CardEffectData spaceEffect = new CardEffectDataBuilder()
            {
                EffectStateType = typeof(CardEffectAdjustRoomCapacity),
                TargetMode = TargetMode.Room,
                TargetTeamType = Team.Type.Monsters,
                ParamInt = -2,
                ShouldTest = true,
                AdditionalTooltips = new AdditionalTooltipData[]
                {
                    new AdditionalTooltipData
                    {
                        titleKey = "Capacity_TooltipTitle",
                        descriptionKey = "Capacity_TooltipText",
                        style = TooltipDesigner.TooltipDesignType.Keyword, // 8
                        isStatusTooltip = false,
                        statusId = "",
                        isTriggerTooltip = false,
                        trigger = CharacterTriggerData.Trigger.OnDeath, // 0
                        isTipTooltip = false
                    }
                }
            }.Build();

            CardEffectData dazeEffect = new CardEffectDataBuilder()
            {
                EffectStateType = typeof(CardEffectAddStatusEffect),
                TargetMode = TargetMode.Room,
                TargetTeamType = Team.Type.Heroes,
                ParamInt = 100,
                ParamStatusEffects = new StatusEffectStackData[]
                {
                    new StatusEffectStackData
                    {
                        count = 1,
                        statusId = VanillaStatusEffectIDs.Dazed
                    }
                }
            }.Build();

            CardEffectData descendEffect = new CardEffectDataBuilder()
            {
                EffectStateType = typeof(CardEffectBump),
                TargetMode = TargetMode.Room,
                TargetTeamType = Team.Type.Heroes,
                ParamInt = -1

            }.Build();

            cardData.GetEffects().Add(spaceEffect);
            cardData.GetEffects().Add(dazeEffect);
            cardData.GetEffects().Add(descendEffect);

        }
    }

}

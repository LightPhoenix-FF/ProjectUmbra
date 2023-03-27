using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;

namespace ProjectUmbra.Relics
{
    class TeethOfGold
    {
        public static void Modify()
        {
            CollectableRelicData relicData =
                ProviderManager.SaveManager.GetAllGameData().FindCollectableRelicData("38990528-6639-4cb2-b310-4e14193256a0");

            string key = ProjectUmbra.GUID + "CollectableRelicData_TeethOfGold_DescriptionKey";
            string description = "Units that eat a Morsel gain +1[attack] and +1[health].";
            Utilities.AddLocalizationKey(key, "", description);
            Traverse.Create(relicData).Field("descriptionKey").SetValue(key);

            // Clear old data
            relicData.GetEffects().Clear();

            RelicEffectData effectData = new RelicEffectDataBuilder
            {
                RelicEffectClassType = typeof(RelicEffectAddTrigger),
                ParamSourceTeam = Team.Type.Monsters,
                TriggerBuilders = new List<CharacterTriggerDataBuilder>
                {
                    new CharacterTriggerDataBuilder
                    {
                        Trigger = CharacterTriggerData.Trigger.OnFeed,
                        DisplayEffectHintText = false, 
                        EffectBuilders = new List<CardEffectDataBuilder>
                        {
                            new CardEffectDataBuilder
                            {
                                EffectStateType = VanillaCardEffectTypes.CardEffectAddTempCardUpgradeToUnits,
                                TargetMode = TargetMode.Self,
                                TargetTeamType = Team.Type.None,
                                ParamCardUpgradeData = new CardUpgradeDataBuilder
                                {
                                    BonusDamage = 1,
                                    BonusHP = 1,
                                    Filters = ProviderManager.SaveManager.GetAllGameData().FindCardUpgradeData("c62d9641-8948-4c00-b67d-60f0e728c1c1").GetFilters()
                                }.Build()
                            }
                        }
                    }
                }
            }.Build();

            relicData.GetEffects().Add(effectData);
        }
    }
}

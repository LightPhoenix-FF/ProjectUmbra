using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using Trainworks.Builders;
using Trainworks.Managers;

namespace ProjectUmbra.Relics
{

    class CommemorativeSpike
    {
        public static void Modify()
        {
            CollectableRelicData relicData =
                ProviderManager.SaveManager.GetAllGameData().FindCollectableRelicData("80a1b361-ee3a-43ae-86bd-da1ddac33a0a");

            string key = ProjectUmbra.GUID + "CollectableRelicData_CommemorativeSpike_DescriptionKey";
            string description = "Morsel units enter with <b>Buffet 2</b>.";
            Utilities.AddLocalizationKey(key, "", description);

            Traverse.Create(relicData).Field("descriptionKey").SetValue(key);

            // Clear old data
            relicData.GetEffects().Clear();

            RelicEffectData effectData = new RelicEffectDataBuilder
            {
                RelicEffectClassType = typeof(RelicEffectAddStatusEffectOnSpawn),
                ParamSourceTeam = Team.Type.Monsters,
                ParamCharacterSubtype = "SubtypesData_Snack",
                ParamStatusEffects = new StatusEffectStackData[] 
                {
                    new StatusEffectStackData()
                    {
                        statusId = "eatmany",
                        count = 2
                    }
                }

            }.Build();

            relicData.GetEffects().Add(effectData);
        }
    }
}

using HarmonyLib;
using System;
using System.Collections.Generic;
using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;

namespace ProjectUmbra.Spells
{
    class PerilsOfProduction
    {
        public static void Modify()
        {
            string ID = VanillaCardIDs.PerilsofProduction;
            var cardEffectData = CustomCardManager.GetCardDataByID(ID).GetEffects()[2];
            Traverse.Create(cardEffectData).Field("paramInt").SetValue(2);
        }
    }

}

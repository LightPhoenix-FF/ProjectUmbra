using HarmonyLib;
using System;
using System.Collections.Generic;
using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;

namespace ProjectUmbra.Spells
{
    class Plink
    {
        public static void Modify()
        {
            string ID = VanillaCardIDs.Plink;
            var cardEffectData = CustomCardManager.GetCardDataByID(ID).GetEffects()[0];
            Traverse.Create(cardEffectData).Field("paramInt").SetValue(2);
            cardEffectData = CustomCardManager.GetCardDataByID(ID).GetEffects()[1];
            Traverse.Create(cardEffectData).Field("paramInt").SetValue(2);
        }
    }

}

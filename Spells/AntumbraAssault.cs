using HarmonyLib;
using System;
using System.Collections.Generic;
using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;

namespace ProjectUmbra.Spells
{
    class AntumbraAssault
    {
        public static void Modify()
        {
            string ID = VanillaCardIDs.AntumbraAssault;
            var cardEffectData = CustomCardManager.GetCardDataByID(ID).GetEffects()[0];
            Traverse.Create(cardEffectData).Field("paramInt").SetValue(5);
        }
    }

}

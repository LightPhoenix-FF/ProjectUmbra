using HarmonyLib;
using System;
using System.Collections.Generic;
using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;

namespace ProjectUmbra.Spells
{
    class UmbraStone
    {
        public static void Modify()
        {
            string ID = VanillaCardIDs.UmbraStone;
            var cardData = CustomCardManager.GetCardDataByID(ID);
            Traverse.Create(cardData).Field("cost").SetValue(1);
        }
    }

}

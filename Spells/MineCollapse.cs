using HarmonyLib;
using System;
using System.Collections.Generic;
using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;

namespace ProjectUmbra.Spells
{
    class MineCollapse
    {
        public static void Modify()
        {
            string ID = VanillaCardIDs.MineCollapse;
            var cardTraitData = CustomCardManager.GetCardDataByID(ID).GetTraits()[0];
            Traverse.Create(cardTraitData).Field("paramInt").SetValue(5);
        }
    }

}

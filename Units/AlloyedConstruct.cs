using HarmonyLib;
using System;
using System.Collections.Generic;
using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;

namespace ProjectUmbra.Units
{
    class AlloyedConstruct
    {
        public static void Modify()
        {
            // Needs to change card, not character
            string cardID = VanillaCardIDs.AlloyedConstruct;
            CardData cardData = CustomCardManager.GetCardDataByID(cardID);
            Traverse.Create(cardData).Field("cost").SetValue(1);
        }
    }

}

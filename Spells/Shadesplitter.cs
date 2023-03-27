using HarmonyLib;
using System;
using System.Collections.Generic;
using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;

namespace ProjectUmbra.Spells
{
    class Shadesplitter
    {
        public static void Modify()
        {
            string ID = VanillaCardIDs.Shadesplitter;
            CardData cardData = CustomCardManager.GetCardDataByID(ID);
            Traverse.Create(cardData).Field("cost").SetValue(0);
        }
    }

}

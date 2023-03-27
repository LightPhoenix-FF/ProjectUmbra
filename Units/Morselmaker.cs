using HarmonyLib;
using System;
using System.Collections.Generic;
using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;

namespace ProjectUmbra.Units
{
    class Morselmaker
    {
        public static void Modify()
        {
            string cardID = VanillaCardIDs.Morselmaker;
            CardData cardData = CustomCardManager.GetCardDataByID(cardID);
            Traverse.Create(cardData).Field("cost").SetValue(1);

            string charID = VanillaCharacterIDs.Morselmaker;
            CharacterData charData = CustomCharacterManager.GetCharacterDataByID(charID);
            Traverse.Create(charData).Field("attackDamage").SetValue(0);
        }
    }

}

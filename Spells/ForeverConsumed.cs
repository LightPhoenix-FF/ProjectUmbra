using HarmonyLib;
using System;
using System.Collections.Generic;
using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;

namespace ProjectUmbra.Spells
{
    class ForeverConsumed
    {
        public static void Modify()
        {
            string ID = VanillaCardIDs.ForeverConsumed;
            var cardData = CustomCardManager.GetCardDataByID(ID);
            CardTraitData traitData = new CardTraitDataBuilder()
            {
                TraitStateType = VanillaCardTraitTypes.CardTraitStrongerMagicPower
            }.Build();
            cardData.GetTraits().Add(traitData);
        }
    }

}

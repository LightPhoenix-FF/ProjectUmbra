using HarmonyLib;
using System;
using System.Collections.Generic;
using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;

namespace ProjectUmbra.Spells
{
    class PrismRetrieval
    {
        public static void Modify()
        {
            string cardID = VanillaCardIDs.PrismRetrieval;
            CardData cardData = CustomCardManager.GetCardDataByID(cardID);
            CardUpgradeData upgradeData = cardData.GetEffects()[0].GetParamCardUpgradeData();
            Traverse.Create(upgradeData).Field("bonusDamage").SetValue(0);
            Traverse.Create(upgradeData).Field("bonusSize").SetValue(-1);

            string key = ProjectUmbra.GUID + "CardData_DescriptionKey_PrismRetrieval";
            CustomLocalizationManager.ImportSingleLocalization(key, "Text", "", "", "CardData_PrismRetrieval", "", 
                "Draw a unit and apply -[x][ember] and -1[capacity].");
            Traverse.Create(cardData).Field("overrideDescriptionKey").SetValue(key);

        }
    }

}

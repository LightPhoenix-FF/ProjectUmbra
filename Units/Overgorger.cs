using HarmonyLib;
using System;
using System.Collections.Generic;
using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;

namespace ProjectUmbra.Units
{
    class Overgorger
    {
        public static void Modify()
        {
            string charID = VanillaCharacterIDs.Overgorger;
            CharacterData charData = CustomCharacterManager.GetCharacterDataByID(charID);
            Traverse.Create(charData).Field("health").SetValue(30);
        }
    }

}

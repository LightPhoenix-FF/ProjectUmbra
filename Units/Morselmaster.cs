using HarmonyLib;
using System;
using System.Collections.Generic;
using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;

namespace ProjectUmbra.Units
{
    class Morselmaster
    {
        public static void Modify()
        {
            string charID = VanillaCharacterIDs.Morselmaster;
            CharacterData charData = CustomCharacterManager.GetCharacterDataByID(charID);
            Traverse.Create(charData).Field("health").SetValue(15);
            Traverse.Create(charData).Field("attackDamage").SetValue(0);
        }
    }

}

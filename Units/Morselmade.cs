using HarmonyLib;
using System;
using System.Collections.Generic;
using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;

namespace ProjectUmbra.Units
{
    class Morselmade
    {
        public static void Modify()
        {
            string charID = "fbd0d6e8-5e82-4466-af6c-9776ca8b5ba9";
            CharacterData charData = CustomCharacterManager.GetCharacterDataByID(charID);
            Traverse.Create(charData).Field("attackDamage").SetValue(0);
        }
    }

}

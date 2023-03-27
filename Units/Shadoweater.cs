using HarmonyLib;
using System;
using System.Collections.Generic;
using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;

namespace ProjectUmbra.Units
{
    class Shadoweater
    {
        public static void Modify()
        {
            string charID = VanillaCharacterIDs.Shadoweater;
            CharacterData charData = CustomCharacterManager.GetCharacterDataByID(charID);
        }
    }

}

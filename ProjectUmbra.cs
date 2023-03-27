using BepInEx;
using Trainworks;
using Trainworks.Interfaces;
using Trainworks.Constants;
using Trainworks.Managers;
using Trainworks.Builders;
using HarmonyLib;
using System.Collections.Generic;
using ProjectUmbra.StatusEffects;
using ProjectUmbra.Champions;
using ProjectUmbra.Relics;
using ProjectUmbra.Spells;
using ProjectUmbra.Upgrades;
using ProjectUmbra.Units;
using TMPro;

namespace ProjectUmbra
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess("MonsterTrain.exe")]
    [BepInProcess("MtLinkHandler.exe")]
    [BepInDependency("tools.modding.trainworks")]
    public class ProjectUmbra : BaseUnityPlugin, IInitializable
    {
        public const string GUID = "com.projectumbra";
        public const string NAME = "Project Umbra";
        public const string VERSION = "1.0.0";

        private void Awake()
        {
            Harmony harmony = new Harmony(GUID);
            harmony.PatchAll();
        }

        public void Initialize()
        {
            // Tiny
            Utilities.AddTextureToTextAtlas("assets/statustiny.png");
            StatusEffectTinyState.Make();

            // Units
            AlloyedConstruct.Modify();
            CrucibleCollector.Modify();
            CrucibleWarden.Modify();
            EmberForge.Modify();
            Morselmade.Modify();
            Morselmaker.Modify();
            Morselmaster.Modify();
            Overgorger.Modify();
            Shadoweater.Modify();
            Shadowsiege.Modify();

            // Champions
            MonstrousPenumbra.Modify();
            GluttonPenumbra.Modify();

            // Upgrades
            Largestone.Modify();

            // Relics
            CommemorativeSpike.Modify();
            TeethOfGold.Modify();

            // Spells
            AntumbraAssault.Modify();
            CaveIn.Modify();
            ForeverConsumed.Modify();
            MineCollapse.Modify();
            PerilsOfProduction.Modify();
            Plink.Modify();
            PrismRetrieval.Modify();
            Shadesplitter.Modify();
            UmbraStone.Modify();

            // Morsels
            //ModifyMorsel(VanillaCharacterIDs.RubbleMorsel);
            //ModifyMorsel(VanillaCharacterIDs.AntumbraMorsel);
            //ModifyMorsel(VanillaCharacterIDs.MagmaMorsel);
            //ModifyMorsel(VanillaCharacterIDs.MorselJeweler);
            //ModifyMorsel(VanillaCharacterIDs.MorselExcavator);
            //ModifyMorsel(VanillaCharacterIDs.MorselMiner);
            ModifyAllMorsels();
        }

        private void ModifyAllMorsels()
        {
            List<CharacterData> allCharacterData = ProviderManager.SaveManager.GetAllGameData().GetAllCharacterData();

            List<CharacterData> allMorselData = new List<CharacterData>();

            foreach (CharacterData characterData in allCharacterData)
            {
                if (characterData.GetSubtypes() != null)
                {
                    foreach (SubtypeData subtype in characterData.GetSubtypes())
                    {
                        if (subtype.Key == "SubtypesData_Snack")
                        {
                            ModifyMorsel(characterData.GetID());
                        }
                    }
                }
            }
        }

        private void ModifyMorsel(string morselID)
        {
            var morselData = CustomCharacterManager.GetCharacterDataByID(morselID);

            StatusEffectStackData tinyData = new StatusEffectStackData();
            tinyData.statusId = "tiny";
            tinyData.count = 1;

            var paramStatusEffects = new StatusEffectStackData[] { tinyData };
            Traverse.Create(morselData).Field("startingStatusEffects").SetValue(paramStatusEffects);
        }

    }

}

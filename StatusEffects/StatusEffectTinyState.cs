using System.Collections.Generic;
using Trainworks.Builders;
using Trainworks.Managers;

namespace ProjectUmbra.StatusEffects
{
    public sealed class StatusEffectTinyState : StatusEffectState
    {
        public const string StatusID = "tiny";

        public static void Make()
        {
            new StatusEffectDataBuilder
            {
                StatusEffectStateName = typeof(StatusEffectTinyState).AssemblyQualifiedName,
                StatusId = "tiny",
                DisplayCategory = StatusEffectData.DisplayCategory.Persistent,
                TriggerStage = StatusEffectData.TriggerStage.None,
                RemoveAtEndOfTurn = false,
                IsStackable = false,
                ShowStackCount = false,
                IconPath = "assets/statustiny.png"
            }.Build();

            string key = "StatusEffect_tiny_";
            CustomLocalizationManager.ImportSingleLocalization(key + "CardText", "Text", "", "", "", "", "<b>Tiny</b>");
            CustomLocalizationManager.ImportSingleLocalization(key + "CardTooltipText", "Text", "", "", "", "", "Not a target in combat if a unit without <b>Tiny</b> is in front of this one.");
            CustomLocalizationManager.ImportSingleLocalization(key + "CharacterTooltipText", "Text", "", "", "", "", "Not a target in combat if a unit without <b>Tiny</b> is in front of this one.");
        }

        public override bool GetUnitIsTargetable(bool inCombat)
        {
            // If the unit is not in combat return true, otherwise check location
            if (!inCombat) return true;
            return !GetIsBehindUnit();
        }

        public bool GetIsBehindUnit()
        {
            List<CharacterState> unitList = new List<CharacterState>();

            CharacterState thisCharacter = this.GetAssociatedCharacter();
            RoomState thisRoom = thisCharacter.GetSpawnPoint().GetRoomOwner();
            int thisIndex = thisCharacter.GetSpawnPoint().GetIndexInRoom();

            if (thisIndex == 0) return false;

            thisRoom.AddCharactersToList(unitList, Team.Type.Monsters, false);

            for (int i = 0; i < thisIndex; i++)
            {
                CharacterState unit = unitList[i];
                // If the unit is not Tiny, Stealth, or Phased; return true
                // Otherwise keep looking until we hit the unit
                if (!unit.HasStatusEffect("tiny") && !unit.HasStatusEffect("stealth") && !unit.HasStatusEffect("untouchable"))
                {
                    return true;
                }
            }
            return false;
        }
    }

}

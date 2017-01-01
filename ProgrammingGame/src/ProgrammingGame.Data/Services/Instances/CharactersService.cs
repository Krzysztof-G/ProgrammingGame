using System;
using System.Linq;
using ProgrammingGame.Common;
using ProgrammingGame.Common.Enums;
using ProgrammingGame.Data.Entities;
using ProgrammingGame.Data.Repositories.Instances;
using ProgrammingGame.Data.Repositories.Interfaces;
using ProgrammingGame.Data.Services.Interfaces;

namespace ProgrammingGame.Data.Services.Instances
{
    public class CharactersService : ICharactersService
    {
        private readonly ICharactersRepository _charactersRepository;
        private readonly IIndicatorsRepository _indicatorsRepository;
        private readonly IIndicatorTypesRepository _indicatorTypesRepository;
        private readonly ISystemActionsRepository _systemActionsRepository;

        public CharactersService(ICharactersRepository charactersRepository, IIndicatorsRepository indicatorsRepository, IIndicatorTypesRepository indicatorTypesRepository, ISystemActionsRepository systemActionsRepository)
        {
            _charactersRepository = charactersRepository;
            _indicatorsRepository = indicatorsRepository;
            _indicatorTypesRepository = indicatorTypesRepository;
            _systemActionsRepository = systemActionsRepository;
        }
        
        public Character GetCharacterByKey(Guid characterKey)
        {
            return _charactersRepository.FindBy(ch => ch.Key == characterKey).FirstOrDefault();
        }

        public void CreateCharacter(string characterName, long userId)
        {
            var newCharacter = new Character
            {
                Key = new Guid(),
                Name = characterName,
                State = (int)CharacterStates.Idle,
                LastStateChangeTime = CommonValues.ActaulaDateTime,
                Level = 0,
                Experience = 0,
                Cash = 0,
                UserId = userId,
            };
            _charactersRepository.Add(newCharacter);

            var energy = new Indicator
            {
                CharacterId = newCharacter.Id,
                IndicatorTypeId = (int)IndicatorTypes.Energy,
                Value = _indicatorTypesRepository.FindBy(x => x.Id == (int)IndicatorTypes.Energy).FirstOrDefault()?.DefaultValue ?? 0
            };
            _indicatorsRepository.Add(energy);

            _systemActionsRepository.Add(new SystemAction { CharacterId = newCharacter.Id, TypeId = (int)SystemActionTypes.SpanBeetwenEnergyAnalyse, LastExecutionTime = CommonValues.ActaulaDateTime });
            _systemActionsRepository.Add(new SystemAction { CharacterId = newCharacter.Id, TypeId = (int)SystemActionTypes.GainPointsForBeingRested, LastExecutionTime = CommonValues.ActaulaDateTime });
            _systemActionsRepository.Add(new SystemAction { CharacterId = newCharacter.Id, TypeId = (int)SystemActionTypes.LostPointsForBeingSleepy, LastExecutionTime = CommonValues.ActaulaDateTime });
            _systemActionsRepository.Add(new SystemAction { CharacterId = newCharacter.Id, TypeId = (int)SystemActionTypes.LostPointsForSleepToMuch, LastExecutionTime = CommonValues.ActaulaDateTime });
        }

        public void SetCharacterState(Character character, CharacterStates newState)
        {
            character.State = (int)newState;
            _charactersRepository.Edit(character);
            _charactersRepository.Save();
        }

        public void AddExperienceToCharacter(Character character, long experienceToAdd)
        {
            character.Experience += experienceToAdd;
            _charactersRepository.Edit(character);
            _charactersRepository.Save();
        }

        public void LevelUpCharacter(Character character, long pointsSurplus)
        {
            character.Level++;
            character.Experience = pointsSurplus;
            _charactersRepository.Edit(character);
            _charactersRepository.Save();
        }
    }
}
using Microsoft.EntityFrameworkCore;
using ProgrammingGame.Common;
using ProgrammingGame.Common.Enums;
using ProgrammingGame.Data.Entities;
using ProgrammingGame.Data.Repositories.Interfaces;
using ProgrammingGame.Data.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<Character> GetAllWithRelatedEntities()
        {
            return _charactersRepository
                .Context
                .Characters
                .Include(x => x.Indicators)
                .ThenInclude(x => x.IndicatorType)
                .Include(x => x.OwnedItems)
                .ThenInclude(x => x.ItemType)
                .Include(x => x.SystemActions)
                .ThenInclude(x => x.Type);
        }

        public Character GetCharacterByKey(Guid characterKey)
        {
            return _charactersRepository.FindBy(ch => ch.Key == characterKey).FirstOrDefault();
        }

        public void ActiveCharacter(long characterId)
        {
            var character = _charactersRepository
                .Context
                .Characters
                .Include(x => x.SystemActions)
                .ThenInclude(x => x.Type)
                .First(x => x.Id == characterId);

            character.IsActive = true;
            character.LastStateChangeTime = CommonValues.ActaulaDateTime;

            foreach (var systemAction in character.SystemActions)
            {
                systemAction.LastExecutionTime = CommonValues.ActaulaDateTime;
                _systemActionsRepository.Edit(systemAction);
            }

            _systemActionsRepository.Save();
            _charactersRepository.Save();
        }

        public void CreateCharacter(string characterName, long userId)
        {
            var newCharacter = new Character
            {
                Key = Guid.NewGuid(),
                IsActive = false,
                Name = characterName,
                State = (int)CharacterStates.Idle,
                LastStateChangeTime = CommonValues.ActaulaDateTime,
                Level = 0,
                Experience = 0,
                Cash = 0,
                UserId = userId,
            };
            _charactersRepository.Add(newCharacter);
            _charactersRepository.Save();

            var energy = new Indicator
            {
                CharacterId = newCharacter.Id,
                IndicatorTypeId = (int)IndicatorTypes.Energy,
                Value = _indicatorTypesRepository.FindBy(x => x.Id == (int)IndicatorTypes.Energy).FirstOrDefault()?.DefaultValue ?? 0
            };
            _indicatorsRepository.Add(energy);
            _indicatorsRepository.Save();

            _systemActionsRepository.Add(new SystemAction { CharacterId = newCharacter.Id, TypeId = (int)SystemActionTypes.SpanBeetwenEnergyAnalyze, LastExecutionTime = CommonValues.ActaulaDateTime });
            _systemActionsRepository.Add(new SystemAction { CharacterId = newCharacter.Id, TypeId = (int)SystemActionTypes.GainPointsForBeingRested, LastExecutionTime = CommonValues.ActaulaDateTime });
            _systemActionsRepository.Add(new SystemAction { CharacterId = newCharacter.Id, TypeId = (int)SystemActionTypes.LostPointsForBeingSleepy, LastExecutionTime = CommonValues.ActaulaDateTime });
            _systemActionsRepository.Add(new SystemAction { CharacterId = newCharacter.Id, TypeId = (int)SystemActionTypes.LostPointsForSleepToMuch, LastExecutionTime = CommonValues.ActaulaDateTime });
            _systemActionsRepository.Save();
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

        public void ResetLastStateChangeTime(Character character)
        {
            character.LastStateChangeTime = CommonValues.ActaulaDateTime;
            _charactersRepository.Edit(character);
            _charactersRepository.Save();
        }

        public bool EnoughTimeHasPassedFromPreviousStatusAnalyze(Character character, TimeSpan delayBeetwenExecuting)
        {
            return character.LastStateChangeTime.Add(delayBeetwenExecuting) <= CommonValues.ActaulaDateTime;
        }

        public void RenewIndicator(Character character, IndicatorTypes indicatorType, int? amount = null)
        {
            var indicator = _indicatorsRepository.Context.Indicators.Include(x => x.IndicatorType).FirstOrDefault(x => x.CharacterId == character.Id && x.IndicatorTypeId == (int)indicatorType);

            indicator.Value += amount ?? indicator.IndicatorType.MaxValue;
            if (indicator.Value > indicator.IndicatorType.MaxValue)
                indicator.Value = indicator.IndicatorType.MaxValue;
            else if (indicator.Value < indicator.IndicatorType.MinValue)
                indicator.Value = indicator.IndicatorType.MinValue;

            _indicatorsRepository.Edit(indicator);
            _indicatorsRepository.Save();
        }
    }
}
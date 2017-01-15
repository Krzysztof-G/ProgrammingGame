using ProgrammingGame.Common;
using ProgrammingGame.Common.Enums;
using ProgrammingGame.Data.Entities;
using ProgrammingGame.Data.Infrastructure;
using ProgrammingGame.Data.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProgrammingGame.Data.Services.Instances
{
    public class CharactersService : ICharactersService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CharactersService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Character> GetAllWithRelatedEntities()
        {
            var charactersRepository = _unitOfWork.Repository<Character>();
            var indicatorsRepository = _unitOfWork.Repository<Indicator>();
            var ownedItemsRepository = _unitOfWork.Repository<OwnedItem>();
            var systemActionsRepository = _unitOfWork.Repository<SystemAction>();

            var characters = charactersRepository.GetAllWithIncluding();

            foreach (var character in characters)
            {
                character.Indicators = indicatorsRepository.FindByWithIncluding(i => i.CharacterId == character.Id, i => i.IndicatorType).ToList();
                character.OwnedItems = ownedItemsRepository.FindByWithIncluding(oi => oi.CharacterId == character.Id, oi => oi.ItemType).ToList();
                character.SystemActions = systemActionsRepository.FindByWithIncluding(sa => sa.CharacterId == character.Id, sa => sa.Type).ToList();
            }

            return characters;
        }

        public Character GetCharacterByIdWithIndicatorsAndItems(long characterId)
        {
            var charactersRepository = _unitOfWork.Repository<Character>();
            var indicatorsRepository = _unitOfWork.Repository<Indicator>();
            var ownedItemsRepository = _unitOfWork.Repository<OwnedItem>();

            var character = charactersRepository.GetSingle(ch => ch.Id == characterId);

            character.Indicators = indicatorsRepository.FindByWithIncluding(i => i.CharacterId == characterId, i => i.IndicatorType).ToList();
            character.OwnedItems = ownedItemsRepository.FindByWithIncluding(oi => oi.CharacterId == characterId, oi => oi.ItemType).ToList();

            return character;
        }

        public Character GetCharacterByKey(Guid characterKey)
        {
            var charactersRepository = _unitOfWork.Repository<Character>();
            return charactersRepository.GetSingle(ch => ch.Key == characterKey);
        }

        public void ActiveCharacter(long characterId)
        {
            var charactersRepository = _unitOfWork.Repository<Character>();
            var systemActionsRepository = _unitOfWork.Repository<SystemAction>();

            var character = charactersRepository.GetSingleWithIncluding(ch => ch.Id == characterId, ch => ch.SystemActions);

            character.IsActive = true;
            character.LastStateChangeTime = CommonValues.ActaulaDateTime;
            charactersRepository.Update(character);

            foreach (var systemAction in character.SystemActions)
            {
                systemAction.LastExecutionTime = CommonValues.ActaulaDateTime;
                systemActionsRepository.Update(systemAction);
            }

            _unitOfWork.SaveChanges();
        }

        public void CreateCharacter(string characterName, long userId)
        {
            var charactersRepository = _unitOfWork.Repository<Character>();
            var indicatorsRepository = _unitOfWork.Repository<Indicator>();
            var indicatorTypesRepository = _unitOfWork.Repository<IndicatorType>();
            var systemActionsRepository = _unitOfWork.Repository<SystemAction>();
            var usersRepository = _unitOfWork.Repository<User>();

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
            charactersRepository.Insert(newCharacter);
            _unitOfWork.SaveChanges();

            var energy = new Indicator
            {
                CharacterId = newCharacter.Id,
                IndicatorTypeId = (int)IndicatorTypes.Energy,
                Value = indicatorTypesRepository.GetSingle(x => x.Id == (int)IndicatorTypes.Energy).DefaultValue
            };
            indicatorsRepository.Insert(energy);

            systemActionsRepository.Insert(new SystemAction { CharacterId = newCharacter.Id, TypeId = (int)SystemActionTypes.SpanBeetwenEnergyAnalyze, LastExecutionTime = CommonValues.ActaulaDateTime });
            systemActionsRepository.Insert(new SystemAction { CharacterId = newCharacter.Id, TypeId = (int)SystemActionTypes.GainPointsForBeingRested, LastExecutionTime = CommonValues.ActaulaDateTime });
            systemActionsRepository.Insert(new SystemAction { CharacterId = newCharacter.Id, TypeId = (int)SystemActionTypes.LostPointsForBeingSleepy, LastExecutionTime = CommonValues.ActaulaDateTime });
            systemActionsRepository.Insert(new SystemAction { CharacterId = newCharacter.Id, TypeId = (int)SystemActionTypes.LostPointsForSleepToMuch, LastExecutionTime = CommonValues.ActaulaDateTime });

            var user = usersRepository.GetSingle(x => x.Id == newCharacter.UserId);
            user.CharacterId = newCharacter.Id;

            usersRepository.Update(user);
            _unitOfWork.SaveChanges();
        }

        public void SetCharacterState(Character character, CharacterStates newState)
        {
            var charactersRepository = _unitOfWork.Repository<Character>();

            character.State = (int)newState;

            charactersRepository.Update(character);
            _unitOfWork.SaveChanges();
        }

        public void AddExperienceToCharacter(Character character, long experienceToAdd)
        {
            var charactersRepository = _unitOfWork.Repository<Character>();

            character.Experience += experienceToAdd;

            charactersRepository.Update(character);
            _unitOfWork.SaveChanges();
        }

        public void LevelUpCharacter(Character character, long pointsSurplus)
        {
            var charactersRepository = _unitOfWork.Repository<Character>();

            character.Level++;
            character.Experience = pointsSurplus;

            charactersRepository.Update(character);
            _unitOfWork.SaveChanges();
        }

        public void ResetLastStateChangeTime(Character character)
        {
            var charactersRepository = _unitOfWork.Repository<Character>();

            character.LastStateChangeTime = CommonValues.ActaulaDateTime;
            charactersRepository.Update(character);
            _unitOfWork.SaveChanges();
        }

        public bool EnoughTimeHasPassedFromPreviousStatusAnalyze(Character character, TimeSpan delayBeetwenExecuting)
        {
            return character.LastStateChangeTime.Add(delayBeetwenExecuting) <= CommonValues.ActaulaDateTime;
        }

        public void RenewIndicator(long characterId, IndicatorTypes indicatorType, int? amount = null)
        {
            var indicatorsRepository = _unitOfWork.Repository<Indicator>();

            var indicator = indicatorsRepository.GetSingleWithIncluding(x => x.CharacterId == characterId, x => x.IndicatorType);

            indicator.Value += amount ?? indicator.IndicatorType.MaxValue;
            if (indicator.Value > indicator.IndicatorType.MaxValue)
                indicator.Value = indicator.IndicatorType.MaxValue;
            else if (indicator.Value < indicator.IndicatorType.MinValue)
                indicator.Value = indicator.IndicatorType.MinValue;

            indicatorsRepository.Update(indicator);
            _unitOfWork.SaveChanges();
        }
    }
}
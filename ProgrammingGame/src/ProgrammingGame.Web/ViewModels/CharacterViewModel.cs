using System;
using System.Collections.Generic;
using ProgrammingGame.Common.Enums;

namespace ProgrammingGame.Web.ViewModels
{
    public class CharacterViewModel
    {
        public bool IsActive { get; set; }
        public Guid Key { get; set; }
        public string Name { get; set; }
        public CharacterStates State { get; set; }
        public DateTime LastStateChangeTime { get; set; }
        public int Level { get; set; }
        public long Experience { get; set; }
        public long Cash { get; set; }

        public List<IndicatorViewModel> Indicators { get; set; }
        public List<OwnedItemViewModel> OwnedItems { get; set; }
        public List<EventLogViewModel> EventLogs { get; set; }
    }
}

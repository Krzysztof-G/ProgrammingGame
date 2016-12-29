﻿using System;
using System.Collections.Generic;

namespace ProgrammingGame.Data.Domain.Entities
{
    public class Character
    {
        public long Id { get; set; }
        public Guid Key { get; set; }
        public string Name { get; set; }
        public int State { get; set; }
        public int Level { get; set; }
        public long Experience { get; set; }
        public long Cash { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }

        public ICollection<Indicator> Indicators { get; set; }
        public ICollection<OwnedItem> OwnedItems { get; set; }
    }
}

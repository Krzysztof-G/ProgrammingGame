using ProgrammingGame.Common.Enums;

namespace ProgrammingGame.Web.ViewModels
{
    public class EventLogViewModel
    {
        public string Header { get; set; }
        public string Contet { get; set; }
        public EventLogTypes Type { get; set; }
    }
}
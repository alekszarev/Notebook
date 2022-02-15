using Notebook.Data.Common;

namespace Notebook.Data.Models.Calendar
{
    public class Note : NotebookInformation

    {
        public int NoteId { get; set; }

        public string NoteName { get; set; }

        public string Content { get; set; }

        public int DayId { get; set; }

        public Day Day { get; set; }

        //TODO: Consider user relation
    }
}

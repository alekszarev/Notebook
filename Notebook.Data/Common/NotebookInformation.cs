namespace Notebook.Data.Common
{
    public class NotebookInformation : INotebookInformation
    {
        public string CreatedOn { get; set; } = DateTime.Now.ToString("dddd, dd MMMM yyyy");
        public string? ModifiedOn { get; set; }
        public bool IsDelete { get; set; }
        public string? DeletedOn { get; set; }
    }
}

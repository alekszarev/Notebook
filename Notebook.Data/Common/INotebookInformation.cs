namespace Notebook.Data.Common
{
    public interface INotebookInformation
    {
        public string CreatedOn { get; set; }

        public string? ModifiedOn { get; set; }
        public bool IsDelete { get; set; }
        public string? DeletedOn { get; set; }
    }
}

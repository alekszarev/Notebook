namespace Notebook.Data.Common
{
    public interface INotebookElement
    {
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}

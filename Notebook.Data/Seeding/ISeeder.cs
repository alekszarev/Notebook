namespace Notebook.Data.Seeding
{
    public interface ISeeder
    {
        Task SeedAsync(NotebookContext dbContext, IServiceProvider serviceProvider);
    }
}

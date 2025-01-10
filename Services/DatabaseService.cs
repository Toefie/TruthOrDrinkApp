using SQLite;
using TruthOrDrinkApp.Models;

namespace TruthOrDrinkApp.Services
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Question>().Wait();
        }

        public Task<List<Question>> GetQuestionsAsync() => _database.Table<Question>().ToListAsync();

        public Task<int> AddQuestionAsync(Question question) => _database.InsertAsync(question);

        public Task<int> DeleteQuestionAsync(Question question) => _database.DeleteAsync(question);

        public Task<Question> GetRandomQuestionAsync()
        {
            return _database.Table<Question>()
                .OrderBy(x => Guid.NewGuid())
                .FirstOrDefaultAsync();
        }
    }
}

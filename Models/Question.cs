
using TruthOrDrinkApp.Models;
using SQLite;

namespace TruthOrDrinkApp.Models
{
    public class Question
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Text { get; set; }
        public bool IsTruth { get; set; } // True for "Truth", False for "Drink"
    }
}

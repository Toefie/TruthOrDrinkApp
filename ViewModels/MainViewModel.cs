using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TruthOrDrinkApp.Models;
using TruthOrDrinkApp.Services;

namespace TruthOrDrinkApp.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        [ObservableProperty]
        private Question selectedQuestion;

        [ObservableProperty]
        private string newQuestionText;

        [ObservableProperty]
        private bool isTruth;

        public ObservableCollection<Question> Questions { get; } = new();

        public MainViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            _ = LoadQuestions();
        }

        [RelayCommand]
        private async Task LoadQuestions()
        {
            Questions.Clear();
            var questions = await _databaseService.GetQuestionsAsync();
            foreach (var question in questions)
                Questions.Add(question);
        }

        [RelayCommand]
        private async Task AddQuestion()
        {
            if (string.IsNullOrWhiteSpace(NewQuestionText))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please enter a question.", "OK");
                return;
            }

            var question = new Question
            {
                Text = NewQuestionText,
                IsTruth = IsTruth
            };

            await _databaseService.AddQuestionAsync(question);
            Questions.Add(question);

            // Reset het invoerveld en de switch
            NewQuestionText = string.Empty; 
            IsTruth = false;

            await Application.Current.MainPage.DisplayAlert("Success", "Question added successfully!", "OK");
        }

        [RelayCommand]
        private async Task GetRandomQuestion()
        {
            var question = await _databaseService.GetRandomQuestionAsync();
            SelectedQuestion = question ?? new Question { Text = "No questions available! Add some questions first." };
        }
    }
}
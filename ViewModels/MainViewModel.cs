using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TruthOrDrinkApp.Models;
using TruthOrDrinkApp.Services;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;  // Zorg ervoor dat dit is toegevoegd

namespace TruthOrDrinkApp.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;
        private readonly ApiService _apiService;

        [ObservableProperty]
        private Question selectedQuestion;

        [ObservableProperty]
        private string newQuestionText;

        [ObservableProperty]
        private bool isTruth;

        public ObservableCollection<Question> Questions { get; } = new();

        public MainViewModel(DatabaseService databaseService, ApiService apiService)
        {
            _databaseService = databaseService;
            _apiService = apiService;
            _ = LoadQuestions();
        }

        [RelayCommand]
        public async Task LoadQuestions() // Gebruik 'public' om toegang te garanderen
        {
            Questions.Clear();

            // Haal vragen op van de API
            var apiQuestions = await _apiService.GetQuestionsFromApiAsync();
            foreach (var question in apiQuestions)
                Questions.Add(question);

            // Optioneel: Haal ook lokale vragen op
            var localQuestions = await _databaseService.GetQuestionsAsync();
            foreach (var question in localQuestions)
                Questions.Add(question);
        }

        [RelayCommand]
        public async Task AddQuestion()
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

            // Voeg vraag toe via API
            await _apiService.AddQuestionToApiAsync(question);

            // Voeg vraag toe aan lokale collectie
            Questions.Add(question);

            // Reset invoer
            NewQuestionText = string.Empty;
            IsTruth = false;

            await Application.Current.MainPage.DisplayAlert("Success", "Question added successfully!", "OK");
        }

        [RelayCommand]
        public async Task GetRandomQuestion()
        {
            var question = await _databaseService.GetRandomQuestionAsync();
            SelectedQuestion = question ?? new Question { Text = "No questions available! Add some questions first." };
        }
    }
}
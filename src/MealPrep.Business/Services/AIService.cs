using Azure.AI.OpenAI;
using MealPrep.Business.Interfaces;
using MealPrep.Business.Models;
using OpenAI.Chat;
using System.ClientModel;

namespace MealPrep.Business.Services;

public class AIService : IAIService
{
    private readonly ChatClient _chatClient;

    public AIService(string apiKey, string model = "gpt-4")
    {
        // Create OpenAI client with API key
        var credential = new ApiKeyCredential(apiKey);
        var openAIClient = new AzureOpenAIClient(new Uri("https://api.openai.com/v1"), credential);
        _chatClient = openAIClient.GetChatClient(model);
    }

    public async Task<string> GenerateMealPlanAsync(MealPlanRequest request)
    {
        var prompt = $@"Generate a {request.NumberOfDays}-day meal plan with the following requirements:
- Dietary Restrictions: {request.DietaryRestrictions}
- Allergies: {request.Allergies}
- Daily Calorie Target: {request.CalorieTarget}
- Preferred Categories: {string.Join(", ", request.PreferredCategories)}

Please provide breakfast, lunch, and dinner for each day with recipe names and brief descriptions.";

        var messages = new List<ChatMessage>
        {
            new SystemChatMessage("You are a helpful nutritionist and meal planning assistant."),
            new UserChatMessage(prompt)
        };

        var response = await _chatClient.CompleteChatAsync(messages);
        return response.Value.Content[0].Text;
    }

    public async Task<string> GetRecipeSuggestionsAsync(string preferences)
    {
        var prompt = $@"Based on these preferences: {preferences}
Suggest 5 healthy and delicious recipes with their main ingredients and cooking difficulty level.";

        var messages = new List<ChatMessage>
        {
            new SystemChatMessage("You are a creative chef and recipe expert."),
            new UserChatMessage(prompt)
        };

        var response = await _chatClient.CompleteChatAsync(messages);
        return response.Value.Content[0].Text;
    }

    public async Task<string> AnalyzeNutritionalInfoAsync(string recipeName, List<string> ingredients)
    {
        var prompt = $@"Analyze the nutritional information for this recipe:
Recipe Name: {recipeName}
Ingredients: {string.Join(", ", ingredients)}

Provide estimated calories, protein, carbs, fats, and key vitamins/minerals.";

        var messages = new List<ChatMessage>
        {
            new SystemChatMessage("You are a nutritionist with expertise in food analysis."),
            new UserChatMessage(prompt)
        };

        var response = await _chatClient.CompleteChatAsync(messages);
        return response.Value.Content[0].Text;
    }
}

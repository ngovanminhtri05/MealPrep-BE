# MealPrep-BE

A 3-layer C# ASP.NET Core application for meal preparation with AI integration.

## Architecture

This application follows a clean 3-layer architecture:

### 1. Presentation Layer (MealPrep.API)
- **Purpose**: Handles HTTP requests and responses
- **Technology**: ASP.NET Core Web API with Swagger
- **Controllers**:
  - `RecipesController`: Manage recipes (CRUD operations)
  - `MealsController`: Manage meal plans (CRUD operations)
  - `AIController`: AI-powered features (meal planning, recipe suggestions, nutritional analysis)

### 2. Business Logic Layer (MealPrep.Business)
- **Purpose**: Implements business rules and service logic
- **Services**:
  - `RecipeService`: Recipe management logic
  - `MealService`: Meal planning logic
  - `AIService`: AI integration using Azure OpenAI/OpenAI API
- **Interfaces**: Service contracts for dependency injection

### 3. Data Access Layer (MealPrep.Data)
- **Purpose**: Database operations and data persistence
- **Technology**: Entity Framework Core with InMemory database
- **Components**:
  - `MealPrepDbContext`: Database context
  - `Repository<T>`: Generic repository pattern
  - `RecipeRepository`: Recipe-specific data operations
  - `MealRepository`: Meal-specific data operations

### Domain Layer (MealPrep.Domain)
- **Purpose**: Core domain entities and models
- **Entities**:
  - `Recipe`: Recipe information with ingredients
  - `Ingredient`: Ingredient details
  - `Meal`: Planned meals with recipes
  - `UserPreference`: User dietary preferences and restrictions

## AI Integration

The application integrates with OpenAI's GPT models to provide:
1. **Meal Plan Generation**: Create personalized meal plans based on user preferences, dietary restrictions, and calorie targets
2. **Recipe Suggestions**: Get AI-powered recipe recommendations
3. **Nutritional Analysis**: Analyze nutritional content of recipes

## Getting Started

### Prerequisites
- .NET 10.0 SDK or later
- (Optional) OpenAI API key for AI features

### Configuration

Update `appsettings.json` with your OpenAI API key:
```json
{
  "OpenAI": {
    "ApiKey": "your-openai-api-key-here",
    "Model": "gpt-4"
  }
}
```

### Build and Run

```bash
# Build the solution
dotnet build

# Run the API
cd src/MealPrep.API
dotnet run
```

The API will be available at:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`
- Swagger UI: `https://localhost:5001/swagger`

## API Endpoints

### Recipes
- `GET /api/recipes` - Get all recipes
- `GET /api/recipes/{id}` - Get recipe by ID
- `GET /api/recipes/category/{category}` - Get recipes by category
- `POST /api/recipes` - Create new recipe
- `PUT /api/recipes/{id}` - Update recipe
- `DELETE /api/recipes/{id}` - Delete recipe

### Meals
- `GET /api/meals` - Get all meals
- `GET /api/meals/{id}` - Get meal by ID
- `GET /api/meals/date-range?startDate={date}&endDate={date}` - Get meals by date range
- `POST /api/meals` - Create new meal
- `PUT /api/meals/{id}` - Update meal
- `DELETE /api/meals/{id}` - Delete meal

### AI Features
- `POST /api/ai/meal-plan` - Generate AI-powered meal plan
- `POST /api/ai/recipe-suggestions` - Get recipe suggestions
- `POST /api/ai/nutritional-analysis` - Analyze nutritional information

## Sample Data

The application seeds sample recipes on startup:
1. Grilled Chicken Salad (Easy, Salad)
2. Spaghetti Carbonara (Medium, Pasta)

## Technologies Used

- **Framework**: .NET 10.0
- **Web Framework**: ASP.NET Core
- **ORM**: Entity Framework Core
- **Database**: InMemory (for development)
- **AI Integration**: Azure.AI.OpenAI
- **API Documentation**: Swagger/OpenAPI
- **Architecture Pattern**: 3-Layer Architecture with Repository Pattern

## Project Structure

```
MealPrep-BE/
├── src/
│   ├── MealPrep.API/          # Presentation Layer
│   │   ├── Controllers/       # API Controllers
│   │   └── Program.cs         # Application entry point
│   ├── MealPrep.Business/     # Business Logic Layer
│   │   ├── Services/          # Service implementations
│   │   ├── Interfaces/        # Service contracts
│   │   └── Models/            # Request/Response models
│   ├── MealPrep.Data/         # Data Access Layer
│   │   ├── Repositories/      # Repository implementations
│   │   └── MealPrepDbContext.cs
│   └── MealPrep.Domain/       # Domain Layer
│       └── Entities/          # Domain entities
└── MealPrep.sln               # Solution file
```

## License

This project is licensed under the MIT License
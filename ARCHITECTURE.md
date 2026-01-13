# MealPrep 3-Layer Architecture

## Layer Diagram

```
┌──────────────────────────────────────────────────────────────┐
│                    Client Applications                        │
│            (Web Browser, Mobile App, Postman, etc.)          │
└────────────────────────┬─────────────────────────────────────┘
                         │ HTTP/HTTPS
                         ▼
┌──────────────────────────────────────────────────────────────┐
│              PRESENTATION LAYER (MealPrep.API)               │
│  ┌─────────────────────────────────────────────────────┐     │
│  │  Controllers:                                       │     │
│  │  - RecipesController (CRUD operations)             │     │
│  │  - MealsController (CRUD operations)               │     │
│  │  - AIController (AI-powered features)              │     │
│  └─────────────────────────────────────────────────────┘     │
│  ┌─────────────────────────────────────────────────────┐     │
│  │  Middleware & Configuration:                        │     │
│  │  - Swagger/OpenAPI                                  │     │
│  │  - CORS Policy                                      │     │
│  │  - Dependency Injection                             │     │
│  │  - JSON Serialization (with cycle handling)        │     │
│  └─────────────────────────────────────────────────────┘     │
└────────────────────────┬─────────────────────────────────────┘
                         │ Service Calls
                         ▼
┌──────────────────────────────────────────────────────────────┐
│           BUSINESS LOGIC LAYER (MealPrep.Business)           │
│  ┌─────────────────────────────────────────────────────┐     │
│  │  Services:                                          │     │
│  │  - RecipeService (Recipe business logic)           │     │
│  │  - MealService (Meal planning logic)               │     │
│  │  - AIService (AI integration via Azure OpenAI)     │     │
│  └─────────────────────────────────────────────────────┘     │
│  ┌─────────────────────────────────────────────────────┐     │
│  │  Interfaces:                                        │     │
│  │  - IRecipeService                                   │     │
│  │  - IMealService                                     │     │
│  │  - IAIService                                       │     │
│  └─────────────────────────────────────────────────────┘     │
│  ┌─────────────────────────────────────────────────────┐     │
│  │  Models:                                            │     │
│  │  - MealPlanRequest                                  │     │
│  │  - Other DTOs                                       │     │
│  └─────────────────────────────────────────────────────┘     │
└────────────────────────┬─────────────────────────────────────┘
                         │ Repository Calls
                         ▼
┌──────────────────────────────────────────────────────────────┐
│            DATA ACCESS LAYER (MealPrep.Data)                 │
│  ┌─────────────────────────────────────────────────────┐     │
│  │  DbContext:                                         │     │
│  │  - MealPrepDbContext (EF Core)                     │     │
│  └─────────────────────────────────────────────────────┘     │
│  ┌─────────────────────────────────────────────────────┐     │
│  │  Repositories:                                      │     │
│  │  - Repository<T> (Generic repository)              │     │
│  │  - RecipeRepository                                │     │
│  │  - MealRepository                                  │     │
│  └─────────────────────────────────────────────────────┘     │
└────────────────────────┬─────────────────────────────────────┘
                         │ Entity Framework Core
                         ▼
┌──────────────────────────────────────────────────────────────┐
│               DOMAIN LAYER (MealPrep.Domain)                 │
│  ┌─────────────────────────────────────────────────────┐     │
│  │  Entities:                                          │     │
│  │  - Recipe (with relationships)                      │     │
│  │  - Ingredient                                       │     │
│  │  - Meal                                            │     │
│  │  - UserPreference                                  │     │
│  └─────────────────────────────────────────────────────┘     │
└────────────────────────┬─────────────────────────────────────┘
                         │
                         ▼
┌──────────────────────────────────────────────────────────────┐
│              DATABASE (InMemory - Development)                │
│                    (Can be replaced with SQL)                │
└──────────────────────────────────────────────────────────────┘

External Integration:
┌──────────────────────────────────────────────────────────────┐
│                    Azure OpenAI / OpenAI API                  │
│  - GPT-4 model for meal plan generation                      │
│  - Recipe suggestions                                         │
│  - Nutritional analysis                                       │
└──────────────────────────────────────────────────────────────┘
```

## Data Flow

### 1. Recipe Retrieval Flow
```
Client → RecipesController → RecipeService → RecipeRepository → DbContext → Database
   ↓                                                                          ↑
Response ←─────────────────────────────────────────────────────────────────┘
```

### 2. AI Meal Plan Generation Flow
```
Client → AIController → AIService → Azure OpenAI API
   ↓                                      ↓
Response ←─────────── AI Generated Plan ──┘
```

### 3. Meal Creation Flow
```
Client → MealsController → MealService → MealRepository → DbContext → Database
   ↓                                                                    ↑
Response ←──────────────────────────────────────────────────────────┘
```

## Key Design Patterns

### 1. **Repository Pattern**
- Generic `Repository<T>` for common CRUD operations
- Specific repositories (RecipeRepository, MealRepository) for specialized queries
- Abstraction between business logic and data access

### 2. **Dependency Injection**
- All services and repositories registered in Program.cs
- Constructor injection throughout all layers
- Promotes testability and loose coupling

### 3. **Service Layer Pattern**
- Business logic encapsulated in service classes
- Controllers remain thin, focused on HTTP concerns
- Services implement interfaces for flexibility

### 4. **Entity Framework Core**
- Code-first approach with fluent API
- InMemory database for development
- Can be swapped with SQL Server, PostgreSQL, etc.

## Benefits of This Architecture

1. **Separation of Concerns**: Each layer has a specific responsibility
2. **Maintainability**: Changes in one layer don't affect others
3. **Testability**: Each layer can be tested independently
4. **Scalability**: Easy to add new features or replace implementations
5. **Flexibility**: Can switch databases or add new data sources easily
6. **AI Integration**: Modular AI service can be extended or replaced

## Security Considerations

- API keys stored in configuration (appsettings.json)
- CORS policy configured for cross-origin requests
- JSON serialization configured to handle cycles
- No vulnerabilities detected in security scan

## Future Enhancements

1. Add authentication and authorization (JWT, OAuth)
2. Replace InMemory database with persistent storage (SQL Server, PostgreSQL)
3. Add caching layer (Redis, MemoryCache)
4. Implement unit tests and integration tests
5. Add logging and monitoring (Serilog, Application Insights)
6. Implement API versioning
7. Add real-time features (SignalR)
8. Implement background jobs for meal planning (Hangfire, Quartz)

DuoCards - English Learning Application
Overview
DuoCards is an interactive web application designed to help users learn English using flashcards. Each card represents a word with its translation, and users can flip the card to reveal the meaning in English. The app supports animations, card flipping, and intuitive navigation for a fun and engaging learning experience.

Technologies Used

Frontend:
React with TypeScript
React Bootstrap for UI components
Custom CSS for animations
JavaScript animations and transitions for card interactions

Backend:
C# with ASP.NET Core
Entity Framework Core
Identity
JWT (JSON Web Token)
xUnit + Moq + FluentAssertions
Hangfire (for background job scheduling)
Unsplash API (word image fetching)
Google Translate API (unofficial, word translation)

Tools:
GitHub for version control
Sourcetree for Git management
Postman for API testing
Visual Studio for backend development
Visual Studio Code for frontend development

Features

User Authentication:
Login and Registration functionality.
Forms with validation and error handling.
Toggle between login and signup forms dynamically.

Card Flip Animation:
Flip cards with smooth animations using React's state management.
Add shaking effect on flipped cards for a better user experience.

Background Job Scheduling:
Periodic background job checks note statuses using Hangfire.
Handles errors, including authorization issues, during API requests.

Progressive Learning:
Cards stack on top of each other.
Learn one card at a time, with the ability to move to the next after each review.

Responsive Design:
Mobile-friendly UI.
The layout adjusts dynamically based on screen size, ensuring a seamless experience across devices.

## ⚙️ Getting Started

### 1. Clone the repo
git clone https://github.com/kazanzhi/DuoCards.git
cd your-repo

### 2. Configuration
Create a appsettings.Development.json file in the api/ folder:
{
  "JWT": {
    "Secret": "your-jwt-secret-key",
    "ValidIssuer": "your-app",
    "ValidAudience": "your-client"
  },
  "Unsplash": {
    "ApiKey": "Client-ID your-unsplash-api-key"
  },
  "ConnectionStrings": {
    "DefaultConnection": "your-db-connection-string"
  }
}

### 3. Apply migrations
cd api
dotnet ef database update

### 4. Run the app
dotnet run

### Features
### Cards (CardController)
GET /api/card — Get all user cards (User only)
GET /api/card/{id} — Get a specific card by ID (User only)
POST /api/card — Create a new card (User only)
PUT /api/card/{id} — Update a card (User only)
DELETE /api/card/{id} — Delete a card (User only)
POST /api/cardmanagement/correct/{cardId} — Mark card as answered correctly (User only)
POST /api/cardmanagement/incorrect/{cardId} — Mark card as answered incorrectly (User only)

### Translation (TranslationController)
GET /translation/{engWord} — Translate English word to Russian using Google Translate (User only)

### Card Image (CardImageController)
Card Image (CardImageController)
GET /cardimage/{word} — Fetch image for a word using Unsplash API (User only)

### Authentication (AuthController)
POST /api/account/register — Register a new user
POST /api/account/login — Login and receive a JWT token

### Running Tests
Unit Tests cd MyLibraryApp.Tests dotnet test

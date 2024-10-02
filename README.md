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
Hangfire (for background job scheduling)
REST API
SQL Database
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

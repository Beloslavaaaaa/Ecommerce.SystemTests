Ecommerce System Tests Framework
Target App: Automation Exercise
Framework: Selenium WebDriver with C# & NUnit

Architecture Overview
This project follows a decoupled Infrastructure vs. Tests architecture to ensure maintainability and stability.
- Infrastructure Project: Contains Page Object Models (POMs), Data Transfer Objects (DTOs), and Factories.
- Tests Project: Contains NUnit test fixtures, BaseTest setup, and environment configuration.

Setup Instructions
 1. Prerequisites
Visual Studio 2022 (or later)
.NET 6.0 / 8.0 SDK
Google Chrome installed
 2. Environment Variables (Requirement 12)
To keep credentials secure and satisfy project requirements, set the following environment variables on your machine:
SAUCE_USER - Your registration email
SAUCE_PASS - 	Your account password
*Note: Restart Visual Studio after setting these so the UserFactory can detect them.
 3. Running Tests
Open the Ecommerce.SystemTests.sln file.
Build the Solution (Ctrl + Shift + B).
Open Test Explorer (Test > Test Explorer).
Click Run All.

Project Highlights
POM Pattern: All page interactions are encapsulated in the Pages folder.
DTO Comparison: Tests compare full objects (e.g., ProductDto) instead of individual strings.
Wait Strategy: Uses WebDriverWait and ExpectedConditions to handle AJAX and page loads.
Data-Driven: Search tests use [TestCase] to validate multiple search terms in a single method.
# QA Hybrid Test Automation Framework (UI + API)
Test automation framework built with **C# (.NET)**, **Selenium WebDriver**, **RestSharp**, and **NUnit**. It features automated HTML reporting with embedded Base64 failure screenshots via **ExtentReports**.

## Tech Stack & Libraries

* **Language:** C#
* **UI Automation:** Selenium WebDriver
* **API Testing:** RestSharp
* **Test Runner & Assertions:** NUnit
* **Reporting:** ExtentReports (.NET Spark Reporter)
* **Design Patterns:** Page Object Model (POM), API Wrapper, Singleton (ExtentManager), Data-Driven Testing (DDT)

  ## Key Architecture & Features

### 1. UI Automation (Page Object Model & Smart Synchronization)
* **Page Object Model (POM):** Clean separation between UI locators, business actions, and test assertions.
* **Dynamic Locator Resolution:** Elements resolved via expression-bodied properties (`=>`) to prevent `StaleElementReferenceException`.
* **Explicit Waits (`WaitHelper`):** Dynamic condition polling using `WebDriverWait` for element visibility and clickability, eliminating arbitrary `Thread.Sleep` calls.
* **Hybrid Login Bypass (Session Injection):** Direct cookie manipulation (`session-username`) to bypass slow UI login workflows.
* **Data Sanitization & Formatting:** Regional-agnostic numerical parsing (`CultureInfo.InvariantCulture`, `decimal.TryParse`) combined with collection sorting assertions (`Is.Ordered.Ascending`).

### 2. REST API Testing Layer
* **API Client Wrapper:** Encapsulated HTTP request logic with strict timeout configurations (fail-fast principle).
* **Full CRUD Coverage:** Automated verification of `GET`, `POST`, `PUT`, `DELETE` methods, plus negative handling for non-existent resources (`404 Not Found`).
* **Strongly-Typed Models (DTO):** Automatic generic JSON deserialization into C# models (`RestResponse<T>`).

### 3. Reporting & Test Lifecycle Management
* **Centralized Teardown Hook:** Automatic execution status tracking and failure diagnostics configured inside `TestBase`.
* **Base64 Embedded Screenshots:** Zero external dependency HTML reports containing visual captures directly inside the DOM upon failure.
* **Dual Screenshot Artifacts:** Automated fallback writing `.png` files directly to local storage (`C:\Tests`) on failure.
* **CI/CD Ready Filtering:** Test categorization via NUnit `[Category]` attributes (`Smoke`, `Regression`, `Negative`, `Hybrid`, `API`, `UI`).

<img width="1912" height="776" alt="image" src="https://github.com/user-attachments/assets/388d2f5f-3d00-4d5e-a21f-d4e05db9661e" />

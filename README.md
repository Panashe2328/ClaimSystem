Claim Management System
Overview
The Claim Management System is an ASP.NET Core MVC web application that allows lecturers to submit claims for hours worked and associated tasks. The system includes administrative functionalities for Programme Coordinators and Academic Managers to approve or reject claims. It also allows users to upload supporting documents for their claims, with file upload validation.

Features
Lecturer Claim Submission: Lecturers can submit claims including hours worked, hourly rate, additional notes, and supporting documents (.pdf, .docx, .xlsx).
Admin Claim Approval: Programme Coordinators and Academic Managers can view, approve, or reject claims.
File Upload: Lecturers can upload supporting documents, which are saved in the wwwroot/Documents folder.
Claim Status Tracking: Both lecturers and administrators can track the status of each claim (Pending, Approved, Rejected).
Role-based Access: The system differentiates between lecturers and admin users, showing appropriate pages based on the user's role.
Technologies Used
ASP.NET Core MVC
C#
Razor Pages
HTML5, CSS3, Bootstrap
xUnit (for unit testing)
Moq (for mock testing)
In-Memory Storage (to manage data within the application without using a database)
Setup and Installation
Prerequisites
.NET Core SDK (version 6.0 or higher)
Visual Studio 2022 or later
Steps to Run the Application
Clone the repository:

bash
Copy code
git clone https://github.com/yourusername/ClaimManagementSystem.git
Navigate to the project directory:

bash
Copy code
cd ClaimManagementSystem
Open the project in Visual Studio.

Build the solution to restore the necessary NuGet packages.

Run the application using F5 or by selecting Debug > Start Debugging in Visual Studio.

The application will be hosted locally, and you can access it by navigating to https://localhost:5001 in your web browser.

Running Unit Tests
To run unit tests:

Open Test Explorer in Visual Studio (Test > Windows > Test Explorer).
Click Run All to execute all unit tests.
Project Structure
Controllers: Contains the logic to handle HTTP requests for lecturers and admins (e.g., ClaimController).
Models: Holds the core business models like Claim and ClaimRepository which manage claims data.
Views: Razor views for displaying pages for lecturers and admins.
wwwroot/Documents: Directory where uploaded claim documents are stored.
Tests: Unit tests using xUnit to validate the behavior of the ClaimRepository and ClaimController.
Usage
Lecturer
Submit Claim: Lecturers can fill out a form with hours worked, hourly rate, notes, and upload documents.
View Claim Status: Lecturers can check the status of their claims (Pending, Approved, or Rejected).
Admin (Programme Coordinator/Academic Manager)
View Claims: Admins can view submitted claims with all details.
Approve/Reject Claims: Admins can approve or reject claims with the click of a button.
View Uploaded Documents: Admins can click a button to view uploaded supporting documents.
Future Enhancements
Integration with a database (e.g., SQL Server) for persistent storage.
Enhanced role-based authentication and authorization using ASP.NET Identity.
Email notifications for claim approvals and rejections.
Add pagination and sorting for the admin claim view.

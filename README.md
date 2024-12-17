# FinanceApp

FinanceApp is a web application designed to help users track income and expenses. It allows users to manage multiple accounts, track transactions, and view insightful charts that visualize financial data.

## Features

- **Add Accounts**: Create and manage multiple financial accounts.
- **Make Transactions**: Record income and expense transactions with categories.
- **Monthly Overview**: Visualize income vs. expenses with a monthly column chart.
- **Expense Breakdown**: View a pie chart that shows expense distribution across categories.
- **Transaction History**: Access a complete list of all past transactions.
- **Manage Accounts**: View and manage all financial accounts in one place.

## Tech Stack

- **Frontend**: Angular, TailwindCSS
- **Backend**: .NET Core
- **Database**: MySQL
- **Charts**: ApexCharts (for data visualization)
- **Authentication**: JWT (or any preferred method)

## Prerequisites

Make sure the following dependencies are installed on your system:

- [Node.js](https://nodejs.org/)
- [Angular CLI](https://angular.io/cli)
- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [MySQL](https://www.mysql.com/downloads/)

## Installation

### Frontend Setup

1. Navigate to the `FinanceApp.Client` folder:
   ```bash
   cd FinanceApp/FinanceApp.Client
   ```
2. Install the dependencies and start the Angular development server:
   ```bash
   npm install
   ng serve -o
   ```
3. Open the app in your browser at http://localhost:4200.

### Backend Setup

1. Navigate to the FinanceApp.Server folder.

2. Open the solution file (FinanceApp.Server.sln) in Visual Studio.

3. Update the appsettings.json file with your MySQL connection string and AES encryption key. You can find this file at FinanceApp.Server/FinanceApp.API/appsettings.json.

   Example appsettings.json configuration:

   ```
   {
   "Logging": {
       "LogLevel": {
       "Default": "Information",
       "Microsoft.AspNetCore": "Warning"
       }
   },
   "AllowedHosts": "*",
   "ConnectionStrings": {
       "DefaultConnectionString": "server=localhost;user=root;database=<database_name>;password=<root_password>;"
   },
   "AES": {
       "Key": "<YOUR_KEY>",
       "IV": "<YOUR_INITIAL_VECTOR>"
   }
   }

   ```

4. Run the application using Visual Studio. Make sure your MySQL server is running, and the database configuration is correctly set up.


## Snapshots
#### Dashboard
![image](https://github.com/user-attachments/assets/70cf00dc-667b-455f-83b6-99aeb9a49016)
#### Add Expanses or Incomes
![image](https://github.com/user-attachments/assets/e92d3669-1c90-4dd1-8a88-f86d96513804)
#### All Transactions
![image](https://github.com/user-attachments/assets/944ef5f1-967d-480e-873c-30cd76ce608f)
#### Add new Account to manage
![image](https://github.com/user-attachments/assets/271963cf-b0cd-4438-ba46-ea6319f3ad54)

## License
This project is licensed under the MIT License. See the [LICENSE](https://github.com/iambhavik99/FinanceApp/tree/master?tab=MIT-1-ov-file) file for details.

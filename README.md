# TradeScopeApp

**TradeScopeApp** is a WPF application that monitors a directory for trade data files (CSV, XML, and TXT), parses them using a plugin-based loader architecture, and displays the data in a user-friendly interface. Built with the MVVM design pattern and dependency injection, the app is both maintainable and testable.

## Features

- **Real-Time Monitoring:** Automatically detects new trade data files in a specified directory.
- **Multi-Format Support:** Parses trade data from CSV, XML, and TXT files.
- **Extensible Loader Architecture:** Easily add new file formats through plugins.
- **Data Visualization:** Displays trade data (Date, Open, High, Low, Close, Volume) in a responsive DataGrid.
- **Logging:** Uses Serilog for comprehensive logging of application events and errors.
- **Robust Testing:** Unit tests implemented with xUnit, Moq, and FluentAssertions.

## Requirements

- **OS:** Windows 7 or later
- **.NET:** .NET 8.0 or later
- **IDE:** Visual Studio 2022 (or newer)


## Setup & Run

1. **Clone the Repository:**

   ```bash
   git clone https://github.com/yourusername/vusalatashov-tradescope.git
   cd vusalatashov-tradescope
Restore Dependencies:
dotnet restore
Build the Solution:
dotnet build
Run the Application:
dotnet run --project TradeScopeApp.GUI
Testing

To run the unit tests, execute:

dotnet test
Logging

Serilog is configured to log events to both the console and a rolling file in the logs directory.

Contributing

Contributions, bug reports, and feature requests are welcome! Please open an issue on GitHub for any feedback.

License

This project is licensed under the MIT License. See the LICENSE file for details.

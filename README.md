TradeScopeApp

TradeScopeApp is a WPF application that monitors a directory for trade data files (CSV, XML, and TXT), parses them using a plugin-based loader architecture, and displays the data in a user-friendly interface. Built with the MVVM design pattern and dependency injection, the app is both maintainable and testable.

Features

Real-Time Monitoring: Automatically detects new trade data files in a specified directory.
Multi-Format Support: Parses trade data from CSV, XML, and TXT files.
Extensible Loader Architecture: Easily add new file formats through plugins.
Data Visualization: Displays trade data (Date, Open, High, Low, Close, Volume) in a responsive DataGrid.
Logging: Uses Serilog for comprehensive logging of application events and errors.
Robust Testing: Unit tests implemented with xUnit, Moq, and FluentAssertions.
Requirements

OS: Windows 7 or later
.NET: .NET 8.0 or later
IDE: Visual Studio 2022 (or newer)
Project Structure

vusalatashov-tradescope/
├── README.md
├── TradeScopeApp.sln
├── Plugins/
│   └── Plugins.csproj
├── TradeScopeApp.Core/
│   ├── TradeScopeApp.Core.csproj
│   ├── appsettings.json
│   ├── Configuration/
│   │   ├── AppSettings.cs
│   │   └── ConfigurationService.cs
│   ├── InputFiles/
│   │   ├── sample_data.csv
│   │   ├── sample_data.txt
│   │   └── sample_data.xml
│   ├── Loading/
│   │   ├── IFileLoader.cs
│   │   ├── LoaderFactory.cs
│   │   └── PluginLoader.cs
│   ├── Models/
│   │   └── TradeData.cs
│   ├── Monitoring/
│   │   └── DirectoryMonitor.cs
│   └── Services/
│       └── TradeDataService.cs
├── TradeScopeApp.GUI/
│   ├── App.xaml
│   ├── App.xaml.cs
│   ├── DependencyInjectionConfig.cs
│   ├── TradeScopeApp.GUI.csproj
│   ├── appsettings.json
│   ├── UserControls/
│   │   ├── TradeDataGrid.xaml
│   │   └── TradeDataGrid.xaml.cs
│   ├── ViewModels/
│   │   └── MainViewModel.cs
│   └── Views/
│       ├── MainWindow.xaml
│       └── MainWindow.xaml.cs
├── TradeScopeApp.Loaders/
│   ├── TradeScopeApp.Loaders.csproj
│   ├── CSV/
│   │   └── CsvFileLoader.cs
│   ├── TXT/
│   │   └── TxtFileLoader.cs
│   └── XML/
│       └── XmlFileLoader.cs
└── TradeScopeApp.Tests/
    ├── TradeScopeApp.Tests.csproj
    ├── LoaderTests/
    │   ├── CsvFileLoaderTests.cs
    │   ├── TxtFileLoaderTests.cs
    │   └── XmlFileLoaderTests.cs
    └── Services/
        └── TradeDataServiceTests.cs
Setup & Run

Clone the Repository:
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
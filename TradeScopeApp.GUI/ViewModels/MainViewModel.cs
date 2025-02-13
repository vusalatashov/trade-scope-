using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;
using TradeScopeApp.Core.Models;
using TradeScopeApp.Core.Services;


namespace TradeScopeApp.GUI.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly TradeDataService _tradeDataService;
        public ObservableCollection<TradeData> TradeDataCollection { get; } = new ObservableCollection<TradeData>();

        public MainViewModel(TradeDataService tradeDataService)
        {
            _tradeDataService = tradeDataService;
            _tradeDataService.TradeDataAdded += OnTradeDataAdded;
        }

        private void OnTradeDataAdded(object sender, IEnumerable<TradeData> data)
        {
            if (Application.Current.Dispatcher.CheckAccess())
            {
                foreach (var trade in data)
                {
                    TradeDataCollection.Add(trade);
                }
            }
            else
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    foreach (var trade in data)
                    {
                        TradeDataCollection.Add(trade);
                    }
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

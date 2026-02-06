using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP2P1.Models.EntityFramework;
using TP2P2.Service;
using TP2P2.Views;

namespace TP2P2.ViewsModels
{
    public class ModelPageSerie : ObservableObject
    {
        public Action? ActionOuvrirDialog { get; set; }
        public Action? ActionOuvrirRechModDialog { get; set; }
        
        public IRelayCommand BtnAjout { get; }
        public IRelayCommand BtnModDelRech { get; }
        
        public ModelPageSerie()
        {
			Service = new WSService();
            GetDataOnLoadAsync();
            BtnAjout = new RelayCommand(() => ActionOuvrirDialog?.Invoke());
            BtnModDelRech = new RelayCommand(() => ActionOuvrirRechModDialog?.Invoke());
        }
        
        public async void GetDataOnLoadAsync()
        {
            var resultat = await service.GetSeriesAsync("series");
            Series = new ObservableCollection<Serie>(resultat);
        }

        private ObservableCollection<Serie> series;

		public ObservableCollection<Serie> Series
        {
			get { return series; }
			set { SetProperty(ref series, value);}
		}

		private IWSService service;

		public IWSService Service
        {
			get { return service; }
			set { service = value; }
		}


	}
}

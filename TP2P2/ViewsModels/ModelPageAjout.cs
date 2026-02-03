using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP2P1.Models.EntityFramework;
using TP2P2.Service;

namespace TP2P2.ViewsModels
{
    public class ModelPageAjout:ObservableObject
    {
       
        public IRelayCommand CommandeValide { get; }
        public ModelPageAjout()
        {
            Service = new WSService();
            Serie = new Serie();
            CommandeValide = new RelayCommand(ActionValider);
        }
        public async void ActionValider()
        {
            var resultat = await service.PostSeriesAsync(Serie);
            Serie = new Serie();
        }
        private Serie serie;

		public Serie Serie
		{
			get { return serie; }
			set { SetProperty(ref serie, value); }
        
		}
        private WSService service;

        public WSService Service
        {
            get { return service; }
            set { service = value; }
        }

    }
}

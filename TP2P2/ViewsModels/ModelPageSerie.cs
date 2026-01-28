using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP2P1.Models.EntityFramework;
using TP2P2.Service;

namespace TP2P2.ViewsModels
{
    public class ModelPageSerie
    {

        public ModelPageSerie()
        {
			Service = new WSService();
            Series = new ObservableCollection<Serie> (Service.GetSeriesAsync("serie").Result);
        }

        private ObservableCollection<Serie> series;

		public ObservableCollection<Serie> Series
        {
			get { return series; }
			set { series = value; }
		}

		private WSService service;

		public WSService Service
        {
			get { return service; }
			set { service = value; }
		}


	}
}

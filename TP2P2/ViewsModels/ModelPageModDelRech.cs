using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    public class ModelPageModDelRech : ObservableObject
    {
        public IRelayCommand CommandeRecherche { get; }
        public IRelayCommand CommandeModifier { get; }

        public IRelayCommand CommandeSupprimer { get; }
        
        public ModelPageModDelRech()
        {
            Service = new WSService();
            Serie = new Serie();
            GetDataOnLoadAsync();
            CommandeRecherche = new RelayCommand(ActionRecherche);
            CommandeModifier = new RelayCommand(ActionModification);
            CommandeSupprimer = new RelayCommand(ActionSuppression);
        }

        public async void ActionSuppression()
        {
            if (Serie != null && Serie.Serieid == IdSerieRecherche)
            {
                var result = await Service.DeleteSeriesAsync(Serie.Serieid);

                if (result) // result est directement un booléen ici
                {
                    GetDataOnLoadAsync();
                    Serie = new Serie();
                    IdSerieRecherche = 0;
                }
                else
                {
                    Serie.Titre = "Erreur de suppression";
                }
            }
            else { Serie.Titre = "Erreur"; }
        }
        public void ActionRecherche()
        {
            var resultat = Series.FirstOrDefault(s => s.Serieid == IdSerieRecherche);
            if (resultat != null)
            {
                Serie = resultat;
            }
            else
            {
                Serie = new Serie();
            }
        }

        public void ActionModification()
        {
            if (Serie != null && Serie.Serieid == IdSerieRecherche)
            {
                var resultat = service.PutSeriesAsync(Serie);
                if (resultat.Result)
                {
                    GetDataOnLoadAsync();
                    Serie = new Serie();
                    IdSerieRecherche = 0;
                }
                else                 {
                    Serie.Titre = "Erreur de modification";

                }

            }
            else { Serie.Titre ="Erreur"; }
            
        }

        private int idSerieRecherche;

		public int IdSerieRecherche
        {
			get { return idSerieRecherche; }
			set { idSerieRecherche = value; }
		}

        private Serie serie;
        public Serie Serie
        {
            get { return serie; }
            set { SetProperty(ref serie, value); }

        }
        private IWSService service;

        public IWSService Service
        {
            get { return service; }
            set { service = value; }
        }

        private List<Serie> series;
        public List<Serie> Series
        {
            get { return series; }
            set { SetProperty(ref series, value); }

        }
        public async void GetDataOnLoadAsync()
        {
            var resultat = await service.GetSeriesAsync("series");
            Series = new List<Serie>(resultat);
        }

    }
}

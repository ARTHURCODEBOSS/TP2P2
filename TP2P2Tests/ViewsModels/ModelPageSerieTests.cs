using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP2P1.Models.EntityFramework;
using TP2P2.Service;
using TP2P2.ViewsModels;

namespace TP2P2.ViewsModels.Tests
{
    [TestClass]
    public class TestViewModels
    {
        // --------------------------------------------------------
        // TEST 1 : PageAjout (Vérifier qu'on peut ajouter)
        // --------------------------------------------------------
        [TestMethod]
        public void Test_AjoutSerie_Fonctionne()
        {
            // 1. Préparation (Arrange)
            var fauxService = new WSService();
            var viewModel = new ModelPageAjout(fauxService); // On injecte le faux

            viewModel.Serie.Titre = "Nouvelle Série Test";
            viewModel.Serie.Nbsaisons = 2;

            // 2. Action (Act)
            viewModel.ActionValider();

            // Petit délai car ActionValider est 'async void'
            Task.Delay(50).Wait();

            // 3. Vérification (Assert)
            // On vérifie directement dans la liste mémoire du faux service
            Assert.AreEqual(1, fauxService.SeriesEnMemoire.Count, "La série aurait dû être ajoutée à la liste");
            Assert.AreEqual("Nouvelle Série Test", fauxService.SeriesEnMemoire[0].Titre);

            // On vérifie que le formulaire s'est vidé
            Assert.IsNull(viewModel.Serie.Titre, "Le formulaire doit être vidé après ajout");
        }

        // --------------------------------------------------------
        // TEST 2 : PageSeries (Vérifier l'affichage de la liste)
        // --------------------------------------------------------
        [TestMethod]
        public void Test_ListeSeries_ChargeCorrectement()
        {
            // 1. Préparation
            var fauxService = new FakeWSService();
            // On remplit le faux service avec des données bidon
            fauxService.SeriesEnMemoire.Add(new Serie { Serieid = 1, Titre = "Série A" });
            fauxService.SeriesEnMemoire.Add(new Serie { Serieid = 2, Titre = "Série B" });

            // 2. Action
            // Le constructeur lance GetDataOnLoadAsync
            var viewModel = new ModelPageSerie(fauxService);
            Task.Delay(50).Wait(); // Attente chargement

            // 3. Vérification
            Assert.IsNotNull(viewModel.Series);
            Assert.AreEqual(2, viewModel.Series.Count, "Il doit y avoir 2 séries dans la liste affichée");
            Assert.AreEqual("Série A", viewModel.Series[0].Titre);
        }

        // --------------------------------------------------------
        // TEST 3 : PageModDelRech (Recherche)
        // --------------------------------------------------------
        [TestMethod]
        public void Test_Recherche_TrouveElement()
        {
            // 1. Préparation
            var fauxService = new FakeWSService();
            fauxService.SeriesEnMemoire.Add(new Serie { Serieid = 10, Titre = "Breaking Bad" });

            var viewModel = new ModelPageModDelRech(fauxService);
            Task.Delay(50).Wait(); // Attente chargement initial

            // 2. Action
            viewModel.IdSerieRecherche = 10;
            viewModel.ActionRecherche();

            // 3. Vérification
            Assert.AreEqual("Breaking Bad", viewModel.Serie.Titre, "Le titre affiché devrait être celui de l'ID 10");
        }

        // --------------------------------------------------------
        // TEST 4 : PageModDelRech (Suppression)
        // --------------------------------------------------------
        [TestMethod]
        public void Test_Suppression_EffaceElement()
        {
            // 1. Préparation
            var fauxService = new FakeWSService();
            fauxService.SeriesEnMemoire.Add(new Serie { Serieid = 5, Titre = "A Supprimer" });

            var viewModel = new ModelPageModDelRech(fauxService);
            Task.Delay(50).Wait();

            // On sélectionne d'abord la série
            viewModel.IdSerieRecherche = 5;
            viewModel.ActionRecherche();

            // 2. Action
            viewModel.ActionSuppression();
            Task.Delay(50).Wait(); // Attente suppression async

            // 3. Vérification
            Assert.AreEqual(0, fauxService.SeriesEnMemoire.Count, "La liste devrait être vide");
            Assert.AreEqual(0, viewModel.IdSerieRecherche, "L'ID recherche devrait être remis à 0");
        }
    }
}
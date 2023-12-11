using Exercici_PPTLS.Infraestructura;
using Exercici_PPTLS.Model;
using Exercici_PPTLS.Repositori;
using Exercici_PPTLS.Vistes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Exercici_PPTLS.Viewmodel
{
    public class CreaJugadorViewModel : ObservableBase
    {
        IRepositoriPartits repositoriPartits;
        string nomJugador;
        int posicio;
        Player jugdor;
        public ObservableCollection<Player> jugadors;

        public CreaJugadorViewModel()
        {
            repositoriPartits = Repo.ObreBd();
            jugadors = repositoriPartits.ObtenJugadors();

            #region Commands:

            CreaJugadorsCommand = new RelayCommand(
                    o => CreaJugadors(NomJugador),
                    o => PotCrearJugadors());

            AfegeixJugadorCommand = new RelayCommand(
                o => AfegeixJugadorNou(),
                o => PotAfegirJugador()
                );

            ManualCommand = new RelayCommand(
               o => ManualJugador(),
               o => PorObrirManual()
               );
           
            #endregion
        }

        private void ManualJugador()
        {
            ManualViewModel manualViewMOdel = new ManualViewModel();

            Window wndManual = new WndManual();

            wndManual.DataContext = manualViewMOdel;
            wndManual.ShowDialog();
        }

        private bool PorObrirManual()
        {
            return true;
        }

        private void CreaJugadors(string nom)
        {
            repositoriPartits.CreaJugador(nom);
            Jugadors = repositoriPartits.ObtenJugadors();
        }

        private bool PotCrearJugadors()
        {
            return jugadors.Count != 0;
        }

        private bool PotAfegirJugador()
        {
            return EsValid && true;
        }

        private void AfegeixJugadorNou()
        {
            Player jugadorNou = new Player()
            {
                Id = Guid.NewGuid().ToString(),
                Nom = NomJugador,
                Puntuacio = 0,
                RondesGuanyades = 0,
                RondesPerdudes = 0
            };
            repositoriPartits.AfegeixJugador(jugadorNou);
            Jugadors = repositoriPartits.ObtenJugadors();
        }

        public ICommand CreaJugadorsCommand { get; set; }
        public ICommand AfegeixJugadorCommand { get; set; }
        public ICommand ManualCommand { get; set; }


        public ObservableCollection<Player> Jugadors
        {
            get => jugadors;
            set => SetProperty(ref jugadors, value);
        }

        public String NomJugador
        {
            get => nomJugador;
            set
            {
                nomJugador = value;
                NotifyPropertyChanged(nameof(NomJugador));
            }
        }

        public bool EsValid
        {
            get
            {
                bool esValid = false;
                if (NomJugador != null)
                {
                    esValid = true;
                }
                return esValid;
            }
        }

        public int Posicio
        {
            get => posicio;
            set
            {
                SetProperty(ref posicio, value);
            }
        }
    }
}

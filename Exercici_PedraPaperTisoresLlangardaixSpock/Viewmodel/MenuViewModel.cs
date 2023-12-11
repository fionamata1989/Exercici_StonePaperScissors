using Exercici_PPTLS.Infraestructura;
using Exercici_PPTLS.Model;
using Exercici_PPTLS.Repositori;
using Exercici_PPTLS.Vistes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Input;

namespace Exercici_PPTLS.Viewmodel
{
    public class MenuViewModel : ObservableBase
    {
        IRepositoriPartits repositoriPartits;
        string nomJugador;
        int nRondes;
        bool esVersioExtesa = false;
        int posicio;
        int rondesdGuanyades, rondesPerdudes, puntuacio;
        Player jugadorBuscat;
        public ObservableCollection<Player> jugadors;

        public MenuViewModel()
        {
            repositoriPartits = Repo.ObreBd();
            jugadors = repositoriPartits.ObtenJugadors();

            #region Commands:

            CreaJugadorsCommand = new RelayCommand(
                    o => CreaJugadors(NomJugador),
                    o => PotCrearJugadors());

            AfegeixJugadorCommand = new RelayCommand(
                o => AfegeixJugadorNou(),
                o => PotAfegirJugador());

            JugaCommand = new RelayCommand(
                o => JugaPartit(),
                o => PotJugar());

            ManualCommand = new RelayCommand(
                o => ManualJugador(),
                o => PorObrirManual());
            

            #endregion

        }

        private bool PorObrirManual()
        {
            return true;
        }
        #region Propietats
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

        public int NRondes
        {
            get => nRondes;
            set
            {
                SetProperty(ref nRondes, value);
                NotifyPropertyChanged(nameof(NRondes));
            }
        }

        public int RondesGuanyades
        {
            get => RondesGuanyades;
            set
            {
                SetProperty(ref rondesdGuanyades, value);
                NotifyPropertyChanged(nameof(RondesGuanyades));
            }
        }

        public int RondesPerdudes
        {
            get => rondesPerdudes;
            set
            {
                SetProperty(ref rondesPerdudes, value);
                NotifyPropertyChanged(nameof(RondesPerdudes));
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

        public bool EsVersioExtesa
        {
            get => esVersioExtesa;
            set
            {
                SetProperty(ref esVersioExtesa, value);
                NotifyPropertyChanged(nameof(EsVersioExtesa));
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
        #endregion

        #region Commands: Propietats
        public ICommand CreaJugadorsCommand { get; set; }
        public ICommand AfegeixJugadorCommand { get; set; }
        public ICommand JugaCommand { get; set; }
        public ICommand ManualCommand { get; set; }

        #endregion

        #region Commands: Codi
        private void CreaJugadors(string nom)
        {
            jugadorBuscat = new();
            jugadorBuscat.Nom = nom;
            if (Jugadors.Any(jugador => jugador.Nom == jugadorBuscat.Nom))
            {
                jugadorBuscat.PartidesGuanyades = jugadors[Posicio].PartidesGuanyades;
                jugadorBuscat.RondesGuanyades = jugadors[Posicio].RondesPerdudes;
                jugadorBuscat.RondesPerdudes = jugadors[Posicio].RondesPerdudes;
                jugadorBuscat.Puntuacio = jugadors[Posicio].Puntuacio;
            }
            else
            {
                repositoriPartits.CreaJugador(nom);
            }
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

        private bool PotJugar()
        {
            return EsValid && true;
        }

        private void JugaPartit()
        {
            //Instanciem JocViewModel:
            JocViewModel jocViewModel = new JocViewModel(jugadorBuscat, NRondes, EsVersioExtesa);

            //Instanciem la finestra:
            Window wndJoc = new MainWindow();

            //Passem el ViewModel com a font de dades per la vista:
            wndJoc.DataContext = jocViewModel;

            //Mostrem la finestra:
            wndJoc.ShowDialog();
        }
        private void ManualJugador()
        {
            ManualViewModel manualViewMOdel = new ManualViewModel();

            Window wndManual = new Window();

            wndManual.DataContext = manualViewMOdel;
            wndManual.ShowDialog();
        }

        #endregion
    }
}



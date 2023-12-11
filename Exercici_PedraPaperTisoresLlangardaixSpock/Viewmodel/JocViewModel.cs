using Exercici_PPTLS.Infraestructura;
using Exercici_PPTLS.Model;
using Exercici_PPTLS.Repositori;
using Exercici_PPTLS.Vistes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Exercici_PPTLS.Viewmodel
{
    public class JocViewModel : ObservableBase
    {
        IRepositoriPartits repositoriPartits;
        public ObservableCollection<Player> jugadors;
        public ObservableCollection<Partit> partits;
        Player jugadorEnEdicio;
        Player jugador;
        Player bot;
        int posicio;
        int nRondes, puntuacio, partida, rondesdGuanyades, rondesPerdudes;
        bool esVersioExtesa;

        public JocViewModel(Player Jugador, int NRondes, bool EsVersioExtesa)
        {
            repositoriPartits = Repo.ObreBd();
            jugadors = repositoriPartits.ObtenJugadors();
            partits = repositoriPartits.ObtenPartits();

            #region Commands
            CreaPartidaCommand = new RelayCommand(
                o => CreaPartida(),
                o => PotCrearPartida());

            EditaJugadorCommand = new RelayCommand(
                o => EditaJugador(),
                o => PotEditarJugador());

            ActualitzaPuntuacioCommand = new RelayCommand(
                o => ActualitzaPuntuacio(),
                o => PotActualizarPuntuacio());


            #endregion
        }

        private bool PotActualizarPuntuacio()
        {
            return true;
        }

        private void ActualitzaPuntuacio()
        {
            
        }

        private bool PotCrearPartida()
        {
            //Podem crear partida si el jugador existeix a la bbdd. 
            return jugadors.Contains(jugador);
        }

        private void CreaPartida()
        {
            //Cridem bots:
            repositoriPartits.CreaBots();
            ObservableCollection<Player> bots = repositoriPartits.ObtenJugadors();
            Player bot = Bot(bots);
            //Associem jugador i bot:

            Partit nouPartit = new()
            {
                Jugador = jugador,
                Bot = bot,
                PuntuacioComputer = bot.Puntuacio,
                PuntuacioJugador = jugador.Puntuacio,
            };
        }

        private bool PotEditarJugador()
        {
            throw new NotImplementedException();
        }

        private void EditaJugador()
        {
            jugadorEnEdicio = new () { Id = Jugadors[Posicio].Id };
            Jugador.Nom = Jugadors[Posicio].Nom;
            Puntuacio = Jugadors[Posicio].Puntuacio;
            RondesGuanyades = Jugadors[Posicio].RondesGuanyades;
            RondesPerdudes = Jugadors[Posicio].RondesPerdudes;
        }

        #region propietats
        public ObservableCollection<Player> Jugadors
        {
            get => jugadors;
            set => SetProperty(ref jugadors, value);
        }
        public ObservableCollection<Partit> Partits
        {
            get => partits;
            set => SetProperty(ref partits, value);
        }

        public Player Jugador
        {
            get => jugador;
            set
            {
                SetProperty(ref jugador, value);
                NotifyPropertyChanged(nameof(Jugador));
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

        public int RondesGuanyades
        {
            get => RondesGuanyades;
            set
            {
                SetProperty(ref rondesdGuanyades, value);
                NotifyPropertyChanged(nameof (RondesGuanyades));    
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

        public int NRondes
        {
            get => nRondes;
            set
            {
                nRondes = value;
                NotifyPropertyChanged(nameof(NRondes));
            }
        }

        public int Puntuacio
        {
            get => puntuacio;
            set
            {
                puntuacio = value;
                NotifyPropertyChanged(nameof(Puntuacio));
            }
        }

        public int Partida
        {
            get => partida;
            set
            {
                partida = value;
                NotifyPropertyChanged(nameof(Partida));
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

        #region Commands

        #endregion
        public ICommand EditaJugadorCommand { get; set; }
        public ICommand CreaPartidaCommand { get; set; }
        public ICommand ActualitzaPuntuacioCommand { get; set; }
        public ICommand ActualitzaRondaCommand { get; set; }
        public ICommand MouCartaCommand { get; set; }

        #endregion

        private Player Bot(ObservableCollection<Player> llistaBots)
        {
            Player bot = new Player();
            int posicio = 0;
            Random random = new Random();

            posicio = random.Next(llistaBots.Count);

            return bot = llistaBots[posicio];
        }


        public static bool Juga(Ma maJugador, Ma maBot)
        {
            bool guanya = false;

            if (maJugador == maBot)
                guanya = false;

            switch (maJugador)
            {
                case Ma.Roca:
                    if (maBot == Ma.Tisores || maBot == Ma.Llangardaix)
                        guanya = true;
                    break;

                case Ma.Paper:
                    return (maBot == Ma.Roca || maBot == Ma.Spock) ? guanya : !guanya;
                case Ma.Tisores:
                    return (maBot == Ma.Paper || maBot == Ma.Llangardaix) ? guanya : !guanya;
                case Ma.Llangardaix:
                    return (maBot == Ma.Spock || maBot == Ma.Paper) ? guanya : !guanya;
                case Ma.Spock:
                    return (maBot == Ma.Tisores || maBot == Ma.Roca) ? guanya : !guanya;
                default:
                    return !guanya;
            }

            return guanya;
        }

    }
}

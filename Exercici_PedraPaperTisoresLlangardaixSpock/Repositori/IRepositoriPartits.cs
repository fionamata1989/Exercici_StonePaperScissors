using Exercici_PPTLS.Model;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Exercici_PPTLS.Repositori
{
    public interface IRepositoriPartits
    {
        bool AfegeixPartit(Partit partit);
        bool AfegeixJugador(Player jugador);
        void CreaPartit(string idJugador, string idComputer);
        void CreaJugador(string nom);
        void CreaBots();
        void DesaPartit(ObservableCollection<Partit> patides);
        void DesaJugador(ObservableCollection<Player> jugadors);
        bool EsborraPartit(string id);
        bool EsborraJugador(string id);
        bool ModificaPartit(Partit partit);
        bool ModificaJugador(Player juagdor);
        ObservableCollection<Partit> ObtenPartits();
        ObservableCollection<Player> ObtenJugadors();
    }
}

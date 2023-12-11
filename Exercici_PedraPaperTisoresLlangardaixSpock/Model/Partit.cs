using Exercici_PPTLS.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Exercici_PPTLS.Model
{
    public enum Ma
    {
        Roca,
        Paper,
        Tisores,
        Llangardaix,
        Spock
    }

    public class Partit
    {
        string id;
        Player jugador;
        Player bot;
        int puntuacioJugador;
        int puntuacioComputer;
        bool guanya;

        public string Id { get => id; set => id = value; }
        public Player Jugador { get => jugador; set => jugador = value; }
        public Player Bot { get => bot; set => bot = value; }
        public int PuntuacioJugador { get => puntuacioJugador; set => puntuacioJugador = value; }
        public int PuntuacioComputer { get => puntuacioComputer; set => puntuacioComputer = value; }
        public bool Guanya { get => guanya; set => guanya = value; }

        public override string ToString()
        {
            return $"Game: {Jugador} vs {Bot}";
        }
    }



}

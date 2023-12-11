using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercici_PPTLS.Model
{
    public class Player
    {
        string id;
        string nom;
        int puntuacio;
        int partidesGuanyades;
        int rondesGuanyades;
        int ronderPerdudes;
        private string foto;
        bool guanya;

        public string Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public int PartidesGuanyades { get => partidesGuanyades; set => partidesGuanyades = value; }
        public int RondesGuanyades { get => rondesGuanyades; set => rondesGuanyades = value; }
        public int RondesPerdudes { get => ronderPerdudes; set => ronderPerdudes = value; }
        public string Foto { get => foto; set => foto = value; }
        public int Puntuacio { get => puntuacio; set => puntuacio = value; }
        public bool Guanya { get => guanya; set => guanya = value; }

        public override string ToString()
        {
            return Foto + " " + Nom;
        }
    }
}

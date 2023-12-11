using Exercici_PPTLS.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Exercici_PPTLS.Repositori.JSON
{
    public class BDPartits : IRepositoriPartits
    {

        const string PARTITS = @"BBDD\Partits.xml";
        const string JUGADORS = @"BBDD\Jugadors.xml";

        string RUTA_PARTITS = Path.Combine(Environment.CurrentDirectory, PARTITS);
        string RUTA_JUGADORS = Path.Combine(Environment.CurrentDirectory, JUGADORS);

        public ObservableCollection<Player> ObtenJugadors()
        {
            ObservableCollection<Player> jugadors;

            using (TextReader fitxer = new StreamReader(RUTA_JUGADORS))
            {
                if (fitxer.Peek() != -1)
                {
                    XmlSerializer serialitzador = new XmlSerializer(typeof(ObservableCollection<Player>));
                    jugadors = (ObservableCollection<Player>)serialitzador.Deserialize(fitxer);
                }
                else
                {
                    jugadors = new ObservableCollection<Player>();
                }

            }
            return jugadors;
        }

        public ObservableCollection<Partit> ObtenPartits()  //deserealitza. Save serialitza
        {
            ObservableCollection<Partit> partits;
            using (TextReader fitxer = new StreamReader(RUTA_PARTITS))
            {
                //Peek() retorna l'objecte a l'inici de la cua, sense treure'l.
                if (fitxer.Peek() != -1)
                {
                    XmlSerializer serialitzador = new XmlSerializer(typeof(ObservableCollection<Partit>));
                    partits = (ObservableCollection<Partit>)serialitzador.Deserialize(fitxer);
                }
                else
                {
                    partits = new ObservableCollection<Partit>();
                }
                return partits;
            }
        }

        public void DesaPartit(ObservableCollection<Partit> partits)
        {
            using (TextWriter fitxer = new StreamWriter(RUTA_PARTITS))
            {
                XmlSerializer serialitzador = new XmlSerializer(typeof(ObservableCollection<Player>));
                serialitzador.Serialize(fitxer, partits);
            }
        }

        public void DesaJugador(ObservableCollection<Player> jugadors)
        {
            using (TextWriter fitxer = new StreamWriter(RUTA_JUGADORS))
            {
                XmlSerializer serialitzador = new XmlSerializer(typeof(ObservableCollection<Player>));
                serialitzador.Serialize(fitxer, jugadors);
            }
        }

        //Només s'haurà d'executar un cop. 
        public void CreaBots()
        {
            Random random = new Random();
            List<String> noms = new()
            {
                "BOTXevi",
                "BOTFerran",
                "BOTJordi",
                "BOTXeviT",
                "BOTArtur"
            };

            ObservableCollection<Player> llistaBots = new ObservableCollection<Player>();
            Player botActual = null;

            for (int nBot = 0; nBot < noms.Count; nBot++)
            {
                botActual = new Player()
                {
                    Id = Guid.NewGuid().ToString(),
                    Nom = noms[nBot],
                    Puntuacio = 0,
                    PartidesGuanyades = 0, 
                    RondesGuanyades = 0,
                    RondesPerdudes = 0,
                };
                botActual.Foto = $"../Imatges/{botActual.Nom}";
                llistaBots.Add(botActual);
            }
            DesaJugador(llistaBots);
        }

        public void CreaJugador(string nom)
        {
            ObservableCollection<Player> jugadors = new ObservableCollection<Player>();
            Player jugador = new Player()
            {
                Id = Guid.NewGuid().ToString(),
                Nom = nom,
                Puntuacio = 0,
                PartidesGuanyades = 0,
                RondesGuanyades = 0,
                RondesPerdudes = 0
            };
            jugador.Foto = $"https://loremflickr.com/320/240/portrait?lock={jugador.Id.GetHashCode() % 113}";
            jugadors.Add(jugador);
            DesaJugador(jugadors);
        }

        public void CreaPartit(string nomJugador, string nomBot)
        {
        //    ObservableCollection<Partit> partits = ObtenPartits();
        //    ObservableCollection<Player> jugadors = ObtenJugadors();
        //    Partit partitNou = new Partit();
        //    bool creat = false;
        }

        public bool AfegeixPartit(Partit partit)
        {
            bool afegit = false;
            ObservableCollection<Partit> partits = ObtenPartits();
            Partit partitModificable = partits.FirstOrDefault(partitActual => partitActual.Id == partit.Id);
            if (partitModificable != null)
            {
                partits.Add(partitModificable);
                afegit = true;
            }
            DesaPartit(partits);
            return afegit;
        }

        public bool AfegeixJugador(Player jugador)
        {
            bool afegit = false;
            ObservableCollection<Player> jugadors = ObtenJugadors();
            Player jugadorPerAfegir = jugadors.FirstOrDefault(jugadorActual => jugadorActual.Id == jugador.Id);
            if (jugadorPerAfegir != null)
            {
                jugadors.Add(jugadorPerAfegir);
                afegit = true;
            }
            DesaJugador(jugadors);
            return afegit;
        }

        public bool EsborraPartit(string id)
        {
            bool esborrat = false;
            ObservableCollection<Partit> partits = ObtenPartits();
            Partit partitAEliminar = partits.FirstOrDefault(partit => partit.Id == id);
            if (partitAEliminar != null)
            {
                partits.Remove(partitAEliminar);
                esborrat = true;
            }
            DesaPartit(partits);
            return esborrat;
        }

        public bool EsborraJugador(string id)
        { 
            bool esborrat = false;
            ObservableCollection<Player> jugadors = ObtenJugadors();
            Player jugadorAEliminar = jugadors.FirstOrDefault(jugador => jugador.Id == id);
            if(jugadorAEliminar != null)
            {
                jugadors.Remove(jugadorAEliminar);
                esborrat = true;
            }
            DesaJugador(jugadors);
            return esborrat; 
        }

        public bool ModificaJugador(Player juagdor)
        {
            throw new NotImplementedException();
        }

        public bool ModificaPartit(Partit partit)
        {
            throw new NotImplementedException();
        }
    }
}

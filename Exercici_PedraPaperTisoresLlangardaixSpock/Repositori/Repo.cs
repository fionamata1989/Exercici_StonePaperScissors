using Exercici_PPTLS.Repositori.JSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Exercici_PPTLS.Repositori
{
    public static class Repo
    {
        private static IRepositoriPartits? repositoriPartits = null;
        public static IRepositoriPartits ObreBd()
        {
            if (repositoriPartits == null)
            {
                repositoriPartits= new BDPartits();
            }
            return repositoriPartits;
        }
        
    }
}

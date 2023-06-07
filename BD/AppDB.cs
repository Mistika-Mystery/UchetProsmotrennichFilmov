using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UchetProsmotrennichFilmov.BD
{
    internal class AppDB
    {
        public static UchetFilmofEntities db = new UchetFilmofEntities();
        public static Users CurrentUser = new Users();
    }
}

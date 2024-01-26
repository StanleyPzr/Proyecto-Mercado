using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class QueryObject
    {
        public string? Symbol { get; set; } = null;
        public string? CompanyName { get; set; } = null;
        public string? SortBy { get; set; } = null; //Ordena los elemtnos de acuerdo al tipo de dato, puede ser Symbol, CompanyName, ETC.
        public bool IsDecsending { get; set; } = false; //Ordena de mayor a menor es de cir de Z-A SI ES TRUE y de A-Z SI ES FALSE
        public int NumPag {get; set; } = 1; //Numero de pagina, va al numero de pagina. Por ejeplo si el tamaño de la apgina fuera de 2, es decir, 2 elementos por pagina, si tienes 6 registros podras recibir 2 resultados por cada pagina 1 o 2 o 3 si busca la numpag 4 devolverá vacío.
        public int TamPag {get; set; } = 20; //Tamaño Pagina o cantidad de elementos que se despliegan por pagina.
    }
}
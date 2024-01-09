using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Wulkanizacja
{
    internal class StorageEntry
    {
        public int id { get; set; }
        public string nr_rejestracyjny { get; set; }
        public string marka_samochodu { get; set; }
        public string kola_opony {  get; set; }
        public string rozmiar {  get; set; }
        public string jakosc {  get; set; }
        public int nr_magazynu { get; set; }
        public string status { get; set; }
    }
}

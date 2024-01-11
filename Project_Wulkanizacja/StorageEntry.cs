using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Wulkanizacja
{
    public class StorageEntry
    {
        public int id { get; set; }
        public String nr_rejestracyjny { get; set; }
        public String marka_samochodu { get; set; }
        public String kola_opony {  get; set; }
        public String rozmiar {  get; set; }
        public String jakosc {  get; set; }
        public int nr_magazynu { get; set; }
        public String status { get; set; }
        public String notatki {  get; set; }
    }
}

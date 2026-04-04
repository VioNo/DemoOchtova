using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp227136
{
    internal static class Core
    {
        public static Entities Context = new Entities();
        public static users AuthUser = null;
    }
    public partial class products
    {
        public bool IsGreatThen10K => min_price > 10000;
        public string Materials => string.Join(", ", product_material.Select(pm => pm.materials.name_material));

    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp227136
{
    internal static class Core
    {
        public static Entities Context = new Entities();
        public static users AuthUser = null;
    }
    public partial class products
    {
        public bool IsGreatThen10K => (min_price ?? 0) > 10000;
        public string Materials => string.Join(", ", product_material.Select(pm => pm.materials.name_material));

        public string ImagePath{ 
            get
            {
                if(ImagePath != null)
                    return Path.GetFullPath(ImagePath);
                return null;
            }
            set 
            {
                try
                {
                    string fileName = Path.GetFileName(value);
                    string NewPath = Path.Combine(
                        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "product"), fileName);
                    File.Copy(value, NewPath, true);
                    picture = NewPath;//"/product/" + NewPath;
                }
                catch (Exception ex) { MessageBox.Show($"{ex}"); }
            }
        }

    }
    public partial class materials
    {
        public bool Min0 => kolvo_on_sklad <= 0;
    }
}

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp227136.Pages;

namespace WpfApp227136
{
    internal static class Core
    {
        public static user227136Entities Context = new user227136Entities();
        public static users AuthUser = null;
    }
    public partial class products
    {
        public bool IsGreatThen10K => (min_price ?? 0) > 10000;
        public string Materials => string.Join(", ", product_material.Select(pm => pm.materials.name_material));

        public string ImagePath{ 
            get
            {
                string path = $"{Environment.CurrentDirectory}{picture}";
                if (!string.IsNullOrWhiteSpace(picture) && File.Exists(path))
                    return path;
                return $"{Environment.CurrentDirectory}\\Resourses\\products\\picture.png";
            }
            set 
            {
                try
                {
                    string fileName = Path.GetFileName(value);
                    string NewPath = Path.Combine(
                        Path.Combine(Environment.CurrentDirectory, "Resourses\\products"), fileName);
                    if(!File.Exists(NewPath))
                        File.Copy(value, NewPath, true);
                    picture = @"\Resourses\products\" + fileName;
                }
                catch (Exception ex) { MessageBox.Show($"{ex}"); }
            }
        }


        //private void imageButton_Click(object sender, RoutedEventArgs e)
        //{
        //    string MainPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "products");

        //    OpenFileDialog NewImage = new OpenFileDialog();
        //    NewImage.InitialDirectory = MainPath;
        //    NewImage.Filter = "(*.png, *.jpg)|*.png;*.jpg";
        //    if (NewImage.ShowDialog() == true)
        //    {
        //        CurrentProduct.ImagePath = NewImage.FileName;
        //        pictureImage.GetBindingExpression(Image.SourceProperty).UpdateTarget();
        //        _messageHelper.ShowInfo("Картинка загружена успешно");
        //    }
        //}





    }
    public partial class materials
    {
        public bool Min0 => kolvo_on_sklad <= 0;
    }
}

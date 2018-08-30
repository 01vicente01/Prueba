using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Controls;
using Xceed.Wpf.AvalonDock.Themes;
using LibreriaDeClases;
using System.Collections;

namespace AcysaManager      //--------------**PRUEBAS**---------------
{
    /// <summary>
    /// Lógica de interacción para vent_albaran.xaml
    /// </summary>
    public partial class vent_albaran : Window, InterfazDeDatos
    {   


        //Variable publica para compartir informacion entre ventanas
        public ArrayList listaArticulos = new ArrayList();

        public void PasarLinea(Linea linea)
        {

        }

        public void PasarArticulo(Articulo articulo)
        {
            listaArticulos.Add(articulo);
        }

        public vent_albaran()
        {
            InitializeComponent();

            //--------------**PRUEBAS**---------------

            listaArticulos.Add(new Articulo("000001", "prueba", 100, 20, "PVC", "fontaneria", "vicenteitems", "vicente"));
            Albaranes albaran = new Albaranes((Articulo)listaArticulos[0], 2, 10, 0);

            albaran.AñadirLinea((Articulo)listaArticulos[0], 50, 2, 0);
            albaran.AñadirLinea((Articulo)listaArticulos[0], 45, 20, 20);

            DataGridAlbaran.Items.Add(albaran.albaran[0]);
            DataGridAlbaran.Items.Add(albaran.albaran[1]);
            DataGridAlbaran.Items.Add(albaran.albaran[2]);

        }

        private void Añadir_Linea_Click(object sender, RoutedEventArgs e)
        {
            vent_añadirLineaAlbaran vent_linea = new vent_añadirLineaAlbaran(listaArticulos);
            vent_linea.Owner = this;
            vent_linea.Show();
        }

        private void ButtonAnalisis_Click(object sender, RoutedEventArgs e)
        {
           if (DataGridAlbaran.ColumnFromDisplayIndex(6).Visibility == Visibility.Collapsed)
            {
                DataGridAlbaran.ColumnFromDisplayIndex(6).Visibility = Visibility.Visible;
                DataGridAlbaran.ColumnFromDisplayIndex(7).Visibility = Visibility.Visible;
                DataGridAlbaran.ColumnFromDisplayIndex(8).Visibility = Visibility.Visible;
                DataGridAlbaran.ColumnFromDisplayIndex(9).Visibility = Visibility.Visible;
                DataGridAlbaran.ColumnFromDisplayIndex(10).Visibility = Visibility.Visible;
                DataGridAlbaran.ColumnFromDisplayIndex(11).Visibility = Visibility.Visible;
            }
            else
            {
                DataGridAlbaran.ColumnFromDisplayIndex(6).Visibility = Visibility.Collapsed;
                DataGridAlbaran.ColumnFromDisplayIndex(7).Visibility = Visibility.Collapsed;
                DataGridAlbaran.ColumnFromDisplayIndex(8).Visibility = Visibility.Collapsed;
                DataGridAlbaran.ColumnFromDisplayIndex(9).Visibility = Visibility.Collapsed;
                DataGridAlbaran.ColumnFromDisplayIndex(10).Visibility = Visibility.Collapsed;
                DataGridAlbaran.ColumnFromDisplayIndex(11).Visibility = Visibility.Collapsed;
            }
        }
    }
}

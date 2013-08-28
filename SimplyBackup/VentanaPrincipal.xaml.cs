/*
 * 
 * Usuario: Fer.d.minguela@gmail.com
 * Fecha: 23/07/2013
 * Hora: 18:14
 * 
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Diagnostics;
using System.IO;

namespace SimplyBackup
{
	/// <summary>
	/// Interaction logic for VentanaPrincipal.xaml
	/// </summary>
	public partial class VentanaPrincipal : Window
	{
		public VentanaPrincipal()
		{
			InitializeComponent();
			
		}
		
		void button1_Click(object sender, RoutedEventArgs e)
		{
			string programa = Path.Combine(comun.DirectorioActual, @"notepad\notepadpp.exe");
			string fichero = Path.Combine(comun.DirectorioActual, "tareas.xml");
			Process.Start(programa, fichero);
		}
		
		void button2_Click(object sender, RoutedEventArgs e)
		{
			this.Visibility = Visibility.Hidden;
		}
		
		void button3_Click(object sender, RoutedEventArgs e)
		{
			App.CerrarPrograma();
		}
		
		void Fondo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.DragMove();
		}
        public void doThings()
        {
            comun.DoEvents(this.Dispatcher);
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            txtLog.Text = "";
            List<string> lista = new List<string>(log.fifo);

            foreach (string f in lista)
            {
                txtLog.Text += f + "\r\n";
                
            }
        }
	}
}
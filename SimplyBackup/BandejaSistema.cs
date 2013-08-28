using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace SimplyBackup
{
    public class BandejaSistema 
    {
        NotifyIcon iconoSistema = null;

        public BandejaSistema()
        {
            iconoSistema = new NotifyIcon();           
            iconoSistema.Text = comun.APP_NAME + " " + comun.APP_VERSION;
            iconoSistema.Icon = new Icon(@".\Images\security.ico");
            iconoSistema.Visible = true;
            iconoSistema.MouseClick += new MouseEventHandler(iconoSistema_MouseClick);
        }

        void iconoSistema_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
              /*  if (Comun.VentanaPrincipal.Visibility == System.Windows.Visibility.Visible)
                {
                    Comun.VentanaPrincipal.Visibility = System.Windows.Visibility.Hidden;
                }
                else Comun.VentanaPrincipal.Visibility = System.Windows.Visibility.Visible;
                */
               verVentanaPrincipal();
            }
            else
            {
                if (e.Button == MouseButtons.Right)
                {
                	verVentanaPrincipal();
                }
            }
        }
        
        private void verVentanaPrincipal(){
        	if(comun.VentanaPrincipal.IsVisible==true){
               	comun.VentanaPrincipal.Hide();
               	GC.Collect();
               }
               else comun.VentanaPrincipal.Show();
        }

       
    }
}

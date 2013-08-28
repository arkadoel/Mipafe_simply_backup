/*
 * 
 * Usuario: Fer.d.minguela@gmail.com
 * Fecha: 07/23/2013
 * Hora: 19:22
 * 
 * 
 */
using System;
using System.Threading;
using System.Linq;
using System.IO;

namespace SimplyBackup
{
	/// <summary>
	/// Description of reloj.
	/// </summary>
	public class reloj 
	{
		public static int intervalo = 40000; //40 segundos
				
		public static void ejecutarReloj(){
			int pasadas = 20;
			
			while(true){
                try
                {
                    log.write(".");
                    //comprobar si es hora de copia
                    string horaActual = DateTime.Now.Hour + ":" + DateTime.Now.Minute;
                    string diaSemana = DateTime.Today.DayOfWeek.ToString();
                    bool tocaEjecutar = false;

                    var listaTareas = from u in comun.Tareas
                                      where u.Hora == horaActual
                                      select u;

                    if (listaTareas.Count() > 0)
                    {
                        //hay tareas a ejecutar					
                        foreach (Tarea ta in listaTareas)
                        {
                            //revisar si toca ejecutar hoy
                            tocaEjecutar = false;

                            switch (diaSemana)
                            {
                                case "Monday":
                                    if (ta.lunes == true) tocaEjecutar = true;
                                    break;
                                case "Tuesday":
                                    if (ta.martes == true) tocaEjecutar = true;
                                    break;
                                case "Wednesday":
                                    if (ta.miercoles == true) tocaEjecutar = true;
                                    break;
                                case "Thursday":
                                    if (ta.jueves == true) tocaEjecutar = true;
                                    break;
                                case "Friday":
                                    if (ta.viernes == true) tocaEjecutar = true;
                                    break;
                                case "Saturday":
                                    if (ta.sabado == true) tocaEjecutar = true;
                                    break;
                                case "Sunday":
                                    if (ta.domingo == true) tocaEjecutar = true;
                                    break;
                            }

                            if (tocaEjecutar)
                            {
                                log.write("\r\nEjecutando " + ta.Nombre.ToString() + " hora " + horaActual + "");

                                if (Directory.Exists(ta.Destino) == false)
                                {
                                    Directory.CreateDirectory(ta.Destino);
                                }
                                //crear directorio copia
                                string nombre = DateTime.Today.Year.ToString() + "_" + DateTime.Today.Month.ToString() + "_" + DateTime.Today.Day.ToString();
                                nombre += "_" + DateTime.Now.Hour.ToString() + "" + DateTime.Now.Minute.ToString();
                                nombre += "_(" + ta.Nombre + ")";
                                nombre = ta.Destino + "\\" + nombre;


                                comun.DirectoryCopy(ta.Origen, nombre, true);
                                log.write("Fin tarea " + ta.Nombre.ToString() + " ");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.write(ex);
                }
					
			    //Recargar las tareas por si se ha modificado el archivo
				if(pasadas >= 20){
                    log.write("Recarga de Tareas");
					pasadas =0;
					comun.cargarXMLtareas();					
				}
				
				Thread.Sleep(reloj.intervalo);
			}
		}
	}
}

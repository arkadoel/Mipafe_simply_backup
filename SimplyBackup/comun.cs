/*
 * 
 * Usuario: Fer.d.minguela@gmail.com
 * Fecha: 23/07/2013
 * Hora: 18:11
 * 
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.IO;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Media;
using System.Threading;


namespace SimplyBackup
{
	/// <summary>
	/// Description of comun.
	/// </summary>
	public class comun
	{
		public const string APP_NAME="MIPAFE Simply Backup";
		public const string APP_VERSION= "2013.07.23";
		
		public static Frame Navegador{get; set;}
        public static VentanaPrincipal VentanaPrincipal { get; set; }
        public static BandejaSistema IconoSistema { get; set; }
        public static FontFamily InconsolataFont{get; set;}
        public static String DirectorioActual{get; set;}
        public static Thread hiloReloj {get; set;}
        
        //public static String[] MESES = {"ENE", "FEB", "MAR", "ABR", "MAY", "JUN", "JUL", "AGO", "SEP", "OCT", "NOV", "DIC"};

        public static List<Tarea> Tareas {get; set; }
        	
        
        
        public static void cargarParametrosIniciales(){
        	//cargar directorio actual
        	DirectorioActual = System.Windows.Forms.Application.StartupPath.ToString();
        	
        	//cargar fuente Inconsolata
        	/*
 			* List<FontFamily> fuentes = Fonts.GetFontFamilies("file:///"+ DirectorioActual +"/Fuentes/").ToList();
        	* InconsolataFont = fuentes.First();
        	*/
        	
        	//cargar la lista de tareas a realizar
        	cargarXMLtareas();
        	
        	
        }
        
        /// <summary>
        /// Carga las tareas
        /// </summary>
        public static void cargarXMLtareas(){
            log.write("cargando listado tareas");
        	Tarea t = null;
        	
        	comun.Tareas = new List<Tarea>();
        	XmlDocument xDoc = new XmlDocument();
        	xDoc.Load(Path.Combine(DirectorioActual, "tareas.xml"));
        	
        	XmlNodeList nodoTareas = xDoc.GetElementsByTagName("tareas");
        	
        	foreach(XmlElement elemento in nodoTareas[0].ChildNodes){
        		t = new Tarea();
        		t.Nombre = elemento.Attributes["nombre"].Value.ToString();
        		t.Origen = elemento.Attributes["origen"].Value.ToString();
        		t.Destino = elemento.Attributes["destino"].Value.ToString();
        		t.Hora = elemento.Attributes["hora"].Value.ToString();
        		t.lunes =Tarea.stringToBool(elemento.Attributes["lunes"].Value.ToString());
        		t.martes =Tarea.stringToBool(elemento.Attributes["martes"].Value.ToString());
        		t.miercoles =Tarea.stringToBool(elemento.Attributes["miercoles"].Value.ToString());
        		t.jueves =Tarea.stringToBool(elemento.Attributes["jueves"].Value.ToString());
        		t.viernes =Tarea.stringToBool(elemento.Attributes["viernes"].Value.ToString());
        		t.sabado =Tarea.stringToBool(elemento.Attributes["sabado"].Value.ToString());
        		t.domingo =Tarea.stringToBool(elemento.Attributes["domingo"].Value.ToString());
        		
        		comun.Tareas.Add(t);
        	}
        	xDoc = null;
        	GC.Collect();
        }
        

        /// <summary>
        /// Coloca una ventana en la zona inferior derecha de la pantalla
        /// </summary>
        /// <param name="_win"></param>
        public static void ventanaInferiorDerechaPantalla(Window _win)
        {
            _win.Top = System.Windows.SystemParameters.WorkArea.Height - _win.Height;
            _win.Left = System.Windows.SystemParameters.WorkArea.Width - _win.Width;
        }
        
        public static void DoEvents(Dispatcher dis){
        	dis.Invoke(DispatcherPriority.Background, new Action(delegate{
        	                                                                                }));
        }
        
        /// <summary>
        /// Permite copiar directorios y hacerlo recursivamente
        /// </summary>
        /// <param name="dirOrigen"></param>
        /// <param name="dirDestino"></param>
        /// <param name="recursivo">¿hacer copia recursiva?</param>
        public static void DirectoryCopy(string dirOrigen, string dirDestino, bool recursivo)
		{
			DirectoryInfo dir = new DirectoryInfo(dirOrigen);
			DirectoryInfo[] dirs = dir.GetDirectories();
			
			if(dir.Exists==true){
				
				if(Directory.Exists(dirDestino)==false){
					Directory.CreateDirectory(dirDestino);
                    log.write("Creando directorio " + dirDestino + "");
				}
                else log.write("El directorio " + dirDestino + " ya existe");
				
				FileInfo[] files = dir.GetFiles();
				foreach(FileInfo file in files){
                    log.write("\tCopiando: " + file.Name.ToString());
					string temppath = Path.Combine(dirDestino,file.Name);
					file.CopyTo(temppath,true);
					
				}
				//copy subdirs
				if(recursivo==true){
					foreach(DirectoryInfo subdir in dirs){
                        log.write("\tCopiando Directorio: " + subdir.Name.ToString());
						string tempPath = Path.Combine(dirDestino, subdir.Name);
						
						//copiar recursivamente
						DirectoryCopy(subdir.FullName, tempPath, recursivo);
					}
				}
			}
		}

	}
}

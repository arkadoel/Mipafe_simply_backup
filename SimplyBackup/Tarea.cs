/*
 * 
 * Usuario: Fer.d.minguela@gmail.com
 * Fecha: 07/23/2013
 * Hora: 18:34
 * 
 * 
 */
using System;

namespace SimplyBackup
{
	/// <summary>
	/// Description of Tarea.
	/// </summary>
	public class Tarea
	{
		public string Nombre {get; set; }
		public string Hora {get; set; }
		public string Origen {get; set; }
		public string Destino {get; set; }
		public bool lunes {get; set; }
		public bool martes {get; set; }
		public bool miercoles {get; set; }
		public bool jueves {get; set; }
		public bool viernes {get; set; }
		public bool sabado {get; set; }
		public bool domingo {get; set; }
		
		public Tarea()
		{
		}
		
		public static bool stringToBool(string valor){
			if(valor == "true") return true;
			else return false;
		}
	}
}

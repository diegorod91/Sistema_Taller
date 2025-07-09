////using BOL.Entidades;
//using Newtonsoft.Json.Linq;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Web;
////using Utilities;

//namespace WebApplication.Ejemplos.Abm
//{
//	/// <summary>
//	/// Descripción breve de Empresa
//	/// </summary>
//	public class EmpresaH : IHttpHandler, System.Web.SessionState.IRequiresSessionState
//	{

//		public void ProcessRequest(HttpContext context)
//		{
//			context.Response.ContentType = "application/json";
//			if (HttpContext.Current.Session["Usuario"] == null)
//			{
//				context.Response.Write((new { Mensaje = "Usted no ha iniciado sesión o sus sesión ha caducado" }).ToJSON());
//				return;
//			}
//			switch (context.Request.PathInfo.Replace("/", ""))
//			{
//				case "GetById":
//					GetById(context);
//					break;
//			}
//		}
//		private static void GetById(HttpContext context)
//		{
//			StreamReader reader = new StreamReader(context.Request.InputStream);
//			string text = reader.ReadToEnd();
//			JObject json = JObject.Parse(text);
//			Empresa x = Empresa.GetById(json["id"].ToString().ToInt(0));
//			if (x == null)
//			{
//				context.Response.Write((new { Status = "NotFound", Message = "No se pudo hallar Empresa seleccionado." }).ToJSON());
//				return;
//			}
//			context.Response.Write((new { Status = "OK", Data = x, Message = "" }).ToJSON());
//		}
//		public bool IsReusable
//		{
//			get
//			{
//				return false;
//			}
//		}
//	}
//}
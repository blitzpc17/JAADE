﻿using CAPADATOS;
using CAPADATOS.ADO.SISTEMA;
using CAPADATOS.Entidades;
using CAPALOGICA.LOGICAS.SISTEMA;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace PRESENTACION.UTILERIAS
{
    public static class Global
    {

        public static clsUsuario ObjUsuario;

        public static bool EsValorEntero(string valor)
        {
            return int.TryParse(valor, out _);
        }

        public static bool EsValorDecimal(string valor)
        {
            return decimal.TryParse(valor, out _);
        }

        public static DateTime FechaServidor()
        {
            using (var contexto = new UtileriasLogica())
            {
                return contexto.ObtenerFechaYHoraServidor();
            }
        }

        public static void CredencialesSesionAcceso(clsUsuario obj)
        {
            ObjUsuario = obj;
        }

        public static dynamic DevulveVariableGlobal(Enumeraciones.VariablesGlobales variable)
        {
            using (var contexto = new VariableGlobalADO())
            {
                return contexto.Obtener(variable.ToString());                
            }
        } 

        public static decimal CalcularPagoMensualRestante(decimal restante, decimal noPagos)
        {
            return restante / noPagos;
        }

        public static string MesALetra(int numeroMes)
        {
            switch (numeroMes)
            {
                case 1: return "enero";
                case 2: return "febrero";
                case 3: return "marzo";
                case 4: return "abril";
                case 5: return "mayo";
                case 6: return "junio";
                case 7: return "julio";
                case 8: return "agosto";
                case 9: return "septiembre";
                case 10: return "octubre";
                case 11: return "noviembre";
                case 12: return "diciembre";
                default: return "Mes no válido";
            }
        }

        public static string DigitoAnio(DateTime fecha)
        {
            return (fecha.Year % 100).ToString();
        }

        public static string ArmarDomicilioCliente(clsClientes obj)
        {
            string Calle = obj.Calle,
                NoExt = obj.NoExt,
                NoInt = obj.NoInt,
                Colonia = obj.Colonia,
                Localidad = obj.Localidad,
                CodigoPostal = obj.CodigoPostal,
                Municipio = "",
                Estado = "";

            return Calle + " NO. " + NoExt + (string.IsNullOrEmpty(obj.NoInt) ? "" : " INT. " + obj.NoInt) + " COL. " + obj.Colonia + ". " + (Municipio.Equals(Localidad) ? (Localidad+", "+Localidad) : (Localidad+", "+Municipio)) + ", " + Estado;


        }

        public static bool GuardarExcepcion(Exception ex, string formularioNombre, string msjAdicional =null)
        {
            try
            {
                using (var contexto = new ExcepcionADO())
                {
                    contexto.Insertar(new EXCEPCION
                    {
                        Fecha = FechaServidor(),
                        Formulario = formularioNombre,
                        Resumen = ex.Message+(msjAdicional!=null?" Mensaje Adicional:"+msjAdicional:""),
                        Detalle = ex.StackTrace,
                        USUARIOId = ObjUsuario.Id
                    }) ;
                    contexto.Guardar();
                    return true;
                }
                    
            }
            catch(Exception)
            {
                //bloqueo en la bd
                return false;
            }
            
        }

        public static bool SubirArchivoFtp(Enumeraciones.VariablesGlobales variableCredenciales, string pathLocal, string pathRemoto)
        {
            clsCredencialesFtp _ObjCredencialesFtp =
               JsonConvert.DeserializeObject<clsCredencialesFtp>(DevulveVariableGlobal(variableCredenciales));

            string ftpServer =   _ObjCredencialesFtp.Server;
            string ftpUsername = _ObjCredencialesFtp.Username;
            string ftpPassword = _ObjCredencialesFtp.Password;

            // Ruta del archivo local que deseas subir
            string archivoLocal = pathLocal;

            // Ruta en el servidor FTP donde deseas guardar el archivo
            string rutaRemota = pathRemoto;

            // Crear una instancia de FtpWebRequest
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create($"ftp://{ftpServer}/{rutaRemota}");
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);

            try
            {
                // Leer el contenido del archivo local
                byte[] archivoBytes = File.ReadAllBytes(archivoLocal);

                // Obtener el flujo de salida y escribir el contenido del archivo
                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(archivoBytes, 0, archivoBytes.Length);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool EliminarArchivoFtp(Enumeraciones.VariablesGlobales variableCredenciales, string pathRemoto)
        {
            clsCredencialesFtp _ObjCredencialesFtp = 
                JsonConvert.DeserializeObject<clsCredencialesFtp>(DevulveVariableGlobal(variableCredenciales));

            string ftpServer =   _ObjCredencialesFtp.Server;
            string ftpUsername = _ObjCredencialesFtp.Username;
            string ftpPassword = _ObjCredencialesFtp.Password;            

            // Ruta en el servidor FTP del archivo que deseas eliminar
            string rutaRemota = pathRemoto;

            // Crear una instancia de FtpWebRequest
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create($"{ftpServer}{rutaRemota}");
            request.Method = WebRequestMethods.Ftp.DeleteFile;
            request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);

            try
            {
                // Enviar la solicitud para eliminar el archivo
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    return true;
                }
            }
            catch (WebException ex)
            {
                return false;
            }
        }

        public static bool EnviarWhatsApp(Enumeraciones.VariablesGlobales variableCredenciales, clsWhatsApp objWhats)
        {
            clsCredencialesTwilio _ObjCredenciales = 
                JsonConvert.DeserializeObject<clsCredencialesTwilio>(DevulveVariableGlobal(variableCredenciales));
            string accountSid = _ObjCredenciales.AccountSId;
            string authToken = _ObjCredenciales.AuthToken;

            TwilioClient.Init(accountSid, authToken);

            // Número de teléfono del destinatario en formato internacional (por ejemplo, +1234567890)
            //string recipientPhoneNumber = "whatsapp:+5212381458680";
            string recipientPhoneNumber = "whatsapp:+521"+objWhats.TelefonoDestino;

            try
            {
                var message = MessageResource.Create(
                    body: objWhats.Cuerpo,
                    from: new PhoneNumber("whatsapp:+"+_ObjCredenciales.TelefonoSalida),
                    to: new PhoneNumber(recipientPhoneNumber),
                    mediaUrl: new List<Uri> { new Uri(objWhats.PathMediaFile) }
                );


                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool EnviarCorreo(Enumeraciones.VariablesGlobales VariableCredencial, clsCorreo objCorreo)
        {
            clsCredencialesCorreo _ObjCorreoCredenciales = 
                JsonConvert.DeserializeObject<clsCredencialesCorreo>(DevulveVariableGlobal(VariableCredencial));
            // Configurar la información del correo electrónico y el servidor SMTP
            string fromAddress = _ObjCorreoCredenciales.EmailBase;
            List<string> toAddress = objCorreo.CorreoDestino;
            string subject = objCorreo.Asunto;
            string body = objCorreo.Cuerpo;
            string smtpHost = _ObjCorreoCredenciales.Hostname;
            int smtpPort = _ObjCorreoCredenciales.Puerto;
            string smtpUsername = _ObjCorreoCredenciales.SmtpUsername;
            string smtpPassword = _ObjCorreoCredenciales.SmtpPassword;

            // Ruta del archivo PDF que deseas adjuntar
            List<string> pdfFilePath = objCorreo.PathAttach; //@"C:\Ruta\Al\Archivo.pdf";

            // Crear el cliente SMTP y configurar las credenciales
            using (SmtpClient smtpClient = new SmtpClient(smtpHost, smtpPort))
            {
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                smtpClient.EnableSsl = true;

                // Crear el mensaje de correo electrónico
                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress(fromAddress);
                    foreach(var item in toAddress)
                    {
                        mailMessage.To.Add(item);
                    }                    
                    mailMessage.Subject = subject;
                    mailMessage.Body = body;

                    // Adjuntar el archivo PDF
                    foreach(var file in pdfFilePath)
                    {
                        Attachment pdfAttachment = new Attachment(file);
                        mailMessage.Attachments.Add(pdfAttachment);
                    }                  

                    try
                    {
                        // Enviar el correo electrónico
                        smtpClient.Send(mailMessage);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }
        }

        public static clsUsuario ObtenerDataUsuario(int usuarioId)
        {
            using (var contexto = new UsuariosADO())
            {
                return contexto.ObtenerDataUsuario(usuarioId);
            }
        }

        public static string ObtenerFolio(Enumeraciones.ProcesoFolio tipo)
        {
            string _Prefijo, _NombreVariable;
            int _Longitud;
            
            switch(tipo){
                case Enumeraciones.ProcesoFolio.PAGO:
                    _Prefijo = "PG";
                    _Longitud = 9;
                    _NombreVariable = Enumeraciones.VariablesGlobales.ConsecutivoPagos.ToString();
                    break;
                case Enumeraciones.ProcesoFolio.CLIENTE:
                    _Prefijo = "C";
                    _Longitud = 4;
                    _NombreVariable = Enumeraciones.VariablesGlobales.ConsecutivoClientes.ToString();
                    break;
                case Enumeraciones.ProcesoFolio.CONTRATO:
                    _Prefijo = "CO";
                    _Longitud = 7;
                    _NombreVariable = Enumeraciones.VariablesGlobales.ConsecutivoContratos.ToString();
                    break;

                default: return null;
            }

           return GenerarFolio(_Prefijo, _Longitud, _NombreVariable);
        }

        private static string GenerarFolio(string Prefijo,int longitud, string nombreVariable)
        {
            string _folio;
            using(var contexto = new VariableGlobalADO())
            {
                int consecutivo = contexto.GenerarFolio(nombreVariable);
                if (consecutivo > 0)
                {
                    _folio = consecutivo.ToString("N0");
                    _folio = _folio.PadLeft(longitud, '0');
                    return Prefijo+_folio;
                }
                return null;
            }
        }

        public static int ObtenerDiferenciaFechas(string Por, DateTime FechaInicial, DateTime FechaFinal)
        {
            return FechaFinal.Year - FechaInicial.Year * 12 + FechaFinal.Month - FechaInicial.Month ;
        }

        public static string ObtenerNombreUsuario(clsUsuario obj)
        {
            /*PERSONA objPersona;
            objPersona = obj.PERSONA;
            if (objPersona != null) return objPersona.Nombres + " " + objPersona.Apellidos;
            return null;*/
            return obj.Nombre;
        }

        public static clsFormatoFechaEscrito ObtenerFechaEscrita(DateTime fecha)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");
            clsFormatoFechaEscrito ObjData = new clsFormatoFechaEscrito();

            DateTime _fecha = fecha.Date;
            TimeSpan _tiempo = fecha.TimeOfDay;

            int _dia = _fecha.Day;
            int _mes = _fecha.Month;  
            int _anio = _fecha.Year;
            int _hora = _tiempo.Hours;
            int _minuto = _tiempo.Minutes;

            
            ObjData.Mes = MesEnLetras(_mes);
            ObjData.Dia =    ConvertirNumeroALetras(_dia);
            ObjData.Anio = ConvertirNumeroALetras(624925);//ConvertirNumeroALetras(_anio);
            ObjData.Hora =   ConvertirNumeroALetras(_hora);
            ObjData.Minuto = ConvertirNumeroALetras(_minuto);

            return ObjData;
                
        }

     
        public static string MesEnLetras(int mes)
        {
            switch (mes)
            {
                case 1: return "ENERO";
                case 2: return "FEBRERO";
                case 3: return "MARZO";
                case 4: return "ABRIL";
                case 5: return "MAYO";
                case 6: return "JUNIO";
                case 7: return "JULIO";
                case 8: return "AGOSTO";
                case 9: return "SEPTIEMBRE";
                case 10: return "OCTUBRE";
                case 11: return "MOVIEMBRE";
                case 12: return "DICIEMBRE";
                default: return null;
            }
        }

        private static readonly string[] Unidades =
    {
        "", "uno", "dos", "tres", "cuatro", "cinco", "seis", "siete", "ocho", "nueve"
    };

        private static readonly string[] Decenas =
        {
        "", "diez", "veinte", "treinta", "cuarenta", "cincuenta", "sesenta", "setenta", "ochenta", "noventa"
    };

        private static readonly string[] Especiales =
        {
        "diez", "once", "doce", "trece", "catorce", "quince", "dieciséis", "diecisiete", "dieciocho", "diecinueve"
    };

        private static readonly string[] Centenas =
        {
        "", "ciento", "doscientos", "trescientos", "cuatrocientos", "quinientos", "seiscientos", "setecientos", "ochocientos", "novecientos"
    };

        public static string ConvertirNumeroALetras(int numero)
        {
            if (numero < 0 || numero > 999999999)
            {
                throw new ArgumentOutOfRangeException(nameof(numero));
            }

            if (numero == 0)
            {
                return "cero";
            }

            return ConvertirParte(numero);
        }

        private static string ConvertirParte(int numero)
        {
            string letras = "";
            bool entroVeintes = false;

            if (numero >= 1000000)
            {
                letras += ConvertirParte(numero / 1000000) + " millones ";
                numero %= 1000000;
            }

            if (numero >= 1000)
            {
                letras += ConvertirParte(numero / 1000) + " mil ";
                numero %= 1000;
            }

            if (numero >= 100)
            {
                letras += Centenas[numero / 100] + " ";
                numero %= 100;
            }

            if (numero >= 20)
            {
                if(numero>20 && numero < 30)
                {
                    letras += "veinti";
                    entroVeintes = true;
                }
                else
                {
                    letras += Decenas[numero / 10] + " ";
                }
                
                numero %= 10;
            }

            if (numero >= 10)
            {
                letras += Especiales[numero - 10] + " ";
                numero = 0;
            }

            if (numero > 0)
            {
                if (!string.IsNullOrEmpty(letras) && numero < 10)
                {
                    if (!entroVeintes)
                    {
                        letras += "y ";
                    }
                    
                }
                letras += Unidades[numero] + " ";
            }

            return letras.Trim();
        }
    }
}

using CAPADATOS;
using CAPADATOS.ADO.SISTEMA;
using CAPADATOS.Entidades;
using CAPALOGICA.LOGICAS.SISTEMA;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace PRESENTACION.UTILERIAS
{
    public static class Global
    {

        public static USUARIO ObjUsuario;

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

        public static void CredencialesSesionAcceso(USUARIO obj)
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

        public static bool GuardarExcepcion(Exception ex, string formularioNombre)
        {
            try
            {
                using (var contexto = new ExcepcionADO())
                {
                    contexto.Insertar(new EXCEPCION
                    {
                        Fecha = FechaServidor(),
                        Formulario = formularioNombre,
                        Resumen = ex.Message,
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

        public static bool SubirArchivoFtp(clsCredencialesFtp objCredenciales, string pathLocal, string pathRemoto)
        {
            string ftpServer = objCredenciales.Server;
            string ftpUsername = objCredenciales.Username;
            string ftpPassword = objCredenciales.Password;

            // Ruta del archivo local que deseas subir
            string archivoLocal = pathLocal;

            // Ruta en el servidor FTP donde deseas guardar el archivo
            string rutaRemota = pathRemoto;

            // Crear una instancia de FtpWebRequest
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create($"{ftpServer}{rutaRemota}");
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

        public static bool EliminarArchivoFtp(clsCredencialesFtp objCredenciales, string pathRemoto)
        {
            string ftpServer = objCredenciales.Server;
            string ftpUsername = objCredenciales.Username;
            string ftpPassword = objCredenciales.Password;

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

        public static bool EnviarWhatsApp(clsCredencialesTwilio objCredenciales, clsWhatsApp objWhats)
        {
            string accountSid = objCredenciales.AccountSId;
            string authToken = objCredenciales.AccountSId;

            TwilioClient.Init(accountSid, authToken);

            // Número de teléfono del destinatario en formato internacional (por ejemplo, +1234567890)
            //string recipientPhoneNumber = "whatsapp:+5212381458680";
            string recipientPhoneNumber = "whatsapp:+52"+objWhats.TelefonoDestino;

            try
            {
                var message = MessageResource.Create(
                    body: objWhats.Cuerpo,
                    from: new PhoneNumber("whatsapp:+52"+objCredenciales.TelefonoSalida),
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

        public static bool EnviarCorreo()
        {
            // Configurar la información del correo electrónico y el servidor SMTP
            string fromAddress = "tuCorreo@gmail.com";
            string toAddress = "destinatario@example.com";
            string subject = "Asunto del Correo";
            string body = "Cuerpo del Correo";
            string smtpHost = "smtp.gmail.com"; // Puedes cambiarlo según tu proveedor de correo
            int smtpPort = 587; // Puedes cambiarlo según tu proveedor de correo
            string smtpUsername = "tuCorreo@gmail.com";
            string smtpPassword = "tuContraseña";

            // Ruta del archivo PDF que deseas adjuntar
            string pdfFilePath = @"C:\Ruta\Al\Archivo.pdf";

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
                    mailMessage.To.Add(toAddress);
                    mailMessage.Subject = subject;
                    mailMessage.Body = body;

                    // Adjuntar el archivo PDF
                    Attachment pdfAttachment = new Attachment(pdfFilePath);
                    mailMessage.Attachments.Add(pdfAttachment);

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

        public static string ObtenerFolio(string tipo)
        {
            string _Prefijo = null, _NombreVariable = null;
            int _Longitud = 0;
            
            switch(tipo){
                case "PAGO":
                    _Prefijo = "PG";
                    _Longitud = 11;
                    _NombreVariable = Enumeraciones.VariablesGlobales.ConsecutivoPagos.ToString();
                    break;
                case "CLIENTE":
                    _Prefijo = "C";
                    _Longitud = 4;
                    _NombreVariable = Enumeraciones.VariablesGlobales.ConsecutivoClientes.ToString();
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
                    _folio.PadRight(longitud, '0');
                    return Prefijo+_folio;
                }
                return null;
            }
        }


        
    }
}

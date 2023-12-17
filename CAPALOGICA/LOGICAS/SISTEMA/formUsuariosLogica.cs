using CAPADATOS;
using CAPADATOS.ADO.SISTEMA;
using CAPADATOS.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPALOGICA.LOGICAS.SISTEMA
{
    public class formUsuariosLogica
    {

        private UsuariosADO contextoUsuario;
        private RolesADO contextoRol;
        private EstadoADO contextoEstado;
        private PersonaADO contextoPersona;


        public USUARIO ObjUsuario;
        public PERSONA ObjPersona;
        public List<ESTADO> LstEstado;
        public List<ROL> LstRol;
        public clsUsuario ObjUsuarioData;
        public List<clsUsuario> LstUsuario;
        public List<clsUsuario> LstUsuarioAux;

        public int index = -1;
        public int indexAux = -1;
        public int Column = 0;


        public formUsuariosLogica()
        {
            contextoUsuario = new UsuariosADO();
            contextoRol = new RolesADO();
            contextoEstado = new EstadoADO();
            contextoPersona = new PersonaADO(); 
        }

        public void InstanciarUsuario()
        {
            ObjUsuario = new USUARIO();
        }

        public void InstanciarPersona()
        {
            ObjPersona = new PERSONA();
        }

     /*   public void ListarUsuarios()
        {
            LstUsuario = contextoUsuario.ListarUsuarios();
        }*/

        public void ListarCatalogos()
        {
            LstEstado = contextoEstado.Listar().Where(x => x.Proceso == "USUARIO").ToList();
            LstRol = contextoRol.Listar();
        }

        public void Guardar()
        {
            if (ObjUsuario.Id == 0)
            {
                contextoPersona.Insertar(ObjPersona);
                contextoPersona.Guardar();
                ObjUsuario.PERSONAId = ObjPersona.Id;
                contextoUsuario.Insertar(ObjUsuario);
                contextoUsuario.Guardar();
            }
            else
            {
                contextoPersona.Guardar();
                contextoUsuario.Guardar();
            }

            
        }

        public USUARIO ObtenerUsuario(int id)
        {
            return contextoUsuario.Obtener(id);
        }
        public PERSONA ObtenerPersona(int idPersona)
        {
            return contextoPersona.Obtener(idPersona);
        }

        public bool Filtrar(int column, string termino)
        {
            if (LstUsuarioAux == null) LstUsuarioAux = LstUsuario;

            switch (column)
            {
                case 1:
                    index = LstUsuarioAux.FindIndex(x => x.Alias.StartsWith(termino));
                    break;
                case 3:
                    index = LstUsuarioAux.FindIndex(x => x.Nombre.ToString().StartsWith(termino));
                    break;
                case 15:
                    index = LstUsuarioAux.FindIndex(x => x.Rol.StartsWith(termino));
                    break;
                case 16:
                    index = LstUsuarioAux.FindIndex(x => x.Estado.StartsWith(termino));
                    break;


                default:
                    index = -1;
                    break;

            }

            return (index >= 0);

        }

        public void Ordenar(int column)
        {
            switch (column)
            {

                case 1:
                    LstUsuarioAux = LstUsuario.OrderBy(x => x.Alias).ThenBy(x => x.Nombre).ThenBy(x => x.Rol).ThenBy(x=>x.Estado).ToList();
                    break;
                case 3:
                    LstUsuarioAux = LstUsuario.OrderBy(x => x.Nombre).ThenBy(x => x.Rol).ThenBy(x => x.Estado).ThenBy(x=>x.Nombre).ToList();
                    break;
                case 15:
                    LstUsuarioAux = LstUsuario.OrderBy(x => x.Rol).ThenBy(x => x.Estado).ThenBy(x => x.Alias).ThenBy(x=>x.Nombre).ToList();
                    break;
                case 16:
                    LstUsuarioAux = LstUsuario.OrderBy(x => x.Estado).ThenBy(x => x.Alias).ThenBy(x => x.Nombre).ThenBy(x=>x.Rol).ToList();
                    break;

                default:
                    LstUsuarioAux = LstUsuario.OrderBy(x => x.Alias).ThenBy(x => x.Nombre).ThenBy(x => x.Rol).ThenBy(x => x.Estado).ToList();
                    break;

            }
        }


        public clsUsuario ObtenerDataUsuario(int id)
        {
            return contextoUsuario.ObtenerDataUsuario(id);
        }

        public void ListarUsuarios()
        {
            LstUsuario = contextoUsuario.ListarUsuarios();
            LstUsuarioAux = LstUsuario;
        }






    }
}

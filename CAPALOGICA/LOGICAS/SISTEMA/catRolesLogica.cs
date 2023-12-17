using CAPADATOS;
using CAPADATOS.ADO.SISTEMA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPALOGICA.LOGICAS.SISTEMA
{
    public class catRolesLogica
    {
        private RolesADO contextoRol;
        public ROL ObjRol;
        public List<ROL> LstRol;
        public List<ROL> LstRolAux;

        public int index = -1;
        public int indexAux = -1;
        public int Column = 0;

        public catRolesLogica()
        {
            contextoRol = new RolesADO();
        }

        public void InstanciarRol()
        {
            ObjRol = new ROL();
        }

        public void Guardar()
        {
            if (ObjRol.Id == 0)
            {
                contextoRol.Insertar(ObjRol);
            }

            contextoRol.Guardar();

        }

        public void Listar()
        {
            LstRol = contextoRol.Listar();
        }

        public ROL Obtener(int id)
        {
            return contextoRol.Obtener(id);
        }

        public void Eliminar(ROL entidad)
        {
            contextoRol.Eliminar(entidad);
        }


        public bool Filtrar(int column, string termino)
        {
            if (LstRolAux == null) LstRolAux = LstRol;

            switch (column)
            {
                case 1:
                    index = LstRolAux.FindIndex(x => x.Nombre.StartsWith(termino));
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
                    LstRolAux = LstRol.OrderBy(x => x.Nombre).ToList();
                    break;

                default:
                    LstRolAux = LstRol.OrderBy(x => x.Nombre).ToList();
                    break;

            }
        }
    }
}

using CAPADATOS;
using CAPADATOS.ADO.SISTEMA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPALOGICA.LOGICAS.SISTEMA
{
    public class catEstadosLogica
    {
        private EstadoADO contexto;
        public ESTADO ObjEstado;
        public List<ESTADO> LstEstado;
        public List<ESTADO> LstEstadoAux;
        public int Index, Column, IndexAux;

        public catEstadosLogica()
        {
            contexto = new EstadoADO();
        }
        public void InstanciarEstado()
        {
            ObjEstado = new ESTADO();
        }

        public void Guardar()
        {
            if (ObjEstado.Id == 0)
            {
                contexto.Insertar(ObjEstado);
            }

            contexto.Guardar();

        }

        public void Listar()
        {
            LstEstado = contexto.Listar();
        }

        public ESTADO Obtener(int id)
        {
            return contexto.Obtener(id);
        }

        public void Eliminar(ESTADO entidad)
        {
            contexto.Eliminar(entidad);
        }

        public bool Filtrar(int column, string termino)
        {
            if (LstEstadoAux == null) LstEstadoAux = LstEstado;

            switch (column)
            {
                case 1:
                    Index = LstEstadoAux.FindIndex(x => x.Nombre.StartsWith(termino));
                    break;

                case 2:
                    Index = LstEstadoAux.FindIndex(x => x.Proceso.StartsWith(termino));
                    break;

                default:
                    Index = -1;
                    break;

            }

            return (Index >= 0);

        }

        public void Ordenar(int column)
        {
            switch (column)
            {

                case 1:
                    LstEstadoAux = LstEstado.OrderBy(x => x.Nombre).ThenBy(x => x.Proceso).ToList();
                    break;

                case 2:
                    LstEstadoAux = LstEstado.OrderBy(x => x.Proceso).ThenBy(x => x.Nombre).ToList();
                    break;

                default:
                    LstEstadoAux = LstEstado.OrderBy(x => x.Nombre).ToList();
                    break;

            }
        }
    }
}

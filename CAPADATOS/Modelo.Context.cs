﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CAPADATOS
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DB_JAADEEntities : DbContext
    {
        public DB_JAADEEntities()
            : base("name=DB_JAADEEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<USUARIO> USUARIO { get; set; }
        public virtual DbSet<PERSONA> PERSONA { get; set; }
        public virtual DbSet<MODULO> MODULO { get; set; }
        public virtual DbSet<ROL> ROL { get; set; }
        public virtual DbSet<MODULO_PERMISO> MODULO_PERMISO { get; set; }
        public virtual DbSet<CLIENTE> CLIENTE { get; set; }
        public virtual DbSet<AGENDA> AGENDA { get; set; }
        public virtual DbSet<ESTADO> ESTADO { get; set; }
        public virtual DbSet<ZONA> ZONA { get; set; }
        public virtual DbSet<LOTE> LOTE { get; set; }
        public virtual DbSet<PERSONA_AGENDA> PERSONA_AGENDA { get; set; }
        public virtual DbSet<VARIABLEGLOBAL> VARIABLEGLOBAL { get; set; }
        public virtual DbSet<EXCEPCION> EXCEPCION { get; set; }
        public virtual DbSet<CONTROL> CONTROL { get; set; }
        public virtual DbSet<CONTROL_PERMISO> CONTROL_PERMISO { get; set; }
        public virtual DbSet<SOCIOS> SOCIOS { get; set; }
        public virtual DbSet<CLIENTES_SOCIOS> CLIENTES_SOCIOS { get; set; }
        public virtual DbSet<CONTRATO> CONTRATO { get; set; }
        public virtual DbSet<PAGO> PAGO { get; set; }
        public virtual DbSet<CONTRATO_LOTES> CONTRATO_LOTES { get; set; }
        public virtual DbSet<ROL_PERMISO> ROL_PERMISO { get; set; }
    }
}

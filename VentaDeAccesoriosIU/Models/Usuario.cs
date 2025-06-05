
﻿    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using System.ComponentModel.DataAnnotations;



    public partial class Usuario
    {
        public int IdUsuario { get; set; }

        public string? Nombre { get; set; }

        public string? Apellido { get; set; }

        public string? CorreoElectronico { get; set; }

        public string? ContrasenaHash { get; set; }


        public bool? Estado { get; set; }

        [JsonIgnore]
        public virtual ICollection<PedidosProveedores> PedidosProveedores { get; set; } = new List<PedidosProveedores>();

        [JsonIgnore]
        public virtual ICollection<UsuariosRoles> UsuariosRoles { get; set; } = new List<UsuariosRoles>();

        [JsonIgnore]
        public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
    }


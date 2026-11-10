using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

class Program
{
    static void Main(string[] args)
    {
        Conexion conexion = new Conexion();

        try
        {
            using (SqlConnection sql = conexion.obtenerConexion())
            {
                sql.Open();
            }
        }
        catch (Exception ex)
        {
            Console.Clear();
            Console.WriteLine("❌ [Error crítico] No se pudo conectar a la Base de Datos.");
            Console.WriteLine($"Detalle: {ex.Message}");
            Console.WriteLine("\nPresione una tecla para salir...");
            Console.ReadKey();
            return;
        }

        InicioSesion();
    }

    // ==========================================
    // INICIO DE SESIÓN
    // ==========================================
    static void InicioSesion()
    {
        bool salir = false;

        while (!salir)
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║            SISTEMA DE STOCK              ║");
            Console.WriteLine("║           ¡Bienvenido a CMR!             ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("╠══════════════════════════════════════════╣");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║    1. Iniciar sesión                     ║");
            Console.WriteLine("║    2. Salir                              ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");
            Console.Write(" Seleccione una opción: ");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Usuarios usuarioEncontrado = VerificacionUsuarioContraseña();
                    if (usuarioEncontrado != null)
                    {
                        Bienvenida(usuarioEncontrado);
                    }
                    break;

                case "2":
                    salir = true;
                    Console.WriteLine("\nSaliendo del sistema... ¡Hasta luego!");
                    break;

                default:
                    Console.WriteLine("\n❌ [Error] Opción no válida. Presione una tecla.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static Usuarios VerificacionUsuarioContraseña()
    {
        Usuarios usuarioLogueado = null;

        while (usuarioLogueado == null)
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║            INICIO DE SESION              ║");
            Console.WriteLine("╠══════════════════════════════════════════╣");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║    Usuario:                              ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║    Contraseña:                           ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");

            Console.SetCursorPosition(14, 4);
            string usuarioIngresado = Console.ReadLine();

            Console.SetCursorPosition(17, 6);
            string contraseñaIngresada = "";
            ConsoleKeyInfo tecla;

            do
            {
                tecla = Console.ReadKey(true);

                if (tecla.Key == ConsoleKey.Backspace && contraseñaIngresada.Length > 0)
                {
                    contraseñaIngresada = contraseñaIngresada.Substring(0, contraseñaIngresada.Length - 1);
                    int currentLeft = Console.CursorLeft;
                    int currentTop = Console.CursorTop;
                    if (currentLeft > 17)
                    {
                        Console.SetCursorPosition(currentLeft - 1, currentTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(currentLeft - 1, currentTop);
                    }
                }
                else if (!char.IsControl(tecla.KeyChar) && contraseñaIngresada.Length < 22)
                {
                    contraseñaIngresada += tecla.KeyChar;
                    Console.Write("*");
                }
            } while (tecla.Key != ConsoleKey.Enter);

            Console.SetCursorPosition(0, 10);

            DatosUsuarios usuarioDB = new DatosUsuarios();
            usuarioLogueado = usuarioDB.ObtenerUsuarioPorNombre(usuarioIngresado);

            if (usuarioLogueado != null && usuarioLogueado.Contrasena == contraseñaIngresada)
            {
                Console.WriteLine("✅ ¡Acceso concedido! Bienvenido, " + usuarioLogueado.Nombre);
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
                return usuarioLogueado;
            }
            else
            {
                Console.WriteLine("❌ [Error] Usuario o contraseña incorrectos.");
                Console.WriteLine("Presione una tecla para intentar de nuevo...");
                Console.ReadKey();
                usuarioLogueado = null;
            }
        }

        return usuarioLogueado;
    }

    static void Bienvenida(Usuarios userValidado)
    {
        if (userValidado != null)
        {
            if (userValidado.Rol == "Administrador")
            {
                MenuAdministrador(userValidado);
            }
            else if (userValidado.Rol == "Empleado")
            {
                MenuEmpleado(userValidado);
            }
        }
    }

    // ==========================================
    // 1. PANEL PRINCIPAL DE ADMINISTRADOR
    // ==========================================
    static void MenuAdministrador(Usuarios userAdmin)
    {
        bool salirMenuAdmin = false;

        while (!salirMenuAdmin)
        {
            Console.Clear();
            Console.WriteLine("\n--- PANEL DE ADMINISTRADOR ---");
            Console.WriteLine($"Conectado como: {userAdmin.Nombre} (DNI: {userAdmin.Dni})");
            Console.WriteLine();

            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║              MENÚ PRINCIPAL              ║");
            Console.WriteLine("╠══════════════════════════════════════════╣");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║    1. INVENTARIO / HISTORIAL             ║");
            Console.WriteLine("║    2. REPUESTOS                          ║");
            Console.WriteLine("║    3. INSUMOS                            ║");
            Console.WriteLine("║    4. USUARIOS                           ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║    0. Salir                              ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");
            Console.WriteLine();
            Console.Write(" Seleccione una opcion: ");
            string gestionElegidaAdmin = Console.ReadLine();

            switch (gestionElegidaAdmin)
            {
                case "1":
                    MenuInventarioHistorial();
                    break;
                case "2":
                    SubMenuRepuestos(userAdmin);
                    break;
                case "3":
                    SubMenuInsumos(userAdmin);
                    break;
                case "4":
                    GestionarUsuarios(userAdmin);
                    break;
                case "0":
                    salirMenuAdmin = true;
                    break;
                default:
                    Console.WriteLine("\n❌ [Error] Opción no válida. Presione una tecla para continuar.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    // ==========================================
    // 2. PANEL PRINCIPAL DE EMPLEADO (ESTRUCTURADO EN SUBMENÚS)
    // ==========================================
    static void MenuEmpleado(Usuarios userEmpleado)
    {
        bool salirMenuEmpleado = false;

        while (!salirMenuEmpleado)
        {
            Console.Clear();
            Console.WriteLine("\n--- PANEL DE EMPLEADO ---");
            Console.WriteLine($"Conectado como: {userEmpleado.Nombre} (DNI: {userEmpleado.Dni})");
            Console.WriteLine();

            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║              MENÚ PRINCIPAL              ║");
            Console.WriteLine("╠══════════════════════════════════════════╣");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║    1. INVENTARIO                         ║");
            Console.WriteLine("║    2. REPUESTOS                          ║");
            Console.WriteLine("║    3. INSUMOS                            ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║    0. Salir                              ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");
            Console.WriteLine();
            Console.Write(" Seleccione una opcion: ");
            string gestionElegidaEmpleado = Console.ReadLine();

            switch (gestionElegidaEmpleado)
            {
                case "1":
                    MenuInventario();
                    break;
                case "2":
                    SubMenuRepuestosEmpleado(userEmpleado);
                    break;
                case "3":
                    SubMenuInsumosEmpleado(userEmpleado);
                    break;
                case "0":
                    salirMenuEmpleado = true;
                    break;
                default:
                    Console.WriteLine("\n❌ [Error] Opción no válida. Presione una tecla para continuar.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    // ==========================================
    // SUBMENÚ DE REPUESTOS (EMPLEADO)
    // ==========================================
    static void SubMenuRepuestosEmpleado(Usuarios userEmpleado)
    {
        bool salir = false;
        while (!salir)
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║                 REPUESTOS                ║");
            Console.WriteLine("╠══════════════════════════════════════════╣");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║    1. Ingresar stock de repuesto         ║");
            Console.WriteLine("║    2. Despachar repuesto                 ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║    0. Volver al menú anterior            ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");
            Console.WriteLine();
            Console.Write(" Seleccione una opcion: ");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    GestionarIngresarStockRepuesto(userEmpleado);
                    break;
                case "2":
                    GestionarDespacharRepuesto(userEmpleado);
                    break;
                case "0":
                    salir = true;
                    break;
                default:
                    Console.WriteLine("\n❌ [Error] Opción no válida. Presione una tecla.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    // ==========================================
    // SUBMENÚ DE INSUMOS (EMPLEADO)
    // ==========================================
    static void SubMenuInsumosEmpleado(Usuarios userEmpleado)
    {
        bool salir = false;
        while (!salir)
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║                  INSUMOS                 ║");
            Console.WriteLine("╠══════════════════════════════════════════╣");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║    1. Ingresar stock de insumo           ║");
            Console.WriteLine("║    2. Despachar insumo                   ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║    0. Volver al menú anterior            ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");
            Console.WriteLine();
            Console.Write(" Seleccione una opcion: ");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    GestionarIngresarStockInsumo(userEmpleado);
                    break;
                case "2":
                    GestionarDespacharInsumo(userEmpleado);
                    break;
                case "0":
                    salir = true;
                    break;
                default:
                    Console.WriteLine("\n❌ [Error] Opción no válida. Presione una tecla.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    // ==========================================
    // SUBMENÚ DE INVENTARIO / HISTORIAL (ADMIN)
    // ==========================================
    static void MenuInventarioHistorial()
    {
        bool salirMenuInvHist = false;
        while (!salirMenuInvHist)
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║          INVENTARIO/HISTORIAL            ║");
            Console.WriteLine("╠══════════════════════════════════════════╣");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║    1. Ver inventario                     ║");
            Console.WriteLine("║    2. Ver historial                      ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║    0. Volver al menú anterior            ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");
            Console.WriteLine();
            Console.Write(" Seleccione una opcion: ");
            string gestionElegidaInvHist = Console.ReadLine();

            switch (gestionElegidaInvHist)
            {
                case "1":
                    MenuInventario();
                    break;
                case "2":
                    MenuHistorial();
                    break;
                case "0":
                    salirMenuInvHist = true;
                    break;
                default:
                    Console.WriteLine("\n❌ [Error] Opción no válida. Presione una tecla.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void MenuInventario()
    {
        bool salirMenuInventario = false;
        while (!salirMenuInventario)
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║            GESTION DE STOCK              ║");
            Console.WriteLine("╠══════════════════════════════════════════╣");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║    INVENTARIO                            ║");
            Console.WriteLine("║    1. Ver repuestos                      ║");
            Console.WriteLine("║    2. Ver insumos                        ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║    0. Volver al menú anterior            ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");
            Console.WriteLine();
            Console.Write(" Seleccione una opcion: ");
            string opcionInventario = Console.ReadLine();

            if (opcionInventario == "1")
            {
                MostrarTablaRepuestos();
            }
            else if (opcionInventario == "2")
            {
                MostrarTablaInsumos();
            }
            else if (opcionInventario == "0")
            {
                salirMenuInventario = true;
            }
            else
            {
                Console.WriteLine("\n❌ [Error] Opción no válida. Presione una tecla.");
                Console.ReadKey();
            }
        }
    }

    static void MenuHistorial()
    {
        Console.Clear();
        DatosHistorial dbHistorial = new DatosHistorial();
        List<Historial> lista = dbHistorial.ObtenerTodoElHistorial();

        Console.WriteLine("HISTORIAL DE MOVIMIENTOS");
        Console.WriteLine("┌──────────────────┬─────────────────────┬──────────────────────┬────────────────────────────────────────┐");
        Console.WriteLine($"│ {"Tipo",-16} │ {"Fecha y Hora",-19} │ {"Usuario",-20} │ {"Elemento",-38} │");
        Console.WriteLine("├──────────────────┼─────────────────────┼──────────────────────┼────────────────────────────────────────┤");

        if (lista.Count == 0)
        {
            Console.WriteLine($"│ {"No hay movimientos registrados.",-79} │");
        }
        else
        {
            foreach (var mov in lista)
            {
                string tipo = string.IsNullOrEmpty(mov.TipoMovimiento) ? "" : (mov.TipoMovimiento.Length > 16 ? mov.TipoMovimiento.Substring(0, 13) + "..." : mov.TipoMovimiento);
                string fecha = mov.FechaHora.ToString("dd/MM/yyyy HH:mm");
                string usuario = string.IsNullOrEmpty(mov.NombreUsuario) ? "" : (mov.NombreUsuario.Length > 20 ? mov.NombreUsuario.Substring(0, 17) + "..." : mov.NombreUsuario);
                string elemento = string.IsNullOrEmpty(mov.NombreElemento) ? "" : (mov.NombreElemento.Length > 38 ? mov.NombreElemento.Substring(0, 35) + "..." : mov.NombreElemento);

                Console.WriteLine($"│ {tipo,-16} │ {fecha,-19} │ {usuario,-20} │ {elemento,-38} │");
            }
        }

        Console.WriteLine("└──────────────────┴─────────────────────┴──────────────────────┴────────────────────────────────────────┘");
        Console.WriteLine("\nPresione una tecla para volver...");
        Console.ReadKey();
    }
    static void MostrarTablaRepuestos()
    {
        Console.Clear();
        DatosRepuestos dbRepuestos = new DatosRepuestos();
        List<Repuestos> repuestos = dbRepuestos.ObtenerTodosLosRepuestos();

        Console.WriteLine("REPUESTOS");
        Console.WriteLine("┌──────────────────────────────┬─────────────┐");
        Console.WriteLine($"│ {"Nombre",-28} │ {"Cant. Disp.",-11} │");
        Console.WriteLine("├──────────────────────────────┼─────────────┤");

        List<string> alertasStock = new List<string>();

        if (repuestos.Count == 0)
        {
            Console.WriteLine($"│ {"No hay repuestos registrados.",-42} │");
        }
        else
        {
            foreach (var repuesto in repuestos)
            {
                string nombreRepuesto = repuesto.Nombre.Length > 28 ? repuesto.Nombre.Substring(0, 25) + "..." : repuesto.Nombre;
                Console.WriteLine($"│ {nombreRepuesto,-28} │ {repuesto.CantidadDisponible,-11} │");

                if (repuesto.CantidadDisponible <= repuesto.StockMinimo)
                {
                    alertasStock.Add($"⚠️ {repuesto.Nombre} (Actual: {repuesto.CantidadDisponible} | Mínimo: {repuesto.StockMinimo})");
                }
            }
        }

        Console.WriteLine("└──────────────────────────────┴─────────────┘");

        if (alertasStock.Count > 0)
        {
            Console.WriteLine("\n🚨 ¡ATENCIÓN! Productos por debajo del stock mínimo:");
            foreach (var alerta in alertasStock)
            {
                Console.WriteLine($"   {alerta}");
            }
        }
        else
        {
            Console.WriteLine("\n✅ Todos los repuestos tienen niveles de stock saludables.");
        }

        Console.WriteLine("\nPresione una tecla para volver...");
        Console.ReadKey();
    }

    static void MostrarTablaInsumos()
    {
        Console.Clear();
        DatosInsumos dbInsumos = new DatosInsumos();
        List<Insumos> insumos = dbInsumos.ObtenerTodosLosInsumos();

        Console.WriteLine("INSUMOS");
        Console.WriteLine("┌──────────────────────────────┬─────────────┐");
        Console.WriteLine($"│ {"Nombre",-28} │ {"Cant. Disp.",-11} │");
        Console.WriteLine("├──────────────────────────────┼─────────────┤");

        List<string> alertasStock = new List<string>();

        if (insumos.Count == 0)
        {
            Console.WriteLine($"│ {"No hay insumos registrados.",-42} │");
        }
        else
        {
            foreach (var insumo in insumos)
            {
                string nombreInsumo = insumo.Nombre.Length > 28 ? insumo.Nombre.Substring(0, 25) + "..." : insumo.Nombre;
                Console.WriteLine($"│ {nombreInsumo,-28} │ {insumo.CantidadDisponible,-11} │");

                if (insumo.CantidadDisponible <= insumo.StockMinimo)
                {
                    alertasStock.Add($"⚠️ {insumo.Nombre} (Actual: {insumo.CantidadDisponible} | Mínimo: {insumo.StockMinimo})");
                }
            }
        }

        Console.WriteLine("└──────────────────────────────┴─────────────┘");

        if (alertasStock.Count > 0)
        {
            Console.WriteLine("\n🚨 ¡ATENCIÓN! Insumos por debajo del stock mínimo:");
            foreach (var alerta in alertasStock)
            {
                Console.WriteLine($"   {alerta}");
            }
        }
        else
        {
            Console.WriteLine("\n✅ Todos los insumos tienen niveles de stock saludables.");
        }

        Console.WriteLine("\nPresione una tecla para volver...");
        Console.ReadKey();
    }

    // ==========================================
    // SUBMENÚ DE REPUESTOS (ADMIN)
    // ==========================================
    static void SubMenuRepuestos(Usuarios userAdmin)
    {
        bool salirSubMenuRepuestos = false;
        while (!salirSubMenuRepuestos)
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║                 REPUESTOS                ║");
            Console.WriteLine("╠══════════════════════════════════════════╣");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║    1. Agregar repuesto                   ║");
            Console.WriteLine("║    2. Ingresar stock de repuesto         ║");
            Console.WriteLine("║    3. Despachar repuesto                 ║");
            Console.WriteLine("║    4. Actualizar repuesto                ║");
            Console.WriteLine("║    5. Eliminar repuesto                  ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║    0. Volver al menú anterior            ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");
            Console.WriteLine();
            Console.Write(" Seleccione una opcion: ");
            string gestionElegidaSubMenuRepuestos = Console.ReadLine();

            switch (gestionElegidaSubMenuRepuestos)
            {
                case "1":
                    GestionarAgregarRepuesto(userAdmin);
                    break;
                case "2":
                    GestionarIngresarStockRepuesto(userAdmin);
                    break;
                case "3":
                    GestionarDespacharRepuesto(userAdmin);
                    break;
                case "4":
                    GestionarActualizarRepuesto(userAdmin);
                    break;
                case "5":
                    GestionarEliminarRepuesto(userAdmin);
                    break;
                case "0":
                    salirSubMenuRepuestos = true;
                    break;
                default:
                    Console.WriteLine("\n❌ [Error] Opción no válida. Presione una tecla.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    // ==========================================
    // SUBMENÚ DE INSUMOS (ADMIN)
    // ==========================================
    static void SubMenuInsumos(Usuarios userAdmin)
    {
        bool salirSubMenuInsumos = false;
        while (!salirSubMenuInsumos)
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║                  INSUMOS                 ║");
            Console.WriteLine("╠══════════════════════════════════════════╣");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║    1. Agregar insumo                     ║");
            Console.WriteLine("║    2. Ingresar stock de insumo           ║");
            Console.WriteLine("║    3. Despachar insumo                   ║");
            Console.WriteLine("║    4. Actualizar insumo                  ║");
            Console.WriteLine("║    5. Eliminar insumo                    ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║    0. Volver al menú anterior            ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");
            Console.WriteLine();
            Console.Write(" Seleccione una opcion: ");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    GestionarAgregarInsumo(userAdmin);
                    break;
                case "2":
                    GestionarIngresarStockInsumo(userAdmin);
                    break;
                case "3":
                    GestionarDespacharInsumo(userAdmin);
                    break;
                case "4":
                    GestionarActualizarInsumo(userAdmin);
                    break;
                case "5":
                    GestionarEliminarInsumo(userAdmin);
                    break;
                case "0":
                    salirSubMenuInsumos = true;
                    break;
                default:
                    Console.WriteLine("\n❌ [Error] Opción no válida. Presione una tecla.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    // ==========================================
    // FUNCIÓN AUXILIAR PARA HISTORIAL
    // ==========================================
    static void RegistrarMovimiento(int idUsuario, string tipoMovimiento, int? idRepuesto, int? idInsumo)
    {
        Historial nuevoMov = new Historial();
        nuevoMov.TipoMovimiento = tipoMovimiento;
        nuevoMov.FechaHora = DateTime.Now;
        nuevoMov.IdUsuario = idUsuario;
        nuevoMov.IdRepuesto = idRepuesto;
        nuevoMov.IdInsumo = idInsumo;

        DatosHistorial dbHistorial = new DatosHistorial();
        dbHistorial.GuardarMovimientos(nuevoMov);
    }

    // ==========================================
    // GESTIONES DE REPUESTOS
    // ==========================================
    static void GestionarAgregarRepuesto(Usuarios userLogueado)
    {
        bool salirAgregarRepuesto = false;

        while (!salirAgregarRepuesto)
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║             AGREGAR REPUESTO             ║");
            Console.WriteLine("╠══════════════════════════════════════════╣");
            Console.WriteLine("║                                          ║");

            Console.Write(" Nombre: ");
            string nombre = Console.ReadLine();

            Console.Write(" Cantidad inicial: ");
            if (!int.TryParse(Console.ReadLine(), out int cantidad)) cantidad = 0;

            Console.Write(" Stock mínimo: ");
            if (!int.TryParse(Console.ReadLine(), out int stockMinimo)) stockMinimo = 0;

            Console.WriteLine("║                                          ║");
            Console.WriteLine("╠══════════════════════════════════════════╣");
            Console.WriteLine("║    1. Confirmar registro                 ║");
            Console.WriteLine("║    0. Cancelar                           ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");

            Console.Write("Seleccione una opcion: ");
            string opcionAgregarRepuesto = Console.ReadLine();

            if (opcionAgregarRepuesto == "1")
            {
                Repuestos nuevoRepuesto = new Repuestos();
                nuevoRepuesto.Nombre = nombre;
                nuevoRepuesto.CantidadDisponible = cantidad;
                nuevoRepuesto.StockMinimo = stockMinimo;

                DatosRepuestos dbRepuestos = new DatosRepuestos();
                int filasAfectadas = dbRepuestos.GuardarRepuestos(nuevoRepuesto);

                if (filasAfectadas > 0)
                {
                    Console.WriteLine("\n✅ Repuesto agregado exitosamente.");
                    Repuestos repuestoGuardado = dbRepuestos.ObtenerRepuestoPorNombre(nombre);
                    int? idRepuestoObtenido = repuestoGuardado != null ? repuestoGuardado.IdRepuesto : (int?)null;

                    RegistrarMovimiento(userLogueado.IdUsuario, "Entrada", idRepuestoObtenido, null);
                }
                else
                {
                    Console.WriteLine("\n❌ No se pudo registrar el repuesto.");
                }

                Console.WriteLine("\n¿Qué desea hacer?");
                Console.WriteLine("1. Agregar otro repuesto");
                Console.WriteLine("0. Volver al menu anterior");
                Console.Write("Seleccione una opcion: ");
                if (Console.ReadLine() == "0")
                {
                    salirAgregarRepuesto = true;
                }
            }
            else if (opcionAgregarRepuesto == "0")
            {
                salirAgregarRepuesto = true;
            }
            else
            {
                Console.WriteLine("\n❌ Opción incorrecta. Presione una tecla.");
                Console.ReadKey();
            }
        }
    }

    static void GestionarIngresarStockRepuesto(Usuarios userLogueado)
    {
        bool salir = false;
        while (!salir)
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║       INGRESAR STOCK DE REPUESTO         ║");
            Console.WriteLine("╠══════════════════════════════════════════╣");
            Console.WriteLine("║    1. Buscar y sumar stock               ║");
            Console.WriteLine("║    0. Volver al menú anterior            ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");
            Console.Write(" Seleccione una opción: ");

            string opcion = Console.ReadLine();

            if (opcion == "1")
            {
                Console.Write("\nIngrese el nombre exacto del repuesto: ");
                string nombreBuscado = Console.ReadLine();

                DatosRepuestos dbRepuestos = new DatosRepuestos();
                Repuestos repuestoEncontrado = dbRepuestos.ObtenerRepuestoPorNombre(nombreBuscado);

                if (repuestoEncontrado != null)
                {
                    Console.WriteLine($"\n[Encontrado] {repuestoEncontrado.Nombre} | Stock actual: {repuestoEncontrado.CantidadDisponible}");
                    Console.Write("Ingrese la cantidad a AGREGAR: ");

                    if (int.TryParse(Console.ReadLine(), out int cantidadAgregada) && cantidadAgregada > 0)
                    {
                        repuestoEncontrado.CantidadDisponible += cantidadAgregada;
                        int filasAfectadas = dbRepuestos.ActualizarRepuesto(repuestoEncontrado);

                        if (filasAfectadas > 0)
                        {
                            Console.WriteLine($"\n✅ Stock actualizado con éxito. Nuevo total: {repuestoEncontrado.CantidadDisponible}");
                            RegistrarMovimiento(userLogueado.IdUsuario, "Entrada", repuestoEncontrado.IdRepuesto, null);
                        }
                        else
                        {
                            Console.WriteLine("\n⚠️ No se pudo actualizar el stock en la base de datos.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\n❌ [Error] Cantidad no válida.");
                    }
                }
                else
                {
                    Console.WriteLine("\n❌ [Error] No se encontró ningún repuesto con ese nombre.");
                }

                Console.WriteLine("\nPresione una tecla para continuar...");
                Console.ReadKey();
            }
            else if (opcion == "0")
            {
                salir = true;
            }
            else
            {
                Console.WriteLine("\n❌ [Error] Opción no válida. Presione una tecla.");
                Console.ReadKey();
            }
        }
    }

    static void GestionarDespacharRepuesto(Usuarios userLogueado)
    {
        bool salirDespacharRepuesto = false;
        while (!salirDespacharRepuesto)
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║           DESPACHAR REPUESTO             ║");
            Console.WriteLine("╠══════════════════════════════════════════╣");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║    1. Confirmar despacho                 ║");
            Console.WriteLine("║    0. Cancelar / Volver                  ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");
            Console.Write(" Seleccione una opcion: ");

            string opcionDespachar = Console.ReadLine();

            if (opcionDespachar == "1")
            {
                Console.Write("\nIngrese el nombre exacto del repuesto a despachar: ");
                string nombreBuscado = Console.ReadLine();

                DatosRepuestos dbRepuestos = new DatosRepuestos();
                Repuestos repuestoEncontrado = dbRepuestos.ObtenerRepuestoPorNombre(nombreBuscado);

                if (repuestoEncontrado != null)
                {
                    Console.WriteLine($"\nRepuesto encontrado: {repuestoEncontrado.Nombre}");
                    Console.WriteLine($"Stock disponible actual: {repuestoEncontrado.CantidadDisponible}");

                    Console.Write("\nIngrese la cantidad a despachar: ");
                    if (int.TryParse(Console.ReadLine(), out int cantidadADespachar) && cantidadADespachar > 0)
                    {
                        if (cantidadADespachar <= repuestoEncontrado.CantidadDisponible)
                        {
                            repuestoEncontrado.CantidadDisponible -= cantidadADespachar;
                            int filasAfectadas = dbRepuestos.ActualizarRepuesto(repuestoEncontrado);

                            if (filasAfectadas > 0)
                            {
                                Console.WriteLine("\n✅ Despacho realizado exitosamente.");
                                RegistrarMovimiento(userLogueado.IdUsuario, "Salida", repuestoEncontrado.IdRepuesto, null);

                                if (repuestoEncontrado.CantidadDisponible <= repuestoEncontrado.StockMinimo)
                                {
                                    Console.WriteLine("\n════════════════════════════════════════════════════════");
                                    Console.WriteLine($"⚠️ ¡ATENCIÓN! El repuesto quedó por debajo del stock mínimo:");
                                    Console.WriteLine($"   Repuesto: {repuestoEncontrado.Nombre}");
                                    Console.WriteLine($"   Stock Actual: {repuestoEncontrado.CantidadDisponible} | Mínimo requerido: {repuestoEncontrado.StockMinimo}");
                                    Console.WriteLine("════════════════════════════════════════════════════════");
                                }
                            }
                            else
                            {
                                Console.WriteLine("\n⚠️ No se pudo actualizar el stock del repuesto.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\n❌ [Error] No hay suficiente stock disponible para realizar este despacho.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\n❌ [Error] Cantidad no válida.");
                    }
                }
                else
                {
                    Console.WriteLine("\n❌ [Error] No se encontró ningún repuesto con ese nombre.");
                }

                Console.WriteLine("\n¿Qué desea hacer?");
                Console.WriteLine("1. Despachar otro repuesto");
                Console.WriteLine("0. Volver al menú anterior");
                Console.Write("Seleccione una opción: ");
                if (Console.ReadLine() == "0") salirDespacharRepuesto = true;
            }
            else if (opcionDespachar == "0")
            {
                salirDespacharRepuesto = true;
            }
            else
            {
                Console.WriteLine("\n❌ [Error] Opción no válida. Presione una tecla.");
                Console.ReadKey();
            }
        }
    }

    static void GestionarActualizarRepuesto(Usuarios userAdmin)
    {
        bool salirActualizarRepuesto = false;
        while (!salirActualizarRepuesto)
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║           ACTUALIZAR REPUESTO            ║");
            Console.WriteLine("╠══════════════════════════════════════════╣");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║    1. Confirmar actualización            ║");
            Console.WriteLine("║    0. Cancelar / Volver                  ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");
            Console.Write(" Seleccione una opcion: ");

            string opcionActRep = Console.ReadLine();

            if (opcionActRep == "1")
            {
                Console.Write("\nIngrese el ID del repuesto a actualizar: ");
                if (int.TryParse(Console.ReadLine(), out int idRepuesto))
                {
                    Console.Write("Ingrese el nuevo Nombre: ");
                    string nuevoNombre = Console.ReadLine();

                    Console.Write("Ingrese la nueva Cantidad disponible: ");
                    if (int.TryParse(Console.ReadLine(), out int nuevaCantidad))
                    {
                        Console.Write("Ingrese el nuevo Stock mínimo: ");
                        if (int.TryParse(Console.ReadLine(), out int nuevoStockMin))
                        {
                            Repuestos repuestoAct = new Repuestos();
                            repuestoAct.IdRepuesto = idRepuesto;
                            repuestoAct.Nombre = nuevoNombre;
                            repuestoAct.CantidadDisponible = nuevaCantidad;
                            repuestoAct.StockMinimo = nuevoStockMin;

                            DatosRepuestos dbRepuestos = new DatosRepuestos();
                            int filasAfectadas = dbRepuestos.ActualizarRepuesto(repuestoAct);

                            if (filasAfectadas > 0)
                            {
                                Console.WriteLine("\n✅ Repuesto actualizado exitosamente.");
                                RegistrarMovimiento(userAdmin.IdUsuario, "Modificación", idRepuesto, null);
                            }
                            else
                            {
                                Console.WriteLine("\n⚠️ No se encontró ningún repuesto con ese ID.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\n❌ [Error] Stock mínimo no válido.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\n❌ [Error] Cantidad no válida.");
                    }
                }
                else
                {
                    Console.WriteLine("\n❌ [Error] ID no válido.");
                }

                Console.WriteLine("\n¿Qué desea hacer?");
                Console.WriteLine("1. Actualizar otro repuesto");
                Console.WriteLine("0. Volver al menú anterior");
                Console.Write("Seleccione una opción: ");
                if (Console.ReadLine() == "0") salirActualizarRepuesto = true;
            }
            else if (opcionActRep == "0")
            {
                salirActualizarRepuesto = true;
            }
            else
            {
                Console.WriteLine("\n❌ [Error] Opción no válida. Presione una tecla.");
                Console.ReadKey();
            }
        }
    }

    static void GestionarEliminarRepuesto(Usuarios userAdmin)
    {
        bool salirEliminarRepuesto = false;
        while (!salirEliminarRepuesto)
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║            ELIMINAR REPUESTO             ║");
            Console.WriteLine("╠══════════════════════════════════════════╣");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║    1. Confirmar eliminación              ║");
            Console.WriteLine("║    0. Cancelar / Volver                  ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");
            Console.Write(" Seleccione una opcion: ");

            string opcionEliminarRep = Console.ReadLine();

            if (opcionEliminarRep == "1")
            {
                Console.Write("\nIngrese el ID del repuesto a eliminar: ");
                if (int.TryParse(Console.ReadLine(), out int idRepuesto))
                {
                    DatosRepuestos dbRepuestos = new DatosRepuestos();
                    int filasAfectadas = dbRepuestos.EliminarRepuesto(idRepuesto);

                    if (filasAfectadas > 0)
                    {
                        Console.WriteLine("\n✅ Repuesto dado de baja exitosamente.");
                        RegistrarMovimiento(userAdmin.IdUsuario, "Baja", idRepuesto, null);
                    }
                    else
                    {
                        Console.WriteLine("\n⚠️ No se encontró ningún repuesto con ese ID.");
                    }
                }
                else
                {
                    Console.WriteLine("\n❌ [Error] ID no válido.");
                }

                Console.WriteLine("\n¿Qué desea hacer?");
                Console.WriteLine("1. Eliminar otro repuesto");
                Console.WriteLine("0. Volver al menú anterior");
                Console.Write("Seleccione una opción: ");
                if (Console.ReadLine() == "0") salirEliminarRepuesto = true;
            }
            else if (opcionEliminarRep == "0")
            {
                salirEliminarRepuesto = true;
            }
            else
            {
                Console.WriteLine("\n❌ [Error] Opción no válida. Presione una tecla.");
                Console.ReadKey();
            }
        }
    }

    // ==========================================
    // GESTIONES DE INSUMOS
    // ==========================================
    static void GestionarAgregarInsumo(Usuarios userLogueado)
    {
        bool salirAgregarInsumo = false;
        while (!salirAgregarInsumo)
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║              AGREGAR INSUMO              ║");
            Console.WriteLine("╠══════════════════════════════════════════╣");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║    1. Confirmar registro                 ║");
            Console.WriteLine("║    0. Cancelar / Volver                  ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");
            Console.Write(" Seleccione una opcion: ");

            string opcionAgregarInsumo = Console.ReadLine();

            if (opcionAgregarInsumo == "1")
            {
                Console.Write("\nIngrese el Nombre del insumo: ");
                string nombre = Console.ReadLine();

                Console.Write("Ingrese la cantidad inicial: ");
                if (int.TryParse(Console.ReadLine(), out int cantidad))
                {
                    Console.Write("Ingrese la unidad de medida (ej: litros, kg, unidades): ");
                    string unidadMedida = Console.ReadLine();

                    Console.Write("Ingrese el stock mínimo: ");
                    if (int.TryParse(Console.ReadLine(), out int stockMinimo))
                    {
                        Insumos nuevoInsumo = new Insumos();
                        nuevoInsumo.Nombre = nombre;
                        nuevoInsumo.CantidadDisponible = cantidad;
                        nuevoInsumo.UnidadMedida = unidadMedida;
                        nuevoInsumo.StockMinimo = stockMinimo;

                        DatosInsumos dbInsumos = new DatosInsumos();
                        int filasAfectadas = dbInsumos.GuardarInsumos(nuevoInsumo);

                        if (filasAfectadas > 0)
                        {
                            Console.WriteLine("\n✅ Insumo agregado exitosamente.");
                            Insumos insumoGuardado = dbInsumos.ObtenerInsumoPorNombre(nombre);
                            int? idInsumoObtenido = insumoGuardado != null ? insumoGuardado.IdInsumo : (int?)null;

                            RegistrarMovimiento(userLogueado.IdUsuario, "Entrada", null, idInsumoObtenido);
                        }
                        else
                        {
                            Console.WriteLine("\n⚠️ No se pudo registrar el insumo.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\n❌ [Error] Stock mínimo no válido.");
                    }
                }
                else
                {
                    Console.WriteLine("\n❌ [Error] Cantidad no válida.");
                }

                Console.WriteLine("\n¿Qué desea hacer?");
                Console.WriteLine("1. Agregar otro insumo");
                Console.WriteLine("0. Volver al menú anterior");
                Console.Write("Seleccione una opción: ");
                if (Console.ReadLine() == "0") salirAgregarInsumo = true;
            }
            else if (opcionAgregarInsumo == "0")
            {
                salirAgregarInsumo = true;
            }
            else
            {
                Console.WriteLine("\n❌ [Error] Opción no válida. Presione una tecla.");
                Console.ReadKey();
            }
        }
    }

    static void GestionarIngresarStockInsumo(Usuarios userLogueado)
    {
        bool salir = false;
        while (!salir)
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║        INGRESAR STOCK DE INSUMO          ║");
            Console.WriteLine("╠══════════════════════════════════════════╣");
            Console.WriteLine("║    1. Buscar y sumar stock               ║");
            Console.WriteLine("║    0. Volver al menú anterior            ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");
            Console.Write(" Seleccione una opción: ");

            string opcion = Console.ReadLine();

            if (opcion == "1")
            {
                Console.Write("\nIngrese el nombre exacto del insumo: ");
                string nombreBuscado = Console.ReadLine();

                DatosInsumos dbInsumos = new DatosInsumos();
                Insumos insumoEncontrado = dbInsumos.ObtenerInsumoPorNombre(nombreBuscado);

                if (insumoEncontrado != null)
                {
                    Console.WriteLine($"\n[Encontrado] {insumoEncontrado.Nombre} | Stock actual: {insumoEncontrado.CantidadDisponible} {insumoEncontrado.UnidadMedida}");
                    Console.Write("Ingrese la cantidad a AGREGAR: ");

                    if (int.TryParse(Console.ReadLine(), out int cantidadAgregada) && cantidadAgregada > 0)
                    {
                        insumoEncontrado.CantidadDisponible += cantidadAgregada;
                        int filasAfectadas = dbInsumos.ActualizarInsumo(insumoEncontrado);

                        if (filasAfectadas > 0)
                        {
                            Console.WriteLine($"\n✅ Stock de insumo actualizado con éxito. Nuevo total: {insumoEncontrado.CantidadDisponible} {insumoEncontrado.UnidadMedida}");
                            RegistrarMovimiento(userLogueado.IdUsuario, "Entrada", null, insumoEncontrado.IdInsumo);
                        }
                        else
                        {
                            Console.WriteLine("\n⚠️ No se pudo actualizar el stock del insumo.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\n❌ [Error] Cantidad no válida.");
                    }
                }
                else
                {
                    Console.WriteLine("\n❌ [Error] No se encontró ningún insumo con ese nombre.");
                }

                Console.WriteLine("\nPresione una tecla para continuar...");
                Console.ReadKey();
            }
            else if (opcion == "0")
            {
                salir = true;
            }
            else
            {
                Console.WriteLine("\n❌ [Error] Opción no válida. Presione una tecla.");
                Console.ReadKey();
            }
        }
    }

    static void GestionarDespacharInsumo(Usuarios userLogueado)
    {
        bool salirDespacharInsumo = false;
        while (!salirDespacharInsumo)
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║            DESPACHAR INSUMO              ║");
            Console.WriteLine("╠══════════════════════════════════════════╣");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║    1. Confirmar despacho                 ║");
            Console.WriteLine("║    0. Cancelar / Volver                  ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");
            Console.Write(" Seleccione una opcion: ");

            string opcionDespacharInsumo = Console.ReadLine();

            if (opcionDespacharInsumo == "1")
            {
                Console.Write("\nIngrese el nombre exacto del insumo a despachar: ");
                string nombreBuscado = Console.ReadLine();

                DatosInsumos dbInsumos = new DatosInsumos();
                Insumos insumoEncontrado = dbInsumos.ObtenerInsumoPorNombre(nombreBuscado);

                if (insumoEncontrado != null)
                {
                    Console.WriteLine($"\nInsumo encontrado: {insumoEncontrado.Nombre}");
                    Console.WriteLine($"Stock disponible actual: {insumoEncontrado.CantidadDisponible} {insumoEncontrado.UnidadMedida}");

                    Console.Write("\nIngrese la cantidad a despachar: ");
                    if (int.TryParse(Console.ReadLine(), out int cantidadADespachar) && cantidadADespachar > 0)
                    {
                        if (cantidadADespachar <= insumoEncontrado.CantidadDisponible)
                        {
                            insumoEncontrado.CantidadDisponible -= cantidadADespachar;
                            int filasAfectadas = dbInsumos.ActualizarInsumo(insumoEncontrado);

                            if (filasAfectadas > 0)
                            {
                                Console.WriteLine("\n✅ Despacho de insumo realizado exitosamente.");
                                RegistrarMovimiento(userLogueado.IdUsuario, "Salida", null, insumoEncontrado.IdInsumo);

                                if (insumoEncontrado.CantidadDisponible <= insumoEncontrado.StockMinimo)
                                {
                                    Console.WriteLine("\n════════════════════════════════════════════════════════");
                                    Console.WriteLine($"⚠️ ¡ATENCIÓN! El insumo quedó por debajo del stock mínimo:");
                                    Console.WriteLine($"   Insumo: {insumoEncontrado.Nombre}");
                                    Console.WriteLine($"   Stock Actual: {insumoEncontrado.CantidadDisponible} {insumoEncontrado.UnidadMedida} | Mínimo: {insumoEncontrado.StockMinimo} {insumoEncontrado.UnidadMedida}");
                                    Console.WriteLine("════════════════════════════════════════════════════════");
                                }
                            }
                            else
                            {
                                Console.WriteLine("\n⚠️ No se pudo actualizar el stock del insumo.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\n❌ [Error] No hay suficiente stock disponible para realizar este despacho.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\n❌ [Error] Cantidad no válida.");
                    }
                }
                else
                {
                    Console.WriteLine("\n❌ [Error] No se encontró ningún insumo con ese nombre.");
                }

                Console.WriteLine("\n¿Qué desea hacer?");
                Console.WriteLine("1. Despachar otro insumo");
                Console.WriteLine("0. Volver al menú anterior");
                Console.Write("Seleccione una opción: ");
                if (Console.ReadLine() == "0") salirDespacharInsumo = true;
            }
            else if (opcionDespacharInsumo == "0")
            {
                salirDespacharInsumo = true;
            }
            else
            {
                Console.WriteLine("\n❌ [Error] Opción no válida. Presione una tecla.");
                Console.ReadKey();
            }
        }
    }

    static void GestionarActualizarInsumo(Usuarios userAdmin)
    {
        bool salirActualizarInsumo = false;
        while (!salirActualizarInsumo)
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║           ACTUALIZAR INSUMO              ║");
            Console.WriteLine("╠══════════════════════════════════════════╣");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║    1. Confirmar actualización            ║");
            Console.WriteLine("║    0. Cancelar / Volver                  ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");
            Console.Write(" Seleccione una opcion: ");

            string opcionActIns = Console.ReadLine();

            if (opcionActIns == "1")
            {
                Console.Write("\nIngrese el ID del insumo a actualizar: ");
                if (int.TryParse(Console.ReadLine(), out int idInsumo))
                {
                    Console.Write("Ingrese el nuevo Nombre: ");
                    string nuevoNombre = Console.ReadLine();

                    Console.Write("Ingrese la nueva Cantidad disponible: ");
                    if (int.TryParse(Console.ReadLine(), out int nuevaCantidad))
                    {
                        Console.Write("Ingrese la nueva Unidad de medida: ");
                        string nuevaMedida = Console.ReadLine();

                        Console.Write("Ingrese el nuevo Stock mínimo: ");
                        if (int.TryParse(Console.ReadLine(), out int nuevoStockMin))
                        {
                            Insumos insumoAct = new Insumos();
                            insumoAct.IdInsumo = idInsumo;
                            insumoAct.Nombre = nuevoNombre;
                            insumoAct.CantidadDisponible = nuevaCantidad;
                            insumoAct.UnidadMedida = nuevaMedida;
                            insumoAct.StockMinimo = nuevoStockMin;

                            DatosInsumos dbInsumos = new DatosInsumos();
                            int filasAfectadas = dbInsumos.ActualizarInsumo(insumoAct);

                            if (filasAfectadas > 0)
                            {
                                Console.WriteLine("\n✅ Insumo actualizado exitosamente.");
                                RegistrarMovimiento(userAdmin.IdUsuario, "Modificación", null, idInsumo);
                            }
                            else
                            {
                                Console.WriteLine("\n⚠️ No se encontró ningún insumo con ese ID.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\n❌ [Error] Stock mínimo no válido.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\n❌ [Error] Cantidad no válida.");
                    }
                }
                else
                {
                    Console.WriteLine("\n❌ [Error] ID no válido.");
                }

                Console.WriteLine("\n¿Qué desea hacer?");
                Console.WriteLine("1. Actualizar otro insumo");
                Console.WriteLine("0. Volver al menú anterior");
                Console.Write("Seleccione una opción: ");
                if (Console.ReadLine() == "0") salirActualizarInsumo = true;
            }
            else if (opcionActIns == "0")
            {
                salirActualizarInsumo = true;
            }
            else
            {
                Console.WriteLine("\n❌ [Error] Opción no válida. Presione una tecla.");
                Console.ReadKey();
            }
        }
    }

    static void GestionarEliminarInsumo(Usuarios userAdmin)
    {
        bool salirEliminarInsumo = false;
        while (!salirEliminarInsumo)
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║            ELIMINAR INSUMO               ║");
            Console.WriteLine("╠══════════════════════════════════════════╣");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║    1. Confirmar eliminación              ║");
            Console.WriteLine("║    0. Cancelar / Volver                  ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");
            Console.Write(" Seleccione una opcion: ");

            string opcionEliminarIns = Console.ReadLine();

            if (opcionEliminarIns == "1")
            {
                Console.Write("\nIngrese el ID del insumo a eliminar: ");
                if (int.TryParse(Console.ReadLine(), out int idInsumo))
                {
                    DatosInsumos dbInsumos = new DatosInsumos();
                    int filasAfectadas = dbInsumos.EliminarInsumo(idInsumo);

                    if (filasAfectadas > 0)
                    {
                        Console.WriteLine("\n✅ Insumo dado de baja exitosamente.");
                        RegistrarMovimiento(userAdmin.IdUsuario, "Baja", null, idInsumo);
                    }
                    else
                    {
                        Console.WriteLine("\n⚠️ No se encontró ningún insumo con ese ID.");
                    }
                }
                else
                {
                    Console.WriteLine("\n❌ [Error] ID no válido.");
                }

                Console.WriteLine("\n¿Qué desea hacer?");
                Console.WriteLine("1. Eliminar otro insumo");
                Console.WriteLine("0. Volver al menú anterior");
                Console.Write("Seleccione una opción: ");
                if (Console.ReadLine() == "0") salirEliminarInsumo = true;
            }
            else if (opcionEliminarIns == "0")
            {
                salirEliminarInsumo = true;
            }
            else
            {
                Console.WriteLine("\n❌ [Error] Opción no válida. Presione una tecla.");
                Console.ReadKey();
            }
        }
    }

    // ==========================================
    // GESTIÓN DE USUARIOS
    // ==========================================
    static void GestionarUsuarios(Usuarios userAdmin)
    {
        bool salirUsuarios = false;
        while (!salirUsuarios)
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║           GESTIÓN DE USUARIOS            ║");
            Console.WriteLine("╠══════════════════════════════════════════╣");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║    1. Agregar nuevo usuario              ║");
            Console.WriteLine("║    2. Eliminar usuario (Baja lógica)     ║");
            Console.WriteLine("║    0. Volver al menú anterior            ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");
            Console.Write(" Seleccione una opcion: ");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    EjecutarAltaUsuario();
                    break;
                case "2":
                    EjecutarBajaUsuario(userAdmin);
                    break;
                case "0":
                    salirUsuarios = true;
                    break;
                default:
                    Console.WriteLine("\n❌ [Error] Opción no válida. Presione una tecla.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void EjecutarAltaUsuario()
    {
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════════╗");
        Console.WriteLine("║           AGREGAR NUEVO USUARIO          ║");
        Console.WriteLine("╠══════════════════════════════════════════╣");
        Console.WriteLine("║                                          ║");

        Console.Write(" Ingresar Nombre completo: ");
        string nombre = Console.ReadLine();

        Console.Write(" Ingresar Nombre de usuario (login): ");
        string usuarioNombre = Console.ReadLine();

        Console.Write(" Ingresar DNI: ");
        if (int.TryParse(Console.ReadLine(), out int dni))
        {
            Console.Write(" Ingresar Contraseña: ");
            string contraseña = Console.ReadLine();

            Console.Write(" Ingresar Rol (Administrador / Empleado): ");
            string rol = Console.ReadLine();

            DatosUsuarios dbUsuarios = new DatosUsuarios();
            int estadoValidacion = dbUsuarios.VerificarUsuarioExistente(usuarioNombre, dni);

            switch (estadoValidacion)
            {
                case 1:
                    Console.WriteLine("\n❌ [Error] El nombre de usuario ya está en uso. Elija otro.");
                    break;
                case 2:
                    Console.WriteLine("\n❌ [Error] Ya existe un usuario registrado con ese DNI.");
                    break;
                case 0:
                    Usuarios nuevoUsuario = new Usuarios();
                    nuevoUsuario.Nombre = nombre;
                    nuevoUsuario.UsuarioNombre = usuarioNombre;
                    nuevoUsuario.Dni = dni;
                    nuevoUsuario.Contrasena = contraseña;
                    nuevoUsuario.Rol = rol;

                    int filas = dbUsuarios.GuardarUsuario(nuevoUsuario);
                    if (filas > 0)
                    {
                        Console.WriteLine("\n✅ ¡Usuario registrado exitosamente!");
                    }
                    else
                    {
                        Console.WriteLine("\n⚠️ No se pudo registrar el usuario.");
                    }
                    break;
                default:
                    Console.WriteLine("\n❌ [Error] Ocurrió un error inesperado.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("\n❌ [Error] DNI no válido.");
        }

        Console.WriteLine("\nPresione una tecla para continuar...");
        Console.ReadKey();
    }

    static void EjecutarBajaUsuario(Usuarios userAdmin)
    {
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════════╗");
        Console.WriteLine("║             ELIMINAR USUARIO             ║");
        Console.WriteLine("╠══════════════════════════════════════════╣");
        Console.WriteLine("║                                          ║");

        Console.Write(" Ingrese el ID del usuario a dar de baja: ");
        if (int.TryParse(Console.ReadLine(), out int idUsuarioBaja))
        {
            if (idUsuarioBaja == userAdmin.IdUsuario)
            {
                Console.WriteLine("\n❌ [Error] No puedes dar de baja tu propia cuenta de administrador activa.");
            }
            else
            {
                DatosUsuarios dbUsuarios = new DatosUsuarios();
                int filas = dbUsuarios.EliminarUsuario(idUsuarioBaja);

                if (filas > 0)
                {
                    Console.WriteLine("\n✅ Usuario dado de baja exitosamente.");
                }
                else
                {
                    Console.WriteLine("\n⚠️ No se encontró ningún usuario con ese ID.");
                }
            }
        }
        else
        {
            Console.WriteLine("\n❌ [Error] ID no válido.");
        }

        Console.WriteLine("\nPresione una tecla para continuar...");
        Console.ReadKey();
    }
}
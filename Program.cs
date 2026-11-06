using Microsoft.Data.SqlClient;
// Instanciamos la clase para poder utilizar los métodos:
Conexion conexion = new Conexion();

using (SqlConnection sql = conexion.obtenerConexion())
{
    sql.Open();
    Console.WriteLine("conexion exitosa");
}
InicioSesion();

// --- MÉTODOS
// FALTA:
// - CORRECCIÓN VISUAL DE MENÚS.
// - AGREGAR AVISO DE POCO STOCK.
// - REVISAR LAS VALIDACIONES EN CASO DE INGRESAR MAL CUALQUIER COSA.

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
        Console.WriteLine("║   1. Iniciar sesion                      ║");
        Console.WriteLine("║   2. Salir                               ║");
        Console.WriteLine("║                                          ║");
        Console.WriteLine("╚══════════════════════════════════════════╝");
        Console.Write(" Seleccione una opcion: ");
        string opcion = Console.ReadLine();

        switch (opcion)
        {
            case "1":
                Console.Clear();
                Console.WriteLine("=== ACCESO AL SISTEMA DE STOCK ===");

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
        Console.Write("║    Usuario: ");
        string usuarioIngresado = Console.ReadLine();
        Console.WriteLine("║                                          ║");
        Console.Write("║    Contraseña: ");

        // --- LÓGICA PARA OCULTAR LA CONTRASEÑA CON ASTERISCOS ---
        string contraseñaIngresada = "";
        ConsoleKeyInfo tecla;

        do
        {
            tecla = Console.ReadKey(true); // Lee la tecla sin mostrarla

            // Si presiona Backspace (borrar), eliminamos el último carácter si existe
            if (tecla.Key == ConsoleKey.Backspace && contraseñaIngresada.Length > 0)
            {
                contraseñaIngresada = contraseñaIngresada.Substring(0, contraseñaIngresada.Length - 1);
                // Borra el asterisco de la consola visualmente
                Console.Write("\b \b");
            }
            // Si es una tecla con carácter válido (letras, números, símbolos)
            else if (!char.IsControl(tecla.KeyChar))
            {
                contraseñaIngresada += tecla.KeyChar;
                Console.Write("*"); // Muestra el asterisco en lugar de la letra
            }
        } while (tecla.Key != ConsoleKey.Enter); // El bucle frena cuando presionan Enter

        Console.WriteLine();
        Console.WriteLine("║                                          ║");
        Console.WriteLine("╚══════════════════════════════════════════╝");

        DatosUsuarios usuarioDB = new DatosUsuarios();
        usuarioLogueado = usuarioDB.ObtenerUsuarioPorNombre(usuarioIngresado);

        if (usuarioLogueado != null && usuarioLogueado.Contrasena == contraseñaIngresada)
        {
            Console.WriteLine("\n✅ ¡Acceso concedido! Bienvenido, " + usuarioLogueado.Nombre);
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
            return usuarioLogueado;
        }
        else
        {
            Console.WriteLine("\n❌ [Error] Usuario o contraseña incorrectos.");
            usuarioLogueado = null;
        }
    }

    return usuarioLogueado;
}
static void Bienvenida(Usuarios userValidado)
{
    if (userValidado != null)
    {
        Console.WriteLine($"\n¡Bienvenido/a {userValidado.Nombre}!");

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
        Console.WriteLine("║   INVENTARIO / HISTORIAL                 ║");
        Console.WriteLine("║   1. Ver inventario                      ║");
        Console.WriteLine("║   2. Ver historial                       ║");
        Console.WriteLine("║                                          ║");
        Console.WriteLine("║   REPUESTOS                              ║");
        Console.WriteLine("║   3. Agregar repuesto                    ║");
        Console.WriteLine("║   4. Despachar repuesto                  ║");
        Console.WriteLine("║   5. Actualizar repuesto                 ║");
        Console.WriteLine("║   6. Eliminar repuesto                   ║");
        Console.WriteLine("║                                          ║");
        Console.WriteLine("║   INSUMOS                                ║");
        Console.WriteLine("║   7. Agregar insumo                      ║");
        Console.WriteLine("║   8. Despachar insumo                    ║");
        Console.WriteLine("║   9. Actualizar insumo                   ║");
        Console.WriteLine("║   10. Eliminar insumo                    ║");
        Console.WriteLine("║                                          ║");
        Console.WriteLine("║   USUARIOS                               ║");
        Console.WriteLine("║   11. Gestionar usuarios                 ║");
        Console.WriteLine("║                                          ║");
        Console.WriteLine("║   0. Salir                               ║");
        Console.WriteLine("║                                          ║");
        Console.WriteLine("╚══════════════════════════════════════════╝");
        Console.WriteLine();
        Console.Write(" Seleccione una opcion: ");
        string gestionElegidaAdmin = Console.ReadLine();

        switch (gestionElegidaAdmin)
        {
            case "1":
                MenuInventario();
                break;
            case "2":
                MenuHistorial();
                break;
            case "3":
                GestionarAgregarRepuesto(userAdmin);
                break;
            case "4":
                GestionarDespacharRepuesto(userAdmin);
                break;
            case "5":
                GestionarActualizarRepuesto(userAdmin);
                break;
            case "6":
                GestionarEliminarRepuesto(userAdmin);
                break;
            case "7":
                GestionarAgregarInsumo(userAdmin);
                break;
            case "8":
                GestionarDespacharInsumo(userAdmin);
                break;
            case "9":
                GestionarActualizarInsumo(userAdmin);
                break;
            case "10":
                GestionarEliminarInsumo(userAdmin);
                break;
            case "11":
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
static void MenuEmpleado(Usuarios userEmpleado)
{
    bool salirMenuEmpleado = false;

    while (!salirMenuEmpleado)
    {
        Console.Clear();
        Console.WriteLine("\n--- PANEL DE ADMINISTRADOR ---");
        Console.WriteLine($"Conectado como: {userEmpleado.Nombre} (DNI: {userEmpleado.Dni})");
        Console.WriteLine();

        Console.WriteLine("╔══════════════════════════════════════════╗");
        Console.WriteLine("║              MENÚ PRINCIPAL              ║");
        Console.WriteLine("╠══════════════════════════════════════════╣");
        Console.WriteLine("║                                          ║");
        Console.WriteLine("║   INVENTARIO                             ║");
        Console.WriteLine("║   1. Ver inventario                      ║");
        Console.WriteLine("║                                          ║");
        Console.WriteLine("║   REPUESTOS                              ║");
        Console.WriteLine("║   2. Agregar repuesto                    ║");
        Console.WriteLine("║   3. Despachar repuesto                  ║");
        Console.WriteLine("║                                          ║");
        Console.WriteLine("║   INSUMOS                                ║");
        Console.WriteLine("║   4. Agregar insumo                      ║");
        Console.WriteLine("║   5. Despachar insumo                    ║");
        Console.WriteLine("║                                          ║");
        Console.WriteLine("║   0. Salir                               ║");
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
                GestionarAgregarRepuesto(userEmpleado);
                break;
            case "3":
                GestionarDespacharRepuesto(userEmpleado);
                break;
            case "4":
                GestionarAgregarInsumo(userEmpleado);
                break;
            case "5":
                GestionarDespacharInsumo(userEmpleado);
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
// 2. SUBMENÚ DE INVENTARIO (Case 1)
// ==========================================
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
        Console.WriteLine("║   INVENTARIO                             ║");
        Console.WriteLine("║   1. Ver repuestos                       ║");
        Console.WriteLine("║   2. Ver insumos                         ║");
        Console.WriteLine("║                                          ║");
        Console.WriteLine("║   0. Volver al menú anterior             ║");
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
    }
}

static void MenuHistorial()
{
    Console.Clear();
    DatosHistorial dbHistorial = new DatosHistorial();
    List<Historial> lista = dbHistorial.ObtenerTodoElHistorial();

    Console.WriteLine("HISTORIAL DE MOVIMIENTOS");
    Console.WriteLine("┌──────────────────┬─────────────────────┬──────────────────────┬──────────────────────┐");
    Console.WriteLine($"│ {"Tipo",-16} │ {"Fecha y Hora",-19} │ {"Usuario",-20} │ {"Elemento",-40} │");
    Console.WriteLine("├──────────────────┼─────────────────────┼──────────────────────┼──────────────────────┤");

    if (lista.Count == 0)
    {
        Console.WriteLine($"│ {"No hay movimientos registrados.",-81} │");
    }
    else
    {
        foreach (var mov in lista)
        {
            string tipo = mov.TipoMovimiento.Length > 16 ? mov.TipoMovimiento.Substring(0, 13) + "..." : mov.TipoMovimiento;
            string fecha = mov.FechaHora.ToString("dd/MM/yyyy HH:mm");
            string usuario = mov.NombreUsuario.Length > 20 ? mov.NombreUsuario.Substring(0, 17) + "..." : mov.NombreUsuario;
            string elemento = mov.NombreElemento.Length > 40 ? mov.NombreElemento.Substring(0, 37) + "..." : mov.NombreElemento;

            Console.WriteLine($"│ {tipo,-16} │ {fecha,-19} │ {usuario,-20} │ {elemento,-40} │");
        }
    }

    Console.WriteLine("└──────────────────┴─────────────────────┴──────────────────────┴──────────────────────┘");
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
        }
    }
    Console.WriteLine("└──────────────────────────────┴─────────────┘");
    Console.WriteLine("\nPresione una tecla para volver...");
    Console.ReadKey();
}

static void MostrarTablaInsumos()
{
    Console.Clear();
    DatosInsumos dbInsumos = new DatosInsumos();
    List<Insumos> insumos = dbInsumos.ObtenerTodosLosInsumos();

    Console.WriteLine("INSUMOS");
    Console.WriteLine("┌──────────────────────────┬───────────┬───────────┐");
    Console.WriteLine($"│ {"Nombre",-24} │ {"Cant.",-9} │ {"Medida",-9} │");
    Console.WriteLine("├──────────────────────────┼───────────┼───────────┤");

    if (insumos.Count == 0)
    {
        Console.WriteLine($"│ {"❌ No hay insumos registrados.",-46} │");
    }
    else
    {
        foreach (var insumo in insumos)
        {
            string nombreInsumo = insumo.Nombre.Length > 24 ? insumo.Nombre.Substring(0, 21) + "..." : insumo.Nombre;
            Console.WriteLine($"│ {nombreInsumo,-24} │ {insumo.CantidadDisponible,-9} │ {insumo.UnidadMedida,-9} │");
        }
    }
    Console.WriteLine("└──────────────────────────┴───────────┴───────────┘");
    Console.WriteLine("\nPresione una tecla para volver...");
    Console.ReadKey();
}

// ==========================================
// 3. FUNCIÓN AUXILIAR PARA HISTORIAL
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
// 4. MÉTODOS DE GESTIÓN (CASOS 2 al 9)
// ==========================================

// Case 2: Agregar Repuesto
static void GestionarAgregarRepuesto(Usuarios userAdmin)
{
    string opcionAgregarRepuesto;
    bool salirAgregarRepuesto = false;

    while (!salirAgregarRepuesto)
    {
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════════╗");
        Console.WriteLine("║             AGREGAR REPUESTO             ║");
        Console.WriteLine("╠══════════════════════════════════════════╣");
        Console.WriteLine("║                                          ║");

        Console.Write("║   Nombre: ");
        string nombre = Console.ReadLine();

        Console.Write("║   Cantidad inicial: ");
        int cantidad = int.Parse(Console.ReadLine());

        Console.Write("║   Stock minimo: ");
        int stockMinimo = int.Parse(Console.ReadLine());

        Console.WriteLine("║                                          ║");
        Console.WriteLine("╠══════════════════════════════════════════╣");
        Console.WriteLine("║   1. Confirmar registro                  ║");
        Console.WriteLine("║   0. Cancelar                            ║");
        Console.WriteLine("╚══════════════════════════════════════════╝");

        Console.Write("Seleccione una opcion: ");
        opcionAgregarRepuesto = Console.ReadLine();

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

                int? idRepuestoObtenido = repuestoGuardado != null
                    ? repuestoGuardado.IdRepuesto
                    : (int?)null;

                RegistrarMovimiento(userAdmin.IdUsuario, "Entrada", idRepuestoObtenido, null);
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
            Console.WriteLine("\n❌ Opcion incorrecta. Intente nuevamente.");
        }
    }
}

// Case 3: Despachar Repuesto
static void GestionarDespacharRepuesto(Usuarios userAdmin)
{
    bool salirDespacharRepuesto = false;
    while (!salirDespacharRepuesto)
    {
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════════╗");
        Console.WriteLine("║           DESPACHAR REPUESTO             ║");
        Console.WriteLine("╠══════════════════════════════════════════╣");
        Console.WriteLine("║                                          ║");
        Console.WriteLine("║   1. Confirmar despacho                  ║");
        Console.WriteLine("║   0. Cancelar / Volver                   ║");
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
                            RegistrarMovimiento(userAdmin.IdUsuario, "Salida", repuestoEncontrado.IdRepuesto, null);
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

// Case 4: Actualizar Repuesto
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
        Console.WriteLine("║   1. Confirmar actualización             ║");
        Console.WriteLine("║   0. Cancelar / Volver                   ║");
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
                    Repuestos repuestoAct = new Repuestos();
                    repuestoAct.IdRepuesto = idRepuesto;
                    repuestoAct.Nombre = nuevoNombre;
                    repuestoAct.CantidadDisponible = nuevaCantidad;

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

// Case 5: Eliminar Repuesto (Baja Lógica)
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
        Console.WriteLine("║   1. Confirmar eliminación               ║");
        Console.WriteLine("║   0. Cancelar / Volver                   ║");
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

// Case 6: Agregar Insumo
static void GestionarAgregarInsumo(Usuarios userAdmin)
{
    bool salirAgregarInsumo = false;
    while (!salirAgregarInsumo)
    {
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════════╗");
        Console.WriteLine("║             AGREGAR INSUMO               ║");
        Console.WriteLine("╠══════════════════════════════════════════╣");
        Console.WriteLine("║                                          ║");
        Console.WriteLine("║   1. Confirmar registro                  ║");
        Console.WriteLine("║   0. Cancelar / Volver                   ║");
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

                Insumos nuevoInsumo = new Insumos();
                nuevoInsumo.Nombre = nombre;
                nuevoInsumo.CantidadDisponible = cantidad;
                nuevoInsumo.UnidadMedida = unidadMedida;

                DatosInsumos dbInsumos = new DatosInsumos();
                int filasAfectadas = dbInsumos.GuardarInsumos(nuevoInsumo);

                if (filasAfectadas > 0)
                {
                    Console.WriteLine("\n✅ Insumo agregado exitosamente.");
                    Insumos insumoGuardado = dbInsumos.ObtenerInsumoPorNombre(nombre);
                    int? idInsumoObtenido = insumoGuardado != null ? insumoGuardado.IdInsumo : (int?)null;

                    RegistrarMovimiento(userAdmin.IdUsuario, "Entrada", null, idInsumoObtenido);
                }
                else
                {
                    Console.WriteLine("\n⚠️ No se pudo registrar el insumo.");
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

// Case 7: Despachar Insumo
static void GestionarDespacharInsumo(Usuarios userAdmin)
{
    bool salirDespacharInsumo = false;
    while (!salirDespacharInsumo)
    {
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════════╗");
        Console.WriteLine("║            DESPACHAR INSUMO              ║");
        Console.WriteLine("╠══════════════════════════════════════════╣");
        Console.WriteLine("║                                          ║");
        Console.WriteLine("║   1. Confirmar despacho                  ║");
        Console.WriteLine("║   0. Cancelar / Volver                   ║");
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
                            RegistrarMovimiento(userAdmin.IdUsuario, "Salida", null, insumoEncontrado.IdInsumo);
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

// Case 8: Actualizar Insumo
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
        Console.WriteLine("║   1. Confirmar actualización             ║");
        Console.WriteLine("║   0. Cancelar / Volver                   ║");
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

                    Insumos insumoAct = new Insumos();
                    insumoAct.IdInsumo = idInsumo;
                    insumoAct.Nombre = nuevoNombre;
                    insumoAct.CantidadDisponible = nuevaCantidad;
                    insumoAct.UnidadMedida = nuevaMedida;

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

// Case 9: Eliminar Insumo (Baja Lógica)
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
        Console.WriteLine("║   1. Confirmar eliminación               ║");
        Console.WriteLine("║   0. Cancelar / Volver                   ║");
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

static void GestionarUsuarios(Usuarios userAdmin)
{
    bool salirUsuarios = false;
    while (!salirUsuarios)
    {
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════════╗");
        Console.WriteLine("║            GESTIÓN DE USUARIOS           ║");
        Console.WriteLine("╠══════════════════════════════════════════╣");
        Console.WriteLine("║                                          ║");
        Console.WriteLine("║   1. Agregar nuevo usuario               ║");
        Console.WriteLine("║   2. Eliminar usuario (Baja lógica)      ║");
        Console.WriteLine("║   0. Volver al menú anterior             ║");
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

// Subfunción modular para el Alta de Usuario
static void EjecutarAltaUsuario()
{
    Console.Clear();
    Console.WriteLine("╔══════════════════════════════════════════╗");
    Console.WriteLine("║            AGREGAR NUEVO USUARIO         ║");
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

// Subfunción modular para la Baja de Usuario
static void EjecutarBajaUsuario(Usuarios userAdmin)
{
    Console.Clear();
    Console.WriteLine("╔══════════════════════════════════════════╗");
    Console.WriteLine("║            ELIMINAR USUARIO              ║");
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




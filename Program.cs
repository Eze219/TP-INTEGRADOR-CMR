using Microsoft.Data.SqlClient;
// Instanciamos la clase para poder utilizar los métodos:
Conexion conexion = new Conexion();

using (SqlConnection sql = conexion.obtenerConexion())
{
    sql.Open();
    Console.WriteLine("conexion exitosa");
}

Console.WriteLine("╔══════════════════════════════════════════╗");
Console.WriteLine("║                                          ║");
Console.WriteLine("║             SISTEMA DE STOCK             ║");
Console.WriteLine("║                                          ║");
Console.WriteLine("║            ¡Bienvenido a CMR!            ║");
Console.WriteLine("║                                          ║");
Console.WriteLine("║       Gestion de repuestos e insumos     ║");
Console.WriteLine("║                                          ║");
Console.WriteLine("╚══════════════════════════════════════════╝");
Console.WriteLine("╔══════════════════════════════════════════╗");
Console.WriteLine("║             INICIO DE SESION             ║");
Console.WriteLine("╠══════════════════════════════════════════╣");
Console.WriteLine("║                                          ║");
Console.WriteLine("║   Nombre de usuario:                     ║");
Console.WriteLine("║   Contraseña:                            ║");
Console.WriteLine("║                                          ║");
Console.WriteLine("╠══════════════════════════════════════════╣");
Console.WriteLine("║   1. Iniciar sesion                      ║");
Console.WriteLine("║   2. Salir                               ║");
Console.WriteLine("║                                          ║");
Console.WriteLine("╚══════════════════════════════════════════╝");
Console.WriteLine();
Console.WriteLine("  Seleccione una opcion: ");

string opcion = Console.ReadLine();
bool salir = false;
switch (opcion)
{
    case "1":
        Console.Clear();
        Console.WriteLine("=== ACCESO AL SISTEMA DE STOCK ===");
        // Llamamos a tu función intacta
        Usuarios usuarioEncontrado = VerificacionUsuarioContraseña();
        Bienvenida(usuarioEncontrado);
        break;

    case "2":
        salir = true;
        Console.WriteLine("\nSaliendo del sistema... ¡Hasta luego!");
        break;

    default:
        Console.WriteLine("\n[Error] Opción no válida. Presione una tecla para intentar de nuevo.");
        Console.ReadKey();
        break;
}

// --- MÉTODOS Y MENÚS ---



// Función que ahora es tipo 'Usuarios' para devolver el usuario que encontró
Usuarios VerificacionUsuarioContraseña()
{
    Usuarios usuarioLogueado = null;

    while (usuarioLogueado == null)
    {
        Console.Write("\nUsuario: ");
        string usuarioIngresado = Console.ReadLine();

        Console.Write("Contraseña: ");
        string contraseñaIngresada = Console.ReadLine();
        DatosUsuarios usuarioDB = new DatosUsuarios();
        usuarioLogueado = usuarioDB.ObtenerUsuarioPorNombre(usuarioIngresado);

        if (usuarioLogueado.UsuarioNombre == usuarioIngresado && usuarioLogueado.Contrasena == contraseñaIngresada)
        {
            Console.WriteLine("Usuario encontrado.");
            return usuarioLogueado;
        }
        else
        {
            Console.WriteLine("\n[Error] Usuario o contraseña incorrectos. Vuelve a intentar.");
        }

    }
    return usuarioLogueado;
}


// void MenuAdministrador(Usuarios userAdmin)
// {
//     bool salirMenuAdmin = false;

//     while (!salirMenuAdmin)
//     {
//         Console.Clear();
//         Console.WriteLine("\n--- PANEL DE ADMINISTRADOR ---");
//         Console.WriteLine($"Conectado como: {userAdmin.Nombre} (DNI: {userAdmin.Dni})");
//         Console.WriteLine();

//         Console.WriteLine("╔══════════════════════════════════════════╗");
//         Console.WriteLine("║                 GESTION DE STOCK         ║");
//         Console.WriteLine("╠══════════════════════════════════════════╣");
//         Console.WriteLine("║                                          ║");
//         Console.WriteLine("║   INVENTARIO                             ║");
//         Console.WriteLine("║   1. Ver inventario                      ║");
//         Console.WriteLine("║                                          ║");
//         Console.WriteLine("║   REPUESTOS                              ║");
//         Console.WriteLine("║   2. Agregar repuesto                    ║");
//         Console.WriteLine("║   3. Despachar repuesto                  ║");
//         Console.WriteLine("║   4. Actualizar repuesto                 ║");
//         Console.WriteLine("║   5. Eliminar repuesto                   ║");
//         Console.WriteLine("║                                          ║");
//         Console.WriteLine("║   INSUMOS                                ║");
//         Console.WriteLine("║   6. Agregar insumo                      ║");
//         Console.WriteLine("║   7. Despachar insumo                    ║");
//         Console.WriteLine("║   8. Actualizar insumo                   ║");
//         Console.WriteLine("║   9. Eliminar insumo                     ║");
//         Console.WriteLine("║                                          ║");
//         Console.WriteLine("║   0. Salir                               ║");
//         Console.WriteLine("║                                          ║");
//         Console.WriteLine("╚══════════════════════════════════════════╝");
//         Console.WriteLine();
//         Console.Write(" Seleccione una opcion: ");
//         string gestionElegidaAdmin = Console.ReadLine();

//         bool salirMenuInventario = false;
//         switch (gestionElegidaAdmin)
//         {
//             case "1":
//                 Console.Clear();
//                 Console.WriteLine("╔══════════════════════════════════════════╗");
//                 Console.WriteLine("║                  GESTION DE STOCK        ║");
//                 Console.WriteLine("╠══════════════════════════════════════════╣");
//                 Console.WriteLine("║                                          ║");
//                 Console.WriteLine("║   INVENTARIO                             ║");
//                 Console.WriteLine("║   1. Ver repuestos                       ║");
//                 Console.WriteLine("║   2. Ver insumos                         ║");
//                 Console.WriteLine("║                                          ║");
//                 Console.WriteLine("║   0. Volver al menú anterior             ║");
//                 Console.WriteLine("║                                          ║");
//                 Console.WriteLine("╚══════════════════════════════════════════╝");
//                 Console.WriteLine();
//                 Console.Write(" Seleccione una opcion: ");
//                 string opcionInventario = Console.ReadLine();

//                 if (opcionInventario == "1")
//                 {
//                     // --- TABLA DE REPUESTOS ---
//                     Console.Clear();

//                     DatosRepuestos dbRepuestos = new DatosRepuestos();
//                     List<Repuestos> repuestos = dbRepuestos.ObtenerTodosLosRepuestos();

//                     Console.WriteLine("REPUESTOS");
//                     Console.WriteLine("┌──────────────────────────────┬─────────────┐");
//                     Console.WriteLine($"│ {"Nombre",-28} │ {"Cant. Disp.",-11} │");
//                     Console.WriteLine("├──────────────────────────────┼─────────────┤");

//                     if (repuestos.Count == 0)
//                     {
//                         Console.WriteLine($"│ {"No hay repuestos registrados.",-42} │");
//                     }
//                     else
//                     {
//                         foreach (var repuesto in repuestos)
//                         {
//                             string nombreRepuesto = repuesto.Nombre.Length > 28 ? repuesto.Nombre.Substring(0, 25) + "..." : repuesto.Nombre;
//                             Console.WriteLine($"│ {nombreRepuesto,-28} │ {repuesto.CantidadDisponible,-11} │");
//                         }
//                     }
//                     Console.WriteLine("└──────────────────────────────┴─────────────┘");

//                     Console.WriteLine("\nPresione una tecla para volver al menú anterior...");
//                     Console.ReadKey();
//                 }
//                 else if (opcionInventario == "2")
//                 {
//                     // --- TABLA DE INSUMOS ---
//                     Console.Clear();

//                     DatosInsumos dbInsumos = new DatosInsumos();
//                     List<Insumos> insumos = dbInsumos.ObtenerTodosLosInsumos();

//                     Console.WriteLine("INSUMOS");
//                     Console.WriteLine("┌──────────────────────────┬───────────┬───────────┐");
//                     Console.WriteLine($"│ {"Nombre",-24} │ {"Cant.",-9} │ {"Medida",-9} │");
//                     Console.WriteLine("├──────────────────────────┼───────────┼───────────┤");

//                     if (insumos.Count == 0)
//                     {
//                         Console.WriteLine($"│ {"No hay insumos registrados.",-46} │");
//                     }
//                     else
//                     {
//                         foreach (var insumo in insumos)
//                         {
//                             string nombreInsumo = insumo.Nombre.Length > 24 ? insumo.Nombre.Substring(0, 21) + "..." : insumo.Nombre;
//                             Console.WriteLine($"│ {nombreInsumo,-24} │ {insumo.CantidadDisponible,-9} │ {insumo.UnidadMedida,-9} │");
//                         }
//                     }
//                     Console.WriteLine("└──────────────────────────┬───────────┬───────────┘");

//                     Console.WriteLine("\nPresione una tecla para volver al menú anterior...");
//                     Console.ReadKey();
//                 }
//                 else if (opcionInventario == "0")
//                 {
//                     salirMenuInventario = true;
//                 }
//                 break;
//             //SEGUIR CON LOS SIGUIENTES CASOS
//             case "2":
//                 bool salirAgregarRepuesto = false;
//                 while (!salirAgregarRepuesto)
//                 {
//                     Console.Clear();
//                     Console.WriteLine("╔══════════════════════════════════════════╗");
//                     Console.WriteLine("║            AGREGAR REPUESTO              ║");
//                     Console.WriteLine("╠══════════════════════════════════════════╣");
//                     Console.WriteLine("║                                          ║");
//                     Console.WriteLine("║   1. Confirmar registro                  ║");
//                     Console.WriteLine("║   0. Cancelar / Volver                   ║");
//                     Console.WriteLine("║                                          ║");
//                     Console.WriteLine("╚══════════════════════════════════════════╝");
//                     Console.Write(" Seleccione una opcion: ");

//                     string opcionAgregarRepuesto = Console.ReadLine();

//                     if (opcionAgregarRepuesto == "1")
//                     {
//                         // 1. Pedimos los datos por consola
//                         Console.Write("\nIngrese el Nombre del repuesto: ");
//                         string nombre = Console.ReadLine();

//                         Console.Write("Ingrese la cantidad inicial: ");
//                         int cantidad = int.Parse(Console.ReadLine());

//                         // 2. Creamos el objeto Repuestos y lo guardamos
//                         Repuestos nuevoRepuesto = new Repuestos();
//                         nuevoRepuesto.Nombre = nombre;
//                         nuevoRepuesto.CantidadDisponible = cantidad;

//                         DatosRepuestos dbRepuestos = new DatosRepuestos();
//                         int filasAfectadas = dbRepuestos.GuardarRepuestos(nuevoRepuesto);

//                         if (filasAfectadas > 0)
//                         {
//                             Console.WriteLine("\n✅ Repuesto agregado exitosamente.");

//                             // ==========================================
//                             // 3. REGISTRAR EN EL HISTORIAL
//                             // ==========================================
//                             Repuestos repuestoGuardado = dbRepuestos.ObtenerRepuestoPorNombre(nombre);
//                             int? idRepuestoObtenido = repuestoGuardado != null ? repuestoGuardado.IdRepuesto : (int?)null;

//                             Historial nuevoMovimiento = new Historial();
//                             nuevoMovimiento.TipoMovimiento = "Entrada";
//                             nuevoMovimiento.FechaHora = DateTime.Now;
//                             nuevoMovimiento.IdUsuario = userAdmin.IdUsuario;
//                             nuevoMovimiento.IdRepuesto = idRepuestoObtenido;
//                             nuevoMovimiento.IdInsumo = null;

//                             DatosHistorial dbHistorial = new DatosHistorial();
//                             dbHistorial.GuardarMovimientos(nuevoMovimiento);
//                         }
//                         else
//                         {
//                             Console.WriteLine("\n⚠️ No se pudo registrar el repuesto.");
//                         }

//                         // 4. Preguntamos si desea agregar otro o volver
//                         Console.WriteLine("\n¿Qué desea hacer?");
//                         Console.WriteLine("1. Agregar otro repuesto");
//                         Console.WriteLine("0. Volver al menú anterior");
//                         Console.Write("Seleccione una opción: ");
//                         string seguir = Console.ReadLine();

//                         if (seguir == "0")
//                         {
//                             salirAgregarRepuesto = true;
//                         }
//                     }
//                     else if (opcionAgregarRepuesto == "0")
//                     {
//                         salirAgregarRepuesto = true;
//                     }
//                     else
//                     {
//                         Console.WriteLine("\n[Error] Opción no válida. Presione una tecla para intentar de nuevo.");
//                         Console.ReadKey();
//                     }
//                 }
//                 break;
//             case "3":
//                 bool salirDespacharRepuesto = false;
//                 while (!salirDespacharRepuesto)
//                 {
//                     Console.Clear();
//                     Console.WriteLine("╔══════════════════════════════════════════╗");
//                     Console.WriteLine("║           DESPACHAR REPUESTO             ║");
//                     Console.WriteLine("╠══════════════════════════════════════════╣");
//                     Console.WriteLine("║                                          ║");
//                     Console.WriteLine("║   1. Confirmar despacho                  ║");
//                     Console.WriteLine("║   0. Cancelar / Volver                   ║");
//                     Console.WriteLine("║                                          ║");
//                     Console.WriteLine("╚══════════════════════════════════════════╝");
//                     Console.Write(" Seleccione una opcion: ");

//                     string opcionDespachar = Console.ReadLine();

//                     if (opcionDespachar == "1")
//                     {
//                         // 1. Mostramos la lista rápida o pedimos el nombre/ID del repuesto a despachar
//                         Console.Write("\nIngrese el nombre exacto del repuesto a despachar: ");
//                         string nombreBuscado = Console.ReadLine();

//                         DatosRepuestos dbRepuestos = new DatosRepuestos();
//                         Repuestos repuestoEncontrado = dbRepuestos.ObtenerRepuestoPorNombre(nombreBuscado);

//                         if (repuestoEncontrado != null)
//                         {
//                             Console.WriteLine($"\nRepuesto encontrado: {repuestoEncontrado.Nombre}");
//                             Console.WriteLine($"Stock disponible actual: {repuestoEncontrado.CantidadDisponible}");

//                             Console.Write("\nIngrese la cantidad a despachar: ");
//                             if (int.TryParse(Console.ReadLine(), out int cantidadADespachar) && cantidadADespachar > 0)
//                             {
//                                 if (cantidadADespachar <= repuestoEncontrado.CantidadDisponible)
//                                 {
//                                     // Calculamos el nuevo stock restando lo despachado
//                                     repuestoEncontrado.CantidadDisponible -= cantidadADespachar;

//                                     // Actualizamos el stock en la base de datos
//                                     int filasAfectadas = dbRepuestos.ActualizarRepuesto(repuestoEncontrado);

//                                     if (filasAfectadas > 0)
//                                     {
//                                         Console.WriteLine("\n✅ Despacho realizado exitosamente.");

//                                         // ==========================================
//                                         // 2. REGISTRAR SALIDA EN EL HISTORIAL
//                                         // ==========================================
//                                         Historial nuevoMovimiento = new Historial();
//                                         nuevoMovimiento.TipoMovimiento = "Salida";
//                                         nuevoMovimiento.FechaHora = DateTime.Now;
//                                         nuevoMovimiento.IdUsuario = userAdmin.IdUsuario;
//                                         nuevoMovimiento.IdRepuesto = repuestoEncontrado.IdRepuesto; // Guardamos el ID del repuesto
//                                         nuevoMovimiento.IdInsumo = null;

//                                         DatosHistorial dbHistorial = new DatosHistorial();
//                                         dbHistorial.GuardarMovimientos(nuevoMovimiento);
//                                     }
//                                     else
//                                     {
//                                         Console.WriteLine("\n⚠️ No se pudo actualizar el stock del repuesto.");
//                                     }
//                                 }
//                                 else
//                                 {
//                                     Console.WriteLine("\n[Error] No hay suficiente stock disponible para realizar este despacho.");
//                                 }
//                             }
//                             else
//                             {
//                                 Console.WriteLine("\n[Error] Cantidad no válida.");
//                             }
//                         }
//                         else
//                         {
//                             Console.WriteLine("\n[Error] No se encontró ningún repuesto con ese nombre.");
//                         }

//                         // Preguntamos si desea despachar otro o volver
//                         Console.WriteLine("\n¿Qué desea hacer?");
//                         Console.WriteLine("1. Despachar otro repuesto");
//                         Console.WriteLine("0. Volver al menú anterior");
//                         Console.Write("Seleccione una opción: ");
//                         string seguir = Console.ReadLine();

//                         if (seguir == "0")
//                         {
//                             salirDespacharRepuesto = true;
//                         }
//                     }
//                     else if (opcionDespachar == "0")
//                     {
//                         salirDespacharRepuesto = true;
//                     }
//                     else
//                     {
//                         Console.WriteLine("\n[Error] Opción no válida. Presione una tecla para intentar de nuevo.");
//                         Console.ReadKey();
//                     }
//                 }
//                 break;
//             case "4":
//                 bool salirActualizarRepuesto = false;
//                 while (!salirActualizarRepuesto)
//                 {
//                     Console.Clear();
//                     Console.WriteLine("╔══════════════════════════════════════════╗");
//                     Console.WriteLine("║            ACTUALIZAR REPUESTO           ║");
//                     Console.WriteLine("╠══════════════════════════════════════════╣");
//                     Console.WriteLine("║                                          ║");
//                     Console.WriteLine("║   1. Confirmar actualización             ║");
//                     Console.WriteLine("║   0. Cancelar / Volver                   ║");
//                     Console.WriteLine("║                                          ║");
//                     Console.WriteLine("╚══════════════════════════════════════════╝");
//                     Console.Write(" Seleccione una opcion: ");

//                     string opcionActRep = Console.ReadLine();

//                     if (opcionActRep == "1")
//                     {
//                         Console.Write("\nIngrese el ID del repuesto a actualizar: ");
//                         if (int.TryParse(Console.ReadLine(), out int idRepuesto))
//                         {
//                             Console.Write("Ingrese el nuevo Nombre: ");
//                             string nuevoNombre = Console.ReadLine();

//                             Console.Write("Ingrese la nueva Cantidad disponible: ");
//                             if (int.TryParse(Console.ReadLine(), out int nuevaCantidad))
//                             {
//                                 Repuestos repuestoAct = new Repuestos();
//                                 repuestoAct.IdRepuesto = idRepuesto;
//                                 repuestoAct.Nombre = nuevoNombre;
//                                 repuestoAct.CantidadDisponible = nuevaCantidad;

//                                 DatosRepuestos dbRepuestos = new DatosRepuestos();
//                                 int filasAfectadas = dbRepuestos.ActualizarRepuesto(repuestoAct);

//                                 if (filasAfectadas > 0)
//                                 {
//                                     Console.WriteLine("\n✅ Repuesto actualizado exitosamente.");

//                                     // Registrar en el historial
//                                     Historial movMod = new Historial();
//                                     movMod.TipoMovimiento = "Modificación";
//                                     movMod.FechaHora = DateTime.Now;
//                                     movMod.IdUsuario = userAdmin.IdUsuario;
//                                     movMod.IdRepuesto = idRepuesto;
//                                     movMod.IdInsumo = null;

//                                     DatosHistorial dbHistorial = new DatosHistorial();
//                                     dbHistorial.GuardarMovimientos(movMod);
//                                 }
//                                 else
//                                 {
//                                     Console.WriteLine("\n⚠️ No se encontró ningún repuesto con ese ID.");
//                                 }
//                             }
//                             else
//                             {
//                                 Console.WriteLine("\n[Error] Cantidad no válida.");
//                             }
//                         }
//                         else
//                         {
//                             Console.WriteLine("\n[Error] ID no válido.");
//                         }

//                         Console.WriteLine("\n¿Qué desea hacer?");
//                         Console.WriteLine("1. Actualizar otro repuesto");
//                         Console.WriteLine("0. Volver al menú anterior");
//                         Console.Write("Seleccione una opción: ");
//                         if (Console.ReadLine() == "0") salirActualizarRepuesto = true;
//                     }
//                     else if (opcionActRep == "0")
//                     {
//                         salirActualizarRepuesto = true;
//                     }
//                     else
//                     {
//                         Console.WriteLine("\n[Error] Opción no válida. Presione una tecla.");
//                         Console.ReadKey();
//                     }
//                 }
//                 break;

//             case "5":
//                 bool salirEliminarRepuesto = false;
//                 while (!salirEliminarRepuesto)
//                 {
//                     Console.Clear();
//                     Console.WriteLine("╔══════════════════════════════════════════╗");
//                     Console.WriteLine("║            ELIMINAR REPUESTO             ║");
//                     Console.WriteLine("╠══════════════════════════════════════════╣");
//                     Console.WriteLine("║                                          ║");
//                     Console.WriteLine("║   1. Confirmar eliminación               ║");
//                     Console.WriteLine("║   0. Cancelar / Volver                   ║");
//                     Console.WriteLine("║                                          ║");
//                     Console.WriteLine("╚══════════════════════════════════════════╝");
//                     Console.Write(" Seleccione una opcion: ");

//                     string opcionEliminarRep = Console.ReadLine();

//                     if (opcionEliminarRep == "1")
//                     {
//                         Console.Write("\nIngrese el ID del repuesto a eliminar: ");
//                         if (int.TryParse(Console.ReadLine(), out int idRepuesto))
//                         {
//                             DatosRepuestos dbRepuestos = new DatosRepuestos();

//                             // 1. Ejecutamos la baja lógica (UPDATE activo = 0)
//                             int filasAfectadas = dbRepuestos.EliminarRepuesto(idRepuesto);

//                             if (filasAfectadas > 0)
//                             {
//                                 Console.WriteLine("\n✅ Repuesto dado de baja exitosamente.");

//                                 // ==========================================
//                                 // 2. REGISTRAR LA BAJA EN EL HISTORIAL AQUÍ
//                                 // ==========================================
//                                 Historial movBaja = new Historial();
//                                 movBaja.TipoMovimiento = "Baja";
//                                 movBaja.FechaHora = DateTime.Now;
//                                 movBaja.IdUsuario = userAdmin.IdUsuario;
//                                 movBaja.IdRepuesto = idRepuesto; // Guarda el ID exacto
//                                 movBaja.IdInsumo = null;

//                                 DatosHistorial dbHistorial = new DatosHistorial();
//                                 dbHistorial.GuardarMovimientos(movBaja);
//                             }
//                             else
//                             {
//                                 Console.WriteLine("\n⚠️ No se encontró ningún repuesto con ese ID.");
//                             }
//                         }
//                         else
//                         {
//                             Console.WriteLine("\n[Error] ID no válido.");
//                         }

//                         Console.WriteLine("\n¿Qué desea hacer?");
//                         Console.WriteLine("1. Eliminar otro repuesto");
//                         Console.WriteLine("0. Volver al menú anterior");
//                         Console.Write("Seleccione una opción: ");
//                         if (Console.ReadLine() == "0") salirEliminarRepuesto = true;
//                     }
//                     else if (opcionEliminarRep == "0")
//                     {
//                         salirEliminarRepuesto = true;
//                     }
//                     else
//                     {
//                         Console.WriteLine("\n[Error] Opción no válida. Presione una tecla.");
//                         Console.ReadKey();
//                     }
//                 }
//                 break;



//             case "6":
//                 bool salirAgregarInsumo = false;
//                 while (!salirAgregarInsumo)
//                 {
//                     Console.Clear();
//                     Console.WriteLine("╔══════════════════════════════════════════╗");
//                     Console.WriteLine("║            AGREGAR INSUMO                ║");
//                     Console.WriteLine("╠══════════════════════════════════════════╣");
//                     Console.WriteLine("║                                          ║");
//                     Console.WriteLine("║   1. Confirmar registro                  ║");
//                     Console.WriteLine("║   0. Cancelar / Volver                   ║");
//                     Console.WriteLine("║                                          ║");
//                     Console.WriteLine("╚══════════════════════════════════════════╝");
//                     Console.Write(" Seleccione una opcion: ");

//                     string opcionAgregarInsumo = Console.ReadLine();

//                     if (opcionAgregarInsumo == "1")
//                     {
//                         // 1. Pedimos los datos del insumo por consola
//                         Console.Write("\nIngrese el Nombre del insumo: ");
//                         string nombre = Console.ReadLine();

//                         Console.Write("Ingrese la cantidad inicial: ");
//                         int cantidad = int.Parse(Console.ReadLine());

//                         Console.Write("Ingrese la unidad de medida (ej: litros, kg, unidades): ");
//                         string unidadMedida = Console.ReadLine();

//                         // 2. Creamos el objeto Insumos y lo guardamos
//                         Insumos nuevoInsumo = new Insumos();
//                         nuevoInsumo.Nombre = nombre;
//                         nuevoInsumo.CantidadDisponible = cantidad;
//                         nuevoInsumo.UnidadMedida = unidadMedida;

//                         DatosInsumos dbInsumos = new DatosInsumos();
//                         int filasAfectadas = dbInsumos.GuardarInsumos(nuevoInsumo);

//                         if (filasAfectadas > 0)
//                         {
//                             Console.WriteLine("\n✅ Insumo agregado exitosamente.");

//                             // ==========================================
//                             // 3. REGISTRAR EN EL HISTORIAL
//                             // ==========================================
//                             // Buscamos el insumo recién creado por su nombre para obtener su ID autoincremental
//                             Insumos insumoGuardado = dbInsumos.ObtenerInsumoPorNombre(nombre);
//                             int? idInsumoObtenido = insumoGuardado != null ? insumoGuardado.IdInsumo : (int?)null;

//                             Historial nuevoMovimiento = new Historial();
//                             nuevoMovimiento.TipoMovimiento = "Entrada";
//                             nuevoMovimiento.FechaHora = DateTime.Now;
//                             nuevoMovimiento.IdUsuario = userAdmin.IdUsuario;
//                             nuevoMovimiento.IdRepuesto = null;                 // No es repuesto
//                             nuevoMovimiento.IdInsumo = idInsumoObtenido;       // Guardamos el ID del insumo

//                             DatosHistorial dbHistorial = new DatosHistorial();
//                             int filasHistorial = dbHistorial.GuardarMovimientos(nuevoMovimiento);
//                         }
//                         else
//                         {
//                             Console.WriteLine("\n⚠️ No se pudo registrar el insumo.");
//                         }

//                         // 4. Preguntamos si desea agregar otro o volver
//                         Console.WriteLine("\n¿Qué desea hacer?");
//                         Console.WriteLine("1. Agregar otro insumo");
//                         Console.WriteLine("0. Volver al menú anterior");
//                         Console.Write("Seleccione una opción: ");
//                         string seguir = Console.ReadLine();

//                         if (seguir == "0")
//                         {
//                             salirAgregarInsumo = true;
//                         }
//                     }
//                     else if (opcionAgregarInsumo == "0")
//                     {
//                         salirAgregarInsumo = true;
//                     }
//                     else
//                     {
//                         Console.WriteLine("\n[Error] Opción no válida. Presione una tecla para intentar de nuevo.");
//                         Console.ReadKey();
//                     }
//                 }
//                 break;

//             case "7":
//                 bool salirDespacharInsumo = false;
//                 while (!salirDespacharInsumo)
//                 {
//                     Console.Clear();
//                     Console.WriteLine("╔══════════════════════════════════════════╗");
//                     Console.WriteLine("║            DESPACHAR INSUMO              ║");
//                     Console.WriteLine("╠══════════════════════════════════════════╣");
//                     Console.WriteLine("║                                          ║");
//                     Console.WriteLine("║   1. Confirmar despacho                  ║");
//                     Console.WriteLine("║   0. Cancelar / Volver                   ║");
//                     Console.WriteLine("║                                          ║");
//                     Console.WriteLine("╚══════════════════════════════════════════╝");
//                     Console.Write(" Seleccione una opcion: ");

//                     string opcionDespacharInsumo = Console.ReadLine();

//                     if (opcionDespacharInsumo == "1")
//                     {
//                         Console.Write("\nIngrese el nombre exacto del insumo a despachar: ");
//                         string nombreBuscado = Console.ReadLine();

//                         DatosInsumos dbInsumos = new DatosInsumos();
//                         Insumos insumoEncontrado = dbInsumos.ObtenerInsumoPorNombre(nombreBuscado);

//                         if (insumoEncontrado != null)
//                         {
//                             Console.WriteLine($"\nInsumo encontrado: {insumoEncontrado.Nombre}");
//                             Console.WriteLine($"Stock disponible actual: {insumoEncontrado.CantidadDisponible} {insumoEncontrado.UnidadMedida}");

//                             Console.Write("\nIngrese la cantidad a despachar: ");
//                             if (int.TryParse(Console.ReadLine(), out int cantidadADespachar) && cantidadADespachar > 0)
//                             {
//                                 if (cantidadADespachar <= insumoEncontrado.CantidadDisponible)
//                                 {
//                                     // Descontamos del stock disponible
//                                     insumoEncontrado.CantidadDisponible -= cantidadADespachar;

//                                     // Actualizamos en la base de datos
//                                     int filasAfectadas = dbInsumos.ActualizarInsumo(insumoEncontrado);

//                                     if (filasAfectadas > 0)
//                                     {
//                                         Console.WriteLine("\n✅ Despacho de insumo realizado exitosamente.");

//                                         // ==========================================
//                                         // REGISTRAR SALIDA EN EL HISTORIAL
//                                         // ==========================================
//                                         Historial nuevoMovimiento = new Historial();
//                                         nuevoMovimiento.TipoMovimiento = "Salida";
//                                         nuevoMovimiento.FechaHora = DateTime.Now;
//                                         nuevoMovimiento.IdUsuario = userAdmin.IdUsuario;
//                                         nuevoMovimiento.IdRepuesto = null;                  // No es repuesto
//                                         nuevoMovimiento.IdInsumo = insumoEncontrado.IdInsumo; // Guardamos el ID del insumo

//                                         DatosHistorial dbHistorial = new DatosHistorial();
//                                         dbHistorial.GuardarMovimientos(nuevoMovimiento);
//                                     }
//                                     else
//                                     {
//                                         Console.WriteLine("\n⚠️ No se pudo actualizar el stock del insumo.");
//                                     }
//                                 }
//                                 else
//                                 {
//                                     Console.WriteLine("\n[Error] No hay suficiente stock disponible para realizar este despacho.");
//                                 }
//                             }
//                             else
//                             {
//                                 Console.WriteLine("\n[Error] Cantidad no válida.");
//                             }
//                         }
//                         else
//                         {
//                             Console.WriteLine("\n[Error] No se encontró ningún insumo con ese nombre.");
//                         }

//                         // Preguntamos si desea despachar otro o volver
//                         Console.WriteLine("\n¿Qué desea hacer?");
//                         Console.WriteLine("1. Despachar otro insumo");
//                         Console.WriteLine("0. Volver al menú anterior");
//                         Console.Write("Seleccione una opción: ");
//                         string seguir = Console.ReadLine();

//                         if (seguir == "0")
//                         {
//                             salirDespacharInsumo = true;
//                         }
//                     }
//                     else if (opcionDespacharInsumo == "0")
//                     {
//                         salirDespacharInsumo = true;
//                     }
//                     else
//                     {
//                         Console.WriteLine("\n[Error] Opción no válida. Presione una tecla para intentar de nuevo.");
//                         Console.ReadKey();
//                     }
//                 }
//                 break;
//             case "8":
//                 bool salirActualizarInsumo = false;
//                 while (!salirActualizarInsumo)
//                 {
//                     Console.Clear();
//                     Console.WriteLine("╔══════════════════════════════════════════╗");
//                     Console.WriteLine("║            ACTUALIZAR INSUMO             ║");
//                     Console.WriteLine("╠══════════════════════════════════════════╣");
//                     Console.WriteLine("║                                          ║");
//                     Console.WriteLine("║   1. Confirmar actualización             ║");
//                     Console.WriteLine("║   0. Cancelar / Volver                   ║");
//                     Console.WriteLine("║                                          ║");
//                     Console.WriteLine("╚══════════════════════════════════════════╝");
//                     Console.Write(" Seleccione una opcion: ");

//                     string opcionActIns = Console.ReadLine();

//                     if (opcionActIns == "1")
//                     {
//                         Console.Write("\nIngrese el ID del insumo a actualizar: ");
//                         if (int.TryParse(Console.ReadLine(), out int idInsumo))
//                         {
//                             Console.Write("Ingrese el nuevo Nombre: ");
//                             string nuevoNombre = Console.ReadLine();

//                             Console.Write("Ingrese la nueva Cantidad disponible: ");
//                             if (int.TryParse(Console.ReadLine(), out int nuevaCantidad))
//                             {
//                                 Console.Write("Ingrese la nueva Unidad de medida: ");
//                                 string nuevaMedida = Console.ReadLine();

//                                 Insumos insumoAct = new Insumos();
//                                 insumoAct.IdInsumo = idInsumo;
//                                 insumoAct.Nombre = nuevoNombre;
//                                 insumoAct.CantidadDisponible = nuevaCantidad;
//                                 insumoAct.UnidadMedida = nuevaMedida;

//                                 DatosInsumos dbInsumos = new DatosInsumos();
//                                 int filasAfectadas = dbInsumos.ActualizarInsumo(insumoAct);

//                                 if (filasAfectadas > 0)
//                                 {
//                                     Console.WriteLine("\n✅ Insumo actualizado exitosamente.");

//                                     // Registrar en el historial
//                                     Historial movModIns = new Historial();
//                                     movModIns.TipoMovimiento = "Modificación";
//                                     movModIns.FechaHora = DateTime.Now;
//                                     movModIns.IdUsuario = userAdmin.IdUsuario;
//                                     movModIns.IdRepuesto = null;
//                                     movModIns.IdInsumo = idInsumo;

//                                     DatosHistorial dbHistorial = new DatosHistorial();
//                                     dbHistorial.GuardarMovimientos(movModIns);
//                                 }
//                                 else
//                                 {
//                                     Console.WriteLine("\n⚠️ No se encontró ningún insumo con ese ID.");
//                                 }
//                             }
//                             else
//                             {
//                                 Console.WriteLine("\n[Error] Cantidad no válida.");
//                             }
//                         }
//                         else
//                         {
//                             Console.WriteLine("\n[Error] ID no válido.");
//                         }

//                         Console.WriteLine("\n¿Qué desea hacer?");
//                         Console.WriteLine("1. Actualizar otro insumo");
//                         Console.WriteLine("0. Volver al menú anterior");
//                         Console.Write("Seleccione una opción: ");
//                         if (Console.ReadLine() == "0") salirActualizarInsumo = true;
//                     }
//                     else if (opcionActIns == "0")
//                     {
//                         salirActualizarInsumo = true;
//                     }
//                     else
//                     {
//                         Console.WriteLine("\n[Error] Opción no válida. Presione una tecla.");
//                         Console.ReadKey();
//                     }
//                 }
//                 break;
//             case "9":
//                 bool salirEliminarInsumo = false;
//                 while (!salirEliminarInsumo)
//                 {
//                     Console.Clear();
//                     Console.WriteLine("╔══════════════════════════════════════════╗");
//                     Console.WriteLine("║            ELIMINAR INSUMO               ║");
//                     Console.WriteLine("╠══════════════════════════════════════════╣");
//                     Console.WriteLine("║                                          ║");
//                     Console.WriteLine("║   1. Confirmar eliminación               ║");
//                     Console.WriteLine("║   0. Cancelar / Volver                   ║");
//                     Console.WriteLine("║                                          ║");
//                     Console.WriteLine("╚══════════════════════════════════════════╝");
//                     Console.Write(" Seleccione una opcion: ");

//                     string opcionEliminarIns = Console.ReadLine();

//                     if (opcionEliminarIns == "1")
//                     {
//                         Console.Write("\nIngrese el ID del insumo a eliminar: ");
//                         if (int.TryParse(Console.ReadLine(), out int idInsumo))
//                         {
//                             DatosInsumos dbInsumos = new DatosInsumos();

//                             // 1. Ejecutamos la baja lógica
//                             int filasAfectadas = dbInsumos.EliminarInsumo(idInsumo);

//                             if (filasAfectadas > 0)
//                             {
//                                 Console.WriteLine("\n✅ Insumo dado de baja exitosamente.");

//                                 // ==========================================
//                                 // 2. REGISTRAR LA BAJA EN EL HISTORIAL
//                                 // ==========================================
//                                 Historial movBaja = new Historial();
//                                 movBaja.TipoMovimiento = "Baja";
//                                 movBaja.FechaHora = DateTime.Now;
//                                 movBaja.IdUsuario = userAdmin.IdUsuario;
//                                 movBaja.IdRepuesto = null;
//                                 movBaja.IdInsumo = idInsumo; // Guarda el ID exacto del insumo

//                                 DatosHistorial dbHistorial = new DatosHistorial();
//                                 dbHistorial.GuardarMovimientos(movBaja);
//                             }
//                             else
//                             {
//                                 Console.WriteLine("\n⚠️ No se encontró ningún insumo con ese ID.");
//                             }
//                         }
//                         else
//                         {
//                             Console.WriteLine("\n[Error] ID no válido.");
//                         }

//                         Console.WriteLine("\n¿Qué desea hacer?");
//                         Console.WriteLine("1. Eliminar otro insumo");
//                         Console.WriteLine("0. Volver al menú anterior");
//                         Console.Write("Seleccione una opción: ");
//                         if (Console.ReadLine() == "0") salirEliminarInsumo = true;
//                     }
//                     else if (opcionEliminarIns == "0")
//                     {
//                         salirEliminarInsumo = true;
//                     }
//                     else
//                     {
//                         Console.WriteLine("\n[Error] Opción no válida. Presione una tecla.");
//                         Console.ReadKey();
//                     }
//                 }
//                 break;
//             case "0":
//                 salirMenuAdmin = true;
//                 Console.WriteLine("\nSaliendo del sistema.\n¡Gracias por usar la aplicación!\nPresione una tecla para continuar...");
//                 Console.ReadKey();
//                 break;

//             default:
//                 Console.WriteLine("\n[Error] Opción no válida. Presione una tecla para continuar.");
//                 Console.ReadKey();
//                 break;
//         }
//     }
// }
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
            Console.WriteLine("║                 GESTION DE STOCK         ║");
            Console.WriteLine("╠══════════════════════════════════════════╣");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║   INVENTARIO                             ║");
            Console.WriteLine("║   1. Ver inventario                      ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║   REPUESTOS                              ║");
            Console.WriteLine("║   2. Agregar repuesto                    ║");
            Console.WriteLine("║   3. Despachar repuesto                  ║");
            Console.WriteLine("║   4. Actualizar repuesto                 ║");
            Console.WriteLine("║   5. Eliminar repuesto                   ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║   INSUMOS                                ║");
            Console.WriteLine("║   6. Agregar insumo                      ║");
            Console.WriteLine("║   7. Despachar insumo                    ║");
            Console.WriteLine("║   8. Actualizar insumo                   ║");
            Console.WriteLine("║   9. Eliminar insumo                     ║");
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
                    GestionarAgregarRepuesto(userAdmin);
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
                case "6":
                    GestionarAgregarInsumo(userAdmin);
                    break;
                case "7":
                    GestionarDespacharInsumo(userAdmin);
                    break;
                case "8":
                    GestionarActualizarInsumo(userAdmin);
                    break;
                case "9":
                    GestionarEliminarInsumo(userAdmin);
                    break;
                case "0":
                    salirMenuAdmin = true;
                    break;
                default:
                    Console.WriteLine("\n[Error] Opción no válida. Presione una tecla para continuar.");
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
            Console.WriteLine("║                 GESTION DE STOCK         ║");
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
            Console.WriteLine($"│ {"No hay insumos registrados.",-46} │");
        }
        else
        {
            foreach (var insumo in insumos)
            {
                string nombreInsumo = insumo.Nombre.Length > 24 ? insumo.Nombre.Substring(0, 21) + "..." : insumo.Nombre;
                Console.WriteLine($"│ {nombreInsumo,-24} │ {insumo.CantidadDisponible,-9} │ {insumo.UnidadMedida,-9} │");
            }
        }
        Console.WriteLine("└──────────────────────────┬───────────┬───────────┘");
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
        bool salirAgregarRepuesto = false;
        while (!salirAgregarRepuesto)
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║            AGREGAR REPUESTO              ║");
            Console.WriteLine("╠══════════════════════════════════════════╣");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║   1. Confirmar registro                  ║");
            Console.WriteLine("║   0. Cancelar / Volver                   ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");
            Console.Write(" Seleccione una opcion: ");

            string opcionAgregarRepuesto = Console.ReadLine();

            if (opcionAgregarRepuesto == "1")
            {
                Console.Write("\nIngrese el Nombre del repuesto: ");
                string nombre = Console.ReadLine();

                Console.Write("Ingrese la cantidad inicial: ");
                if (int.TryParse(Console.ReadLine(), out int cantidad))
                {
                    Repuestos nuevoRepuesto = new Repuestos();
                    nuevoRepuesto.Nombre = nombre;
                    nuevoRepuesto.CantidadDisponible = cantidad;

                    DatosRepuestos dbRepuestos = new DatosRepuestos();
                    int filasAfectadas = dbRepuestos.GuardarRepuestos(nuevoRepuesto);

                    if (filasAfectadas > 0)
                    {
                        Console.WriteLine("\n✅ Repuesto agregado exitosamente.");
                        Repuestos repuestoGuardado = dbRepuestos.ObtenerRepuestoPorNombre(nombre);
                        int? idRepuestoObtenido = repuestoGuardado != null ? repuestoGuardado.IdRepuesto : (int?)null;

                        RegistrarMovimiento(userAdmin.IdUsuario, "Entrada", idRepuestoObtenido, null);
                    }
                    else
                    {
                        Console.WriteLine("\n⚠️ No se pudo registrar el repuesto.");
                    }
                }
                else
                {
                    Console.WriteLine("\n[Error] Cantidad no válida.");
                }

                Console.WriteLine("\n¿Qué desea hacer?");
                Console.WriteLine("1. Agregar otro repuesto");
                Console.WriteLine("0. Volver al menú anterior");
                Console.Write("Seleccione una opción: ");
                if (Console.ReadLine() == "0") salirAgregarRepuesto = true;
            }
            else if (opcionAgregarRepuesto == "0")
            {
                salirAgregarRepuesto = true;
            }
            else
            {
                Console.WriteLine("\n[Error] Opción no válida. Presione una tecla.");
                Console.ReadKey();
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
                            Console.WriteLine("\n[Error] No hay suficiente stock disponible para realizar este despacho.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\n[Error] Cantidad no válida.");
                    }
                }
                else
                {
                    Console.WriteLine("\n[Error] No se encontró ningún repuesto con ese nombre.");
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
                Console.WriteLine("\n[Error] Opción no válida. Presione una tecla.");
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
                        Console.WriteLine("\n[Error] Cantidad no válida.");
                    }
                }
                else
                {
                    Console.WriteLine("\n[Error] ID no válido.");
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
                Console.WriteLine("\n[Error] Opción no válida. Presione una tecla.");
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
                    Console.WriteLine("\n[Error] ID no válido.");
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
                Console.WriteLine("\n[Error] Opción no válida. Presione una tecla.");
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
                    Console.WriteLine("\n[Error] Cantidad no válida.");
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
                Console.WriteLine("\n[Error] Opción no válida. Presione una tecla.");
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
                            Console.WriteLine("\n[Error] No hay suficiente stock disponible para realizar este despacho.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\n[Error] Cantidad no válida.");
                    }
                }
                else
                {
                    Console.WriteLine("\n[Error] No se encontró ningún insumo con ese nombre.");
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
                Console.WriteLine("\n[Error] Opción no válida. Presione una tecla.");
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
                        Console.WriteLine("\n[Error] Cantidad no válida.");
                    }
                }
                else
                {
                    Console.WriteLine("\n[Error] ID no válido.");
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
                Console.WriteLine("\n[Error] Opción no válida. Presione una tecla.");
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
                    Console.WriteLine("\n[Error] ID no válido.");
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
                Console.WriteLine("\n[Error] Opción no válida. Presione una tecla.");
                Console.ReadKey();
            }
        }
    }
void MenuEmpleado(Usuarios userEmpleado)
{
    Console.WriteLine("\n--- PANEL DE EMPLEADO ---");
    Console.WriteLine($"Conectado como: {userEmpleado.Nombre} (DNI: {userEmpleado.Dni})");
    // Acá podemos agregar la lógica para registrar entradas/salidas de repuestos e insumos.

    Console.WriteLine("\nPresione una tecla para continuar...");
    Console.ReadKey();

}

void Bienvenida(Usuarios userValidado)
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




//Colocar menús y seguir con las funcionalidades de cada rol




































// Console.WriteLine("╔══════════════════════════════════════════╗");
// Console.WriteLine("║           MODIFICAR / ELIMINAR           ║");
// Console.WriteLine("╠══════════════════════════════════════════╣");
// Console.WriteLine("║                                          ║");
// Console.WriteLine("║   REPUESTOS                              ║");
// Console.WriteLine("║   1. Modificar repuesto                  ║");
// Console.WriteLine("║   2. Eliminar repuesto                   ║");
// Console.WriteLine("║                                          ║");
// Console.WriteLine("║   INSUMOS                                ║");
// Console.WriteLine("║   3. Modificar insumo                    ║");
// Console.WriteLine("║   4. Eliminar insumo                     ║");
// Console.WriteLine("║                                          ║");
// Console.WriteLine("║   0. Volver al menu principal            ║");
// Console.WriteLine("║                                          ║");
// Console.WriteLine("╚══════════════════════════════════════════╝");
// Console.WriteLine();
// Console.WriteLine("  Seleccione una opcion: ");



// Console.WriteLine("╔══════════════════════════════════════════╗");
// Console.WriteLine("║                INVENTARIO                ║");
// Console.WriteLine("╠══════════════════════════════════════════╣");
// Console.WriteLine("║                                          ║");
// Console.WriteLine("║   1. Ver repuestos                       ║");
// Console.WriteLine("║   2. Ver insumos                         ║");
// Console.WriteLine("║                                          ║");
// Console.WriteLine("║   0. Volver al menu principal            ║");
// Console.WriteLine("║                                          ║");
// Console.WriteLine("╚══════════════════════════════════════════╝");
// Console.WriteLine();
// Console.WriteLine("  Seleccione una opcion: ");
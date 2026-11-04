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
//     Console.WriteLine("\n--- PANEL DE ADMINISTRADOR ---");
//     Console.WriteLine($"Conectado como: {userAdmin.Nombre} (DNI: {userAdmin.Dni})");

//     Console.WriteLine("\nPresione una tecla para continuar...");
//     Console.ReadKey();
//     //Acá podemos agregar la lógica de gestión de usuarios, ver reportes, etc.
//     Console.WriteLine("╔══════════════════════════════════════════╗");
//     Console.WriteLine("║              GESTION DE STOCK            ║");
//     Console.WriteLine("╠══════════════════════════════════════════╣");
//     Console.WriteLine("║                                          ║");
//     Console.WriteLine("║   INVENTARIO                             ║");
//     Console.WriteLine("║   1. Ver inventario.                     ║");
//     Console.WriteLine("║                                          ║");
//     Console.WriteLine("║   REPUESTOS                              ║");
//     Console.WriteLine("║   2. Agregar repuesto                    ║");
//     Console.WriteLine("║   3. Despachar repuesto                  ║");
//     Console.WriteLine("║                                          ║");
//     Console.WriteLine("║   INSUMOS                                ║");
//     Console.WriteLine("║   4. Agregar insumo                      ║");
//     Console.WriteLine("║   5. Despachar insumo                    ║");
//     Console.WriteLine("║                                          ║");
//     Console.WriteLine("║   0. Volver al menu principal            ║");
//     Console.WriteLine("║                                          ║");
//     Console.WriteLine("╚══════════════════════════════════════════╝");
//     Console.WriteLine();
//     Console.WriteLine("  Seleccione una opcion: ");
//     string gestionElegidaAdmin = Console.ReadLine();
//     bool salirMenuAdmin = false;
//     switch (gestionElegidaAdmin)
//     {
//         case "1":
//             Console.WriteLine("");
//             Console.WriteLine("╔══════════════════════════════════════════╗");
//             Console.WriteLine("║              GESTION DE STOCK            ║");
//             Console.WriteLine("╠══════════════════════════════════════════╣");
//             Console.WriteLine("║                                          ║");
//             Console.WriteLine("║   INVENTARIO                             ║");
//             Console.WriteLine("║   1. Repuestos                           ║");
//             Console.WriteLine("║   2. Insumos                             ║");
//             Console.WriteLine("║                                          ║");
//             Console.WriteLine("║   0. Volver al menú anterior             ║");
//             Console.WriteLine("║                                          ║");
//             Console.WriteLine("╚══════════════════════════════════════════╝");
//             Console.WriteLine();
//             Console.WriteLine("  Seleccione una opcion: ");
//             string opcionInventario = Console.ReadLine();
//             bool salirInventario = false;
//             switch (opcionInventario)
//             {
//                 case "1":

//                 break;
//                 case "2":
//                 break;
//                 case "0":
//                 default:
//                 break;
//             }
//             break;
//         case "2":
//             Console.WriteLine("╔══════════════════════════════════════════╗");
//             Console.WriteLine("║             AGREGAR REPUESTO             ║");
//             Console.WriteLine("╠══════════════════════════════════════════╣");
//             Console.WriteLine("║                                          ║");
//             Console.WriteLine("║   Nombre:                                ║");
//             Console.WriteLine("║   Cantidad inicial:                      ║");
//             Console.WriteLine("║   Stock minimo:                          ║");
//             Console.WriteLine("║                                          ║");
//             Console.WriteLine("╠══════════════════════════════════════════╣");
//             Console.WriteLine("║   1. Confirmar registro                  ║");
//             Console.WriteLine("║   0. Cancelar                            ║");
//             Console.WriteLine("╚══════════════════════════════════════════╝");

//             break;
//         case "3":
//             Console.WriteLine("╔══════════════════════════════════════════╗");
//             Console.WriteLine("║            DESPACHAR REPUESTO            ║");
//             Console.WriteLine("╠══════════════════════════════════════════╣");
//             Console.WriteLine("║                                          ║");
//             Console.WriteLine("║   Codigo o nombre del producto:          ║");
//             Console.WriteLine("║   Stock disponible:                      ║");
//             Console.WriteLine("║   Cantidad a despachar:                  ║");
//             Console.WriteLine("║   Motivo del despacho:                   ║");
//             Console.WriteLine("║                                          ║");
//             Console.WriteLine("╠══════════════════════════════════════════╣");
//             Console.WriteLine("║   1. Confirmar despacho                  ║");
//             Console.WriteLine("║   0. Cancelar                            ║");
//             Console.WriteLine("╚══════════════════════════════════════════╝");
//             break;
//         case "4":
//             Console.WriteLine("╔══════════════════════════════════════════╗");
//             Console.WriteLine("║               AGREGAR INSUMO             ║");
//             Console.WriteLine("╠══════════════════════════════════════════╣");
//             Console.WriteLine("║                                          ║");
//             Console.WriteLine("║   Codigo o nombre del producto:          ║");
//             Console.WriteLine("║   Stock disponible:                      ║");
//             Console.WriteLine("║   Cantidad a despachar:                  ║");
//             Console.WriteLine("║   Motivo del despacho:                   ║");
//             Console.WriteLine("║                                          ║");
//             Console.WriteLine("╠══════════════════════════════════════════╣");
//             Console.WriteLine("║   1. Confirmar despacho                  ║");
//             Console.WriteLine("║   0. Cancelar                            ║");
//             Console.WriteLine("╚══════════════════════════════════════════╝");
//             break;
//         case "5":
//             Console.WriteLine("╔══════════════════════════════════════════╗");
//             Console.WriteLine("║             DESPACHAR INSUMO             ║");
//             Console.WriteLine("╠══════════════════════════════════════════╣");
//             Console.WriteLine("║                                          ║");
//             Console.WriteLine("║   Codigo o nombre del producto:          ║");
//             Console.WriteLine("║   Stock disponible:                      ║");
//             Console.WriteLine("║   Cantidad a despachar:                  ║");
//             Console.WriteLine("║   Motivo del despacho:                   ║");
//             Console.WriteLine("║                                          ║");
//             Console.WriteLine("╠══════════════════════════════════════════╣");
//             Console.WriteLine("║   1. Confirmar despacho                  ║");
//             Console.WriteLine("║   0. Cancelar                            ║");
//             Console.WriteLine("╚══════════════════════════════════════════╝");
//             break;
//         default:
//             Console.WriteLine("Volviendo al menú principal...");
//             salirMenuAdmin = true;
//             break;
//     }

// }
void MenuAdministrador(Usuarios userAdmin)
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
        Console.WriteLine("║                                          ║");
        Console.WriteLine("║   INSUMOS                                ║");
        Console.WriteLine("║   4. Agregar insumo                      ║");
        Console.WriteLine("║   5. Despachar insumo                    ║");
        Console.WriteLine("║                                          ║");
        Console.WriteLine("║   0. Volver al menu principal            ║");
        Console.WriteLine("║                                          ║");
        Console.WriteLine("╚══════════════════════════════════════════╝");
        Console.WriteLine();
        Console.Write(" Seleccione una opcion: ");
        string gestionElegidaAdmin = Console.ReadLine();

        switch (gestionElegidaAdmin)
        {
            case "1":
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
                    // --- TABLA DE REPUESTOS ---
                    Console.Clear();
                    try
                    {
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
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"❌ Error al cargar repuestos: {ex.Message}");
                    }

                    Console.WriteLine("\nPresione una tecla para volver al menú anterior...");
                    Console.ReadKey();
                }
                else if (opcionInventario == "2")
                {
                    // --- TABLA DE INSUMOS ---
                    Console.Clear();
                    try
                    {
                        DatosInsumos dbInsumos = new DatosInsumos();
                        List<Insumos> insumos = dbInsumos.ObtenerTodosLosInsumos();

                        Console.WriteLine("INSUMOS");
                        Console.WriteLine("┌──────────────────────────┬───────────┬─────────┐");
                        Console.WriteLine($"│ {"Nombre",-24} │ {"Cant.",-9} │ {"Medida",-7} │");
                        Console.WriteLine("├──────────────────────────┼───────────┼─────────┤");

                        if (insumos.Count == 0)
                        {
                            Console.WriteLine($"│ {"No hay insumos registrados.",-46} │");
                        }
                        else
                        {
                            foreach (var insumo in insumos)
                            {
                                string nombreInsumo = insumo.Nombre.Length > 24 ? insumo.Nombre.Substring(0, 21) + "..." : insumo.Nombre;
                                Console.WriteLine($"│ {nombreInsumo,-24} │ {insumo.CantidadDisponible,-9} │ {insumo.UnidadMedida,-7} │");
                            }
                        }
                        Console.WriteLine("└──────────────────────────┬───────────┬─────────┘");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"❌ Error al cargar insumos: {ex.Message}");
                    }

                    Console.WriteLine("\nPresione una tecla para volver al menú anterior...");
                    Console.ReadKey();
                }
                break;
//SEGUIR CON LOS SIGUIENTES CASOS
            case "2":
                Console.Clear();
                Console.WriteLine("╔══════════════════════════════════════════╗");
                Console.WriteLine("║            AGREGAR REPUESTO              ║");
                Console.WriteLine("╠══════════════════════════════════════════╣");
                Console.WriteLine("\nPresione una tecla para continuar...");
                Console.ReadKey();
                break;

            case "3":
                Console.Clear();
                Console.WriteLine("╔══════════════════════════════════════════╗");
                Console.WriteLine("║           DESPACHAR REPUESTO             ║");
                Console.WriteLine("╠══════════════════════════════════════════╣");
                Console.WriteLine("\nPresione una tecla para continuar...");
                Console.ReadKey();
                break;

            case "4":
                Console.Clear();
                Console.WriteLine("╔══════════════════════════════════════════╗");
                Console.WriteLine("║             AGREGAR INSUMO               ║");
                Console.WriteLine("╠══════════════════════════════════════════╣");
                Console.WriteLine("\nPresione una tecla para continuar...");
                Console.ReadKey();
                break;

            case "5":
                Console.Clear();
                Console.WriteLine("╔══════════════════════════════════════════╗");
                Console.WriteLine("║            DESPACHAR INSUMO              ║");
                Console.WriteLine("╠══════════════════════════════════════════╣");
                Console.WriteLine("\nPresione una tecla para continuar...");
                Console.ReadKey();
                break;

            case "0":
                salirMenuAdmin = true;
                Console.WriteLine("\nVolviendo al menú principal...");
                break;

            default:
                Console.WriteLine("\n[Error] Opción no válida. Presione una tecla para continuar.");
                Console.ReadKey();
                break;
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
// Console.WriteLine("║              SISTEMA DE STOCK            ║");
// Console.WriteLine("╠══════════════════════════════════════════╣");
// Console.WriteLine("║                                          ║");
// Console.WriteLine("║   1. Agregar / Despachar                 ║");
// Console.WriteLine("║   2. Modificar / Eliminar                ║");
// Console.WriteLine("║   3. Ver inventario                      ║");
// Console.WriteLine("║   4. Salir                               ║");
// Console.WriteLine("║                                          ║");
// Console.WriteLine("╚══════════════════════════════════════════╝");
// Console.WriteLine();
// Console.WriteLine("  Seleccione una opcion: ");


//------------------------------------------------------------------------
// ---------------------------MENU EMPLEADO-------------------------------
//------------------------------------------------------------------------
// Console.WriteLine("╔══════════════════════════════════════════╗");
// Console.WriteLine("║              SISTEMA DE STOCK            ║");
// Console.WriteLine("╠══════════════════════════════════════════╣");
// Console.WriteLine("║                                          ║");
// Console.WriteLine("║   1. Despachar                           ║");
// Console.WriteLine("║   2. Ver inventario                      ║");
// Console.WriteLine("║   3. Salir                               ║");
// Console.WriteLine("║                                          ║");
// Console.WriteLine("╚══════════════════════════════════════════╝");
// Console.WriteLine();
// Console.WriteLine("  Seleccione una opcion: ");

//------------------------------------------------------------------------
// -----------------------MENU ADMINISTRADOR------------------------------
//------------------------------------------------------------------------
// Console.WriteLine("╔══════════════════════════════════════════╗");
// Console.WriteLine("║              GESTION DE STOCK            ║");
// Console.WriteLine("╠══════════════════════════════════════════╣");
// Console.WriteLine("║                                          ║");
// Console.WriteLine("║   REPUESTOS                              ║");
// Console.WriteLine("║   1. Agregar repuesto                    ║");
// Console.WriteLine("║   2. Despachar repuesto                  ║");
// Console.WriteLine("║                                          ║");
// Console.WriteLine("║   INSUMOS                                ║");
// Console.WriteLine("║   3. Agregar insumo                      ║");
// Console.WriteLine("║   4. Despachar insumo                    ║");
// Console.WriteLine("║                                          ║");
// Console.WriteLine("║   0. Volver al menu principal            ║");
// Console.WriteLine("║                                          ║");
// Console.WriteLine("╚══════════════════════════════════════════╝");
// Console.WriteLine();
// Console.WriteLine("  Seleccione una opcion: ");

//------------------------------------------------------------------------
// ---------------------------MENU EMPLEADO-------------------------------
//------------------------------------------------------------------------
// Console.WriteLine("╔══════════════════════════════════════════╗");
// Console.WriteLine("║              GESTION DE STOCK            ║");
// Console.WriteLine("╠══════════════════════════════════════════╣");
// Console.WriteLine("║                                          ║");
// Console.WriteLine("║   REPUESTOS                              ║");
// Console.WriteLine("║   1. Despachar repuesto                  ║");
// Console.WriteLine("║                                          ║");
// Console.WriteLine("║   INSUMOS                                ║");
// Console.WriteLine("║   2. Despachar insumo                    ║");
// Console.WriteLine("║                                          ║");
// Console.WriteLine("║   0. Volver al menu principal            ║");
// Console.WriteLine("║                                          ║");
// Console.WriteLine("╚══════════════════════════════════════════╝");
// Console.WriteLine();
// Console.WriteLine("  Seleccione una opcion: ");




// Console.WriteLine("╔══════════════════════════════════════════╗");
// Console.WriteLine("║              GESTION DE STOCK            ║");
// Console.WriteLine("╠══════════════════════════════════════════╣");
// Console.WriteLine("║                                          ║");
// Console.WriteLine("║   1. Agregar producto                    ║");
// Console.WriteLine("║   2. Despachar producto                  ║");
// Console.WriteLine("║                                          ║");
// Console.WriteLine("║   0. Volver al menu principal            ║");
// Console.WriteLine("║                                          ║");
// Console.WriteLine("╚══════════════════════════════════════════╝");
// Console.WriteLine("╔══════════════════════════════════════════╗");
// Console.WriteLine("║            TIPO DE PRODUCTO              ║");
// Console.WriteLine("╠══════════════════════════════════════════╣");
// Console.WriteLine("║                                          ║");
// Console.WriteLine("║   1. Repuestos                           ║");
// Console.WriteLine("║   2. Insumos                             ║");
// Console.WriteLine("║                                          ║");
// Console.WriteLine("║   0. Volver                              ║");
// Console.WriteLine("║                                          ║");
// Console.WriteLine("╚══════════════════════════════════════════╝");
// Console.WriteLine("╔══════════════════════════════════════════╗");
// Console.WriteLine("║             AGREGAR PRODUCTO             ║");
// Console.WriteLine("╠══════════════════════════════════════════╣");
// Console.WriteLine("║                                          ║");
// Console.WriteLine("║   Codigo:                                ║");
// Console.WriteLine("║   Nombre:                                ║");
// Console.WriteLine("║   Cantidad inicial:                      ║");
// Console.WriteLine("║   Precio de compra por unidad:           ║");
// Console.WriteLine("║   Stock minimo:                          ║");
// Console.WriteLine("║                                          ║");
// Console.WriteLine("╠══════════════════════════════════════════╣");
// Console.WriteLine("║   1. Confirmar registro                  ║");
// Console.WriteLine("║   0. Cancelar                            ║");
// Console.WriteLine("╚══════════════════════════════════════════╝");
// Console.WriteLine("╔══════════════════════════════════════════╗");
// Console.WriteLine("║            DESPACHAR PRODUCTO            ║");
// Console.WriteLine("╠══════════════════════════════════════════╣");
// Console.WriteLine("║                                          ║");
// Console.WriteLine("║   Codigo o nombre del producto:          ║");
// Console.WriteLine("║   Stock disponible:                      ║");
// Console.WriteLine("║   Cantidad a despachar:                  ║");
// Console.WriteLine("║   Motivo del despacho:                   ║");
// Console.WriteLine("║                                          ║");
// Console.WriteLine("╠══════════════════════════════════════════╣");
// Console.WriteLine("║   1. Confirmar despacho                  ║");
// Console.WriteLine("║   0. Cancelar                            ║");
// Console.WriteLine("╚══════════════════════════════════════════╝");




































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
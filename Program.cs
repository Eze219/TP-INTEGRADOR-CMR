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

        Console.WriteLine("\nPresione una tecla para volver al menú principal...");
        Console.ReadKey();
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

void MenuAdministrador(Usuarios userAdmin)
{
    Console.WriteLine("\n--- PANEL DE ADMINISTRADOR ---");
    Console.WriteLine($"Conectado como: {userAdmin.Nombre} (DNI: {userAdmin.Dni})");
    // Acá podemos agregar la lógica de gestión de usuarios, ver reportes, etc.
}

void MenuEmpleado(Usuarios userEmpleado)
{
    Console.WriteLine("\n--- PANEL DE EMPLEADO ---");
    Console.WriteLine($"Conectado como: {userEmpleado.Nombre} (DNI: {userEmpleado.Dni})");
    // Acá podemos agregar la lógica para registrar entradas/salidas de repuestos e insumos.
}

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
            } else
        {
            Console.WriteLine("\n[Error] Usuario o contraseña incorrectos. Vuelve a intentar.");
        } 
        
    }
    return usuarioLogueado;
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
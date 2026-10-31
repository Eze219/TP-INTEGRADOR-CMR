using Microsoft.Data.SqlClient;
// Instanciamos la clase para poder utilizar los métodos:
// Conexion conexion = new Conexion();

// using (SqlConnection sql = conexion.obtenerConexion())
// {
//     sql.Open();
//     Console.WriteLine("conexion exitosa");
// }


// Creación de usuarios
List<Usuarios> listaUsuarios = new List<Usuarios>();

Usuarios usuarioAdmin = new Usuarios();
usuarioAdmin.IdUsuario = 1;
usuarioAdmin.Nombre = "Carlos";
usuarioAdmin.Dni = 35489123;
usuarioAdmin.UsuarioNombre = "cmartinez";
usuarioAdmin.Contrasena = "Admin2026*";
usuarioAdmin.Rol = "Administrador"; 
listaUsuarios.Add(usuarioAdmin);
DatosUsuarios datosUsuarios = new DatosUsuarios();
int DatosUsuarios = datosUsuarios.GuardarUsuario(usuarioAdmin);
Console.WriteLine("se guardo");

Usuarios usuarioEmpleado = new Usuarios();
usuarioEmpleado.IdUsuario = 2;
usuarioEmpleado.Nombre = "Lucía";
usuarioEmpleado.Dni = 40123789;
usuarioEmpleado.UsuarioNombre = "lfernandez";
usuarioEmpleado.Contrasena = "EmpClave1";
usuarioEmpleado.Rol = "Empleado"; 
listaUsuarios.Add(usuarioEmpleado);

int datosUsuariosEmp1 = datosUsuarios.GuardarUsuario(usuarioEmpleado);
Console.WriteLine("se guardo");


Usuarios usuarioEmpleado2 = new Usuarios();
usuarioEmpleado2.IdUsuario = 3;
usuarioEmpleado2.Nombre = "Matías";
usuarioEmpleado2.Dni = 38765432;
usuarioEmpleado2.UsuarioNombre = "mgomez";
usuarioEmpleado2.Contrasena = "Taller2026";
usuarioEmpleado2.Rol = "Empleado"; 
listaUsuarios.Add(usuarioEmpleado2);

int datosUsuariosEmp2 = datosUsuarios.GuardarUsuario(usuarioEmpleado2);
Console.WriteLine("se guardo");

// Creación de historial de movimientos


List<Historial> listaHistorial = new List<Historial>();

Historial historialIngresoRepuestos = new Historial();
historialIngresoRepuestos.FechaHora = DateTime.Now;
historialIngresoRepuestos.TipoMovimiento = "Ingreso de repuestos";
listaHistorial.Add(historialIngresoRepuestos);

DatosHistorial datosHistorial = new DatosHistorial();
int DatosHistorialIngresoRep = datosHistorial.GuardarMovimientos(historialIngresoRepuestos);
Console.WriteLine("se guardo");

Historial historialEgresoRepuestos = new Historial();
historialEgresoRepuestos.FechaHora = DateTime.Now;
historialEgresoRepuestos.TipoMovimiento = "Egreso de repuestos";
listaHistorial.Add(historialEgresoRepuestos);


int DatosHistorialEgresoRep = datosHistorial.GuardarMovimientos(historialEgresoRepuestos);
Console.WriteLine("se guardo");

Historial historialIngresoInsumos = new Historial();
historialIngresoInsumos.FechaHora = DateTime.Now;
historialIngresoInsumos.TipoMovimiento = "Ingreso de insumos";

listaHistorial.Add(historialIngresoInsumos);
int DatosHistorialIngresoIns = datosHistorial.GuardarMovimientos(historialIngresoInsumos);
Console.WriteLine("se guardo");


Historial historialEgresoInsumos = new Historial();
historialEgresoInsumos.FechaHora = DateTime.Now;
historialEgresoInsumos.TipoMovimiento = "Egreso de insumos";

listaHistorial.Add(historialEgresoInsumos);
int DatosHistorialEgresoIns = datosHistorial.GuardarMovimientos(historialEgresoInsumos);
Console.WriteLine("se guardo");


// Creación de insumos
List<Insumos> listaInsumos = new List<Insumos>();
Insumos insumos = new Insumos();


// Creación de repuestos
List<Repuestos> listaRepuestos = new List<Repuestos>();
Repuestos repuestos = new Repuestos();

/*
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

    switch (opcion)
    {
        case "1":
            Console.Clear();
            Console.WriteLine("=== ACCESO AL SISTEMA DE STOCK ===");
            // Llamamos a tu función intacta
            Usuarios usuarioEncontrado = VerificacionUsuarioContraseña(listaUsuarios);
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
Usuarios VerificacionUsuarioContraseña(List<Usuarios> lista)
{
    Usuarios usuarioLogueado = null;

    while (usuarioLogueado == null)
    {
        Console.Write("\nUsuario: ");
        string usuarioIngresado = Console.ReadLine();

        Console.Write("Contraseña: ");
        string contraseñaIngresada = Console.ReadLine();

        foreach (var u in lista)
        {
            if (u.UsuarioNombre == usuarioIngresado && u.Contrasena == contraseñaIngresada)
            {
                usuarioLogueado = u;
                break; // Encontró coincidencia, salimos del foreach
            }
        }
    
        if (usuarioLogueado == null)
        {
            Console.WriteLine("\n[Error] Usuario o contraseña incorrectos. Vuelve a intentar.");
        }
    }

    return usuarioLogueado; // Devolvemos el usuario ya validado
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
*/





Console.WriteLine("╔══════════════════════════════════════════╗");
Console.WriteLine("║              SISTEMA DE STOCK            ║");
Console.WriteLine("╠══════════════════════════════════════════╣");
Console.WriteLine("║                                          ║");
Console.WriteLine("║   1. Agregar / Despachar                 ║");
Console.WriteLine("║   2. Modificar / Eliminar                ║");
Console.WriteLine("║   3. Ver inventario                      ║");
Console.WriteLine("║   4. Salir                               ║");
Console.WriteLine("║                                          ║");
Console.WriteLine("╚══════════════════════════════════════════╝");
Console.WriteLine();
Console.WriteLine("  Seleccione una opcion: ");



Console.WriteLine("╔══════════════════════════════════════════╗");
Console.WriteLine("║              GESTION DE STOCK            ║");
Console.WriteLine("╠══════════════════════════════════════════╣");
Console.WriteLine("║                                          ║");
Console.WriteLine("║   REPUESTOS                              ║");
Console.WriteLine("║   1. Agregar repuesto                    ║");
Console.WriteLine("║   2. Despachar repuesto                  ║");
Console.WriteLine("║                                          ║");
Console.WriteLine("║   INSUMOS                                ║");
Console.WriteLine("║   3. Agregar insumo                      ║");
Console.WriteLine("║   4. Despachar insumo                    ║");
Console.WriteLine("║                                          ║");
Console.WriteLine("║   0. Volver al menu principal            ║");
Console.WriteLine("║                                          ║");
Console.WriteLine("╚══════════════════════════════════════════╝");
Console.WriteLine();
Console.WriteLine("  Seleccione una opcion: ");
Console.WriteLine("╔══════════════════════════════════════════╗");
Console.WriteLine("║              GESTION DE STOCK            ║");
Console.WriteLine("╠══════════════════════════════════════════╣");
Console.WriteLine("║                                          ║");
Console.WriteLine("║   1. Agregar producto                    ║");
Console.WriteLine("║   2. Despachar producto                  ║");
Console.WriteLine("║                                          ║");
Console.WriteLine("║   0. Volver al menu principal            ║");
Console.WriteLine("║                                          ║");
Console.WriteLine("╚══════════════════════════════════════════╝");
Console.WriteLine("╔══════════════════════════════════════════╗");
Console.WriteLine("║            TIPO DE PRODUCTO              ║");
Console.WriteLine("╠══════════════════════════════════════════╣");
Console.WriteLine("║                                          ║");
Console.WriteLine("║   1. Repuestos                           ║");
Console.WriteLine("║   2. Insumos                             ║");
Console.WriteLine("║                                          ║");
Console.WriteLine("║   0. Volver                              ║");
Console.WriteLine("║                                          ║");
Console.WriteLine("╚══════════════════════════════════════════╝");
Console.WriteLine("╔══════════════════════════════════════════╗");
Console.WriteLine("║             AGREGAR PRODUCTO             ║");
Console.WriteLine("╠══════════════════════════════════════════╣");
Console.WriteLine("║                                          ║");
Console.WriteLine("║   Codigo:                                ║");
Console.WriteLine("║   Nombre:                                ║");
Console.WriteLine("║   Cantidad inicial:                      ║");
Console.WriteLine("║   Precio de compra por unidad:           ║");
Console.WriteLine("║   Stock minimo:                          ║");
Console.WriteLine("║                                          ║");
Console.WriteLine("╠══════════════════════════════════════════╣");
Console.WriteLine("║   1. Confirmar registro                  ║");
Console.WriteLine("║   0. Cancelar                            ║");
Console.WriteLine("╚══════════════════════════════════════════╝");
Console.WriteLine("╔══════════════════════════════════════════╗");
Console.WriteLine("║            DESPACHAR PRODUCTO            ║");
Console.WriteLine("╠══════════════════════════════════════════╣");
Console.WriteLine("║                                          ║");
Console.WriteLine("║   Codigo o nombre del producto:          ║");
Console.WriteLine("║   Stock disponible:                      ║");
Console.WriteLine("║   Cantidad a despachar:                  ║");
Console.WriteLine("║   Motivo del despacho:                   ║");
Console.WriteLine("║                                          ║");
Console.WriteLine("╠══════════════════════════════════════════╣");
Console.WriteLine("║   1. Confirmar despacho                  ║");
Console.WriteLine("║   0. Cancelar                            ║");
Console.WriteLine("╚══════════════════════════════════════════╝");




































Console.WriteLine("╔══════════════════════════════════════════╗");
Console.WriteLine("║           MODIFICAR / ELIMINAR           ║");
Console.WriteLine("╠══════════════════════════════════════════╣");
Console.WriteLine("║                                          ║");
Console.WriteLine("║   REPUESTOS                              ║");
Console.WriteLine("║   1. Modificar repuesto                  ║");
Console.WriteLine("║   2. Eliminar repuesto                   ║");
Console.WriteLine("║                                          ║");
Console.WriteLine("║   INSUMOS                                ║");
Console.WriteLine("║   3. Modificar insumo                    ║");
Console.WriteLine("║   4. Eliminar insumo                     ║");
Console.WriteLine("║                                          ║");
Console.WriteLine("║   0. Volver al menu principal            ║");
Console.WriteLine("║                                          ║");
Console.WriteLine("╚══════════════════════════════════════════╝");
Console.WriteLine();
Console.WriteLine("  Seleccione una opcion: ");



Console.WriteLine("╔══════════════════════════════════════════╗");
Console.WriteLine("║                INVENTARIO                ║");
Console.WriteLine("╠══════════════════════════════════════════╣");
Console.WriteLine("║                                          ║");
Console.WriteLine("║   1. Ver repuestos                       ║");
Console.WriteLine("║   2. Ver insumos                         ║");
Console.WriteLine("║                                          ║");
Console.WriteLine("║   0. Volver al menu principal            ║");
Console.WriteLine("║                                          ║");
Console.WriteLine("╚══════════════════════════════════════════╝");
Console.WriteLine();
Console.WriteLine("  Seleccione una opcion: ");
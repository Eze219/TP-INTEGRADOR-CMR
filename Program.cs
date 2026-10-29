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

/*
// 1. Llamamos a la función de verificación y guardamos el resultado
Console.WriteLine("=== ACCESO AL SISTEMA DE STOCK ===");
Usuarios usuarioEncontrado = VerificacionUsuarioContraseña(listaUsuarios);

// 2. Una vez que tenemos el usuario verificado, pasamos a la bienvenida/menú
Bienvenida(usuarioEncontrado);



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
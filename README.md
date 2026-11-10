# Programación
# Sistema CMR (Control y Mantenimiento de Repuestos e Insumos)

* **Grupo de Trabajo:** CMR
* **Integrantes:** 
  * Erika Cabrera
  * Ezequiel Tomás Melano
  * Ismael Ramos

---

## 1. Descripción breve del sistema
El sistema CMR es una aplicación de consola desarrollada en C# y SQL Server diseñada para optimizar la administración de inventario, el control de stock de repuestos e insumos, y la trazabilidad de los movimientos operativos dentro de un taller de chapa y pintura.

## 2. Principales funcionalidades
* **Autenticación y Control de Roles:** Inicio de sesión diferenciado por perfiles de seguridad (`Admin` y `Empleado`), restringiendo el acceso a las funciones críticas de gestión.
* **Gestión de Inventario (CRUD):** Alta, consulta, modificación y baja lógica (soft-delete) de repuestos e insumos.
* **Módulo de Despachos y Movimientos:** Registro de entradas y salidas de materiales con actualización automática del stock.
* **Alertas de Stock Mítico/Crítico:** Detección en tiempo real de quiebres de inventario o niveles inferiores al mínimo requerido, mostrando avisos dinámicos en consola.
* **Historial de Auditoría:** Registro detallado de cada operación ejecutada vinculada al operador responsable.

## 3. Requisitos necesarios para ejecutar el proyecto
* **Entorno de ejecución:** .NET SDK (Versión 10.0 o superior).
* **Base de datos:** Microsoft SQL Server (o SQL Server Express / LocalDB).
* **Conector:** Paquete NuGet `Microsoft.Data.SqlClient`.
* **Entorno de desarrollo recomendado:** Visual Studio Code o Visual Studio con .NET CLI instalado.

## 4. Modo de uso
1. **Configuración de la Base de Datos:** Ejecutar el script SQL provisto en el repositorio para crear la estructura de tablas (`Usuarios`, `Repuestos`, `Insumos`, `Historial`) e insertar los datos iniciales de prueba.
2. **Cadena de Conexión:** Verificar que la cadena de conexión en el archivo de configuración del proyecto apunte correctamente a tu instancia local de SQL Server.
3. **Ejecución:** Abrir la terminal en la carpeta raíz del proyecto y ejecutar el siguiente comando:
   ```bash
   dotnet run







# Ética y Deontología Profesional
**Grupo:** CMR Tech

**Integrantes:** Erika Cabrera - Ezequiel Melano - Ismael Ramos

**Fecha de la versión presentada:** [15/11/2026]

**Finalidad y usuarios del sistema:** El programa tiene como finalidad facilitar el control y la gestión del stock de repuestos e insumos del almacén de **"Garage"**, un taller de chapa y pintura. Permite consultar el inventario, registrar ingresos y egresos de productos, modificar datos y eliminarlos, manteniendo un control organizado de las existencias.

# 1. Uso de IA y verificación
**Decisión sobre el uso de IA:** El equipo decidió emplear Inteligencia Artificial de forma complementaria y supervisada, utilizándola únicamente como herramienta de apoyo para agilizar la generación de datos de prueba para la base de datos, conservando el control total sobre la arquitectura, la sintaxis final y las reglas de negocio del sistema.
| Aspecto | Registro del equipo |
| - | - |
| Herramienta y fecha | **Google**, Gemini, Modelos de Gemini 3, Nivel de razonamiento: Bajo (Rápido y eficiente) - 22/10/2026 09:00 pm. |
| Delegación | **Tarea delegada:** Generación de sentencias de inserción de datos para 30 repuestos, 35 insumos, 3 usuarios (roles Admin/empleado) y 15 movimientos de historial. **Motivo:** Agilizar la carga de datos de prueba para el sistema. **Decisión conservada:** Conservar el diseño relacional y las restricciones de las tablas del sistema bajo propio juicio, sin realizar cambios a las sentencias de inserción brindadas por la Inteligencia Artificial. |
| Descripción | **Objetivo:** Obtener un script de inserción de datos iniciales para las tablas del proyecto. **Contexto y restricciones:** Se solicitó estructurar los datos para el motor de base de datos correspondiente, especificando la necesidad de ceñirse estrictamente a la sintaxis y buenas prácticas trabajadas en la materia. Además se le brindó una imagen con las tablas relacionales para que guíe su trabajo a través de ella. |
| Discernimiento | Se **rechazó** la primera versión porque la IA generó el código con la sintaxis de MySQL en lugar de SQL Server. Se **corrigió** la directiva solicitando explícitamente el cambio de motor y se **aceptó** la nueva respuesta tras comprobar que la sintaxis de las sentencias coincidía con el lenguaje de SQL Server.|
| Diligencia | **Comprobación:** Se realizó una lectura del código generado y se validó su correcta ejecución en el entorno de SQL Server para asegurar que no arrojara errores de sintaxis. **Evidencia:** Script de inserción SQL adaptado en el entorno de desarrollo. **Verificación:** Realizada por Ezequiel Melano. |

# 2. Datos y finalidad
El sistema de gestión CMR (Control y Mantenimiento de Repuestos e Insumos) es una aplicación de consola desarrollada en C# y SQL Server diseñada para optimizar la administración de inventario y el control de stock en un taller de chapa y pintura. La herramienta es utilizada por el Administrador del taller (para supervisar inventarios, gestionar usuarios y auditar el historial de movimientos) y por los Empleados/Operarios (para consultar disponibilidad y registrar entradas o salidas de repuestos e insumos en el trabajo diario).

| Dato o campo | ¿Identifica o permite identificar a una persona? | Finalidad en el sistema | Decisión y motivo |
| - | - | - | - |
| `nombre_usuario` (Tabla `Usuarios`) | **Sí.** Corresponde al nombre de cuenta (login) de la persona encargada de operar el sistema. | Autenticar el acceso a la aplicación y determinar el rol del usuario (Administrador / Empleado). | **Conservar.** Es indispensable para restringir acciones según privilegios de seguridad. |
| `nombre` y `apellido` (Tabla `Usuarios`) | **Sí.** Guarda el nombre y apellido real del usuario del taller (ej: Carlos Martínez). | Identificar al operador responsable en el log de auditoría al realizar un movimiento de stock. | **Conservar.** Necesario por trazabilidad y responsabilidad operativa dentro del taller. |
| `dni` (Tabla `Usuarios`) | **Sí.** Corresponde al documento nacional de identidad del operario o administrador. | Permitir el registro formal y unívoco del personal vinculado al taller dentro del legajo de usuarios. | **Conservar.** Requerido para la identificación fehaciente del personal autorizado en el sistema. |
| `clave` (Tabla `Usuarios`) | **No.** Es una credencial técnica de acceso (almacenada de forma segura). | Autenticar y validar la identidad del usuario durante la pantalla de inicio de sesión. | **Conservar.** Requisito esencial de seguridad informática para evitar accesos no autorizados. |
| `rol` (Tabla `Usuarios`) | **No.** Es un atributo de perfil del sistema ('Admin' o 'Empleado'). | Definir la estructura de menús y permisos asignados al usuario autenticado. | **Conservar.** Permite segmentar el acceso a funciones críticas (como alta/baja de insumos). |
| `NombreUsuario` (Tabla `Historial`) | **Sí.** Registra qué persona física ejecutó un movimiento específico en el inventario. | Mantener la trazabilidad histórica de entradas, salidas, modificaciones y bajas de materiales. | **Conservar.** Permite responder ante inconsistencias o quiebres injustificados de inventario. |
| Datos de productos (`nombre`, `cantidad`, `stock_minimo`, `precio`) | **No.** Son atributos exclusivamente técnicos y comerciales de los repuestos e insumos. | Registrar la disponibilidad, calcular umbrales de alerta y controlar el valor del stock. | **Conservar.** Constituye el núcleo operativo de la finalidad principal del sistema. | 

**Dato que decidimos no solicitar:** Dirección particular, teléfono personal, correo electrónico personal o fecha de nacimiento de los usuarios. Se excluyeron dado que el sistema opera de forma interna en el taller y no requiere canales de notificación externa, marketing o validación de datos de contacto masivos, aplicando el principio de minimización de datos al prescindir de información no estrictamente necesaria para la gestión de inventario.

**Acceso e información sobre el uso:** Los datos personales de los operadores (nombre, apellido y dni) son consultados únicamente por el Administrador del taller desde el módulo de gestión de usuarios y en los reportes del historial de movimientos. Al momento de registrar un usuario en el sistema, se le informa que su identificación y nombre quedarán vinculados a las operaciones de inventario que realice (entradas, salidas y modificaciones) a fines de auditoría interna y seguridad del taller.

**Demostración:** Todos los registros visualizados en las demostraciones, capturas de pantalla, pruebas de consola y scripts SQL (nombres de usuarios como "Martín Gómez" o "Lucía Fernández") corresponden a datos 100% ficticios diseñados exclusivamente para entornos de prueba. Todos los campos tabulados se encuentran plenamente implementados en la base de datos SQL Server y en la arquitectura actual del proyecto.

# 3. Autoría y licencias
**Aportes del equipo:**
| Nombre | Aportes al equipo |
| - | - |
| Erika Cabrera |  |
| Ezequiel Tomás Melano | Desarrollo de la lógica general en C#, arquitectura del sistema de consola, diseño de la base de datos relacional en SQL Server (tablas Usuarios, Repuestos, Insumos, Historial), implementación de menús de navegación, validaciones de stock mínimo, gestión de control de accesos por roles (Admin/Empleado) y redacción de la documentación técnica y ética del proyecto. |
| Ismael Ramos |  |

| Material utilizado | Fuente oficial y versión | Licencia o autorización | Condiciones relevantes y cómo las cumplimos |
| - | - | - | - |
| .NET SDK / Runtime (Entorno de ejecución de C#) | Microsoft .NET official site (Versión 10.0) | MIT License / Microsoft .NET Library License | Permite el uso y la redistribución del runtime. Se cumple indicando el uso de la plataforma sin modificar sus binarios internos. | 
| Microsoft.Data.SqlClient (Conector SQL Server para C#) | NuGet Gallery (Versión 5.x) | MIT License | Permite su integración comercial y libre en aplicaciones. Se incluye la dependencia en el proyecto y se respeta su licencia de código abierto. |

**Recursos que no utilizamos:** No se utilizaron frameworks gráficos externos (como Windows Forms o WPF, tratándose de una aplicación íntegra de consola), tampoco se incorporaron bibliotecas de terceros para manejo de bases de datos diferentes a las nativas de .NET, ni se integraron recursos gráficos o multimedia externos (imágenes, iconos ni fuentes tipográficas propietarias).

**Licencia del código propio:** MIT License (Versión 2024 / Estándar)

**Fuente oficial del texto:** Open Source Initiative - MIT License. 

**Alcance de LICENSE:** Se aplica de forma exclusiva a todo el código fuente original desarrollado por el equipo en los archivos .cs y scripts de la solución CMR. Los paquetes y librerías externas de Microsoft (.NET y Microsoft.Data.SqlClient) conservan de manera independiente sus propias licencias especificadas por sus autores originales.

**Permiso 1:** **Uso y modificación:** La licencia permite expresamente a cualquier persona obtener una copia de este software y de los archivos de documentación asociados para utilizarlo, copiarlo, modificarlo y fusionarlo sin restricciones, lo cual se reconoce en el texto oficial bajo la cláusula "to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software".

**Permiso 2:** **Distribución y uso comercial:** Otorga el derecho de comercializar y redistribuir el software bajo los términos de la misma licencia, reconocido en la fórmula "permission is hereby granted, free of charge... to deal in the Software without restriction".

**Condición:** **Aviso de autoría y copyright:** La única condición estricta que exige la licencia MIT es incluir el aviso de copyright original y la declaración de permiso en todas las copias o partes sustanciales del software, tal como se establece en la cláusula final: "The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software". Esto se cumplirá incorporando el archivo LICENSE en la raíz del repositorio de GitHub.

**Justificación y acuerdo:** El equipo acordó unánimemente seleccionar la licencia **MIT** porque se alinea perfectamente con el espíritu académico y de código abierto del proyecto. Permite total libertad de uso, modificación y aprendizaje para cualquier evaluador o compañero que consulte el repositorio (cumpliendo con lo que el equipo desea permitir), exigiendo únicamente como contraprestación la conservación del aviso de autoría original sin imponer restricciones complejas de copyleft.
# 4. Medida de cuidado y pruebas
**Operación y riesgo:** La consulta de stock disponible de insumos o repuestos en el taller de chapa y pintura. Busca prevenir el desabastecimiento operativo al no advertir a tiempo cuando un producto crítico se encuentra por debajo de los límites mínimos configurados.

**Personas afectadas:** Protege al administrador del taller (brindándole información oportuna para la gestión de compras) y, de forma indirecta, a los operarios y clientes al evitar paradas en la producción.

**Medida implementada:** Se añadió una doble validación condicional en el sistema. Por un lado, al consultar el inventario (MostrarTablaRepuestos y MostrarTablaInsumos), el sistema evalúa la cantidad disponible contra el stock mínimo definido para generar alertas visuales automáticas en la consola. Por otro lado, al despachar stock (GestionarDespacharRepuesto y GestionarDespacharInsumo), se valida de forma inmediata si el stock remanente tras la salida queda por debajo del mínimo, emitiendo una advertencia en tiempo real para prevenir el desabastecimiento operativo.

**Ubicación en el proyecto:** Program.csProgram.cs (métodos MostrarTablaRepuestos, MostrarTablaInsumos, GestionarDespacharRepuesto y GestionarDespacharInsumo).

**Ubicación de la prueba:** Verificación manual en la pantalla de gestión de stock y en el módulo de despachos dentro del panel de administración/empleado.

**Integrante que verificó:** Ezequiel Melano.

Código de la medida:
```csharp
// 1. Alerta visual al consultar el inventario (MostrarTablaRepuestos/MostrarTablaInsumos)
if (repuesto.CantidadDisponible <= repuesto.StockMinimo)
{
    alertasStock.Add($"⚠️ {repuesto.Nombre} (Actual: {repuesto.CantidadDisponible} | Mínimo: {repuesto.StockMinimo})");
};
// ========================================================================================
if (insumo.CantidadDisponible <= insumo.StockMinimo)
{
alertasStock.Add($"⚠️ {insumo.Nombre} (Actual: {insumo.CantidadDisponible} | Mínimo: {insumo.StockMinimo})");
};

// 2. Alerta visual inmediata en tiempo real al despachar (GestionarDespacharRepuesto/GestionarDespacharInsumo)
if (repuestoEncontrado.CantidadDisponible <= repuestoEncontrado.StockMinimo)
{
    Console.WriteLine("\n════════════════════════════════════════════════════════");
    Console.WriteLine($"⚠️ ¡ATENCIÓN! El repuesto quedó por debajo del stock mínimo:");
    Console.WriteLine($"   Repuesto: {repuestoEncontrado.Nombre}");
    Console.WriteLine($"   Stock Actual: {repuestoEncontrado.CantidadDisponible} | Mínimo requerido: {repuestoEncontrado.StockMinimo}");
    Console.WriteLine("════════════════════════════════════════════════════════");
};
// ==========================================================================================
if (insumoEncontrado.CantidadDisponible <= insumoEncontrado.StockMinimo)
{
    Console.WriteLine("\n════════════════════════════════════════════════════════");
    Console.WriteLine($"⚠️ ¡ATENCIÓN! El insumo quedó por debajo del stock mínimo:");
    Console.WriteLine($"   Insumo: {insumoEncontrado.Nombre}");
    Console.WriteLine($"   Stock Actual: {insumoEncontrado.CantidadDisponible} {insumoEncontrado.UnidadMedida} | Mínimo: {insumoEncontrado.StockMinimo} {insumoEncontrado.UnidadMedida}");
    Console.WriteLine("════════════════════════════════════════════════════════");
};
```
**Comprobación realizada:** Se realizó una prueba de ejecución funcional levantando el sistema de consola conectado a la base de datos SQL Server. Se ingresó tanto al módulo de inventario (Ver repuestos e Insumos) como al módulo de salidas (Despachar repuesto e Insumo) para comprobar que el sistema evalúa el umbral mínimo y despliega dinámicamente el panel de advertencia en caso de stock crítico.

| Caso probado | Datos o acción | Resultado esperado | Resultado obtenido |
| --- | --- | --- | --- |
| Operación válida (Consulta) | Consultar un repuesto con cantidad superior al stock mínimo (ej: 15 unidades con mínimo de 5). | Mostrar la tabla de inventario de forma normal con el mensaje de estado saludable (`✅`). | La tabla se despliega correctamente indicando que los niveles son óptimos y sin advertencias activas. |
| Situación problemática (Consulta) | Consultar un repuesto con stock igual o menor al mínimo (ej: 2 unidades con mínimo de 5). | El sistema lista el elemento y genera una alerta visual destacada (`🚨 / ⚠️`) informando el déficit. | El sistema detecta el quiebre de stock y arroja el panel de alerta detallando el nombre, cantidad actual y el mínimo requerido. |
| Despacho crítico (Salida) | Despachar unidades de un repuesto provocando que el stock remanente sea menor o igual al mínimo. | Confirmar la salida del material y mostrar inmediatamente en pantalla una alerta destacada advirtiendo el quiebre de stock. | Se descuenta el stock correctamente, se registra el movimiento de "Salida" en el historial y se imprime la caja de advertencia con el nivel crítico actual. |

**Límite de esta medida:** La medida es puramente informativa y visual dentro de la consola; actualmente no genera una orden de compra automática a proveedores ni envía notificaciones externas por correo electrónico al administrador.

### 5. Alcance y limitaciones

| Función o limitación | Estado y evidencia | A quién podría afectar y cómo | Acción realizada o mejora necesaria |
| --- | --- | --- | --- |
| Control de accesos y roles (Admin / Empleado) | **Probada.** Verificado en la pantalla de inicio de sesión y filtrado de menús según permisos en la base de datos SQL Server. | Afecta al personal del taller garantizando que los operarios no accedan a funciones críticas de configuración y que el administrador supervise todo el flujo. | **Acción realizada:** Implementación de consultas parametrizadas con autenticación de DNI/Usuario y validación de roles en C#. |
| Alertas automáticas de stock mínimo | **Probada.** Comprobado al consultar inventarios (`MostrarTablaRepuestos`/`Insumos`) y al ejecutar salidas en el módulo de despachos. | Afecta al administrador y operarios advirtiendo de manera visual e inmediata sobre quiebres inminentes o productos críticos desabastecidos. | **Acción realizada:** Doble validación condicional que compara la cantidad disponible contra el umbral mínimo y despliega paneles dinámicos en consola. |
| Generación automática de órdenes de compra a proveedores | **Pendiente.** La funcionalidad no se encuentra desarrollada en esta versión de consola. | Afecta al administrador del taller, quien debe realizar las compras de reabastecimiento de forma manual externa al sistema. | **Próximo paso / Medida:** Incorporar en futuras versiones un módulo de notificación por correo o generación de archivos de pedido para los proveedores. |

**Uso que podemos mostrar:** El sistema se encuentra totalmente operativo para realizar altas, bajas lógicas, consultas de inventario, control de roles de usuarios, registro de historial de movimientos y alertas de stock en tiempo real dentro del entorno local de un taller de chapa y pintura.

**Antes de un uso más exigente:** Sería necesario realizar una migración de la interfaz de consola a una aplicación web o de escritorio con interfaz gráfica (UI), implementar pruebas unitarias automatizadas (`xUnit`), añadir encriptación de contraseñas de nivel avanzado y conectar un servicio de correo electrónico automatizado para alertas externas antes de ofrecerlo para un entorno de producción masivo o multi-sucursal.
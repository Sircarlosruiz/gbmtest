Gbm-Test
Este proyecto es una aplicación backend de prueba que trata sobre un sistema facturación que permite a los usuarios crear y gestionar facturas.

Requisitos
Para ejecutar este proyecto, necesitarás:

.NET Core 3.1 o superior
Un servidor de base de datos SQL Server
Configuración
Clona este repositorio en tu máquina local usando git clone git@github.com:Sircarlosruiz/gbmtest.git

Asegúrate de que tienes .NET Core SDK instalado en tu máquina. Puedes verificarlo ejecutando el comando dotnet --version en tu terminal. Si no lo tienes instalado, puedes descargarlo desde aquí.

Navega hasta el directorio del proyecto clonado y ejecuta dotnet restore para restaurar las dependencias del proyecto.

Asegúrate de tener un servidor de base de datos MySql en ejecución. Deberás actualizar la cadena de conexión en el archivo appsettings.json para que apunte a tu servidor de base de datos.

Ejecuta dotnet ef database update para aplicar las migraciones de la base de datos y crear la base de datos en tu servidor SQL Server.

Ejecución
Para ejecutar el proyecto, simplemente ejecuta dotnet run en la raíz del proyecto. Esto iniciará la aplicación en http://localhost:5000.

Funcionalidad
La aplicación permite a los usuarios crear facturas con múltiples detalles. Cada detalle de la factura está asociado a un producto. La aplicación también calcula el IVA para cada detalle de la factura.

Contribución
Las contribuciones son bienvenidas. Por favor, abre un issue para discutir lo que te gustaría cambiar o añadir.

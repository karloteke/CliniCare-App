# CliniCare :hospital:

Esta aplicación está diseñada para llevar a cabo la gestión de una clínica médica. Toda la información es guardada en ficheros JSON independientes.

Dicha aplicación esta dividida en dos zonas diferenciadas:

# Zona pública:

### Los pacientes podrán:

-	Añadir una cita.

-	Buscar citas: Los pacientes podrán buscar las citas registradas en la aplicación por su DNI.

### El personal de la clínica podrá:

-	Registrarse con sus datos (Nombre, contraseña, email y una clave de acceso) Esta clave es conocida únicamente por el personal de la clínica, garantizando así la privacidad de la información de los pacientes en la zona privada.

-	Acceder a la zona privada: Introduciendo el usuario y la contraseña.

# Zona privada:

Esta zona será exclusivamente para el personal de la clínica donde podrán:

-	Añadir nuevos pacientes.

-	Visualizar datos de los pacientes.

-	Añadir y visualizar citas de los pacientes.

-	Añadir y visualizar el historial médico de los pacientes.

-	Buscar pacientes: El personal de la clínica podrá buscar a los pacientes por su DNI.


# Instrucciones de ejecución en local

  dotnet run --project Presentation\CliniCareApp.Presentation.csproj


# Instrucciones de ejecución en un contenedor Docker

- ### Descarga la imagen del contenedor desde Docker Hub
   
  docker pull carlota36/clinicare:1.0

- ### Ejecuta el contenedor
   
  docker run -it -p 7011:7011 -v ${PWD}:/app/SharedFolder carlota36/clinicare:1.0





# CliniCare - API RESTful :hospital:

CliniCare es una aplicación diseñada para facilitar la gestión de la clínica, ofreciendo una solución para la programación de citas, seguimiento de pacientes/usuarios y acceso a información médica. 
La aplicación administra la información en una base de datos utilizando un ORM (Mapeo Objeto-Relacional) y sigue una estrategia "Code First", utilizando migraciones para crear y mantener la estructura de la base de datos.

### ➡️ Este proyecto se integra con una interfaz web contenerizada [CliniCare-Vue](https://github.com/karloteke/CliniCare-Vue)

### ➡️ Orquesta los contenedores de la API, la base de datos y la interfaz web mediante Docker Compose

## Funcionalidades 

### Zona pública (Usuarios no registrados)

1. **Registro de usuarios**: Los usuarios pueden registrarse proporcionando el nombre de usuario, email y contraseña.
2. **Inicio de sesión**: Los usuarios pueden iniciar sesión en la aplicación utilizando sus credenciales registradas (Nombre de usuario y contraseña).

### Zona Privada (Usuarios registrados con Rol de usuario)

1. **Creación de pacientes**: Los usuarios pueden crear nuevos perfiles de pacientes ingresando su nombre, apellido, dirección, DNI y teléfono.
2. **Creación de citas**: Los usuarios pueden crear citas con los siguientes campos: área, nombre del médico, fecha, hora, urgencia y DNI del paciente al que se asocia la cita.
3. **Consulta de citas**: Los usuarios pueden ver sus citas introduciendo su DNI. Pueden ordenarse por fecha.

### Zona Privada (Usuarios registrados con Rol de administrador)

1. **Gestión de Pacientes**: Los administradores pueden listar, crear y eliminar pacientes. Pueden filtrar por nombre, apellido y Dni y pueden ordenar por nombre.
2. **Gestión de Citas**: Los administradores pueden listar, crear y eliminar citas programadas. Pueden filtrar por área, nombre de medico y ordenar por ugencia.
3. **Gestión de Usuarios**: Los administradores pueden listar todos los usuarios registrados, crear y eliminar cuentas de usuarios. Pueden buscar por nombre de usuarios.
4. **Gestión del Historial Médico**: Los administradores pueden listar, crear y eliminar el historial médico de los pacientes. Pueden buscar por el nombre del médico y por el DNI del paciente.

# Intrucción creación Docker-compose
```sh
docker-compose up --build --force-recreate -d
```
# Instrucción borrar Docker-compose
```sh
docker compose down --rmi all --volumes
```

<br>
<br>


# CliniCare - Consola :hospital:

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
  ```sh
  dotnet run --project Presentation\CliniCareApp.Presentation.csproj
  ```

# Instrucciones de ejecución en un contenedor Docker

### Descarga la imagen del contenedor desde Docker Hub
  ```sh 
  docker pull carlota36/clinicare:1.0
  ```
### Ejecuta el contenedor
  ```sh
  docker run -it -p 7011:7011 -v ${PWD}:/app/SharedFolder carlota36/clinicare:1.0
  ```




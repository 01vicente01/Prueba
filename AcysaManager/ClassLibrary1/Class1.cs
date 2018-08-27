using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ClassLibrary1
{
    class Articulo
    {
        private string producto;
        private string referencia;
        private double precioProveedor;
        private int dtoProveedor;
        private double precioProveedorNeto;
        private double pvpAcysa;
        private DateTime actualizacionDePrecio;
        private string material;
        private string aplicacion;
        private string marca;
        private string proveedor;
        //Los historicos se van añadiendo por el final, por lo que el que se tomará como actual será el último de la lista.

        public Articulo()
        {
            //Constructor con los valores nulos
        }


        //Constructores con diferentes campos de incialización
        public Articulo(string referencia, string producto, double precioProveedor, int dtoProveedor, string marca, string proveedor)
        {
            this.referencia = referencia;
            this.producto = producto;
            this.precioProveedor = precioProveedor;
            this.dtoProveedor = dtoProveedor;
            this.marca = marca;
            this.proveedor = proveedor;
            precioProveedorNeto = CalcularPrecioProveedorNeto();    //Actualizar precio proveedor neto con los datos anteriores
            pvpAcysa = CalcularPvpAcysa();                          //Actualizar P.V.P Acysa con los datos anteriores
            actualizacionDePrecio = ObtenerFechaActual();           //Ponemos la fecha actual
        }


        public Articulo(string referencia, string producto, double precioProveedor, int dtoProveedor, string material, string aplicacion, string marca, string proveedor)
        {
            this.referencia = referencia;
            this.producto = producto;
            this.precioProveedor = precioProveedor;
            this.dtoProveedor = dtoProveedor;
            this.material = material;
            this.aplicacion = aplicacion;
            precioProveedorNeto = CalcularPrecioProveedorNeto();    //Actualizar precio proveedor neto con los datos anteriores
            pvpAcysa = CalcularPvpAcysa();                          //Actualizar P.V.P Acysa con los datos anteriores
            actualizacionDePrecio = ObtenerFechaActual();           //Ponemos la fecha actual

        }

        /// <summary>
        /// Cambiar el código de referencia de un arituclo o leerlo
        /// </summary>
        public string Referencia
        {
            get
            {
                return referencia;
            }

            set
            {
                referencia = value;
            }
        }

        /// <summary>
        /// Cambiar el nombre de un arituclo o leerlo
        /// </summary>
        public string Producto
        {
            get
            {
                return producto;
            }

            set
            {
                producto = value;
            }
        }

        /// <summary>
        /// Cambiar el precio de proveedor de un arituclo (se actualiza el precio proveedir neto, el P.V.P Acysa y la actualizacion de precio), o leerlo
        /// </summary>
        public double PrecioProveedor
        {
            get
            {
                return precioProveedor;
            }

            set
            {
                precioProveedor = value;
                precioProveedorNeto = CalcularPrecioProveedorNeto();  //Asignamos el nuevo precio de proveedor y actualizamos el precio de proveedor neto, el P.V.P Acysa y la actualización de precio
                pvpAcysa = CalcularPvpAcysa();
                actualizacionDePrecio = ObtenerFechaActual();
            }
        }

        /// <summary>
        /// Cambiar el descuento de proveedor de un arituclo (se actualiza el precio proveedir neto y el P.V.P Acysa), o leerlo
        /// </summary>
        public int DtoProveedor
        {
            get
            {
                return dtoProveedor;
            }

            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException(String.Format("{0) no está permitido, debe ser un número entero entre 0 y 100", value));
                }
                else
                {
                    dtoProveedor = value;
                    precioProveedorNeto = CalcularPrecioProveedorNeto();  //Asignamos el nuevo descuento de proveedor y actualizamos el precio de proveedor neto y el P.V.P Acysa
                    pvpAcysa = CalcularPvpAcysa();
                }

            }
        }

        /// <summary>
        /// Leer el precio de proveedor neto de un arituclo, no tiene permisos de escritura
        /// </summary>
        public double PrecioProveedorNeto
        {
            get
            {
                return precioProveedorNeto;
            }
        }

        /// <summary>
        /// Leer el P.V.P Acysa de un arituclo, no tiene permisos de escritura
        /// </summary>
        public double PvpAcysa
        {
            get
            {
                return pvpAcysa;
            }
        }

        /// <summary>
        /// Leer a últiam actualización de precio en formato DateTime, no tiene permisos de escritura
        /// </summary>
        public DateTime ActualizacionDePrecio
        {
            get
            {
                return actualizacionDePrecio;
            }
        }

        /// <summary>
        /// Cambiar el material de un arituclo o leerlo
        /// </summary>
        public string Material
        {
            get
            {
                return material;
            }

            set
            {
                material = value;
            }
        }

        /// <summary>
        /// Cambiar la aplicacion de un arituclo o leerlo
        /// </summary>
        public string Aplicacion
        {
            get
            {
                return aplicacion;
            }

            set
            {
                aplicacion = value;
            }
        }

        /// <summary>
        /// Lee la marca del artículo
        /// </summary>
        public string Marca
        {
            get
            {
                return marca;
            }
        }

        /// <summary>
        /// Lee el proveedor del producto
        /// </summary>
        public string Proveedor
        {
            get
            {
                return proveedor;
            }
        }



        // Funciones privadas de la clase para calcular datos.

        /// <summary>
        /// Calcula el precio neto de proveedor de un articulo teniendo en cunato el precio de proveedor y el descuento de proveedor
        /// </summary>
        /// <returns>El precio neto del proveedor calculado</returns>
        private double CalcularPrecioProveedorNeto()
        {
            return precioProveedor * (1 - dtoProveedor / 100);
        }

        /// <summary>
        /// Calcula el PVPAcysa de un articulo teniendo en cuneta el precio de proveedor neto
        /// </summary>
        /// <returns>El pvp Acysa calculado</returns>
        private double CalcularPvpAcysa()
        {
            return precioProveedorNeto * 2;
        }

        private DateTime ObtenerFechaActual()
        {
            return DateTime.Now;
        }



        class Linea        // Linea de un Albrarán
        {
            private Articulo articulo;
            private double numero;
            private string referencia;
            private string descripcion;
            private double precio;
            private int dto;
            private double importe;
            private int dtoCorrectorDePrecios;
            private double coste;
            private double importeAnalisis;
            private double beneficio;
            private double benefPorCoste;

            /// <summary>
            /// Constructor de la clase de una linea de un albarán (los calculos los hace la propia estructura)
            /// </summary>
            /// <param name="articulo">Artículo el cual queremos poner en la linea</param>
            /// <param name="numero">Número total de artículos a poner en el albarán</param>
            /// <param name="dto">Descuento del cliente</param>
            /// <param name="dtoCorrectorDePrecios">Descuento corrector de precios</param>
            public Linea(Articulo articulo, double numero, int dto, int dtoCorrectorDePrecios)
            {
                this.articulo = articulo;
                if (numero < 0)
                    throw new ArgumentException(String.Format("{0} no es valido", numero));
                else
                    this.numero = numero;
                if (dto < 0 || dto > 100)
                {
                    throw new ArgumentException(String.Format("{0) no está permitido, debe ser un número entero entre 0 y 100", dto));
                }
                this.dtoCorrectorDePrecios = dtoCorrectorDePrecios;
                descripcion = articulo.Producto;
                referencia = articulo.Referencia;
                precio = articulo.PvpAcysa;
                importe = CalcularImporte();
                coste = articulo.PrecioProveedorNeto;
                importeAnalisis = CalcularImporteAnalisis();
                beneficio = CalcularBeneficio();
                benefPorCoste = CalcularBeneficioPorCoste();
            }

            /// <summary>
            /// Lee el artículo
            /// </summary>
            public Articulo Articulo
            {
                get
                {
                    return articulo;
                }
            }

            /// <summary>
            /// Lee el número de artículos
            /// </summary>
            public double Numero
            {
                get
                {
                    return numero;
                }

                set
                {
                    if (value < 0)          //Comprobamos si el valor es negativo y en su caso mandamos un error
                        throw new ArgumentException(String.Format("{0} no es valido", value));
                    else
                        numero = value;
                }
            }

            /// <summary>
            /// Lee la referencia del artículo
            /// </summary>
            public string Referencia
            {
                get
                {
                    return referencia;
                }
            }

            /// <summary>
            /// Lee la descripción del artículo
            /// </summary>
            public string Descripion
            {
                get
                {
                    return descripcion;
                }
            }

            /// <summary>
            /// Lee el precio del artículo
            /// </summary>
            public double Precio
            {
                get
                {
                    return precio;
                }
            }

            /// <summary>
            /// Leer el descuento o cambiarlo, volviendo a calcular los demás datos
            /// </summary>
            public int Dto
            {
                get
                {
                    return dto;
                }

                set
                {
                    if (value < 0 || value > 100)
                    {
                        throw new ArgumentException(String.Format("{0) no está permitido, debe ser un número entero entre 0 y 100", value));
                    }
                    else
                    {           //Asignamos el nuevo valor de descuento y volvemos a calcular los demás datos
                        dto = value;
                        importe = CalcularImporte();
                        importeAnalisis = CalcularImporteAnalisis();
                        beneficio = CalcularBeneficio();
                        benefPorCoste = CalcularBeneficioPorCoste();
                    }
                }
            }

            /// <summary>
            /// Lee el importe al cliente del artículo
            /// </summary>
            public double Importe
            {
                get
                {
                    return importe;
                }
            }

            /// <summary>
            /// Lee o Modifica el descuento corrector de precios
            /// </summary>
            public int DtoCorrectorDePrecios
            {
                get
                {
                    return dtoCorrectorDePrecios;
                }

                set
                {
                    dtoCorrectorDePrecios = value;
                }
            }

            /// <summary>
            /// Lee el coste del artículo
            /// </summary>
            public double Coste
            {
                get
                {
                    return coste;
                }
            }

            /// <summary>
            /// Lee el importe del analisis
            /// </summary>
            public double ImporteAnalisis
            {
                get
                {
                    return importeAnalisis;
                }
            }

            /// <summary>
            /// Lee el beneficio de una linea
            /// </summary>
            public double Beneficio
            {
                get
                {
                    return beneficio;
                }
            }

            /// <summary>
            /// Lee el beneficio en porcentaje por coste
            /// </summary>
            public double BeneficioPorCoste
            {
                get
                {
                    return benefPorCoste;
                }
            }

            //Funciones para calcular datos

            /// <summary>
            /// Calcula el importe para el cliente
            /// </summary>
            /// <returns>el importe del cliente</returns>
            private double CalcularImporte()
            {
                return numero * (precio * (1 - dto / 100));
            }

            /// <summary>
            /// Calcula el importe del analisis
            /// </summary>
            /// <returns>El importe del analisi</returns>
            private double CalcularImporteAnalisis()
            {
                return coste * numero;
            }

            /// <summary>
            /// Calcula el Beneicio
            /// </summary>
            /// <returns>El beneficio</returns>
            private double CalcularBeneficio()
            {
                return importe - importeAnalisis;
            }

            private double CalcularBeneficioPorCoste()
            {
                return 100 * beneficio / importeAnalisis;
            }
        }

        class Albaranes
        {
            // Empezamos a definir el Albaran como una lista de lineas

            private ArrayList albaran;

            /// <summary>
            /// Constructor de la clase
            /// </summary>
            public Albaranes()
            {
                albaran = new ArrayList();
            }

            /// <summary>
            /// Constructor de la clase con una linea creada
            /// </summary>
            /// <param name="articulo">Artículo que se quiere incluir en la línea</param>
            /// <param name="numero">Número de artículos</param>
            /// <param name="dto">Descuento del cliente</param>
            /// <param name="dtoCorrectorDePrecios">Descuento corrector de precios</param>
            public Albaranes(Articulo articulo, double numero, int dto, int dtoCorrectorDePrecios)
            {
                albaran = new ArrayList();
                albaran.Add(new Linea(articulo, numero, dto, dtoCorrectorDePrecios));
            }

            //Definimos los metodos de la clase que nos ayudaran a organizar el albaran

            /// <summary>
            /// Añade una linea al Albarán
            /// </summary>
            /// <param name="articulo">Artículo que se quiere incluir en la línea</param>
            /// <param name="numero">Número de artículos</param>
            /// <param name="dto">Descuento del cliente</param>
            /// <param name="dtoCorrectorDePrecios">Descuento corrector de precios</param>
            public void AñadirLinea(Articulo articulo, double numero, int dto, int dtoCorrectorDePrecios)
            {
                albaran.Add(new Linea(articulo, numero, dto, dtoCorrectorDePrecios));
            }

            /// <summary>
            /// Devuleve la cantidad de lineas que hay en el albarán
            /// </summary>
            /// <returns></returns>
            private int CantidaLineas()
            {
                return albaran.Count;
            }

            /// <summary>
            /// Inserta una linea en una posicion concreta del albarán
            /// </summary>
            /// <param name="posicion">posición en la que se quiere insertar (Recordar que la primera psición es la posición '0')</param>
            /// <param name="articulo">Artículo que se quiere incluir en la línea</param>
            /// <param name="numero">Número de artículos</param>
            /// <param name="dto">Descuento del cliente</param>
            /// <param name="dtoCorrectorDePrecios">Descuento corrector de precios</param>
            public void InsertarEnPosicion(int posicion, Articulo articulo, double numero, int dto, int dtoCorrectorDePrecios)
            {
                albaran.Insert(posicion, new Linea(articulo, numero, dto, dtoCorrectorDePrecios));
            }

            /// <summary>
            /// Elimina un linea en una posición en concreto
            /// </summary>
            /// <param name="posicion">Posición que se quiere eliminar</param>
            public void EliminarLinea(int posicion)
            {
                albaran.RemoveAt(posicion);
            }

            /// <summary>
            /// Elimina todas las lineas del albarán
            /// </summary>
            public void BorrarTodo()
            {
                albaran.Clear();
            }

            /// <summary>
            /// Devuelve la línea del albaran del índice dado
            /// </summary>
            /// <param name="indice">Índice el la linea buscada</param>
            /// <returns>La línea buscada</returns>
            public Linea LeerLinea(int indice)
            {
                return (Linea)albaran[indice];
            }

            /// <summary>
            /// Busca si existe una línea en concreto
            /// </summary>
            /// <param name="buscada">Línea buscada</param>
            /// <returns>El índice de la línea si está se encuentra en el albarán o -1 si está no se encuentra.</returns>
            public int BuscarLinea(Linea buscada)
            {
                return albaran.IndexOf(buscada);
            }
        }

        class Cliente
        {
            private string nombre;
            private string tlf;
            private string email;
            private Proyecto obra;
            private List<Tarea> tareas;

            //Varios constructores 


            public Cliente()
            {

            }


            public Cliente(string nombre, string tlf, string email, Proyecto obra)
            {
                this.nombre = nombre;
                this.tlf = tlf;
                this.email = email;
                this.obra = obra;
                tareas = new List<Tarea>();
            }

            //Permisos

            /// <summary>
            /// Lee y cambia el nombre del cliente
            /// </summary>
            public string Nombre
            {
                get
                {
                    return nombre;
                }

                set
                {
                    nombre = value;
                }
            }

            /// <summary>
            /// Lee y cambia el teléfono del cliente
            /// </summary>
            public string Tlf
            {
                get
                {
                    return tlf;
                }

                set
                {
                    tlf = value;
                }
            }

            /// <summary>
            /// Lee y cambia el email del cliente
            /// </summary>
            public string Email
            {
                get
                {
                    return email;
                }

                set
                {
                    email = value;
                }
            }

            /// <summary>
            /// Lee y cambia la obra del cliente
            /// </summary>
            public Proyecto Obra
            {
                get
                {
                    return obra;
                }

                set
                {
                    obra = value;
                }
            }


            /// <summary>
            /// Añade una tarea a la lista de tareas del cliente
            /// </summary>
            /// <param name="tarea">Nombre de la tarea</param>
            /// <param name="horas">Número de horas</param>
            /// <param name="cliente"></param>
            /// <param name="fecha"></param>
            /// <param name="empleado"></param>
            public void AñadirTarea(string tarea, int horas, Cliente cliente, DateTime fecha, Empleado empleado)
            {
                tareas.Add(new Tarea(tarea, horas, cliente, fecha, empleado));
            }

            /// <summary>
            /// Añade un empleado a una tarea
            /// </summary>
            /// <param name="tarea">Tarea a la que se quiere agregar el empleado</param>
            /// <param name="empleado">Empleado que se quiere añadir</param>
            public void AñadirEmpleadoATarea(Tarea tarea, Empleado empleado)
            {
                tarea.AñadirEmpleado(empleado);
            }

            public void AñadirEmpleadoATarea(int indice, Empleado empleado)
            {
                tareas[indice].AñadirEmpleado(empleado);
            }
        }

        class Empleado
        {
            private string nombre;
            private string tlf;
            private string email;
            private string dni;
            private Proyecto obra;
            private ArrayList Tareas;


            //Varios constructores

            public Empleado()
            {

            }

            public Empleado(string nombre, string tlf, string email, string dni, Proyecto obra)
            {
                this.nombre = nombre;
                this.tlf = tlf;
                this.email = email;
                this.dni = dni;
                this.obra = obra;
            }

            /// <summary>
            /// Lee y cambia el nombre del empleado
            /// </summary>
            public string Nombre
            {
                get
                {
                    return nombre;
                }

                set
                {
                    nombre = value;
                }
            }

            /// <summary>
            /// Lee y cambia el teléfono del empleado
            /// </summary>
            public string Tlf
            {
                get
                {
                    return tlf;
                }

                set
                {
                    tlf = value;
                }
            }

            /// <summary>
            /// Lee y cambia el email del empleado
            /// </summary>
            public string Email
            {
                get
                {
                    return email;
                }

                set
                {
                    email = value;
                }
            }

            /// <summary>
            /// Lee y cambia el dni del empleado
            /// </summary>
            public string DNI
            {
                get
                {
                    return dni;
                }

                set
                {
                    dni = value;
                }
            }

            /// <summary>
            /// Lee y cambia la obra del empleado
            /// </summary>
            public Proyecto Obra
            {
                get
                {
                    return obra;
                }

                set
                {
                    obra = value;
                }
            }



        }

        class Proyecto
        {
            private string obra;
            private Cliente cliente;
            private Empleado[] empleados = new Empleado[7];
            private Documentos documentos;
            ArrayList Tareas;

        }

        class Documentos
        {

        }

        class Tarea
        {
            private string tarea;
            private int horas;
            private Cliente cliente;
            private Empleado[] empleados = new Empleado[7];
            private int n = 0;              //Numero de elementos actuales en el array
            private DateTime fecha;

            //Varios constructores
            // Hay un constructor para cada número de empleados para la tarea

            public Tarea()
            {

            }

            public Tarea(string tarea, int horas, Cliente cliente, DateTime fecha, Empleado empleado)
            {
                this.tarea = tarea;
                this.horas = horas;
                this.cliente = cliente;
                this.fecha = fecha;
                empleados[0] = empleado;
            }

            public Tarea(string tarea, int horas, Cliente cliente, DateTime fecha, Empleado empleado1, Empleado empleado2)
            {
                this.tarea = tarea;
                this.horas = horas;
                this.cliente = cliente;
                this.fecha = fecha;
                empleados[0] = empleado1;
                empleados[1] = empleado2;
            }

            public Tarea(string tarea, int horas, Cliente cliente, DateTime fecha, Empleado empleado1, Empleado empleado2, Empleado empleado3)
            {
                this.tarea = tarea;
                this.horas = horas;
                this.cliente = cliente;
                this.fecha = fecha;
                empleados[0] = empleado1;
                empleados[1] = empleado2;
                empleados[2] = empleado3;
            }

            public Tarea(string tarea, int horas, Cliente cliente, DateTime fecha, Empleado empleado1, Empleado empleado2, Empleado empleado3, Empleado empleado4)
            {
                this.tarea = tarea;
                this.horas = horas;
                this.cliente = cliente;
                this.fecha = fecha;
                empleados[0] = empleado1;
                empleados[1] = empleado2;
                empleados[2] = empleado3;
                empleados[3] = empleado4;
            }

            public Tarea(string tarea, int horas, Cliente cliente, DateTime fecha, Empleado empleado1, Empleado empleado2, Empleado empleado3, Empleado empleado4, Empleado empleado5)
            {
                this.tarea = tarea;
                this.horas = horas;
                this.cliente = cliente;
                this.fecha = fecha;
                empleados[0] = empleado1;
                empleados[1] = empleado2;
                empleados[2] = empleado3;
                empleados[3] = empleado4;
                empleados[4] = empleado5;
            }

            public Tarea(string tarea, int horas, Cliente cliente, DateTime fecha, Empleado empleado1, Empleado empleado2, Empleado empleado3, Empleado empleado4, Empleado empleado5, Empleado empleado6)
            {
                this.tarea = tarea;
                this.horas = horas;
                this.cliente = cliente;
                this.fecha = fecha;
                empleados[0] = empleado1;
                empleados[1] = empleado2;
                empleados[2] = empleado3;
                empleados[3] = empleado4;
                empleados[4] = empleado5;
                empleados[5] = empleado6;
            }

            public Tarea(string tarea, int horas, Cliente cliente, DateTime fecha, Empleado empleado1, Empleado empleado2, Empleado empleado3, Empleado empleado4, Empleado empleado5, Empleado empleado6, Empleado empleado7)
            {
                this.tarea = tarea;
                this.horas = horas;
                this.cliente = cliente;
                this.fecha = fecha;
                empleados[0] = empleado1;
                empleados[1] = empleado2;
                empleados[2] = empleado3;
                empleados[3] = empleado4;
                empleados[4] = empleado5;
                empleados[5] = empleado6;
                empleados[6] = empleado7;
            }

            /// <summary>
            /// Lee y cambia la tarea
            /// </summary>
            public string TArea
            {
                get
                {
                    return tarea;
                }

                set
                {
                    tarea = value;
                }
            }

            /// <summary>
            /// Lee y cambia el número de horas
            /// </summary>
            public int Horas
            {
                get
                {
                    return horas;
                }

                set
                {
                    horas = value;
                }
            }

            /// <summary>
            /// Lee y cambia el cliente de la tarea
            /// </summary>
            public Cliente Cliente
            {
                get
                {
                    return cliente;
                }

                set
                {
                    cliente = value;
                }
            }

            /// <summary>
            /// Lee y cambia la fecha de la tarea
            /// </summary>
            public DateTime Fecha
            {
                get
                {
                    return fecha;
                }

                set
                {
                    fecha = value;
                }
            }

            /// <summary>
            /// Devuelve el número de empleados en una tarea
            /// </summary>
            /// <returns>El número de empleados</returns>
            public int NumeroEmpleados()
            {
                return n;
            }

            /// <summary>
            /// Añade un empleado a la tarea en la última posición
            /// </summary>
            /// <param name="nuevo">Empleado a añadir</param>
            public void AñadirEmpleado(Empleado nuevo)
            {
                empleados[n] = nuevo;
                n++;
            }

            /// <summary>
            /// Elimina un empleado en concreto de la tarea
            /// </summary>
            /// <param name="indice">Indice del empleado a eliminar</param>
            public void EliminarEmpleado(int indice)
            {
                for (int i = n - 1; i > indice; i--)
                {
                    empleados[i - 1] = empleados[i];
                }
                n--;
            }

            /// <summary>
            /// Elimina todos los empleados de la tarea;
            /// </summary>
            public void EliminarTodo()
            {
                n = 0;
            }

            /// <summary>
            /// Lee un empleado en concreto (Recordar que el pimero elemento es el elemento '0')
            /// </summary>
            /// <param name="indice">Índice del empleado a leer</param>
            /// <returns>El empleado buscado</returns>
            public Empleado LeerEmpleado(int indice)
            {
                return empleados[indice];
            }

            /// <summary>
            /// Comprueba si el empleado buscado está en la tarea y si está devuelve su índice, si no, devuelve -1
            /// </summary>
            /// <param name="buscado">Empleado buscado</param>
            /// <returns>El indice del empleado buscado</returns>
            public int BuscarEmpleado(Empleado buscado)
            {
                int i = 0;
                while (i < n && empleados[i] != buscado)        //Hacemos una busqueda en el array
                {
                    i++;
                }
                if (i >= n) return -1; //Si el bucle ha llegado al final del array es que no lo ha encontrado, devolvemos -1
                else return i;          //En caso contrario lo ha encontrado y devolvemos su índice;
            }


        }

        class Historico
        {
            private DateTime inicio;
            private DateTime final;
            private string marca;
            private string proveedor;
            private double precioProveedor;
            private int dtoProveedor;
            private double precioProveedorNeto;

            //Varios constructores

            public Historico()
            {

            }

            public Historico(DateTime inicio, string marca, string proveedor, double precioProveedor)
            {
                this.inicio = inicio;
                this.marca = marca;
                this.proveedor = proveedor;
                this.precioProveedor = precioProveedor;
                precioProveedorNeto = precioProveedor;
            }

            public Historico(DateTime inicio, string marca, string proveedor, double precioProveedor, int dtoProveedor)
            {
                this.inicio = inicio;
                this.marca = marca;
                this.proveedor = proveedor;
                this.precioProveedor = precioProveedor;
                this.dtoProveedor = dtoProveedor;
                this.precioProveedorNeto = CalcularPrecioProveedorNeto();
            }

            public Historico(DateTime inicio, DateTime final, string marca, string proveedor, double precioProveedor)
            {
                this.inicio = inicio;
                this.final = final;
                this.marca = marca;
                this.proveedor = proveedor;
                this.precioProveedor = precioProveedor;
                precioProveedorNeto = precioProveedor;
            }

            public Historico(DateTime inicio, DateTime final, string marca, string proveedor, double precioProveedor, int dtoProveedor)
            {
                this.inicio = inicio;
                this.final = final;
                this.marca = marca;
                this.proveedor = proveedor;
                this.precioProveedor = precioProveedor;
                this.dtoProveedor = dtoProveedor;
                precioProveedorNeto = CalcularPrecioProveedorNeto();
            }


            //Métodos

            /// <summary>
            /// Lee y cambia la fecha de inicio del histórico
            /// </summary>
            public DateTime Inicio
            {
                get
                {
                    return inicio;
                }

                set
                {
                    inicio = value;
                }
            }

            /// <summary>
            /// Actualiza la fecha de incio del periodo a la fecha actual
            /// </summary>
            public void ActualizarInicio()
            {
                inicio = DateTime.Now;
            }

            /// <summary>
            /// Lee y cambia la fecha de final de periodo del histórico
            /// </summary>
            public DateTime Final
            {
                get
                {
                    return final;
                }

                set
                {
                    final = value;
                }
            }

            /// <summary>
            /// Actualiza la fecha de final de periodo del histórico a la fecha actual;
            /// </summary>
            public void ActualizaFinal()
            {
                final = DateTime.Now;
            }

            /// <summary>
            /// Lee la marca 
            /// </summary>
            public string Marca
            {
                get
                {
                    return marca;
                }
            }

            /// <summary>
            /// Lee el proveedor
            /// </summary>
            public string Proveedor
            {
                get
                {
                    return proveedor;
                }
            }

            /// <summary>
            /// Lee el precio
            /// </summary>
            public double PrecioProveedor
            {
                get
                {
                    return precioProveedor;
                }
            }

            public int DtoProveedor
            {
                get
                {
                    return dtoProveedor;
                }
            }

            public double PrecioProveedorNeto
            {
                get
                {
                    return precioProveedorNeto;
                }
            }

            //Funciones privadas


            /// <summary>
            /// Calcula el precio neto de proveedor de un articulo teniendo en cunato el precio de proveedor y el descuento de proveedor
            /// </summary>
            /// <returns>El precio neto del proveedor calculado</returns>
            private double CalcularPrecioProveedorNeto()
            {
                return precioProveedor * (1 - dtoProveedor / 100);
            }
        }
    }
}

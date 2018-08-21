using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    class Articulo
    {
        private string producto;
        private int referencia;
        private double precioProveedor;
        private int dtoProveedor;
        private double precioProveedorNeto;
        private double pvpAcysa;
        private double actualizacionDePrecio;
        private string material;
        private string aplicacion;

        public Articulo()
        {
            //Constructor con los valores nulos
        }


        //Constructores con diferentes campos de incialización
        public Articulo(int codigo,string producto, double precioProveedor, int dtoProveedor, double precioProveedorNeto,
            double pvpAcysa)
        {
            this.referencia = codigo;
            this.producto = producto;
            this.precioProveedor = precioProveedor;
            this.dtoProveedor = dtoProveedor;
            this.precioProveedorNeto = precioProveedorNeto;
            this.pvpAcysa = pvpAcysa;
        }

        public Articulo(int codigo, string producto, double precioProveedor, int dtoProveedor, double precioProveedorNeto,
            double pvpAcysa, double actualizacionDePrecio)
        {
            this.referencia = codigo;
            this.producto = producto;
            this.precioProveedor = precioProveedor;
            this.dtoProveedor = dtoProveedor;
            this.precioProveedorNeto = precioProveedorNeto;
            this.pvpAcysa = pvpAcysa;
            this.actualizacionDePrecio = actualizacionDePrecio;
        }

        public Articulo(int codigo, string producto, double precioProveedor, int dtoProveedor, double precioProveedorNeto,
            double pvpAcysa, double actualizacionDePrecio, string material, string aplicacion)
        {
            this.referencia = codigo;
            this.producto = producto;
            this.precioProveedor = precioProveedor;
            this.dtoProveedor = dtoProveedor;
            this.precioProveedorNeto = precioProveedorNeto;
            this.pvpAcysa = pvpAcysa;
            this.actualizacionDePrecio = actualizacionDePrecio;
            this.material = material;
            this.aplicacion = aplicacion;
        }

        /// <summary>
        /// Devuelve el código del articulo
        /// </summary>
        /// <returns>El código del producto</returns>
        public int GetCodigo ()  
        {
            return referencia;
        }

        /// <summary>
        /// Modifica el código del árticulo
        /// </summary>
        /// <param name="codigo">Nuevo código a implementar en el articulo</param>
        public void SetCodigo(int codigo)    
        {
            this.referencia = codigo;
        }

        /// <summary>
        /// Devuleve el nombre del articulo
        /// </summary>
        /// <returns>El nombre del articulo</returns>
        public string GetProducto()  
        {
            return producto;
        }

        /// <summary>
        /// Modifica el nombre del articulo
        /// </summary>
        /// <param name="producto">Nuevo nombre a implementar en el articulo</param>
        public void SetProducto(string producto)  
        {
            this.producto = producto;
        }

        /// <summary>
        /// Devuleve el precio del proveedor de un articulo
        /// </summary>
        /// <returns>El precio del proveedor</returns>
        public double GetPrecioProveedor()
        {
            return precioProveedor;
        }

        /// <summary>
        /// Modifica el precio de proveedor de un articulo
        /// </summary>
        /// <param name="precioProveedor">El nuevo precio de proveedor</param>
        public void SetPrecioProveedor(double precioProveedor)
        {
            this.precioProveedor = precioProveedor;
        }

        /// <summary>
        /// Devuelve el descuento del proveedor en un articulo
        /// </summary>
        /// <returns>El descuento dek proveedor</returns>
        public int GetDtoProveedor ()
        {
            return dtoProveedor;
        }

        /// <summary>
        /// Modifica el descuento de proveedor de un arituclo
        /// </summary>
        /// <param name="dtoProveedor">Nuevo descuento de proveedor</param>
        public void SetDtoProveedor( int dtoProveedor)
        {
            this.dtoProveedor = dtoProveedor;
            this.CalcularPrecioNetoProveedor();
        }

        /// <summary>
        /// Devuelve el precio de proveedor neto de un articulo
        /// </summary>
        /// <returns>El precio de proveedor neto</returns>
        public double GetPrecioProveedorNeto(Articulo articulo)
        {
            return precioProveedorNeto;
        }

        /// <summary>
        /// Modifica el precio de proveedor neto de un articulo
        /// </summary>
        /// <param name="precioProveedorNeto">Nuevo precio de proveedor neto</param>
        public void SetPrecioProveedorNeto(double precioProveedorNeto)
        {
            this.precioProveedorNeto = precioProveedorNeto;
        }

        /// <summary>
        /// Devuelve el PVPAcysa de un articulo
        /// </summary>
        /// <returns>El PVPAcysa</returns>
        public double GetPvpAcysa()
        {
            return pvpAcysa;
        }

        /// <summary>
        /// Modifica el PVPAcysa de un articulo
        /// </summary>
        /// <param name="pvpAcysa">Nuevo PVPAcysa</param>
        public void SetPvpAcysa (double pvpAcysa)
        {
            this.pvpAcysa = pvpAcysa;
        }

        /// <summary>
        /// Devuelve la actualización de precio de un articulo
        /// </summary>
        /// <returns>La actualización de precio</returns>
        public double GetActualizacionDePrecio()
        {
            return actualizacionDePrecio;
        }

        /// <summary>
        /// Modifica la actualización de precio de un arituclo
        /// </summary>
        /// <param name="actualizaciondePrecio">Nueva catualización de precio</param>
        public void SetActualizacionDePrecio (double actualizaciondePrecio)
        {
            this.actualizacionDePrecio = actualizaciondePrecio;
        }

        /// <summary>
        /// Devuelve el material de un articulo
        /// </summary>
        /// <returns>El materia </returns>
        public string GetMaterial ()
        {
            return material;
        }

        /// <summary>
        /// Modifica el material de un articulo
        /// </summary>
        /// <param name="material">Nuevo material</param>
        public void SetMaterial (string material)
        {
            this.material = material;
        }

        /// <summary>
        /// Devuelve la apliación de un articulo
        /// </summary>
        /// <returns>La aplicación del material</returns>
        public string GetAplicacion ()
        {
            return aplicacion;
        }

        /// <summary>
        /// Modifica la aplicación de un material
        /// </summary>
        /// <param name="aplicacion">La nueva aplicación</param>
        public void SetAplicacion (string aplicacion)
        {
              this.aplicacion = aplicacion; 
        }

        /// <summary>
        /// Calcula el precio neto de proveedor de un articulo teniendo en cunato el precio de proveedor y el descuento de proveedor
        /// </summary>
        /// <returns>El precio neto del proveedor</returns>
        public double CalcularPrecioNetoProveedor()
        {
            return precioProveedor * (dtoProveedor / 100);
        }
    }
}

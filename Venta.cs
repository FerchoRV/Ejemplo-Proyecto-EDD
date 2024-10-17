using System.Runtime.InteropServices.Marshalling;

public class ProductoVenta
{
    public Producto Producto;
    public int Cantidad;

    public ProductoVenta(Producto producto, int cantidad)
    {
        Producto = producto;
        Cantidad = cantidad;
    }
}

public class NodoVenta
{
    public ProductoVenta ProductoVenta;
    public NodoVenta? Siguiente;

    public NodoVenta(ProductoVenta productoVenta)
    {
        ProductoVenta = productoVenta;
        Siguiente = null;
    }
}

public class Venta
{
    public string DNI { get; set; }
    public string NombreCliente { get; set; }
    public NodoVenta? cabeza;

    public Venta(string dni, string nombreCliente)
    {
        DNI = dni;
        NombreCliente = nombreCliente;
        cabeza = null;
    }

    public void AgregarProducto(Producto producto, int cantidad)
    {
        ProductoVenta nuevoProductoVenta = new ProductoVenta(producto, cantidad);
        NodoVenta nuevoNodo = new NodoVenta(nuevoProductoVenta);

        if (cabeza == null)
        {
            cabeza = nuevoNodo;
        }
        else
        {
            NodoVenta? actual = cabeza;
            while (actual.Siguiente != null)
            {
                actual = actual.Siguiente;
            }
            actual.Siguiente = nuevoNodo;
        }
    }

    public void MostrarFactura()
    {
        NodoVenta? actual = cabeza;
        double totalVenta = 0;

        Console.WriteLine($"Factura - Cliente: {NombreCliente}, DNI: {DNI}");
        while (actual != null)
        {
            double subtotal = actual.ProductoVenta.Cantidad * actual.ProductoVenta.Producto.PrecioUnidad;
            Console.WriteLine($"Codigo: {actual.ProductoVenta.Producto.Codigo}, Producto: {actual.ProductoVenta.Producto.Nombre}, Cantidad: {actual.ProductoVenta.Cantidad}, Subtotal: {subtotal}");
            totalVenta += subtotal;
            actual = actual.Siguiente;
        }
        Console.WriteLine($"Total a pagar: {totalVenta}");
    }

    public Venta EditarVenta()
    {
        bool continuar = true;

        while (continuar)
        {
            Console.WriteLine("¿Qué deseas hacer?:");
            Console.WriteLine("1. Eliminar Producto");
            Console.WriteLine("2. Modificar cantidad");
            Console.WriteLine("3. Finalizar edición");

            Console.Write("Seleccione una opción: ");
            int opcion = int.Parse(Console.ReadLine() ?? "-1");
        
            switch (opcion)
            {
                case 1:
                    MostrarFactura();
                    Console.Write("Ingrese el código del producto a eliminar: ");
                    int codigoEliminar = int.Parse(Console.ReadLine()!);

                    EliminarProducto(codigoEliminar); // Se llama al método para eliminar un producto
                    Console.WriteLine("Producto eliminado.");
                    MostrarFactura();
                    break;

                case 2:
                    MostrarFactura();
                    Console.Write("Ingrese el código del producto a modificar: ");
                    int codigoModificar = int.Parse(Console.ReadLine()!);

                    Console.Write("Ingrese la nueva cantidad: ");
                    int nuevaCantidad = int.Parse(Console.ReadLine()!);

                    bool actualizado = ActualizarCantidad(codigoModificar, nuevaCantidad); // Actualiza la cantidad
                    if (actualizado)
                    {
                        Console.WriteLine("Cantidad actualizada exitosamente.");
                    }
                    else
                    {
                        Console.WriteLine("Producto no encontrado.");
                    }
                    MostrarFactura();
                    break;

                case 3:
                    continuar = false; // Sale del ciclo para finalizar la edición
                    break;

                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        }
        return this; // Devuelve el objeto actual de la venta
    }




    public void EliminarProducto(int codigo)
    {
        NodoVenta? actual = cabeza;
        NodoVenta? anterior = null;

        while (actual != null)
        {
            if (actual.ProductoVenta.Producto.Codigo == codigo)
            {
                if (anterior == null)
                {
                    cabeza = actual.Siguiente;
                }
                else
                {
                    anterior.Siguiente = actual.Siguiente;
                }
                break;
            }
            anterior = actual;
            actual = actual.Siguiente;
        }
    }

    public bool ActualizarCantidad(int codigo, int nuevaCantidad)
    {
        ProductoVenta? producto = BuscarProducto(codigo);
        if (producto != null)
        {
            producto.Cantidad = nuevaCantidad;
            return true;
        }
        return false;
    }

    public ProductoVenta? BuscarProducto(int codigo)
    {
        NodoVenta? actual = cabeza;
        while (actual != null)
        {
            if (actual.ProductoVenta.Producto.Codigo == codigo)
            {
                return actual.ProductoVenta;
            }
            actual = actual.Siguiente;
        }
        return null; // No se encontró el producto
    }


}

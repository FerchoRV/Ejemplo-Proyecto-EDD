public class NodoP
{
    public Producto Producto { get; set; }
    public NodoP? Siguiente { get; set; }

    public NodoP(Producto producto)
    {
        Producto = producto;
        Siguiente = null;
    }
}

public class Inventario
{
    private NodoP? cabeza;

    public Inventario()
    {
        cabeza = null;
    }
    public void InsertarProducto(Producto producto)
    {
        NodoP nuevoNodo = new NodoP(producto);
        if (cabeza == null)
        {
            cabeza = nuevoNodo;
        }
        else
        {
            NodoP actual = cabeza;
            while (actual.Siguiente != null)
            {
                actual = actual.Siguiente;
            }
            actual.Siguiente = nuevoNodo;
        }
    }

    public void MostrarProductos()
    {
        NodoP? actual = cabeza;
        while (actual != null)
        {
            Console.WriteLine(actual.Producto.ToString());
            actual = actual.Siguiente;
        }
    }

    public Producto? BuscarProducto(int codigo)
    {
        NodoP? actual = cabeza;
        while (actual != null)
        {
            if (actual.Producto.Codigo == codigo)
            {
                return actual.Producto;
            }
            actual = actual.Siguiente;
        }
        return null; // No se encontró el producto
    }

    public bool EliminarProducto(int codigo)
    {
        if (cabeza == null)
            return false;

        if (cabeza.Producto.Codigo == codigo)
        {
            cabeza = cabeza.Siguiente;
            return true;
        }

        NodoP? actual = cabeza;
        NodoP? anterior = null;

        while (actual != null && actual.Producto.Codigo != codigo)
        {
            anterior = actual;
            actual = actual.Siguiente;
        }

        if (actual == null)
            return false; // No se encontró el producto

        anterior!.Siguiente = actual.Siguiente;
        return true;
    }

    public bool ActualizarCantidad(int codigo, int nuevaCantidad)
    {
        Producto? producto = BuscarProducto(codigo);
        if (producto != null)
        {
            producto.CantidadEnStock = nuevaCantidad;
            return true;
        }
        return false;
    }

    public void ActualizarStock(int codigo, int cantidadVendida)
    {
        Producto? producto = BuscarProducto(codigo);
        if (producto != null)
        {
            producto.CantidadEnStock -= cantidadVendida;
        }
    }


}

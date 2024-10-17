public class Producto
{
    public int Codigo { get; set; }
    public string Nombre { get; set; }
    public string Categoria { get; set; }
    public int CantidadEnStock { get; set; }
    public double PrecioUnidad { get; set; }

    public Producto(int codigo, string nombre, string categoria, int cantidadEnStock, double precioUnidad)
    {
        Codigo = codigo;
        Nombre = nombre;
        Categoria = categoria;
        CantidadEnStock = cantidadEnStock;
        PrecioUnidad = precioUnidad;
    }

    public override string ToString()
    {
        return $"Código: {Codigo}, Nombre: {Nombre}, Categoría: {Categoria}, Cantidad: {CantidadEnStock}, Precio: {PrecioUnidad:C}";
    }
}

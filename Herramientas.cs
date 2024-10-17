public class Herramientas
{
    private Inventario inventario = new Inventario();

    public void MenuPrincipal()
    {
        int opcion = -1;

        while (opcion != 0)
        {
            Console.WriteLine("Menu Principal:");
            Console.WriteLine("1. Gestionar Inventario");
            Console.WriteLine("2. Facturación");
            Console.WriteLine("0. Salir");
            Console.Write("Seleccione una opción: ");
            opcion = int.Parse(Console.ReadLine() ?? "-1");

            switch (opcion)
            {
                case 1:
                    GestionarInventario();
                    break;
                case 2:
                    GestionarVenta();
                    break;
                case 0:
                    Console.WriteLine("Saliendo...");
                    break;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        }
    }

    public void GestionarInventario()
    {
        Console.WriteLine("Opciones de Inventario:");
        Console.WriteLine("1. Agregar Producto");
        Console.WriteLine("2. Mostrar Inventario");
        Console.WriteLine("3. Buscar Producto");
        Console.WriteLine("4. Eliminar Producto");
        Console.WriteLine("5. Actualizar Cantidad en Stock");
        Console.Write("Seleccione una opción: ");
        int opcion = int.Parse(Console.ReadLine() ?? "-1");

        switch (opcion)
        {
            case 1:
                AgregarProducto();
                break;
            case 2:
                inventario.MostrarProductos();
                break;

            case 3:
                BuscarProducto(inventario);
                break;

            case 4:
                EliminarProducto(inventario);
                break;
            case 5:
                ActualizarCantidad(inventario);
                break;
            default:
                Console.WriteLine("Opción no válida.");
                break;
        }
    }

    public void AgregarProducto()
    {
        Console.Write("Ingrese el código numérico del producto: ");
        int codigo = int.Parse(Console.ReadLine() ?? "0");
        Console.Write("Ingrese el nombre del producto: ");
        string nombre = Console.ReadLine() ?? "0";
        Console.Write("Ingrese la categoría: ");
        string categoria = Console.ReadLine() ?? "0";
        Console.Write("Ingrese la cantidad en stock: ");
        int cantidad = int.Parse(Console.ReadLine() ?? "0");
        Console.Write("Ingrese el precio por unidad: ");
        double precio = double.Parse(Console.ReadLine() ?? "0");

        Producto producto = new Producto(codigo, nombre, categoria, cantidad, precio);
        inventario.InsertarProducto(producto);
    }

    static void EliminarProducto(Inventario inventario)
    {
        Console.Write("Ingrese el código del producto a eliminar: ");
        int codigo = int.Parse(Console.ReadLine()!);

        bool eliminado = inventario.EliminarProducto(codigo);
        if (eliminado)
        {
            Console.WriteLine("Producto eliminado exitosamente.");
        }
        else
        {
            Console.WriteLine("Producto no encontrado.");
        }
    }

    static void ActualizarCantidad(Inventario inventario)
    {
        Console.Write("Ingrese el código del producto a actualizar: ");
        int codigo = int.Parse(Console.ReadLine()!);

        Console.Write("Ingrese la nueva cantidad en stock: ");
        int nuevaCantidad = int.Parse(Console.ReadLine()!);

        bool actualizado = inventario.ActualizarCantidad(codigo, nuevaCantidad);
        if (actualizado)
        {
            Console.WriteLine("Cantidad en stock actualizada exitosamente.");
        }
        else
        {
            Console.WriteLine("Producto no encontrado.");
        }
    }

    static void BuscarProducto(Inventario inventario)
    {
        Console.Write("Ingrese el código del producto a buscar: ");
        int codigo = int.Parse(Console.ReadLine()!);

        Producto? producto = inventario.BuscarProducto(codigo);
        if (producto != null)
        {
            Console.WriteLine("Producto encontrado:");
            Console.WriteLine(producto.ToString());
        }
        else
        {
            Console.WriteLine("Producto no encontrado.");
        }
    }


    public void GestionarVenta()
    {
        Console.Write("Ingrese DNI del Cliente: ");
        string dni = Console.ReadLine() ?? "0";
        Console.Write("Ingrese Nombre del Cliente: ");
        string nombreCliente = Console.ReadLine() ?? "0";

        Venta venta = new Venta(dni, nombreCliente); // Inicializa la venta para el cliente
        bool continuar = true;

        while (continuar)
        {
            inventario.MostrarProductos();
            Console.WriteLine("Ingrese el código numérico del producto (o 'fin' para terminar): ");
            string entrada = Console.ReadLine() ?? "-1";

            if (entrada.ToLower() == "fin")
                break;

            if (!int.TryParse(entrada, out int codigo))
            {
                Console.WriteLine("Código no válido. Debe ingresar un número o 'fin' para terminar.");
                continue;
            }

            Producto? producto = inventario.BuscarProducto(codigo); // Busca el producto en el inventario
            if (producto == null)
            {
                Console.WriteLine("Producto no encontrado.");
                continue;
            }

            Console.Write("Ingrese la cantidad a comprar: ");
            int cantidad = int.Parse(Console.ReadLine() ?? "0");

            venta.AgregarProducto(producto, cantidad); // Agrega el producto a la venta
        }

        venta.MostrarFactura(); // Muestra la factura con los productos agregados
        Console.WriteLine("Seleccione una opción: 0 = Cancelar, 1 = Aceptar, 2 = Editar");
        int opcion = int.Parse(Console.ReadLine() ?? "0");

        switch (opcion)
        {
            case 0:
                Console.WriteLine("Compra cancelada.");
                break;

            case 1:
                ActualizarInventario(venta); // Actualiza el inventario basado en la venta confirmada
                Console.WriteLine("Compra aceptada.");
                break;

            case 2:
                // Llama al método EditarVenta y recibe la venta editada
                venta = venta.EditarVenta(); 
            
                // Después de la edición, muestra la factura actualizada
                venta.MostrarFactura();

                // Luego, permite aceptar la compra o cancelar
                Console.WriteLine("Seleccione una opción: 0 = Cancelar, 1 = Aceptar");
                int opcionPostEdicion = int.Parse(Console.ReadLine() ?? "0");

                if (opcionPostEdicion == 1)
                {
                    ActualizarInventario(venta); // Actualiza el inventario después de editar
                    Console.WriteLine("Compra aceptada.");
                }
                else
                {
                    Console.WriteLine("Compra cancelada.");
                }
                break;

            default:
                Console.WriteLine("Opción no válida.");
                break;
        }
    }


    public void ActualizarInventario(Venta venta)
    {
        NodoVenta? actual = venta.cabeza;
        while (actual != null)
        {
            inventario.ActualizarStock(actual.ProductoVenta.Producto.Codigo, actual.ProductoVenta.Cantidad);
            actual = actual.Siguiente;
        }
    }
}

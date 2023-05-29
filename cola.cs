namespace cola_class;
public class colacircular<T>
{
    private T[] contenedor;  // contenedor de informacion, 
                             //T permite seleccionar el tipo de dato
    private int frente;        // contiene la posicion el primer elemento de la cola
    private int ultimo;         // contiene la posicion el ultimo elemento de la cola
    private int cantidad;    // cantidad de items actuales en la cola
    private int cap_maxima;     // capacidad maxima de la cola

    public colacircular(int maxSize)
    {
        cap_maxima = maxSize + 1;  // se le agrega un espacio extra, permite diferenciar entre llena y vacia
        contenedor = new T[cap_maxima];
        frente = 0;
        ultimo = 0;
        cantidad = 0;
    }

    public void encolar(T dato)//agrega un dato a la cola
    {
        if (llena())
        {
            // la cola esta llena y no se pueden agregar mas entradas
            throw new Exception("cola llena");
        }

        contenedor[ultimo] = dato;
        ultimo = (ultimo + 1) % cap_maxima;
        cantidad++;
    }

    public T descolar()//saca un elemento de la cola circular,se considera como si el elemento fue eliminado
    {
        if (vacia())
        {
            // la cola esta vacia, no se pueden sacar elementos
            throw new Exception("cola vacia");
        }

        T desencolarElementos = contenedor[frente];
        frente = (frente + 1) % cap_maxima;
        cantidad--;
        return desencolarElementos;
    }

    public bool vacia()//si la cola esta vacia regresa true
    {
        return (cantidad == 0);
    }

    public bool llena()//si la cola esta llena regresa true
    {
        return (cantidad == cap_maxima - 1);
    }

    public int largo()//regresa la cantidad de elementos actuales en la cola circular
    {
        return cantidad;
    }
}

namespace pilas;
public class pila<T>
{
    private T[] contenedor;//contenedor de informacion
                           //T permite seleccionar el tipo de dato deseado
    private int final;//contiene la posicion del ultimo elemento de la pila
    private int contador;//cantidad de items actuales en la pila
    private int cap_maxima;//capacidad maxima de la pila
    public pila(int size)
    {
        contenedor = new T[size];
        final = -1;
        contador = 0;
        cap_maxima = size;
    }

    public void Push(T item)
    {
        if (llena()) throw new Exception("pila llena");
        else
        {
            contador++;
            final++;
            contenedor[final] = item;
        }
    }

    public T Pop()
    {
        if (vacia())
        {
            throw new Exception("pila vacia");
        }
        else
        {
            contador--;
            final--;
            return contenedor[final];
        }


    }

    public T observar(int pos)
    {
        if (vacia())
        {
            throw new Exception("pila vacia");
        }
        else
        {
            if ((pos < 0) || (pos > final - 1))
            {
                throw new Exception("posicion fuera de rango");
            }
            return contenedor[pos];
        }


    }

    public bool vacia()
    {
        if (final == -1) return true;
        else return false;

    }
    public bool llena()
    {
        if (final == cap_maxima) return true;
        else return false;
    }

    public int largo()
    {
        return contador;
    }


}

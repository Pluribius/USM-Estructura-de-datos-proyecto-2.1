
using proceso_class;
using cola_class;
using boundingbox;
using menu_class;

class Program
{
    // Inicialization of classes
    static int alto = 40;
    static int ancho = 130;
    static colacircular<proceso> nuevos = new colacircular<proceso>(10);
    static colacircular<proceso> listos = new colacircular<proceso>(10);
    static colacircular<proceso> ejecutando = new colacircular<proceso>(1);
    static colacircular<proceso> terminados = new colacircular<proceso>(100);
    static colacircular<proceso> bloqueados = new colacircular<proceso>(10);
    static colacircular<proceso> suspendidos = new colacircular<proceso>(10);
    static box gui = new box(alto, ancho, "Universidad Santa Maria-Judelys Cadenas");

    static int[] procesos_activos = new int[10 * 4 + 1];
    static void Main()
    {


        gui.startup();
        ConsoleColor color_a = ConsoleColor.Magenta;
        ConsoleColor color_b = ConsoleColor.Black;
        gui.cambiar_color(color_a, color_b);
        Console.Clear();
        gui.cuadro('■');



        for (int i = 0; i < 10 * 4 + 1; i++)
        {
            procesos_activos[i] = -1;
        }

        gui.vertical_line(41, 0, alto);
        gui.horizontal_line(41, 21, ancho - 41);
        gui.horizontal_line(0, 15, 41);
        Console.SetCursorPosition(15, 16);
        Console.WriteLine("PID activos");

        Console.SetCursorPosition(60, 1);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("nuevos");
        Console.SetCursorPosition(60, 4);
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine("listos");
        Console.SetCursorPosition(60, 16);
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("terminados");
        Console.SetCursorPosition(60, 10);
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("bloqueados");
        Console.SetCursorPosition(60, 13);
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("suspendidos");
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.SetCursorPosition(60, 7);
        Console.WriteLine("ejecutando");
        Console.ForegroundColor = ConsoleColor.Magenta;

        actualizar_gui();
        menu();
    }

    static void actualizar_gui()
    {
        gui.clear_area(1, 17, 40, 15, ConsoleColor.Black);
        Console.SetCursorPosition(1, 17);
        bool actividad = false;
        for (int i = 0; i < procesos_activos.GetLength(0); i++)
        {
            if (procesos_activos[i] != -1)
            {
                actividad = true;
                if (i % 10 == 0) { Console.SetCursorPosition(1, Console.CursorTop + 1); }
                Console.Write(procesos_activos[i].ToString("D3") + "-");
            }
        }
        if (actividad == false)
        {
            Console.Write("vacio");
        }
        gui.horizontal_progress_bar(80, 10, nuevos.largo(), 45, 2, ConsoleColor.Red, ConsoleColor.DarkGray); // nuevos
        gui.horizontal_progress_bar(80, 10, listos.largo(), 45, 5, ConsoleColor.DarkCyan, ConsoleColor.DarkGray); // listos

        gui.horizontal_progress_bar(80, 10, terminados.largo(), 45, 17, ConsoleColor.DarkBlue, ConsoleColor.DarkGray); // terminados
        gui.horizontal_progress_bar(80, 10, bloqueados.largo(), 45, 11, ConsoleColor.DarkGreen, ConsoleColor.DarkGray); // bloqueados
        gui.horizontal_progress_bar(80, 10, suspendidos.largo(), 45, 14, ConsoleColor.DarkYellow, ConsoleColor.DarkGray); // suspendidos
        gui.horizontal_progress_bar(80, 10, ejecutando.largo(), 45, 8, ConsoleColor.DarkMagenta, ConsoleColor.DarkGray); // ejecutando
    }
    static int contar_procesos_activos()
    {
        int contador = 0;
        for (int i = 0; i < 10 * 4 + 1; i++)
        {

            if (procesos_activos[i] != -1)
            {
                contador++;
            }
        }
        return contador;
    }
    static void crear_proceso()
    {
        if (contar_procesos_activos() == 10 * 4 + 1)
        {
            gui.center_insidebox("no se puede ingresar mas procesos", 41, ancho, 22, alto);
        }
        else
        {
            int repeat;
            do
            {
                gui.center_insidebox("cantidad de procesos a ingresar?", 41, ancho, 22, alto);
                repeat = Convert.ToInt32(Console.ReadLine());
                if (repeat < 1 || 10 < nuevos.largo() + repeat)
                {
                    gui.center_insidebox("cantidad invalida", 41, ancho, 22, alto);
                }
                else
                {
                    break;
                }
            } while (true);
            for (int j = 0; j < repeat; j++)
            {


                gui.center_insidebox("Ingrese un numero entre 000 y 999", 41, ancho, 22, alto);
                try
                {
                    int pid_temporal = Convert.ToInt32(Console.ReadLine());
                    for (int i = 0; i < 10 * 4 + 1; i++)
                    {
                        if (pid_temporal == procesos_activos[i])
                            throw new Exception("pid repetiddo");

                    }
                    if (pid_temporal < 0 || pid_temporal > 999)
                    { throw new Exception("pid fuera de rango"); }
                    gui.center_insidebox("ingrese una expresion aritmetica", 41, ancho, 22, alto);
                    string expresion_temporal = Console.ReadLine();
                    proceso proceso_obj = new proceso(pid_temporal, expresion_temporal);
                    nuevos.encolar(proceso_obj);
                    procesos_activos[contar_procesos_activos()] = pid_temporal;
                    int coordy_temp = Console.CursorTop;
                    actualizar_gui();
                    Console.SetCursorPosition(41, 23);
                    gui.clear_area(42, 22, ancho - 43, alto - 23, ConsoleColor.Black);

                }

                catch (Exception e)
                {
                    gui.cambiar_color(ConsoleColor.Red, ConsoleColor.Black);
                    gui.center_insidebox("numero invalido: " + e.Message, 41, ancho, 22, alto - 1);
                    gui.cambiar_color(ConsoleColor.Magenta, ConsoleColor.Black);


                }
            }

        }

    }
    static void mover_proceso(colacircular<proceso> a, colacircular<proceso> b)
    {
        try
        {
            proceso temp = a.descolar();
            b.encolar(temp);
            gui.center_insidebox("objeto movido : ", 41, ancho, 22, alto - 1);
            System.Threading.Thread.Sleep(500);

            actualizar_gui();
        }
        catch (System.Exception e)
        {
            gui.cambiar_color(ConsoleColor.Red, ConsoleColor.Black);
            gui.center_insidebox("Error: " + e.Message, 41, ancho, 22, alto - 1);
            gui.cambiar_color(ConsoleColor.Magenta, ConsoleColor.Black);

        }


    }
    static void menu()
    {
        bool salir = false;
        int position = 3;
        menu opciones = new menu();
        do
        {


            Console.SetCursorPosition(8, 2);


            for (int i = 0; i < opciones.menu_principal.GetLength(0); i++)
            {
                if (position == i + 2) { Console.ForegroundColor = ConsoleColor.Red; }
                Console.SetCursorPosition(8, Console.CursorTop);
                Console.WriteLine(opciones.menu_principal[i]);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            //selec=opciones.selec_opcion(2,opciones.menu_principal.GetLength(0)+2,opciones.menu_principal);


            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(8, 6);
            for (int i = 0; i < opciones.menu_mover.GetLength(0); i++)
            {
                if (position == i + 2 + opciones.menu_principal.GetLength(0)) { Console.ForegroundColor = ConsoleColor.Red; }
                Console.SetCursorPosition(8, Console.CursorTop);
                Console.WriteLine(opciones.menu_mover[i]);
                Console.ForegroundColor = ConsoleColor.Gray;

            }
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.SetCursorPosition(2, position);
            ConsoleKey var = Console.ReadKey(true).Key;
            switch (var)
            {
                case ConsoleKey.Escape:
                    salir = true;
                    Console.Clear();
                    break;
                case ConsoleKey.UpArrow:
                    if (Console.CursorTop - 1 > 2)
                    {
                        position = Console.CursorTop - 1;
                        Console.SetCursorPosition(1, Console.CursorTop - 1);
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (Console.CursorTop + 1 < 11)
                    {
                        position = Console.CursorTop + 1;
                        Console.SetCursorPosition(1, Console.CursorTop + 1);
                    }
                    break;
                case ConsoleKey.Enter:
                    Console.SetCursorPosition(41, 22);
                    switch (position)
                    {

                        case (3):
                            gui.clear_area(42, 22, ancho - 43, alto - 23, ConsoleColor.Black);
                            crear_proceso();
                            Console.SetCursorPosition(1, 3);
                            break;
                        /*case (4):
                            gui.clear_area(42, 22, ancho - 43, alto - 23, ConsoleColor.Black);
                            gui.center_insidebox("eliminar proceso", 41, ancho, 22, alto - 1);


                            break;*/
                        case (6):
                            gui.clear_area(42, 22, ancho - 43, alto - 23, ConsoleColor.Black);
                            gui.center_insidebox("nuevos-------->listos", 41, ancho, 22, alto - 1);
                            mover_proceso(nuevos, listos);
                            break;
                        case (7):
                            gui.clear_area(42, 22, ancho - 43, alto - 23, ConsoleColor.Black);
                            gui.center_insidebox("listos-------->ejecutando", 41, ancho, 22, alto - 1);
                            mover_proceso(listos, ejecutando);
                            break;
                        case (8):
                            gui.clear_area(42, 22, ancho - 43, alto - 23, ConsoleColor.Black);
                            gui.clear_area(42, 22, ancho - 43, alto - 23, ConsoleColor.Black);
                            gui.center_insidebox("ejecutando---->terminados", 41, ancho, 22, alto - 1);
                            proceso temporal = ejecutando.descolar();
                            ejecutando.encolar(temporal);
                            for (int i = 0; i < 10 * 4 + 1; i++)
                            {
                                if (procesos_activos[i] == temporal.PID)
                                {
                                    procesos_activos[i] = -1;
                                }
                            }

                            mover_proceso(ejecutando, terminados);
                            break;
                        case (9):
                            gui.clear_area(42, 22, ancho - 43, alto - 23, ConsoleColor.Black);
                            gui.center_insidebox("ejecutando---->bloqueado", 41, ancho, 22, alto - 1);
                            mover_proceso(ejecutando, bloqueados);
                            if (bloqueados.largo() == 9)
                            {
                                for (int i = 0; i < 9; i++)
                                {
                                    mover_proceso(bloqueados, suspendidos);
                                }
                            }
                            break;
                        case (10):
                            gui.clear_area(42, 22, ancho - 43, alto - 23, ConsoleColor.Black);
                            gui.center_insidebox("suspendidos--->listo", 41, ancho, 22, alto - 1);
                            mover_proceso(suspendidos, listos);
                            break;
                    }

                    break;
            }

        } while (salir == false);
    }
}
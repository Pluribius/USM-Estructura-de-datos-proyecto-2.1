using System.Dynamic;
using System.Threading.Tasks.Dataflow;
using pilas;
namespace boundingbox;
public class box : pila<int>
{
    //86width //25heigth
    #region atributos
    public int height { get; set; }
    public int width { get; set; }
    private ConsoleColor Colorfore;
    private ConsoleColor Colorback;
    private string title;
    private int Counter = 1;
    public pila<int>? guimemmo;
    int LastCursorx;
    int LastCursory;
    #endregion

    #region constructor
    public box(int Alto, int Ancho,string titulo) : base(23)
    {
        if (Alto <= 1 || Alto > Console.LargestWindowHeight)
        {
            //default vom viewurfel setzen 
            height = 25;
        }
        else { height = Alto; }
        if (Ancho <= 1 || Ancho > Console.LargestWindowWidth)
        {
            //default vom viewurfel setzen 
            width = 86;
        }
        else { width = Ancho; }
        title=titulo;
    }

    #endregion
    #region metodos
    public void cambiar_color(ConsoleColor Fore, ConsoleColor back)
    {
        Colorfore = Fore;
        Colorback = back;
    }
    public void startup()
    {
        Console.ResetColor();
        Console.Clear();
        
        Console.SetWindowSize(width, height);
        Console.SetBufferSize(width * 2, height * 2);
        Console.Title = title;
    }



    public void cuadro(char character)
    {
        Console.Clear();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if ((y == 0) || (y == height - 1) || (x == 0) || (x == width - 1))
                {
                    Console.ForegroundColor = Colorfore;
                    Console.BackgroundColor = Colorfore;
                    Console.Write(character);
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write('■');
                    Console.ResetColor();
                }
            }
            Console.WriteLine("");
        }
        Console.SetCursorPosition(1, 1);
    }
    public void horizontal_line(int x1, int y1, int length)
    {

        LastCursorx=Console.CursorLeft;
        LastCursory=Console.CursorTop;
        Console.ForegroundColor=Colorfore;
        if (x1 + length > width)
        {
            x1 = width - length;
        }
        for (int i = 0; i < length; i++)
        {
            Console.BackgroundColor = Colorfore;
            Console.SetCursorPosition(x1 + i, y1);
            Console.Write("■");

        }
        Console.BackgroundColor=Colorback;
        Console.SetCursorPosition(LastCursorx,LastCursory);
    }

    public void vertical_line(int x1, int y1, int length)
    {
        LastCursorx=Console.CursorLeft;
        LastCursory=Console.CursorTop;
        Console.ForegroundColor=Colorfore;
        if (y1 + length > height)
        {
            y1 = height - length;
        }
        for (int i = 0; i < length; i++)
        {
            
            Console.BackgroundColor = Colorfore;
            Console.SetCursorPosition(x1, y1 + i);
            Console.Write("■");

        }
        Console.BackgroundColor=Colorback;
        Console.SetCursorPosition(LastCursorx,LastCursory);
    }
    public void line(char character, int x1, int y1, int x2, int y2)
    {   /*
        //m=y2-y1
        //  -------   y=mx+b---> y-mx=b
        //   x2-x1
        int b = 0, y = 0, m = 0;
        float upper, downer;
        double temp_m, temp_m2;
        upper = y2 - y1;
        downer = x2 - x1;
        temp_m = (upper / downer);
        temp_m2 = temp_m;
        Console.WriteLine("");
        Console.WriteLine("temp_m=" + temp_m);
        Console.WriteLine("temp_m2=" + temp_m2);
        /*
        if (temp_m == 0)
        {
            Console.WriteLine("temp_m es negativo");
            b = y1;
            Console.WriteLine("b=" + b + "y1" + y1);
        }
        else
        {
        if (temp_m < 0)
        {
            Console.WriteLine("temp_m es menor que 0");
            Console.WriteLine("temp_m=temp_m*-1" + temp_m + "=" + temp_m * -1);
            temp_m = temp_m * -1;
            //negativo=true;
        }
        do
        {
            Console.WriteLine("temp_m=temp_m/10: " + temp_m / 10 + "=" + temp_m + "/" + "10");
            temp_m = temp_m / 10;
        } while (temp_m >= 10);

        if (temp_m % 1 >= 0.5)
        {
            Console.WriteLine("temp_m%10(" + temp_m % 10 + ")>=0.5");
            m = ((int)temp_m2) + 1;
            Console.WriteLine("m=" + m);
        }
        else
        {
            Console.WriteLine("temp_m%10(" + temp_m % 10 + ")=<0.5");
            m = ((int)temp_m2);
        }


        b = y1 - (m * x1);
        Console.WriteLine("b = y1 - (m * x1)");
        Console.WriteLine(b + "=" + y1 + "-(+" + m + "*" + x1 + ")");
        //}
        for (int x_axis = 0; x_axis < width; x_axis++)
        {
            y = (m * x_axis) + b;
            if (y <= height)
            {
                Console.SetCursorPosition(x_axis, y);
                Console.ForegroundColor = Colorfore;
                Console.BackgroundColor = Colorback;
                Console.Write(character);
            }
        }
        Console.ResetColor();
        Console.ReadKey();*/
    }
    public void center_insidebox(string text,int minx,int maxx,int miny,int maxy)
    {
        
            int rectangleWidth = maxx - minx;
        int rectangleHeight = maxy - miny;

        // Calculate the width and height of the text
        int textWidth = text.Length;
       
        // Calculate the X and Y offsets to center the text
        int offsetX = (rectangleWidth - textWidth) / 2;
        offsetX=offsetX+minx;
        if(Console.CursorTop+1>=maxy)
        {
            clear_area(minx+1,miny,maxx-minx-2,maxy-miny,ConsoleColor.Black);
            Console.SetCursorPosition(Console.CursorLeft,miny);
        }
        Console.SetCursorPosition(offsetX,Console.CursorTop);
             Console.BackgroundColor=Colorback;
             Console.ForegroundColor=Colorfore;
             Console.WriteLine(text);
        
       Console.SetCursorPosition(rectangleWidth,Console.CursorTop);
    }
    public void margin_check(int x,int y)
    {
       
        if(height-3<y)
        {
            Console.SetCursorPosition(x,1);
        }
        else
        {
            Console.SetCursorPosition(x,y);
        }
    }
    public void point(char character, int x2, int y2)
    {
        if (x2 >= 0 && y2 >= 1 && x2 < width && y2 < height + 1)
        {
            LastCursorx=Console.CursorLeft;
            LastCursory=Console.CursorTop;
            Console.SetCursorPosition(x2, y2);
            Console.ForegroundColor = Colorfore;
            Console.BackgroundColor = Colorback;
            Console.Write(character);
            Console.ResetColor();
            Console.SetCursorPosition(LastCursorx,LastCursory);
        }
    }
    public void clear()
    {
        Console.Clear();
        Counter = 1;
        cuadro('■');
    }
    public void sync(int val)
    { Counter = val; }
    public void texthandler_offset(string text,int margin_left)
    {
        Console.BackgroundColor=Colorback;
        Console.ForegroundColor=Colorfore;
        Console.SetCursorPosition(margin_left,Console.CursorTop);
        Console.WriteLine(text);
        margin_check(margin_left,Console.CursorTop);

    }
    public void textHandler_loopback(string text, int margin_left)
    {
        Console.BackgroundColor=Colorback;
        Console.ForegroundColor=Colorfore;
        
            Counter++;
            int center = 1;
            foreach (var i in text)
            {
                center++;
            }
            center = (width / 2 - (center / 2)) + margin_left;
            Console.SetCursorPosition(center, Console.CursorTop);
            Console.WriteLine(text);
           

        
        margin_check(margin_left,Console.CursorTop);
        
        
    }

    public void textHandler_stay(string text)
    {
        Console.BackgroundColor=Colorback;
        Console.ForegroundColor=Colorfore;
        
            Counter++;
            int center = 1;
            foreach (var i in text)
            {
                center++;
            }
            center = (width / 2 - (center / 2));
            Console.SetCursorPosition(center, Console.CursorTop);
            Console.WriteLine(text);
            margin_check(width / 2,Console.CursorTop);

        
        
    }


    public void horizontal_progress_bar(int length,int max, int progress, int pos_x, int pos_y, ConsoleColor Active, ConsoleColor Inactive)
    {
        LastCursorx=Console.CursorLeft;
            LastCursory=Console.CursorTop;


           /*
           35,sizeLavado, 1

           100% /35 px=2.8%/px

           saco el porcentaje actual
           y pinto tantos pixeles sean equivalentes al porcentaje

           max-----35
           progress


           progress*lenght/max
           
           */
        double temp=progress*length;
        temp=temp/max;

        clear_area(pos_x,pos_y,length,1,Colorback);
       
        
        for (int i = 0; i < length; i++)
        {
            Console.BackgroundColor=ConsoleColor.White;
            Console.SetCursorPosition(pos_x + i, pos_y);
            if (i <= temp)
            {   
                
                Console.ForegroundColor = Active;
                Console.Write("■");

            }
            else
            {   
                
                Console.ForegroundColor = Inactive;
                Console.Write("■");

            }
        }
        Console.ForegroundColor=Colorfore;
        Console.BackgroundColor = Colorback;
        Console.SetCursorPosition(LastCursorx,LastCursory);
    }

    public void vertical_progress_bar(int length,int max, int progress, int pos_x, int pos_y, ConsoleColor Active, ConsoleColor Inactive)
    {
        LastCursorx=Console.CursorLeft;
            LastCursory=Console.CursorTop;
            double temp=progress*length;
        temp=temp/max;
        clear_area(pos_x,pos_y,1,length,Colorback);
        Console.BackgroundColor = Colorback;

        for (int i = 0; i < length; i++)
        {
            Console.SetCursorPosition(pos_x, pos_y + i);
            if (i <= temp)
            {
                Console.ForegroundColor = Active;
                Console.Write("■");

            }
            else
            {
                Console.ForegroundColor = Inactive;
                Console.Write("■");

            }
        }
        Console.ForegroundColor=Colorfore;
        Console.BackgroundColor = Colorback;
        Console.SetCursorPosition(LastCursorx,LastCursory);
    }
    public void clear_area(int initial_x, int initial_y, int length_x, int length_y, ConsoleColor background)
    {
        LastCursorx=Console.CursorLeft;
        LastCursory=Console.CursorTop;

        Console.BackgroundColor=background;
        Console.ForegroundColor=background;
        
        for (int y = 0; y < length_y; y++)
        {
            for (int x = 0; x < length_x; x++)
            {
                Console.SetCursorPosition(initial_x+x,initial_y+y);
                Console.Write("■");
            }
            Console.WriteLine("");
        }
        Console.ForegroundColor = Colorfore;
        Console.BackgroundColor = Colorback;
        Console.SetCursorPosition(LastCursorx,LastCursory);
        
    }
    #endregion
}

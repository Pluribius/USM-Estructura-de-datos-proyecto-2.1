
namespace menu_class
{
    public class menu
    {
        public string[] menu_principal = new string[4];
        public string[] menu_mover = new string[5];
        public menu()
        {
            menu_principal[0] = "---menu de opciones (1)---";
            menu_principal[1] = "crear proceso";
            //menu_principal[2] = "2-eliminar proceso";
            
            //==============================================
            
            menu_mover[0] = "nuevos-------->listos";
            menu_mover[1] = "listos-------->ejecutando";
            menu_mover[2] = "ejecutando---->terminados";
            menu_mover[3] = "ejecutando---->bloqueado";
            menu_mover[4] = "suspendidos--->listo";

        }
    
    

        
    }
}
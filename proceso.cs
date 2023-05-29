namespace proceso_class
{
    class proceso
    {
        public int PID;
        public string expresion;
        public proceso(int Pid,string Expresion)
        {
            PID=Pid;
            expresion=Expresion;
        }
        public int GetPID()
        {
            return PID;
        }
        public string GetExpresion()
        {
            return expresion;
        }
    }
}
using System;

namespace IngameScript
{
    partial class Program
    {
        public bool logError(string errMsg)
        {
            try
            {
                errMsg = '\n' + errMsg;
                errLog += errMsg;
            }
            catch (Exception err)
            {
                errLog += '\n' + err.Message;
                Echo(err.Message);
                return false;
            }
            return true;
        }
    }
}

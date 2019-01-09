using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyHook;

using System.IO.MemoryMappedFiles;
namespace TryToOnTopAndImageCatch
{
     class MemoryDXManager
    {
        public class StrideInterface : MarshalByRefObject
        {
            public delegate void Messages(string txt);
            public event Messages onMessage;

            public void Text(string txt)
            {
                if (onMessage != null)
                    onMessage(txt);
            }
            public void Ping()
            {

            }
        }
        public static StrideInterface _interface = new StrideInterface();
        static string channelName;
        void _interface_onMessage(string txt)
        {
            
        }
        MemoryMappedFile sharedMemory;
        const int LengthMessage = 20;
         public void InitializeDX(Process p)
        {
            _interface.onMessage += _interface_onMessage;
            RemoteHooking.IpcCreateServer<StrideInterface>(ref channelName, System.Runtime.Remoting.WellKnownObjectMode.SingleCall, _interface);
            RemoteHooking.Inject(p.Id, "InjectionDLL.dll", "InjectionDLL.dll", channelName);

            //Channel
            char[] message = Console.ReadLine().ToCharArray();
            //Размер введенного сообщения

            sharedMemory = MemoryMappedFile.CreateOrOpen("MemoryDXManager", LengthMessage * 2 + 4);
            //Создаем объект для записи в разделяемый участок памяти
          
            // _interface.
        }
        public void sendMessage(string txt)
        {
            using (MemoryMappedViewAccessor writer = sharedMemory.CreateViewAccessor(0, LengthMessage * 2 + 4))
            {
                //запись в разделяемую память
                //запись размера с нулевого байта в разделяемой памяти
                writer.Write(0, LengthMessage);
                //запись сообщения с четвертого байта в разделяемой памяти
                writer.WriteArray<char>(4, txt.ToCharArray(0,txt.Length), 0, txt.Length);
            }
        }
    }
}

using SharpAdbClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WpfApplication2
{
    class ADBClass
    {
        public StartServerResult getServer()
        {
            AdbServer serv = new AdbServer();
            return serv.StartServer(@"adb.exe", restartServerIfNewer: false);
        }

        public bool downloadFile()
        {
            bool loaded = false;

            try
            {

                var device = AdbClient.Instance.GetDevices().First();

                using (SyncService service = new SyncService(new AdbSocket(AdbServer.Instance.EndPoint), device))
                using (Stream stream = File.Open("sqlite.db.m",FileMode.Create, FileAccess.Write, FileShare.Read))
                {
                    service.Pull("/data/data/aorjoa.com.managetruckabdroid/databases/sqlite.db", stream, null, System.Threading.CancellationToken.None);
                }
                loaded = true;
            }
            catch (Exception)
            {

            }
            return loaded;
        }

        public bool uploadFile()
        {
            bool loaded = false;

            try
            {
                var device = AdbClient.Instance.GetDevices().First();

                using (SyncService service = new SyncService(new AdbSocket(AdbServer.Instance.EndPoint), device))
                using (var fs = new FileStream(@"db.sqlite", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var stream = new StreamReader(fs, Encoding.Default))
                {
                    service.Push(stream.BaseStream, "/data/data/aorjoa.com.managetruckabdroid/databases/sqlite.db", 444, DateTime.Now, null, CancellationToken.None);
                }
                
                loaded = true;
            }
            catch (Exception)
            {

            }
            return loaded;
        }
    }
}

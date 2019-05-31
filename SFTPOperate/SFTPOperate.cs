using Renci.SshNet;
using System;
using System.IO;


namespace SFTPOperate
{
    public class SFTPOperate
    {
        private SftpClient sftp;
        public bool Connected { get { return sftp.IsConnected; } }

        //构造
        public SFTPOperate(string host, string user, string pwd)
        {
            string[] arr = host.Split(':');
            string ip = arr[0];
            int port = arr.Length > 1 ? Int32.Parse(arr[1]) : 22;
            sftp = new SftpClient(ip, port, user, pwd);
        }

        //连接
        public bool Connect()
        {
            try
            {
                if (!Connected)
                {
                    sftp.Connect();
                }
                return true;
            }
            catch (Exception e)

            {
                throw new Exception("连接SFTP失败，原因：" + e.Message);
            }
        }

        //断开连接
        public void Disconnect()
        {
            try
            {
                if (sftp != null && Connected)
                {
                    sftp.Disconnect();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("断开SFTP失败，原因：{0}", ex.Message));
            }
        }

        //上传文件
        public void Put(string localPath, string remotePath)
        {
            try
            {
                using (var file = File.OpenRead(localPath))
                {
                    Connect();
                    sftp.UploadFile(file, remotePath);
                    Disconnect();
                }
            }
            catch (Exception e)
            {
                throw new Exception("SFTP文件上传失败，原因：" + e.Message);
            }
        }
    }
}

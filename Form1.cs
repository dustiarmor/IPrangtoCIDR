using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IP_range_to_CIDR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> myCIDR = new List<string>();

            foreach (string line in richTextBox1.Lines)
            {
                string[] startIPandEndIP= line.Trim().Split(new string[] { "," }, StringSplitOptions.None);
                //                MessageBox.Show(startIPandEndIP[0]);
                foreach (string item in iprange2cidr(startIPandEndIP[0], startIPandEndIP[1]))
                {
                    myCIDR.Add(item);
                }


            }


            richTextBox2.Lines = myCIDR.ToArray();
            
            

            
        }
        public static List<string> iprange2cidr(string ipStart, string ipEnd)
        {
            long start = ip2long(ipStart);
            long end = ip2long(ipEnd);
            var result = new List<string>();

            while (end >= start)
            {
                byte maxSize = 32;
                while (maxSize > 0)
                {
                    long mask = iMask(maxSize - 1);
                    long maskBase = start & mask;

                    if (maskBase != start)
                    {
                        break;
                    }

                    maxSize--;
                }
                double x = Math.Log(end - start + 1) / Math.Log(2);
                byte maxDiff = (byte)(32 - Math.Floor(x));
                if (maxSize < maxDiff)
                {
                    maxSize = maxDiff;
                }
                string ip = long2ip(start);
                result.Add(ip + "/" + maxSize);
                start += (long)Math.Pow(2, (32 - maxSize));
            }
            return result;
        }

        public static List<string> iprange2cidr(int ipStart, int ipEnd)
        {
            long start = ipStart;
            long end = ipEnd;
            var result = new List<string>();

            while (end >= start)
            {
                byte maxSize = 32;
                while (maxSize > 0)
                {
                    long mask = iMask(maxSize - 1);
                    long maskBase = start & mask;

                    if (maskBase != start)
                    {
                        break;
                    }

                    maxSize--;
                }
                double x = Math.Log(end - start + 1) / Math.Log(2);
                byte maxDiff = (byte)(32 - Math.Floor(x));
                if (maxSize < maxDiff)
                {
                    maxSize = maxDiff;
                }
                string ip = long2ip(start);
                result.Add(ip + "/" + maxSize);
                start += (long)Math.Pow(2, (32 - maxSize));
            }
            return result;
        }

        private static long iMask(int s)
        {
            return (long)(Math.Pow(2, 32) - Math.Pow(2, (32 - s)));
        }

        private static string long2ip(long ipAddress)
        {
            System.Net.IPAddress ip;
            if (System.Net.IPAddress.TryParse(ipAddress.ToString(), out ip))
            {
                return ip.ToString();
            }
            return "";
        }

        private static long ip2long(string ipAddress)
        {
            System.Net.IPAddress ip;
            if (System.Net.IPAddress.TryParse(ipAddress, out ip))
            {
                return (((long)ip.GetAddressBytes()[0] << 24) | ((long)ip.GetAddressBytes()[1] << 16) | ((long)ip.GetAddressBytes()[2] << 8) | ip.GetAddressBytes()[3]);
            }
            return -1;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void encrypt_Click(object sender, RoutedEventArgs e)
        {
            Keys key = new Keys();
            int p = key.GenerateP();
            int g = key.GenerateG(p);
            int x = key.GenerateX(p);
            int y = key.FastExponentiation(p, g, x);

            cipher_a.Text = "";
            cipher_b.Text = "";

            string src = source.Text;

            ELGamal elGamal = new ELGamal();
            int[] res = new int[2];
            for (int i = 0; i < src.Length; i++)
            {
                int symbol = Convert.ToInt32(source.Text[i]);
                res = elGamal.Encrypt(p, g, y, symbol);
                cipher_a.Text += Convert.ToString(res[0]);
                cipher_b.Text += Convert.ToString(res[1]);
                cipher_a.Text += " ";
                cipher_b.Text += " ";
            }

            keyP.Text = Convert.ToString(p);
            keyG.Text = Convert.ToString(g);
            keyY.Text = Convert.ToString(y);
            keyX.Text = Convert.ToString(x);
        }

        private void decrypt_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            string text_a = textA.Text;
            text_a.Trim();
            string text_b = textB.Text;
            text_b.Trim();

            for (int i = 0; i < text_a.Length; i++)
            {
                if (text_a[i] == 32)
                    count++;
            }
            count++;

            int[] a = CheckInput(text_a, count);
            int[] b = CheckInput(text_b, count);

            int p = Convert.ToInt32(keyP.Text);
            int x = Convert.ToInt32(keyX.Text);
            ELGamal elGamal = new ELGamal();
            string result = "";

            for (int i = 0; i < count; i++)
            {
                result += elGamal.Decrypt(p, x, a[i], b[i]);
            }

            cipher.Text = result;
        }

        public int[] CheckInput(string text, int count)
        {
            int[] src = new int[count];
            int k = 0, j = 0;
            string buf;
            for (int i = 0; i < text.Length; i++)
            {
                if ((text[i] == 32))
                {
                    buf = text.Substring(j, i - j);
                    j = i + 1;
                    src[k] = int.Parse(buf);
                    k++;
                }
                else if (i == text.Length - 1)
                {
                    buf = text.Substring(j, i - j + 1);
                    j = i + 1;
                    src[k] = int.Parse(buf);
                    k++;
                }
            }

            return src;
        }
    }
}

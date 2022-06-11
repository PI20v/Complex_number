using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComplexForm
{
    public partial class MainForm : Form
    {
        Complex A;
        Complex B;
        Complex result;
        Autorization autorization;
        string name;

        public MainForm(Autorization autorization, string name)
        {
            InitializeComponent();

            this.autorization = autorization;
            this.name = name;

            using (StreamWriter writer = new StreamWriter(name + ".txt", File.Exists(name + ".txt")))
            {
                writer.WriteLine("Пользователь " + name + " авторизовался [" + DateTime.Now.ToString().Split(' ')[0] + "]");
            }

            if (name == "Admin") label10.Text = "Администратор";
            else label10.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox5.Clear();
            textBox6.Clear();

            if (!ComplexRead()) return;

            result = A + B;

            using (StreamWriter writer = new StreamWriter(name + ".txt", File.Exists(name + ".txt")))
            {
                writer.WriteLine("Введены значения A=" + A.Real + "+i" + A.Imaginary + ", " +
                    "B=" + B.Real + "+i" + B.Imaginary + "; Действие: сумма; Результат=" + result.Real + "+i" +result.Imaginary);
            }

            textBox5.Text = result.Real.ToString();
            textBox6.Text = result.Imaginary.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox5.Clear();
            textBox6.Clear();

            if (!ComplexRead()) return;

            result = A - B;

            using (StreamWriter writer = new StreamWriter(name + ".txt", File.Exists(name + ".txt")))
            {
                writer.WriteLine("Введены значения A=" + A.Real + "+i" + A.Imaginary + ", " +
                    "B=" + B.Real + "+i" + B.Imaginary + "; Действие: разность; Результат=" + result.Real + "+i" + result.Imaginary);
            }

            textBox5.Text = result.Real.ToString();
            textBox6.Text = result.Imaginary.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox5.Clear();
            textBox6.Clear();

            if (!ComplexRead()) return;

            result = A * B;

            using (StreamWriter writer = new StreamWriter(name + ".txt", File.Exists(name + ".txt")))
            {
                writer.WriteLine("Введены значения A=" + A.Real + "+i" + A.Imaginary + ", " +
                    "B=" + B.Real + "+i" + B.Imaginary + "; Действие: умножение; Результат=" + result.Real + "+i" + result.Imaginary);
            }

            textBox5.Text = result.Real.ToString();
            textBox6.Text = result.Imaginary.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox5.Clear();
            textBox6.Clear();

            if (!ComplexRead()) return;

            if (B.Real != 0 && B.Imaginary != 0) result = A / B;
            else
            {
                MessageBox.Show("Число B='0+i0', деление на ноль", "Ошибка");

                using (StreamWriter writer = new StreamWriter(name + ".txt", File.Exists(name + ".txt")))
                {
                    writer.WriteLine("Введены значения A=" + A.Real + "+i" + A.Imaginary + ", " +
                        "B=" + B.Real + "+i" + B.Imaginary + "; Действие: деление; Результат=Ошибка! Деление на 0");
                }

                return;
            }

            using (StreamWriter writer = new StreamWriter(name + ".txt", File.Exists(name + ".txt")))
            {
                writer.WriteLine("Введены значения A=" + A.Real + "+i" + A.Imaginary + ", " +
                    "B=" + B.Real + "+i" + B.Imaginary + "; Действие: деление; Результат=" + result.Real + "+i" + result.Imaginary);
            }

            textBox5.Text = result.Real.ToString();
            textBox6.Text = result.Imaginary.ToString();
        }

        private bool ComplexRead()
        {
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text))
            {
                try
                {
                    A = new Complex(double.Parse(textBox1.Text), double.Parse(textBox2.Text));
                }
                catch
                {
                    MessageBox.Show("Не коректные данные числа A", "Ошибка");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Не заполнены поля числа A", "Ошибка");
                return false;
            }

            if (!string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrEmpty(textBox4.Text))
            {
                try
                {
                    B = new Complex(double.Parse(textBox3.Text), double.Parse(textBox4.Text));
                }
                catch
                {
                    MessageBox.Show("Не коректные данные числа B", "Ошибка");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Не заполнены поля числа B", "Ошибка");
                return false;
            }

            return true;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            autorization.Show();
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists("Справка.txt")) Process.Start("Справка.txt");
            else
            {
                MessageBox.Show("Файл со справкой отсутствует", "Ошибка");
                return;
            }
        }
    }
}

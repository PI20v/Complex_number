using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ComplexForm
{
    public partial class Registration : Form
    {
        Autorization aut;

        List<string> login = new List<string>();
        List<string> password = new List<string>();

        bool reg;

        public Registration(bool reg, Autorization aut)
        {
            InitializeComponent();
            this.reg = reg;
            this.aut = aut;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            reg = false;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0)
            {
                if (textBox1.Text != "Admin")
                {
                    login.Clear();
                    password.Clear();

                    if (!reg)
                    {
                        try
                        {
                            using (StreamReader reader = new StreamReader("Account.acc"))
                            {
                                while (!reader.EndOfStream)
                                {
                                    if (!reader.EndOfStream) login.Add(reader.ReadLine());
                                    if (!reader.EndOfStream) password.Add(reader.ReadLine());
                                }
                            }

                            if (login.Count == password.Count)
                            {
                                foreach (string str in login)
                                    if (str == textBox1.Text)
                                    {
                                        MessageBox.Show("Такой аккаунт уже есть", "Ошибка");
                                        return;
                                    }
                                login.Add(textBox1.Text);
                                password.Add(textBox2.Text);
                            }
                            else
                            {
                                DialogResult dialogResult = MessageBox.Show("Файл с аккаунтами поврежден или изменен\nПересоздать файл?", "Ошибка", MessageBoxButtons.YesNo);
                                if (dialogResult == DialogResult.Yes)
                                {
                                    login.Clear();
                                    password.Clear();

                                    using (StreamWriter writer = new StreamWriter("Account.acc"))
                                    {
                                        login.Add(textBox1.Text);
                                        password.Add(textBox2.Text);

                                        writer.WriteLine(textBox1.Text);
                                        writer.WriteLine(textBox2.Text);
                                    }

                                    reg = false;
                                    this.Close();
                                }
                                else if (dialogResult == DialogResult.No)
                                {
                                    login.Clear();
                                    password.Clear();

                                    this.Close();
                                }
                            }

                            using (StreamWriter writer = new StreamWriter("Account.acc"))
                            {
                                for (int i = 0; i < login.Count; i++)
                                {
                                    writer.WriteLine(login[i]);
                                    writer.WriteLine(password[i]);
                                }

                                reg = false;
                                this.Close();
                            }
                        }
                        catch
                        {
                            try
                            {
                                login.Clear();
                                password.Clear();

                                using (StreamWriter writer = new StreamWriter("Account.acc"))
                                {
                                    login.Add(textBox1.Text);
                                    password.Add(textBox2.Text);

                                    writer.WriteLine(textBox1.Text);
                                    writer.WriteLine(textBox2.Text);
                                }
                                reg = false;
                                this.Close();
                            }
                            catch
                            {
                                MessageBox.Show("Ошибка доступа к файлам компьютера", "Ошибка");
                                this.Close();
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            login.Clear();
                            password.Clear();

                            using (StreamWriter writer = new StreamWriter("Account.acc"))
                            {
                                login.Add(textBox1.Text);
                                password.Add(textBox2.Text);

                                writer.WriteLine(textBox1.Text);
                                writer.WriteLine(textBox2.Text);
                            }

                            reg = false;
                            this.Close();
                        }
                        catch
                        {
                            MessageBox.Show("Ошибка доступа к файлам компьютера", "Ошибка");
                            this.Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Нельзя создавать аккаунт с логинов Admin", "Ошибка");
                    return;
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Back) return;
            else if (!Char.IsNumber(e.KeyChar) && !Char.IsLetter(e.KeyChar)) e.KeyChar = '\0';
        }

        private void Registration_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (reg) aut.Close();
            else aut.Show();
        }
    }
}

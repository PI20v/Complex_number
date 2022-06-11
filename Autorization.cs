using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ComplexForm
{
    public partial class Autorization : Form
    {
        MainForm main;
        Registration registration;

        List<string> login = new List<string>();
        List<string> password = new List<string>();

        public Autorization()
        {
            InitializeComponent();
        }

        private void Autorization_Shown(object sender, EventArgs e)
        {
            if (!ReaderAccount()) this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "Admin" && textBox2.Text == "admin")
            {
                main = new MainForm(this, textBox1.Text);
                main.Show();
                this.Hide();
                return;
            }

            if (!ReaderAccount()) this.Hide();
            else
            {
                for (int i = 0; i < login.Count; i++)
                {
                    if (login[i] == textBox1.Text)
                    {
                        if (password[i] == textBox2.Text)
                        {
                            main = new MainForm(this, textBox1.Text);
                            main.Show();
                            this.Hide();
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Неправильный аккаунт или пароль", "Ошибка");
                            return;
                        }
                    }
                }
                MessageBox.Show("Неправильный аккаунт или пароль", "Ошибка");
                return;
            }

            textBox1.Text = "";
            textBox2.Text = "";
        }

        private bool ReaderAccount()
        {
            try
            {
                login.Clear();
                password.Clear();

                using (StreamReader reader = new StreamReader("Account.acc"))
                {
                    while (!reader.EndOfStream)
                    {
                        if (!reader.EndOfStream) login.Add(reader.ReadLine());
                        if (!reader.EndOfStream) password.Add(reader.ReadLine());
                    }
                }

                if (login.Count != 0 || password.Count != 0)
                {
                    if (login.Count != password.Count)
                    {
                        DialogResult dialogResult = MessageBox.Show("Файл с аккаунтами поврежден или изменен\nПересоздать файл и зарегистрироваться?", "Ошибка", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            login.Clear();
                            password.Clear();

                            registration = new Registration(true, this);
                            registration.Show();
                            return false;
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            login.Clear();
                            password.Clear();

                            return true;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < login.Count; i++)
                        {
                            if (login.IndexOf(login[i]) != login.LastIndexOf(login[i]))
                            {
                                DialogResult dialogResult = MessageBox.Show("В файле с аккаунтами есть одинаковые логины\nПересоздать файл и зарегистрироваться?", "Ошибка", MessageBoxButtons.YesNo);
                                if (dialogResult == DialogResult.Yes)
                                {
                                    login.Clear();
                                    password.Clear();

                                    registration = new Registration(true, this);
                                    registration.Show();
                                    return false;
                                }
                                else if (dialogResult == DialogResult.No)
                                {
                                    login.Clear();
                                    password.Clear();

                                    return true;
                                }
                            }
                        }
                        return true;
                    }
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Файл с аккаунтами пуст\nХотите зарегистрироваться?", "Ошибка", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        registration = new Registration(true, this);
                        registration.Show();
                        return false;
                    }
                    else if (dialogResult == DialogResult.No) return true;
                }
            }
            catch
            {
                DialogResult dialogResult = MessageBox.Show("Файл с аккаунтами отсутствует\nХотите зарегистрироваться?", "Ошибка", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    registration = new Registration(true, this);
                    registration.Show();
                    return false;
                }
                else if (dialogResult == DialogResult.No) return true;
            }
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            registration = new Registration(false, this);
            registration.Show();
            this.Hide();

            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Back) return;
            else if (!Char.IsNumber(e.KeyChar) && !Char.IsLetter(e.KeyChar)) e.KeyChar = '\0';
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (ReaderAccount())
            {
                try
                {
                    login.Clear();
                    password.Clear();

                    using (StreamReader reader = new StreamReader("Account.acc"))
                    {
                        while (!reader.EndOfStream)
                        {
                            if (!reader.EndOfStream) login.Add(reader.ReadLine());
                            if (!reader.EndOfStream) password.Add(reader.ReadLine());
                        }
                    }

                    int index = 0;

                    for (int i = 0; i < login.Count; i++)
                    {
                        if (login[i] == textBox1.Text)
                        {
                            if (password[i] == textBox2.Text)
                            {
                                index = i;
                                break;
                            }

                            MessageBox.Show("Неверный пароль от аккаунта", "Ошибка");
                            return;
                        }

                        if (i == login.Count - 1)
                        {
                            MessageBox.Show("Такого аккаунта нет", "Ошибка");
                            return;
                        }
                    }

                    DialogResult dialogResult = MessageBox.Show("Удалить аккаунт:\nlogin: " + textBox1.Text + "\npassword: " + textBox2.Text, "Ошибка", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            login.RemoveAt(index);
                            password.RemoveAt(index);

                            using (StreamWriter writer = new StreamWriter("Account.acc"))
                            {
                                for (int i = 0; i < login.Count; i++)
                                {
                                    writer.WriteLine(login[i]);
                                    writer.WriteLine(password[i]);
                                }
                            }
                        }
                        catch
                        {
                            MessageBox.Show("Ошибка при удалении аккаунта", "Ошибка");
                            return;
                        }
                    }
                    else if (dialogResult == DialogResult.No) return;
                }
                catch
                {
                    MessageBox.Show("Ошибка при открытия файла Account.acc", "Ошибка");
                    return;
                }

                textBox1.Clear();
                textBox2.Clear();
            }
        }
    }
}

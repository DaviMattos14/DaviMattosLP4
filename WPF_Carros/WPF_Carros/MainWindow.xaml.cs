﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace WPF_Carros
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

        private void Consultar()
        {
            List<Carros> carro = new List<Carros>();

            MySqlCommand cmd = new MySqlCommand()
            {
                Connection = new MySqlConnection("Server=localhost;Database=test;Uid=root;Pwd="),
                CommandText = "SELECT * FROM CARROS WHERE Placa = @placa"
            };

            cmd.Parameters.AddWithValue("@placa", txtConsulta.Text);

            MySqlDataReader r = cmd.ExecuteReader();

            if (r.HasRows)
            {
                while (r.Read())
                {
                    Carros c = new Carros();
                    c.Id = r.GetInt32(0);
                    c.Modelo = r.GetString(1);
                    c.Ano = r.GetInt32(2);
                    c.Placa = r.GetString(3);
                    c.Dono = r.GetString(4);
                    carro.Add(c);
                }
            }
            //else MessageBox.Show("NÃO EXISTE NENHUM CARRO COM ESSA PLACA!");

            
        }

        private void ListaDeCarros()
        {
            MySqlCommand cmd = new MySqlCommand()
            {
                Connection = new MySqlConnection("Server=localhost;Database=test;Uid=root;Pwd="),
                CommandText = "SELECT Modelo, Placa, Dono FROM CARROS"
            };

            cmd.Connection.Open();

            MySqlDataReader result = cmd.ExecuteReader();

            if (result.HasRows)
            {
                while (result.Read())
                {
                    lboxModelo.Items.Add(result.GetString(0));
                    lboxPlaca.Items.Add(result.GetString(1));
                    lboxDono.Items.Add(result.GetString(2));
                }
            }

            cmd.Connection.Close();
        }

        private void Cadastrar()
        {
            MySqlCommand cmd = new MySqlCommand()
            {
                Connection = new MySqlConnection("Server=localhost;Database=test;Uid=root;Pwd="),
                CommandText = "INSERT INTO Carros(Modelo, Ano, Placa, Dono) VALUES (@modelo, @ano, @placa, @dono)"
            };

            cmd.Parameters.AddWithValue("@modelo", txtModelo.Text);
            cmd.Parameters.AddWithValue("@ano", txtAno.Text);
            cmd.Parameters.AddWithValue("@placa", txtPlaca.Text);
            cmd.Parameters.AddWithValue("@dono", txtDono.Text);

            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        private void Limpar()
        {
            txtModelo.Clear();
            txtAno.Clear();
            txtDono.Clear();
            txtPlaca.Clear();
        }

        private void btCadastrar_Click(object sender, RoutedEventArgs e)
        {
            Cadastrar();
            Limpar();
        }

        private void btAtualizar_Click(object sender, RoutedEventArgs e)
        {
            lboxModelo.Items.Clear();
            lboxPlaca.Items.Clear();
            lboxDono.Items.Clear();
            ListaDeCarros();
        }

        private void btnConsulta_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

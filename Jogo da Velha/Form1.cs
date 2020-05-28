using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jogo_da_Velha
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Click += new EventHandler(button1_Click);
            button2.Click += new EventHandler(button1_Click);
            button3.Click += new EventHandler(button1_Click);
            button4.Click += new EventHandler(button1_Click);
            button5.Click += new EventHandler(button1_Click);
            button6.Click += new EventHandler(button1_Click);
            button7.Click += new EventHandler(button1_Click);
            button8.Click += new EventHandler(button1_Click);
            button9.Click += new EventHandler(button1_Click);

            foreach (Control item in this.Controls)
            {
                if (item is Button)
                {
                    item.TabStop = false;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ((Button)sender).Text = "O";

            VerificarGanhador();
        }

        private void VerificarGanhador()
        {
            if (button1.Text != String.Empty && button1.Text == button2.Text && button2.Text == button3.Text ||
                button4.Text != String.Empty && button4.Text == button5.Text && button5.Text == button6.Text ||
                button7.Text != String.Empty && button7.Text == button8.Text && button8.Text == button9.Text ||
                button1.Text != String.Empty && button1.Text == button4.Text && button4.Text == button7.Text ||
                button2.Text != String.Empty && button2.Text == button5.Text && button5.Text == button8.Text ||
                button3.Text != String.Empty && button3.Text == button6.Text && button6.Text == button9.Text ||
                button1.Text != String.Empty && button1.Text == button5.Text && button5.Text == button9.Text ||
                button3.Text != String.Empty && button3.Text == button5.Text && button5.Text == button7.Text)
            {
                Reiniciar();
            }
            else
            {
                VerificarEmpate();
            }
        }

        private void VerificarEmpate()
        {
            bool todosDesabilitados = true;

            foreach (Control item in this.Controls)
            {
                if (item is Button && item.Enabled)
                {
                    todosDesabilitados = false;
                    break;
                }
            }

            if (todosDesabilitados)
            {
                MessageBox.Show(String.Format("Deu velha"), "Opa!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                Reiniciar();
            }
        }

        private void Reiniciar()
        {
            foreach (Control item in this.Controls)
            {
                if (item is Button)
                {
                    item.Enabled = true;
                    item.Text = String.Empty;
                }
            }
        }
    }

    namespace TicTacToe_MinMax
    {
        public class State
        {
            public int[] table_configuration;
            public int point = 0;
            public bool isFinal;
            public int winner;

            public State(int[] configuration, int point)
            {
                this.table_configuration = configuration;
                this.point = point;
            }


            public void setMove(int position, int value)
            {
                this.table_configuration[position] = value;
            }

            public int getStaticPotuation()
            {
                return 1;
            }

            public int[] getConfiguration()
            {
                return this.table_configuration;
            }

        }

        public class Tree<T>
        {
            public TreeNode<T> Root { get; set; }
        }

        public class TreeNode<T>
        {
            public T Data { get; set; }
            public TreeNode<T> Parent { get; set; }
            public List<TreeNode<T>> Children { get; set; }
            public int minMaxValue = 0;

            public int GetHeight()
            {
                int height = 1;
                TreeNode<T> current = this;

                while (current.Parent != null)
                {
                    height++;
                    current = current.Parent;
                }

                return height;
            }

        }

        public class TicTacToeTree
        {
            private int count_move;
            private Tree<State> tree;

            public TicTacToeTree()
            {

                int[] initial_configuration = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

                State initial_state = new State(initial_configuration, 0);

                tree = new Tree<State>
                {
                    Root = new TreeNode<State>
                    {
                        Data = initial_state,
                        Children = new List<TreeNode<State>>()
                    }
                };

                GenerateTreeGame(tree);
            }

            public Tree<State> GetTree()
            {
                return this.tree;
            }

            public void GenerateTreeGame(Tree<State> tree)
            {
                TreeNode<State> currentNode = tree.Root;

                UpdateChildren(currentNode);
            }

            public static int CheckIfWin(int[] configuration)
            {

                if (configuration[0] == 1 && configuration[1] == 1 && configuration[2] == 1) return 1;
                else if (configuration[0] == 2 && configuration[1] == 2 && configuration[2] == 2) return 2;
                else if (configuration[3] == 1 && configuration[4] == 1 && configuration[5] == 1) return 1;
                else if (configuration[3] == 2 && configuration[4] == 2 && configuration[5] == 2) return 2;
                else if (configuration[6] == 1 && configuration[7] == 1 && configuration[8] == 1) return 1;
                else if (configuration[6] == 2 && configuration[7] == 2 && configuration[8] == 2) return 2;
                else if (configuration[0] == 1 && configuration[3] == 1 && configuration[6] == 1) return 1;
                else if (configuration[0] == 2 && configuration[3] == 2 && configuration[6] == 2) return 2;
                else if (configuration[1] == 1 && configuration[4] == 1 && configuration[7] == 1) return 1;
                else if (configuration[1] == 2 && configuration[4] == 2 && configuration[7] == 2) return 2;
                else if (configuration[2] == 1 && configuration[5] == 1 && configuration[8] == 1) return 1;
                else if (configuration[2] == 2 && configuration[5] == 2 && configuration[8] == 2) return 2;
                else if (configuration[0] == 1 && configuration[4] == 1 && configuration[8] == 1) return 1;
                else if (configuration[0] == 2 && configuration[4] == 2 && configuration[8] == 2) return 2;
                else if (configuration[2] == 1 && configuration[4] == 1 && configuration[6] == 1) return 1;
                else if (configuration[2] == 2 && configuration[4] == 2 && configuration[6] == 2) return 2;

                return 0;
            }

            public void UpdateChildren(TreeNode<State> node)
            {


                if (node.Parent == null)
                {
                    count_move = 1;
                }
                else
                {
                    if (node.minMaxValue == 1)
                    {
                        count_move = 2;
                    }
                    else if (node.minMaxValue == 2)
                    {
                        count_move = 1;
                    }
                }

                if (node.Children == null) return;

                int[] root_configuration = node.Data.getConfiguration();
                int winner = CheckIfWin(root_configuration);
                if (winner != 0)
                {
                    node.Data.isFinal = true;
                    node.Data.winner = winner;
                    return;
                }

                List<int[]> possible_configurations = GeneratePossibleConfiguration(root_configuration, count_move);

                if (possible_configurations == null)
                {
                    node.Children = null;
                }
                else
                {

                    foreach (int[] move in possible_configurations)
                    {
                        State state = new State(move, 0);
                        TreeNode<State> temp_node = new TreeNode<State>
                        {
                            Data = state,
                            Children = new List<TreeNode<State>>(),
                            Parent = node
                        };
                        node.Children.Add(temp_node);
                    }

                    foreach (TreeNode<State> childnode in node.Children)
                    {
                        UpdateChildren(childnode);
                    }
                }

            }

            public List<int[]> GeneratePossibleConfiguration(int[] source, int marca)
            {
                List<int[]> all_configuration = new List<int[]>();
                for (int i = 0; i < source.Length; i++)
                {
                    if (source[i] == 0)
                    {
                        int[] new_configuration = (int[])source.Clone();
                        new_configuration[i] = marca;
                        all_configuration.Add(new_configuration);
                    }

                }

                return all_configuration;
            }
        }
    }
}
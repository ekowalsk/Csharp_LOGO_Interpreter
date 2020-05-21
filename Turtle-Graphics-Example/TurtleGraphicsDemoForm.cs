using Nakov.TurtleGraphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Turtle_Graphics_Example
{
    public partial class TurtleGraphicsDemoForm : Form
    {
        public TurtleGraphicsDemoForm()
        {
            InitializeComponent();
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            StringSource s = new StringSource(this.textBox.Text);
            this.textBox.Text = "";
            Lexer lex = new Lexer(s);
            Parser p = new Parser(lex);
            Interpreter interpreter = new Interpreter(p);
            SemanticAnalyzer stv = new SemanticAnalyzer();
            try
            {
                IAST tree = p.program();
                tree.accept(stv);
                tree.accept(interpreter);
            }
            catch (Exception exc)
            {
                textBox.Text += ' ' + exc.Message;
            }
        } 

        private void buttonReset_Click(object sender, EventArgs e)
        {
            Turtle.Reset();
        }

        private void buttonShowHideTurtle_Click(object sender, EventArgs e)
        {
            if (Turtle.ShowTurtle)
            {
                Turtle.ShowTurtle = false;
                this.buttonShowHideTurtle.Text = "Show Turtle";
            }
            else
            {
                Turtle.ShowTurtle = true;
                this.buttonShowHideTurtle.Text = "Hide Turtle";
            }
        }
        private void buttonShowHideTextBox_Click(object sender, EventArgs e)
        {
            if (this.textBox.Visible)
            {
                this.textBox.Visible = false;
                this.buttonShowHideTextBox.Text = "Show Text";
            }
            else
            {
                this.textBox.Visible = true;
                this.buttonShowHideTextBox.Text = "Hide Text";
            }
        }
    }
}


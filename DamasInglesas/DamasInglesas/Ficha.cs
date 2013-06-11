using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DamasInglesas
{
    public class Ficha : Canvas
    {
        public static string JUGADOR_A = "A";
        public static string JUGADOR_B = "B";
        
        private CompositeTransform transform;
        private double posicionX;
        private double posicionY;


        private string jugador;

        public string Jugador
        {
            get { return jugador; }
            set { jugador = value; }
        }

        private Grid grilla;

        public Grid Grilla
        {
            get { return grilla; }
            set { grilla = value; }
        }

        public Ficha()
        {
            transform = new CompositeTransform();
            this.ManipulationStarted += InicioManipulacion;
            this.ManipulationDelta += Manipulacion;
            this.ManipulationCompleted += FinManipulacion;
            this.RenderTransform = transform;
        }

        
        private void InicioManipulacion(object sender, System.Windows.Input.ManipulationStartedEventArgs e)
        {
            posicionX = 0;
            posicionY = 0;
        }

        private void Manipulacion(object sender, System.Windows.Input.ManipulationDeltaEventArgs e)
        {
            transform.TranslateX += e.DeltaManipulation.Translation.X;
            transform.TranslateY += e.DeltaManipulation.Translation.Y;
        }

        private void FinManipulacion(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            var ficha = sender as Ficha;
            int fila;
            int columna;

            if (Damas.getInstance().TURNO.Equals(Jugador))
            {
                if (Jugador.Equals(JUGADOR_B))
                {
                    if (posicionY > transform.TranslateY)
                    {
                        fila = Int16.Parse(ficha.GetValue(Grid.RowProperty).ToString()) - 1;
                        if (posicionX > transform.TranslateX)
                        {
                            columna = Int16.Parse(ficha.GetValue(Grid.ColumnProperty).ToString()) - 1;
                        }
                        else
                        {
                            columna = Int16.Parse(ficha.GetValue(Grid.ColumnProperty).ToString()) + 1;
                        }

                        if (Damas.getInstance().PosicionValida(fila, columna, ficha))
                        {
                            ficha.SetValue(Grid.RowProperty, fila);
                            ficha.SetValue(Grid.ColumnProperty, columna);
                            Damas.getInstance().TURNO = JUGADOR_A;
                        }
                    }
                }
                else if (Jugador.Equals(JUGADOR_A))
                {
                    if (posicionY < transform.TranslateY)
                    {
                        fila = Int16.Parse(ficha.GetValue(Grid.RowProperty).ToString()) + 1;
                        if (posicionX > transform.TranslateX)
                        {
                            columna = Int16.Parse(ficha.GetValue(Grid.ColumnProperty).ToString()) - 1;
                        }
                        else
                        {
                            columna = Int16.Parse(ficha.GetValue(Grid.ColumnProperty).ToString()) + 1;
                        }
                        if (Damas.getInstance().PosicionValida(fila, columna, ficha))
                        {
                            ficha.SetValue(Grid.RowProperty, fila);
                            ficha.SetValue(Grid.ColumnProperty, columna);
                            Damas.getInstance().TURNO = JUGADOR_B;
                        }
                    }
                }
            }
            Damas.getInstance().ValidarCaptura(ficha);

            transform.TranslateX = 0;
            transform.TranslateY = 0;
        }

    }
}

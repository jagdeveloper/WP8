using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DamasInglesas
{
    public class Damas: NotificationEnabledObject
    {

        public string TURNO = "A";
        private int fichasA = 12;
        private int fichasB = 12;
        public Grid tablero;

        private static Damas instance;

        public static Damas getInstance()
        {
            if (instance == null)
            {
                instance = new Damas();

                
            }
            return instance;
        }


        public Damas()
        {
            instance = this;
        }

        public int FichasA
        {
            get { return fichasA; }
            set { fichasA = value;
            OnPropertyChanged();        
            }
        }

        public int FichasB
        {
            get { return fichasB; }
            set { fichasB = value;
            OnPropertyChanged();
            }
        }

        public void ValidarCaptura(Ficha ficha)
        {
            Ficha fichaCaptura = PiezaEnPosicion(Int16.Parse(ficha.GetValue(Grid.RowProperty).ToString()),
                                                Int16.Parse(ficha.GetValue(Grid.ColumnProperty).ToString()),
                                                ficha.Jugador.Equals(Ficha.JUGADOR_A) ? Ficha.JUGADOR_B : Ficha.JUGADOR_A);

            if (fichaCaptura != null)
            {
                tablero.Children.Remove(fichaCaptura);
                if (fichaCaptura.Jugador.Equals(Ficha.JUGADOR_A))
                {
                    FichasA--;
                }
                else
                {
                    FichasB--;
                }
            }
        }

        public bool PosicionValida(int fila, int columna, Ficha ficha)
        {
            bool valida = true;
            Ficha fichaPosicion = PiezaEnPosicion(fila, columna, ficha.Jugador);

            if (fichaPosicion != null)
            {
                valida = false;
            }
            

            return valida;
        }

        private Ficha PiezaEnPosicion(int fila, int columna, string jugador)
        {
            Ficha ficha = null;

            if (tablero != null)
            {
                for (int i = 0; i < tablero.Children.Count; i++)
                {
                    if (tablero.Children[i].GetType() ==  typeof(Ficha))
                    {
                        ficha= tablero.Children[i] as Ficha;

                        if (Int16.Parse(ficha.GetValue(Grid.RowProperty).ToString()) == fila &&
                            Int16.Parse(ficha.GetValue(Grid.ColumnProperty).ToString()) == columna &&
                            ficha.Jugador.Equals(jugador))
                        {
                            break;
                        }
                        else
                        {
                            ficha = null;
                        }
                    }
                }
            }

            return ficha;
        }
    }
}

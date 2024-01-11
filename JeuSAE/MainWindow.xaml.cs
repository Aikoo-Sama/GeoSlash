﻿using System;
using System.Numerics;
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
using System.Windows.Threading;

namespace JeuSAE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private int countTick = 0, vitesseJoueur = 150, tempsRechargeArme = 60, tempsRechargeActuel = 0, vitesseBalle = 3;
        private bool gauche = false, droite = false, haut = false, bas = false, tirer = false;
        private List<Balle> balleList = new List<Balle>();

        

        private Rect player = new Rect(910, 490, 50, 50);



        public MainWindow()
        {
            InitializeComponent();
            dispatcherTimer.Tick += GameEngine;
            // rafraissement toutes les 16 milliseconds
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(16);
            // lancement du timer
            dispatcherTimer.Start();

            MapGenerator.load(carte);

            Menu menu = new Menu();
            menu.ShowDialog();


        }

        private void monCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tirer = true;
        }

        private void monCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            tirer = false;
        }

        private void CanvasKeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
                gauche = true;
            if (e.Key == Key.Right)
                droite = true;
            if (e.Key == Key.Up)
                haut = true;
            if (e.Key == Key.Down)
                bas = true;
        }

        private void CanvasKeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
                gauche = false;
            if (e.Key == Key.Right)
                droite = false;
            if (e.Key == Key.Up)
                haut = false;
            if (e.Key == Key.Down)
                bas = false;
        }


        private void GameEngine(object sender, EventArgs e)
        {
            
#if DEBUG
            Console.WriteLine(Canvas.GetLeft(carte));
            Console.WriteLine(Canvas.GetTop(carte));
#endif




            MouvementJoueur();
            TirJoueur();
        }

        private void MouvementJoueur()
        {
            if (gauche)
                if (Canvas.GetLeft(carte) + vitesseJoueur < 910)
                {
                    Canvas.SetLeft(carte, Canvas.GetLeft(carte) + vitesseJoueur);
                }

                else
                {
                    Canvas.SetLeft(carte, 910);
                }

            if (droite)
                if (Canvas.GetLeft(carte) - vitesseJoueur > -18240)
                {
                    Canvas.SetLeft(carte, Canvas.GetLeft(carte) - vitesseJoueur);
                }
                else
                {
                    Canvas.SetLeft(carte, -18240);
                }
            if (haut)
            {
                if (Canvas.GetTop(carte) + vitesseJoueur < 490)
                {
                    Canvas.SetTop(carte, Canvas.GetTop(carte) + vitesseJoueur);
                }
                else
                {
                    Canvas.SetTop(carte, 490);
                }
            }
            if (bas)
                if (Canvas.GetTop(carte) - vitesseJoueur > -10360)
                {
                    Canvas.SetTop(carte, Canvas.GetTop(carte) - vitesseJoueur);
                }
                else
                {
                    Canvas.SetTop(carte, -10260);
                }
        }

        private void TirJoueur()
        {
            if (tempsRechargeActuel > 0)
                tempsRechargeActuel--;

            
            if (tirer && tempsRechargeActuel <= 0)
            {
                var posEcran = Mouse.GetPosition(Application.Current.MainWindow);
                var posCarte = Mouse.GetPosition(carte);
#if DEBUG
                Console.WriteLine(posCarte.X.ToString() + "  " + posCarte.Y.ToString());
#endif
                tempsRechargeActuel = tempsRechargeArme;
                /*
                Vector2 vecteurTir = new Vector2((float)posEcran.X - 910, (float)posEcran.Y - 490);
                Vector2 vecteurNormalise = Vector2.Normalize(vecteurTir);
                Balle balleJoueur = new Balle(vitesseBalle, 20, 0, "joueur", 0, 810, 390, vecteurNormalise);

                Canvas.SetTop(balleJoueur.Graphique, 390);
                Canvas.SetLeft(balleJoueur.Graphique, 810);
                */
                Vector2 vecteurTir = new Vector2((float)posEcran.X - 910, (float)posEcran.Y - 490);

                Balle balleJoueur = new Balle(vitesseBalle, 20, 0, "joueur", 0, 910, 490, vecteurTir);
                Canvas.SetLeft(balleJoueur.Graphique, balleJoueur.PosX);
                Canvas.SetTop(balleJoueur.Graphique, balleJoueur.PosY);


                monCanvas.Children.Add(balleJoueur.Graphique);
                balleList.Add(balleJoueur);
                
            }



            if (balleList != null)
            {

                foreach (Balle balle in balleList)
                {
                    balle.Deplacement();
#if DEBUG
                    Console.WriteLine("Balle PosX : " + balle.PosX);
                    Console.WriteLine("Balle PosY : " + balle.PosY);

#endif
                    Canvas.SetLeft(balle.Graphique, balle.PosX);
                    Canvas.SetTop(balle.Graphique, balle.PosY);
                }

            }


            
            
        }

        


    }
}

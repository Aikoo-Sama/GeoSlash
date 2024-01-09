﻿using System;
using System.Drawing;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace JeuSAE
{
    internal class Ennemi
    {
        private double vie;
        private double vitesse; // En pixel/tick
        private double cadenceTir; // En seconde/tir donc si = 3 alors l'ennemi tir une fois toutes les 3 secondes, donc pour 3 fois par seconde c'est approx 0.33
        private int type;
        private String nom;
        private int posX;
        private int posY;
        private Guid id = Guid.NewGuid();
        private Rect rect;
        private Rectangle graphique;
        private ImageBrush ennemiImage = new ImageBrush();
        private Uri dossierImage = new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/");


        public double Vie
        {
            get { return vie; }
            set { vie = value; }
        }


        public double Vitesse
        {
            get { return vitesse; }
            set { vitesse = value; }
        }

        public double CadenceTir
        {
            get { return cadenceTir; }
            set { cadenceTir = value; }
        }

        public int Type
        {
            get { return type; }
            set { type = value; }
        }


        public String Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        public int PosX
        {
            get { return posX; }
            set { posX = value; }
        }

        public int PosY
        {
            get { return posY; }
            set { posY = value; }
        }

        public Guid Id
        {
            get { return id; }
        }

        public Rect Rect { get => rect; set => rect = value; }

        public Rectangle Graphique { get => graphique; set => graphique = value; }

        public Ennemi(int type, int posX, int posY)
        {
            Type = type;

            switch (type)
            {
                case 0: // Triangle équilatéral
                    this.Vie = Constantes.VIE_TRIANGLE_EQ;
                    this.Vitesse = Constantes.VITESSE_TRIANGLE_EQ;
                    this.CadenceTir = Constantes.CADENCE_TRIANGLE_EQ;
                    this.Nom = Constantes.NOM_TRIANGLE_EQ;
                    ennemiImage.ImageSource = new BitmapImage(new Uri(dossierImage + "triangle.png"));// dossierImage c'est un Uri donc ça vas peut-être bugger
                    break; //TODO finir de mettre les images sur les ennemis
                    /*
                case 1:
                    this.Vie = 5;
                    this.Vitesse = 1;
                    this.CadenceTir = 2;
                    this.Nom = "Rectangle";
                    break;
                    */
                case 1: // Carré
                    this.Vie = Constantes.VIE_CARRE;
                    this.Vitesse = Constantes.VITESSE_CARRE;
                    this.CadenceTir = Constantes.CADENCE_CARRE;
                    this.Nom = Constantes.NOM_CARRE;
                    break;
                case 2: // Pentagone
                    this.Vie = Constantes.VIE_PENTAGONE;
                    this.Vitesse = Constantes.VITESSE_PENTAGONE;
                    this.CadenceTir = Constantes.CADENCE_PENTAGONE;
                    this.Nom = Constantes.NOM_PENTAGONE;
                    break;
                case 3: // Hexagone
                    this.Vie = Constantes.VIE_HEXAGONE;
                    this.Vitesse = Constantes.VITESSE_HEXAGONE;
                    this.CadenceTir = Constantes.CADENCE_HEXAGONE;
                    this.Nom = Constantes.NOM_HEXAGONE;
                    break;
                case 4: // Heptagone
                    this.Vie = Constantes.VIE_HEPTAGONE;
                    this.Vitesse = Constantes.VITESSE_HEPTAGONE;
                    this.CadenceTir = Constantes.CADENCE_HEPTAGONE;
                    this.Nom = Constantes.NOM_HEPTAGONE;
                    break;
                case 5: // Octogone
                    this.Vie = Constantes.VIE_OCTOGONE;
                    this.Vitesse = Constantes.VITESSE_OCTOGONE;
                    this.CadenceTir = Constantes.CADENCE_OCTOGONE;
                    this.Nom = Constantes.NOM_OCTOGONE;
                    break;
                case 6: // Cercle
                    this.Vie = Constantes.VIE_CERCLE;
                    this.Vitesse = Constantes.VITESSE_CERCLE;
                    this.CadenceTir = Constantes.CADENCE_CERCLE;
                    this.Nom = Constantes.NOM_CERCLE;
                    break;
                case 7: // Triangle rectangle
                    this.Vie = Constantes.VIE_TRIANGLE_RECT;
                    this.Vitesse = Constantes.VITESSE_TRIANGLE_RECT;
                    this.CadenceTir = Constantes.CADENCE_TRIANGLE_RECT;
                    this.Nom = Constantes.NOM_TRIANGLE_RECT;
                    break; //TODO ajouter plus d'ennemis si on a des idées
            }
            PosX = posX;
            PosY = posY;
            Rect = new Rect(PosX, PosY, Constantes.ENNEMI_RECT_LARGEUR, Constantes.ENNEMI_RECT_HAUTEUR);
            Graphique = new Rectangle(PosX, PosY, Constantes.ENNEMI_RECT_LARGEUR, Constantes.ENNEMI_RECT_HAUTEUR);
        }

        public override bool Equals(object? obj)
        {
            return obj is Ennemi ennemi &&
                   Id.Equals(ennemi.Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id);
        }

        public override string? ToString()
        {
            return this.Nom;
        }
    }
}

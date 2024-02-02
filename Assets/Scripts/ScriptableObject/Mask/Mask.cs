using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Mask",menuName="Inventory/Mask")]
public class Mask : ScriptableObject
{
    public int id;
    public string name;
    public Sprite image;
    public int price;
    [TextArea(3, 10)]
    public string desc;
    public int capawater;
    public int capacold;
    public int res;
    public int degbrut;
    public int degpourcent;
    public int tpsglace; //si couleur blanche équipée
    public int rayonExplo; //si couleur rouge / couleur bleu
    public int largeurslash; //si couleur jaune
    public int degstones; //si couleur orange
    public int regainitems; //couleur verte :  permet de regagner ses items utilisé pendant le rollback après avoir fait un rollback sans annuler les effets des items consommés
    public bool canuseitem;
    public bool canusemana;
    public int dashstatus; //0 cannot; 1 can ; 2 can and invincible
    public int cooldowndash;
    public int cooldownspell;
    public int healeffect;
    public int speedeffect;
    public int manaeffect;
    public int manause;
    public int speed;
    public int jumphigh;
    public bool canseehp; //entrevoit les pv des ennemis après les avoir tapés
    public bool canseestats; //masque la barre de pv et de mana du joueur si false
    public int tpsinv; //temps d'invincibilité après avoir reçu un coup
    [TextArea(3, 10)]
    public List<string> positifs = new List<string>();
    [TextArea(3, 10)]
    public List<string> negatifs = new List<string>();

}

﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollisionDetectorHack : MonoBehaviour
{
    public BoxCollider2D[] BoxCollidersWithCallBacks;
    BoxCollider2D[] allTheBoxColliders;

    void Start ()
    {
        allTheBoxColliders = FindObjectsOfType<BoxCollider2D>() as BoxCollider2D[];
    }

	void Update ()
    {
        foreach (BoxCollider2D box1 in BoxCollidersWithCallBacks)
        {
            float Box1Left = box1.transform.position.x + box1.offset.x - (box1.size.x * 0.5f);
            float Box1Right = box1.transform.position.x + box1.offset.x + (box1.size.x * 0.5f);// * box2.transform.localScale.x);
            float Box1Top = box1.gameObject.transform.position.y + box1.offset.y + (box1.size.y * 0.5f);
            float Box1Bottom = box1.transform.position.y + box1.offset.y - (box1.size.y * 0.5f);// * box2.transform.localScale.y);

            //Debug.Log("STP->box1:" + box1.name + ":Box1Left=" + Box1Left + ":Box1Right=" + Box1Right + "\t:Box1Top=" + Box1Top + "\t:Box1Bottom=" + Box1Bottom);

            foreach (BoxCollider2D box2 in allTheBoxColliders)
            {
                if (box1 == box2) continue;

                float Box2Left = box2.transform.position.x + box2.offset.x - (box2.size.x * 0.5f);
                float Box2Right = box2.transform.position.x + box2.offset.x + (box2.size.x * 0.5f);// * box2.transform.localScale.x);
                float Box2Top = box2.gameObject.transform.position.y + box2.offset.y + (box2.size.y * 0.5f);
                float Box2Bottom = box2.transform.position.y + box2.offset.y - (box2.size.y * 0.5f);// * box2.transform.localScale.y);

                //Debug.Log("STP->box2:" + box2.name + ":Box2Left=" + Box2Left + ":Box2Right=" + Box2Right + "\t:Box2Top=" + Box2Top + "\t:Box2Bottom=" + Box2Bottom + "\tbox2.transform.position.y=" + box2.gameObject.transform.position.y + "\t:box2.offset.y=" + box2.offset.y);
                bool collisionOnBox2Left = ((Box1Left <= Box2Left) && (Box1Right >= Box2Left)); //collision on box2Left if 
                bool collisionOnBox2Right = ((Box1Left <= Box2Right) && (Box1Right >= Box2Right)); //collision on Box2Right
                bool collisionOnBox2Top = ((Box1Top >= Box2Top) && (Box1Bottom <= Box2Top)); //collision on Box2Top
                bool collisionOnBox2Bottom = ((Box1Top >= Box2Bottom) && (Box1Bottom <= Box2Bottom)); //collision on Box2Right

                bool box1ContainsBox2Horizontally = ((Box1Left >= Box2Left) && (Box1Right <= Box2Right));
                bool box1ContainsBox2Vertically = ((Box1Top <= Box2Top) && (Box1Bottom >= Box2Bottom));

                //Debug.Log("STP->collisionOnBox2Left=" + collisionOnBox2Left + "\t collisionOnBox2Right=" + collisionOnBox2Right + "\t collisionOnBox2Top=" + collisionOnBox2Top + "\t collisionOnBox2Bottom=" + collisionOnBox2Bottom 
                //    + "\t box1ContainsBox2Horizontally=" + box1ContainsBox2Horizontally + "\t box1ContainsBox2Vertically=" + box1ContainsBox2Vertically);
                bool thereIsACollision = false;

                if((collisionOnBox2Right && collisionOnBox2Top) || (collisionOnBox2Right && collisionOnBox2Bottom) || (collisionOnBox2Right && box1ContainsBox2Vertically))
                {
                    //Debug.Log("STP->collision true on box 2 right");
                    thereIsACollision = true;
                }

                if ((collisionOnBox2Left && collisionOnBox2Top) || (collisionOnBox2Left && collisionOnBox2Bottom) || (collisionOnBox2Left && box1ContainsBox2Vertically))
                {
                    //Debug.Log("STP->collision true on box 2 leftt");
                    thereIsACollision = true;
                }


                if ((box1ContainsBox2Horizontally && collisionOnBox2Top) || (box1ContainsBox2Horizontally && collisionOnBox2Bottom) || (box1ContainsBox2Horizontally && box1ContainsBox2Vertically))
                {
                    //Debug.Log("STP->collision true on box 2 contains box 1 horizontally");
                    thereIsACollision = true;
                }

                if (thereIsACollision)
                {
                    CharacterControllerScript character = box1.GetComponent<CharacterControllerScript>();
                    if(character != null)
                    {
                        if (box2.tag == "Ground") { character.GroundCollision(); }
                        if (box2.tag == "Finish") { character.FinishCollision(); }

                    }
                }
            }
        }	
	}
}

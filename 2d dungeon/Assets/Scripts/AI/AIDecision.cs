using Main;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Main.RankStructure;

public class AIDecision : MonoBehaviour
{
    public float ShootingWeightMultiplier;
    public float ShootingWeight = 0;
    public float MovementWeight = 0;
    public float AdminWeight = 0;
    public bool Active = false;
    public Rigidbody2D Player;
    public float ShootingPower;
    public AIModel AI;
    public Rigidbody2D Bullet;
    public Transform MovePoint;
    public float ShotSpeed;
    public float constz;
    public float movespeed;
    private Transform NewMovePoint;

    private void Start()
    {
        NewMovePoint = Instantiate(MovePoint); // instantiates (clones) and makes the movepoint independent 
        NewMovePoint.parent = null;
        NewMovePoint.position = transform.position;
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, constz), new Vector3(NewMovePoint.position.x, NewMovePoint.position.y, constz), movespeed * Time.deltaTime); // moves to the move point on each update at a constant speed
    }
    public void Action() //called on each player action
    {
        RaycastHit2D i = CastRay(this.transform.position, Player.transform.position, LayerMask.GetMask("MapObjects", "Player"));
        if (i.collider.tag == "Player" &&!Active && LinearDistance(this.transform.position,Player.transform.position)<240) // checks if the player can be seen and becomes activeif it can see it and isn't already active as well as if the player is within a reasonable distance
        {
            Active = true;
        }
        if (Active) //if it has seen the player or has seen the player
        {
            
            MovementWeight = 0; // resets weighting of decisions
            AdminWeight = 0;
            ShootingWeight = 0;
            GetShootingWeight(); //weights the decisions
            GetMovementWeight();
            GetAdminWeight();
            

            WeightedDecision(); // uses the weights to make a decision
        }
    }

    private void GetAdminWeight()
    {
        AdminWeight += 100 / AI.Health; //makes the admin weight inversely proportional to their health
    }

    private void GetMovementWeight()
    {

        
        if(CastRay(this.transform.position,Player.transform.position,LayerMask.GetMask("MapObjects", "Player")).collider.tag == "Player")
        {
            RaycastHit2D CoverHit = CastRay(this.transform.position,Player.transform.position,LayerMask.GetMask("Cover", "Player", "EnemyAI"));
            if (CoverHit.collider.tag == "Cover")
            {
                MovementWeight +=5/LinearDistance(this.transform.position,CoverHit.collider.transform.position);
            }
            else if(CoverHit.collider.tag =="EnemyAI")
            {
                MovementWeight += 15;
            }
            else if(CoverHit.collider.tag == "Player" && AI.Health<25)
            {
                MovementWeight += 30;
            }
            else if(CoverHit.collider.tag == "Player")
            {
                if(Physics2D.OverlapCircle(this.transform.position,64f,LayerMask.GetMask("Cover")))
                {
                    MovementWeight += 30;
                }
                else
                {
                    MovementWeight += 5;
                }
            }
        }
        else
        {
            MovementWeight += 50;
        }
    }

    private void GetShootingWeight()
    {
        RaycastHit2D Hit = CastRay(this.transform.position, Player.transform.position, LayerMask.GetMask("MapObjects", "Player")); //TODO add detection for enemyAI, at the moment detects itself then stops
        if (Hit.collider.tag == "Player")// && Hit.collider.tag !="EnemyAI")
        {
            
            ShootingWeight = ShootingWeightMultiplier / (float)Math.Pow(LinearDistance(this.transform.position, Player.transform.position), ShootingPower); //inversely propotional to the distance multiplied by a multiplier
        }
    }

    private void WeightedDecision()
    {
        float TotWeight = ShootingWeight + MovementWeight + AdminWeight;
        float ChosenWeight = UnityEngine.Random.Range(0,TotWeight); // generates a random number between 0 and the total weight
        if(ChosenWeight<=ShootingWeight)// chooses the correct weight for the random number generated
        {
            ShootPlayer();
        }
        else if(ChosenWeight<=MovementWeight)
        {
            MoveDecision();
        }
        else if (ChosenWeight<=AdminWeight)
        {
            AdminDecision();
        }

    }

    private void AdminDecision()
    {
        AI.Health += 5; // efectively using a healing kit
    }

    private void ShootPlayer()
    {
        float RandAngle = UnityEngine.Random.Range(-5f, 5f);
        Rigidbody2D Shot = Instantiate(Bullet);
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), Shot.GetComponent<Collider2D>(), true);
        Shot.position = this.transform.position;
        Shot.gameObject.transform.Rotate(new Vector3(0, 0, LinearAngle(Player.transform.position,transform.position)+ RandAngle));
        Shot.GetComponent<SpriteRenderer>().enabled = true;
        Shot.velocity = new Vector2((Player.transform.position.x - this.transform.position.x) , (Player.transform.position.y - this.transform.position.y)).normalized * ShotSpeed;
    }

    private void MoveDecision()
    {
       Vector2 NodeLoc = ToNodeLoc(this.transform.position);
        if (Physics2D.OverlapCircle(this.transform.position, 64f, LayerMask.GetMask("Cover")))
        {
            LinearAngle(transform.position, ToNodeLoc(Physics2D.OverlapCircle(this.transform.position, 64f, LayerMask.GetMask("Cover")).gameObject.transform.position));
        }
        else if(LinearDistance(this.transform.position,Player.position)>160)
        {

        }
        else if (!Physics2D.OverlapCircle(new Vector2(NewMovePoint.position.x, NewMovePoint.position.y) + ToNormalizedAngle(LinearAngle(transform.position, Player.transform.position) + 180) * 16f,7f, LayerMask.GetMask("MapObjects", "EnemyAI")))
        {
            NewMovePoint.position += new Vector3( (ToNormalizedAngle(LinearAngle(transform.position,Player.transform.position)+180)*16f).x, (ToNormalizedAngle(LinearAngle(transform.position, Player.transform.position) + 180) * 16f).y,0f);
            
        }
    }

    private Vector2 ToNormalizedAngle(float angle)
    {
        if (angle < 0)
        {
            angle = 360 + angle;
        }
        if(315<=angle || angle<45)
        {
            return Vector2.up;
        }
        else if (45 <= angle && angle < 135)
        {
            return Vector2.right;
        }
        else if (135 <= angle && angle < 225)
        {
            return Vector2.down;
        }
        else if (225 <= angle && angle < 315)
        {
            return Vector2.left;
        }
        else
        {
            return Vector2.zero;
        }
    }
    private float LinearDistance(Vector2 position1, Vector2 position2)
    {
        return (float)(Math.Sqrt(Math.Pow((position1.x - position2.x), 2) + Math.Pow(position1.y - position2.y, 2)));
    }

    private float LinearAngle(Vector2 position1, Vector2 position2)
    {
        float pos1x = position1.x;
        float pos1y = position1.y;
        float pos2x = position2.x;
        float pos2y = position2.y;
        float ans = 0;
        if(pos2x>pos1x && pos2y>pos1y)
        {
            ans =  Mathf.Atan((pos2y - pos1y) / (pos2x - pos1x)) * 180 / Mathf.PI;
            //ans += 180;
        }
        else
        {
            ans = Mathf.Atan((pos1y - pos2y) / (pos1x - pos2x)) * 180 / Mathf.PI;
        }
        if(ans<0)
        {
            ans += 360;
        }

        return ans;
    }

    private RaycastHit2D CastRay(Vector3 From,Vector3 Target, LayerMask LayerColliders)
    {
        
        Ray2D SeePlayer = new Ray2D(this.transform.position, new Vector2((Target.x - From.x),(Target.y - From.y)));
        RaycastHit2D hitData;
        Debug.DrawRay(SeePlayer.origin, SeePlayer.direction);
        hitData = Physics2D.Raycast(SeePlayer.origin, SeePlayer.direction, Mathf.Infinity, LayerColliders);
        return hitData;
        
    }
    public Vector2 ToNodeLoc(Vector2 ActualLoc)
    {
        return new Vector2(Mathf.Round((ActualLoc.x - 0.08f) / 0.16f), Mathf.Round((ActualLoc.y - 0.08f) / 0.16f));
    }
    public void TakeDmg(int dmg)
    {
        AI.Health -= dmg;
        if (AI.Health <= 0)
        {
            //Destroy(MovePoint.gameObject);
            Destroy(this.gameObject);
        }
    }
}


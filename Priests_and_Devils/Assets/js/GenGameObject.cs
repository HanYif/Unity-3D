using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NewGame;

public class GenGameObject : MonoBehaviour
{
    // Record the numbers of priests and devil.
    /*public int priestLf = 3;
    public int priestRi = 3;
    public int devilLf = 0;
    public int devilRi = 0;*/

    Stack<GameObject> priestLf = new Stack<GameObject>();
    Stack<GameObject> priestRi = new Stack<GameObject>();
    Stack<GameObject> devilLf = new Stack<GameObject>();
    Stack<GameObject> devilRi = new Stack<GameObject>();
    GameObject[] side = new GameObject[2];
    GameObject[] boat_meb = new GameObject[2];

    GameObject boat;

    public int onBoat = 0;

    float speed = 5;
    public float gap = 1.5f;

    GameController controller;

    Vector3 sideLf = new Vector3(-12 , 0, 0);
    Vector3 sideRi = new Vector3(12, 0, 0);
    Vector3 boatLf = new Vector3(-4, 0, 0);
    Vector3 boatRi = new Vector3(4, 0, 0);

    Vector3 priestLfPos = new Vector3(-11f, 2.7f, 0);
    Vector3 priestRiPos = new Vector3(8f, 2.7f, 0);
    Vector3 devilLfPos = new Vector3(-16f, 2.7f, 0);
    Vector3 devilRiPos = new Vector3(13f, 2.7f, 0);


    
    // Use this for instantiate objects;
    void Start()
    {
        controller = GameController.GetInstance();
        controller.setGenGameObject(this);
        loadSrc();
    }

    // Update is called once per frame;
    void Update() {

        //Update the position of the objects that on the side according to the stack;
        setPosition(priestLf, priestLfPos);
        setPosition(devilLf, devilLfPos);
        setPosition(priestRi, priestRiPos);
        setPosition(devilRi, devilRiPos);

        //Update the position of the boat;
        BoatMove();

        //Check the state;
        Check();
    }

    public void Check() {
        Debug.Log("state " + controller.state);

        int pon = 0, don = 0;
        for (int i = 0;  i < 2; i++)
        {
            if (boat_meb[i] != null && boat_meb[i].name[0] == 'p') pon++;
            if (boat_meb[i] != null && boat_meb[i].name[0] == 'd') don++;
        }

        Debug.Log("pon " + pon + " don " + don);
        if (controller.state == State.BOATLEFT) {
            if ((priestLf.Count + pon < devilLf.Count + don && (priestLf.Count != 0 || pon != 0)) 
                || (priestRi.Count < devilRi.Count && priestRi.Count != 0)) {
                controller.state = State.FAIL;
            }
        }
        else if (controller.state == State.BOATRIGHT) {
            if ((priestLf.Count < devilLf.Count && priestLf.Count != 0)
                || (priestRi.Count + pon < devilRi.Count + don && (priestRi.Count != 0 || pon != 0))) {
                controller.state = State.FAIL;
            }
            else if (priestRi.Count == 3 && devilRi.Count == 3)
            {
                controller.state = State.WIN;
            }
        }
        
        else if (controller.state == State.MOVELEFT && boat.transform.position == boatLf) {
            controller.state = State.BOATLEFT;
        }
        else if (controller.state == State.MOVERIGHT && boat.transform.position == boatRi) {
            controller.state = State.BOATRIGHT;
        }
    }

    public void getOnBoat(GameObject obj) {
            obj.transform.parent = boat.transform;
            if (boat_meb[0] == null) {
                boat_meb[0] = obj;
                obj.transform.localPosition = new Vector3(-0.3f, 1.2f, 0);
            } else if (boat_meb[1] == null) {
                boat_meb[1] = obj;
                obj.transform.localPosition = new Vector3(0.3f, 1.2f, 0);
            }
    }

    public void PriestLeftOn() {
        if (onBoat < 2 && priestLf.Count != 0 && controller.state == State.BOATLEFT) {
            getOnBoat(priestLf.Pop());
            onBoat++;
        }
    }

    public void PriestRightOn() {
        if (onBoat < 2 && priestRi.Count != 0 && controller.state == State.BOATRIGHT) {
            getOnBoat(priestRi.Pop());
            onBoat++;
        }
    }

    public void DevilLeftOn() {
        if (onBoat < 2 && devilLf.Count != 0 && controller.state == State.BOATLEFT) {
            getOnBoat(devilLf.Pop());
            onBoat++;
        }
    }

    public void DevilRightOn() {
        if (onBoat < 2 && devilRi.Count != 0 && controller.state == State.BOATRIGHT) {
            getOnBoat(devilRi.Pop());
            onBoat++;
        }
    }

    
    public void getOffBoat(string name, State state) {
        if (controller.state == state) {
            Debug.Log("0");
            if (name == "p") {
                if (boat_meb[0] != null && boat_meb[0].name[0] == 'p') {
                    boat_meb[0].transform.parent = null;
                    onBoat--;
                    if (state == State.BOATLEFT)
                        priestLf.Push(boat_meb[0]);
                    else priestRi.Push(boat_meb[0]);
                    boat_meb[0] = null;
                } else if (boat_meb[1] != null && boat_meb[1].name[0] == 'p') {
                    boat_meb[1].transform.parent = null;
                    onBoat--;
                    if (state == State.BOATLEFT)
                        priestLf.Push(boat_meb[1]);
                    else priestRi.Push(boat_meb[1]);
                    boat_meb[1] = null;
                }

            } else if (name == "d") {
                Debug.Log("name d");
                if (boat_meb[0] != null && boat_meb[0].name[0] == 'd')
                {
                    Debug.Log("1");
                    boat_meb[0].transform.parent = null;
                    onBoat--;
                    if (state == State.BOATLEFT)
                    {
                        devilLf.Push(boat_meb[0]);Debug.Log("2");
                    }
                    else devilRi.Push(boat_meb[0]);
                    boat_meb[0] = null;
                }
                else if (boat_meb[1] != null && boat_meb[1].name[0] == 'd')
                {
                    boat_meb[1].transform.parent = null;
                    onBoat--;
                    if (state == State.BOATLEFT)
                        devilLf.Push(boat_meb[1]);
                    else devilRi.Push(boat_meb[1]);
                    boat_meb[1] = null;
                }
            }
        }
    }

    public void PriestLeftOff() {
        if (onBoat != 0)
            getOffBoat("p", State.BOATLEFT);
    }

    public void PriestRightOff() {
        if (onBoat != 0)
            getOffBoat("p", State.BOATRIGHT);
    }

    public void DevilLeftOff() {
        if (onBoat != 0)
            getOffBoat("d", State.BOATLEFT);
    }

    public void DevilRightOff() {
        if (onBoat != 0)
            getOffBoat("d", State.BOATRIGHT);
    }

    public void BoatMove() {
        if (boat_meb[0] == null && boat_meb[1] == null && controller.state == State.MOVELEFT) { controller.state = State.BOATRIGHT; return; }
        else if (boat_meb[0] == null && boat_meb[1] == null && controller.state == State.MOVERIGHT) { controller.state = State.BOATLEFT; return; }
        if (controller.state == State.MOVELEFT)
            boat.transform.position = Vector3.MoveTowards(boat.transform.position, boatLf, speed * Time.deltaTime);
        else if (controller.state == State.MOVERIGHT)
            boat.transform.position = Vector3.MoveTowards(boat.transform.position, boatRi, speed * Time.deltaTime);
    }

    //Create Objects;
    public void loadSrc() {
        boat = GameObject.CreatePrimitive(PrimitiveType.Cube);
        boat.transform.localScale += new Vector3(3, 0, 0);
        boat.name = "boat";
        for (int i = 0; i < 3; i++) {
            priestLf.Push(GameObject.CreatePrimitive(PrimitiveType.Sphere));
            devilLf.Push(GameObject.CreatePrimitive(PrimitiveType.Cube));

            priestLf.Peek().name = "p" + i;
            devilLf.Peek().name = "d" + i;

            //Debug.Log(priestLf.Peek().name);
            
            if (i != 2) {
                side[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                side[i].transform.localScale += new Vector3(10, 3, 3);
            }
        }

        //Init the position of the side and the boat;
        side[0].name = "Leftside";
        side[1].name = "Rightside";
        side[0].transform.position = sideLf;
        side[1].transform.position = sideRi;
        boat.transform.position = boatLf;
    }


    //Position API;
    void setPosition(Stack<GameObject> stack, Vector3 pos)
    {
        GameObject[] array = stack.ToArray();
        for (int i = 0; i < stack.Count; ++i)
        {
            array[i].transform.position = new Vector3(pos.x + gap * i, pos.y, pos.z);
        }
    }
   
    public void reset()
    {
        for (int i = 0; i < 2; i++)
            if (boat_meb[i] != null) {    
                boat_meb[i].transform.parent = null;
                if (boat_meb[i].name[0] == 'p') priestLf.Push(boat_meb[i]);
                else devilLf.Push(boat_meb[i]);
            }

        while (priestRi.Count > 0) priestLf.Push(priestRi.Pop());
        while (devilRi.Count > 0) devilLf.Push(devilRi.Pop());

        boat.transform.position = boatLf;
    }

}
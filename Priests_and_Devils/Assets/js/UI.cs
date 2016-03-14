using UnityEngine;
using System.Collections;
using NewGame;

public class UI : MonoBehaviour {

    GameController controller;
    IUserActions ua;
    float width, height;

    // Use this for initialization
    void Start () {
        width = Screen.width / 12;
        height = Screen.height / 12;
        controller = GameController.GetInstance();
        //ua = GameController.GetInstance() as IUserActions;
	}

   

    float castw(float scale)
    {
        return (Screen.width - width) / scale;
    }

    float casth(float scale)
    {
        return (Screen.height - height) / scale;
    }

    void OnGUI() {
  
        if (controller.state == State.WIN) {
            if (GUI.Button(new Rect(castw(2f), casth(6f), width, height), "WIN!")) {
                controller.reset();
            }
        }
        else if (controller.state == State.FAIL) {
            if (GUI.Button(new Rect(castw(2f), casth(6f), width, height), "LOSE!")) {
                controller.reset();
            }
        }
        else if (controller.state == State.BOATLEFT || controller.state == State.BOATRIGHT) {
            Debug.Log("in");
            if (GUI.Button(new Rect(castw(2f), casth(6f), width, height), "Go"))
            {
                controller.boatMove();
            }
            if (GUI.Button(new Rect(castw(10.5f), casth(4f), width, height), "pOn"))
            {
                controller.priestLeftOn();
            }
            if (GUI.Button(new Rect(castw(4.3f), casth(4f), width, height), "dOn"))
            {
                controller.devilLeftOn();
            }
            if (GUI.Button(new Rect(castw(1.1f), casth(4f), width, height), "pOn"))
            {
                controller.priestRightOn();
            }
            if (GUI.Button(new Rect(castw(1.3f), casth(4f), width, height), "dOn"))
            {
                controller.devilRightOn();
            }
            if (GUI.Button(new Rect(castw(2.5f), casth(1.3f), width, height), "pOff"))
            {
                if (controller.state == State.BOATLEFT)
                    controller.priestLeftOff();
                else if (controller.state == State.BOATRIGHT)
                controller.priestRightOff();
            }
            if (GUI.Button(new Rect(castw(1.7f), casth(1.3f), width, height), "dOff"))
            {
                if (controller.state == State.BOATLEFT)
                    controller.devilLeftOff();
                else if (controller.state == State.BOATRIGHT)
                    controller.devilRightOff();
            }
        }  

    }
}

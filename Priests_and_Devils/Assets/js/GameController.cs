using UnityEngine;
using System.Collections;



namespace NewGame {

    public enum State { WIN, FAIL, BOATLEFT, BOATRIGHT, MOVELEFT, MOVERIGHT};
    

    public interface IUserActions {

        void priestLeftOn();
        void priestRightOn();

        void devilLeftOn();
        void devilRightOn();

        void priestLeftOff();
        void priestRightOff();

        void devilLeftOff();
        void devilRightOff();

        void boatMove();
        void reset();

    };



    public class GameController : IUserActions {

        private static GameController instance;
        private BaseCode basecode;
        private GenGameObject gengameobj;
        public State state = State.BOATLEFT;

        public static GameController GetInstance() {
            if (instance == null) {
                instance = new GameController();
            }
            return instance;
        }

        public GenGameObject getGenGameObject() {
            return gengameobj;
        }

        public void setGenGameObject(GenGameObject obj) {
            if (null == gengameobj)
                gengameobj = obj;
        }

        public void priestLeftOn() {
            gengameobj.PriestLeftOn();
        }

        public void priestRightOn() {
            gengameobj.PriestRightOn();
        }

        public void devilLeftOn() {
            gengameobj.DevilLeftOn();
        }

        public void devilRightOn() {
            gengameobj.DevilRightOn();
        }

        public void priestLeftOff() {
            gengameobj.PriestLeftOff();
        }
        public void priestRightOff() {
            gengameobj.PriestRightOff();
        }

        public void devilLeftOff() {
            gengameobj.DevilLeftOff();
        }
        public void devilRightOff() {
            gengameobj.DevilRightOff();
        }

        public void boatMove() {
            if (state == State.BOATLEFT) { state = State.MOVERIGHT; }
            else if (state == State.BOATRIGHT) { state = State.MOVELEFT; }
            gengameobj.BoatMove();
        }


        public void reset()
        {
            state = State.BOATLEFT;
            gengameobj.reset();
        }

    }
}


// gamename gamerule;
public class BaseCode : MonoBehaviour {



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

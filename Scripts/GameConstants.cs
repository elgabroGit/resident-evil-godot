public class GameConstants
{
    // Player Animations
    public const string ANIM_IDLE = "idle";
    public const string ANIM_WALK = "walk";
    public const string ANIM_RUN = "run";
    public const string ANIM_PISTOL_IDLE = "pistole_idle";
    public const string ANIM_RIFLE_IDLE = "rifle_idle";
    public const string ANIM_PISTOL_SHOT = "pistole_shot";
    public const string ANIM_RIFLE_SHOT = "rifle_shot";

    // Inputs
    public const string INPUT_LEFT = "left";
    public const string INPUT_RIGHT = "right";
    public const string INPUT_BACKWARD = "backward";
    public const string INPUT_FORWARD = "forward";
    public const string INPUT_AIM = "aim";
    public const string INPUT_RUN = "run";
    public const string INPUT_SWITCH = "switch";
    public const string INPUT_PAUSE = "pause";
    public const string INPUT_INTERACT = "interact";
    public const string INPUT_SWITCH_UP = "switch_up";
    public const string INPUT_SWITCH_DOWN = "switch_down";
    public const string INPUT_MENU = "menu";

    public enum GameState{
        RUNNING,
        PLAYER_DEAD,
        GAMEOVER,
        MENU,
        CUTSCENE,
        MAINMENU
    }

    public enum FireType{
        MANUAL,
        AUTOMATIC,
        SEMIAUTOMATIC
    }

    public enum AmmoType{
        GUN,
        RIFLE
    }

    // Notification System
    public const int NOTIFICATION_ENTER_STATE = 5001;
    public const int NOTIFICATION_EXIT_STATE = 5002;

    // Internal Game Values
    public const int TIMER_TO_DEATH = 5;
}

namespace Framework
{
    public enum GameServerEvent
    {
        SEARCHOPPONENT = 4001,
        QUIT_GAME = 4003,
        ATTACK = 4004,
        QUIT_SEARCH = 4005,
        LOGIN = 4999,
        TransactionResponse,

        START = 4501,
        ENEMY_OUT_GAME = 4503,
        BEINGATTACKED = 4504,
        NEW_TURN = 4507,
        COUNTDOWN = 4508,
        ENDGAME = 4510,
        TransactionRequest,

    }
}
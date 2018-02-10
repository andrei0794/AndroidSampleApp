using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  Global variable conventions
 *      - constants must be named with UPPER_CASE only
 *      - global variables must be named with "_" at the beginning
 *      - global variables, like all the other ones, must be named using camelCase
 */

public enum TileDirection
{
    _right,
    _left
}

public class Globals {

    private static Globals _instance;

    public float _minX, _maxX, _minY, _maxY;
    public bool _gameOver;
    public int _tileCounter;
    public float _tileCurrentSpeed;
    public float _tileSpeedIncrement;
    public float _playerJumpForceIncrement;
    public int _tileNumberFreeze;

    public const float TILE_START_SPEED = 2f;
    public const int TILE_NUMBER_DIFFICULTY_INCREMENT = 2;
    public const float TILE_SPEED_VARIATION = 0.005f;
    public const float PLAYER_PUSH_FORCE_X = 0.005f;
    public const float PLAYER_PUSH_FORCE_Y = 0.005f;
    public const float INITIAL_TILE_GRAVITY = 1f;
    public const float STACKED_TILE_GRAVITY = 0.2f;

    private Globals()
    {
        _tileSpeedIncrement = 0.00f;
        _playerJumpForceIncrement = 0.00000f;
        _tileCurrentSpeed = TILE_START_SPEED;
        _tileNumberFreeze = 10;
    }

    public static Globals GetInstance()
    {
        if (_instance == null)
        {
            _instance = new Globals();
        }

        return _instance;
    }
}

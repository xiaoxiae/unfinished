using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using IronPython.Hosting;
using IronPython.Runtime;
using Microsoft.Scripting.Hosting;

public class Editor : MonoBehaviour
{
    public InputField InputField;
    public Player player;
    public PlayerMovement movement;
    
    public Image Image;
    public Sprite GoodSprite;
    public Sprite BadSprite;
    
    private ScriptEngine engine;
    private ScriptScope scope;

    void Start()
    {
        InputField = GetComponent<InputField>();
        
        engine = Python.CreateEngine();
        scope = engine.CreateScope();

        InputField.text = @"def update(player_position):  # TODO: enemy pos
    # TODO: something with the positions
    #       if you want to move or shoot, return
    #       ((int, int) or None, (int, int) or None)";

        InputField.text = @"def update(player_position, enemy_positions):
  p_x, p_y = player_position
  for x, y in enemy_positions:
    if abs(p_x - x) + abs(p_y - y) < 2:
      return (p_x - x, p_y - y)
  return (0.0, 0.0)";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!InputField.isFocused)
            {
                EventSystem.current.SetSelectedGameObject(InputField.gameObject);
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
        
        if (InputField.isFocused) {
            Time.timeScale = 0;
        }
        else
            Time.timeScale = 1;

        try
        {
            engine = Python.CreateEngine();
            scope = engine.CreateScope();
            
            var source = engine.CreateScriptSourceFromString(InputField.text);
            source.Execute(scope);
            
            var enemies = FindObjectsOfType<Enemy>();
            var enemyPositions = new List<(float x, float y)>();
            foreach (Enemy enemy in enemies)
                enemyPositions.Add((enemy.transform.position.x, enemy.transform.position.y));

            PythonTuple tuple = new PythonTuple(enemyPositions);
            var playerPos = (player.transform.position.x, player.transform.position.y);
            
            PythonTuple result = scope.GetVariable("update")(playerPos, tuple);
            
            //Debug.Log(result);
            //Debug.Log((double)result[0]);

            // I'm going to hell for this
            try { movement.scriptDirection = new Vector2((float)result[0], (float)result[1]).normalized; } catch (Exception e) {}
            try { movement.scriptDirection = new Vector2((float)(double)result[0], (float)(double)result[1]).normalized; } catch (Exception e) {}
            try { movement.scriptDirection = new Vector2((int)result[0], (int)result[1]).normalized; } catch (Exception e) {}

            Image.sprite = GoodSprite;
        }
        catch (Exception e)
        {
            //Debug.Log(e);
            // TODO: change color of border
            movement.scriptDirection = Vector2.zero;
            Image.sprite = BadSprite;
        }
    }
}
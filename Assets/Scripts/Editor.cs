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
    
    public Spawner spawner;
    
    public Image Image;
    public Sprite GoodSprite;
    public Sprite BadSprite;
    
    private ScriptEngine engine;
    private ScriptScope scope;
    
    private string oldText;

    void Start()
    {
        InputField = GetComponent<InputField>();
        
        engine = Python.CreateEngine();
        scope = engine.CreateScope();

        InputField.text = @"def update(player_position, enemy_positions):
  p_x, p_y = player_position
  for x, y in enemy_positions:
    if abs(p_x - x) + abs(p_y - y) < 2:
      return (p_x - x, p_y - y, True)
  return (0.0, 0.0, False)";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!InputField.isFocused)
            {
                EventSystem.current.SetSelectedGameObject(InputField.gameObject);
                InputField.caretPosition = 0;
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(null);
                InputField.text = oldText;
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

            var enemyPositions = new List<(float x, float y)>();
            foreach (Enemy enemy in spawner.enemies)
                enemyPositions.Add((enemy.transform.position.x, enemy.transform.position.y));

            PythonTuple tuple = new PythonTuple(enemyPositions);
            var playerPos = (player.transform.position.x, player.transform.position.y);
            
            PythonTuple result = scope.GetVariable("update")(playerPos, tuple);
            
            Debug.Log(result);

            // I'm going to hell for this
            try { movement.scriptDirection = new Vector2((float)result[0], (float)result[1]).normalized; } catch (Exception e) {}
            try { movement.scriptDirection = new Vector2((float)(double)result[0], (float)(double)result[1]).normalized; } catch (Exception e) {}
            try { movement.scriptDirection = new Vector2((int)result[0], (int)result[1]).normalized; } catch (Exception e) {}
            movement.scriptSprinting = (bool)result[2];

            Image.sprite = GoodSprite;
        }
        catch (Exception e)
        {
            //Debug.Log(e);
            // TODO: change color of border
            movement.scriptDirection = Vector2.zero;
            Image.sprite = BadSprite;
        }

        oldText = InputField.text;
    }
}
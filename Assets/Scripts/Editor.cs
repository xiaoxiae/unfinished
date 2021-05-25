using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using IronPython.Hosting;

public class Editor : MonoBehaviour
{
    InputField InputField;

    public string executed;

    void Start()
    {
        InputField = GetComponent<InputField>();
        
        var eng = Python.CreateEngine();
        var scope = eng.CreateScope();
        //eng.Execute(@"
        //def greetings(name):
        //    return 'Hello ' + name.title() + '!'
        //", scope);
        //dynamic greetings = scope.GetVariable("greetings");
        //executed = greetings("test");
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
    }
}
using UnityEngine;

public class mixupScripts : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
    }

        // Update is called once per frame
        void Update()
    {
        
    }
    public void DoMixup(string Name)
    {
        switch (Name)
        {
            case "Random Controls":
                DoRandomCon();
                break;

        }
    }

    private void DoRandomCon()
        {
            //Random Control
        }
    }

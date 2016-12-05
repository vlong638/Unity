using UnityEngine;
using System.Collections;

public class CertainEventScript : MonoBehaviour {

    // Use this for initialization
    int start_Number = 1;
	void Start () {
        print(string.Format("01 Started for times:{0}",start_Number));
        start_Number += 1;
    }
	
	// Update is called once per frame
    int update_Number = 1;
	void Update() {
        print(string.Format("01 Update for times:{0}", update_Number));
        update_Number += 1;
    }

    int awake_Number = 1;
    private void Awake()
    {
        print(string.Format("01 Awake for times:{0}", awake_Number));
        awake_Number += 1;
    }

    int fixed_Number = 1;
    private void FixedUpdate()
    {
        print(string.Format("01 FixedUpdate for times:{0}", fixed_Number));
        fixed_Number += 1;
    }

    int late_Number = 1;
    private void LateUpdate()
    {
        print(string.Format("01 LateUpdate for times:{0}", late_Number));
        late_Number += 1;
    }
}
